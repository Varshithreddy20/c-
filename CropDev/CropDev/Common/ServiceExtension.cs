using CropDev.Repository.Concrete;
using CropDev.Repository.Interface;
using CropDev.Service.Concrete;
using CropDev.Service.Interface;
using CropDev.Utilities;
using System.Runtime.CompilerServices;



namespace CropDev.Common
{
    public static class ServiceExtension
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<AppSettings, AppSettings>();
            services.AddScoped<IFarmersRepository, FarmersRepository>();    
            services.AddScoped<IFarmersService, FarmersService>();
            return services;
        }
    }
}
