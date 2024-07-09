using CropDev.JwtInterface;
using CropDev.Repository.Concrete;
using CropDev.Repository.Interface;
using CropDev.Service.Concrete;
using CropDev.Service.Interface;
using CropDev.JwtService;
using CropDev.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using CropDev.Service.Concrete.CropDev.Service.Concrete;

namespace CropDev
{
    public class Program
    {
        public static AppSettings? AppSettings { get; set; }
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            builder.Services.AddSingleton<IConfiguration>(configuration);

            builder.Services.AddControllers();

            // Repository and Service registrations
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IFarmersRepository, FarmersRepository>();
            builder.Services.AddScoped<IFarmersService, FarmersService>();
            builder.Services.AddScoped<IFarmerLandDetailsService, FarmerLandDetailsService>();
            builder.Services.AddScoped<IFarmerLandDetailsRepository, FarmerLandDetailsRepository>();
            builder.Services.AddScoped<IAgentUsersRepository, AgentUsersRepository>();
            builder.Services.AddScoped<IAgentUsersService, AgentUsersService>();
            builder.Services.AddScoped<IPriceQuoteService, PriceQuoteService>();
            builder.Services.AddScoped<IPriceQuoteRepository, PriceQuoteRepository>();
            builder.Services.AddScoped<IFarmerRequestRepository, FarmerRequestRepository>();
            builder.Services.AddScoped<IFarmerRequestService, FarmerRequestService>();
            builder.Services.AddScoped<IFarmerPaymentTransactionRepository, FarmerPaymentTransactionRepository>();
            builder.Services.AddScoped<IFarmerPaymentTransactionService, FarmerPaymentTransactionService>();

            builder.Services.AddScoped<IJwtService, JwtService.JwtService>();

            builder.Services.Configure<AppSettings>(configuration.GetSection("AppSettings"));

            // JWT Authentication
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                    };
                });

            builder.Services.AddAuthorization();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("http://example.com")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod());
            });

            // Swagger Configuration
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CropDev API", Version = "v1" });

                // Add JWT Authentication
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CropDev API v1"));
            }
            app.UseCors("AllowSpecificOrigin");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
