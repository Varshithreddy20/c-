using CropDev.Repository.Concrete;
using CropDev.Repository.Interface;
using CropDev.Service.Concrete;
using CropDev.Service.Interface;
using CropDev.Utilities;
using CropDev.JwtInterface;
using CropDev.JwtService;
using CropDev.Service.Concrete.CropDev.Service.Concrete;

namespace CropDev.Common
{
    public static class ServiceExtension
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<AppSettings, AppSettings>();
            services.AddScoped<IFarmersRepository, FarmersRepository>();
            services.AddScoped<IFarmersService, FarmersService>();
            //services.AddScoped<IFarmerLandDetailsRepository, FarmerLandDetailsRepository>();
            services.AddScoped<IFarmerLandDetailsService, FarmerLandDetailsService>();
            services.AddScoped<IAgentUsersRepository, AgentUsersRepository>();
            services.AddScoped<IAgentUsersService, AgentUsersService>();
            services.AddScoped<IPriceQuoteRepository, PriceQuoteRepository>();
            services.AddScoped<IPriceQuoteService, PriceQuoteService>();
            services.AddScoped<IFarmerRequestService, FarmerRequestService>();
            services.AddScoped<IFarmerRequestRepository, FarmerRequestRepository>();
            services.AddScoped<IFarmerPaymentTransactionService, FarmerPaymentTransactionService>();
            services.AddScoped<IFarmerPaymentTransactionRepository, FarmerPaymentTransactionRepository>();
        
            services.AddScoped<IJwtService, JwtService.JwtService>();

            services.AddScoped<IUserRepository, UserRepository>();  
            services.AddScoped<IUserService, UserService>();
          
            return services;
        }
    }
}
