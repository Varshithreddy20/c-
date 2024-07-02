using CropDev.Repository.Concrete;
using CropDev.Repository.Interface;
using CropDev.Service.Concrete;
using CropDev.Service.Concrete.CropDev.Service.Concrete;
using CropDev.Service.Interface;
using CropDev.Utilities;


public class Program
{
    public static AppSettings? AppSettings { get; set; }
    public static void Main(string[] args) 
    {
        var builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration;
        AppSettings =configuration.GetSection("AppSettings").Get<AppSettings>();
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


        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.Configure<AppSettings>(configuration.GetSection("AppSettings"));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}







