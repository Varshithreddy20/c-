using CRAVENEST.Repository.Concrete;
using CRAVENEST.Repository.Interface;
using CRAVENEST.Service.Concrete;
using CRAVENEST.Service.Interfce;
using CRAVENEST.Utilities;
using CRAVENEST;
using Microsoft.Extensions.Options;
using CRAVENEST.Service.Interface;

public static class ServiceExtension
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Configure AppSettings
        services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
        services.AddSingleton<AppSettings>(sp => sp.GetRequiredService<IOptions<AppSettings>>().Value);

        // Register services
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IBookingsRepository, BookingsRepository>();
        services.AddScoped<IBookingsService, BookingsService>();  
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped <IFoodItemRepository, FoodItemRepository>();
        services.AddScoped<IFoodItemService, FoodItemService>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderService, OrderService>();

        return services;
    }
}
