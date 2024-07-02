using CropDev.Repository.Concrete;
using CropDev.Repository.Interface;
using CropDev.Service.Concrete;
using CropDev.Service.Concrete.CropDev.Service.Concrete;
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
            services.AddScoped<IFarmerLandDetailsRepository, FarmerLandDetailsRepository>();
            services.AddScoped<IFarmerLandDetailsService, FarmerLandDetailsService>();
            services.AddScoped<IAgentUsersRepository, AgentUsersRepository>();
            services.AddScoped<IAgentUsersService, AgentUsersService>();
            services.AddScoped<IPriceQuoteRepository, PriceQuoteRepository>();
            services.AddScoped<IPriceQuoteService, PriceQuoteService>();
            services.AddScoped<IFarmerRequestService, FarmerRequestService>();
            services.AddScoped<IFarmerRequestRepository, FarmerRequestRepository>();
            services.AddScoped<IFarmerPaymentTransactionService, FarmerPaymentTransactionService>();
            services.AddScoped<IFarmerPaymentTransactionRepository, FarmerPaymentTransactionRepository>();

            return services;
        }
    }
}
