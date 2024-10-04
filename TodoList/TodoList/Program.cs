using TodoList.Utilities;
using TodoList.Repository.Concrete;
using TodoList.Repository.Interface;
using TodoList.Services.Interface;
using TodoList.Services.Concrete;
using TodoList.Model;
using Microsoft.OpenApi.Models;

namespace TodoList
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

            // Register HttpClient
            builder.Services.AddHttpClient();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4300")
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });

           
            // Repository and Service registrations
            builder.Services.AddScoped<ITodoService, TodoService>();
            builder.Services.AddScoped<ITodoRepository, TodoRepository>();

            builder.Services.Configure<AppSettings>(configuration.GetSection("AppSettings"));

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TodoList API", Version = "v1" });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TodoList API v1"));
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
