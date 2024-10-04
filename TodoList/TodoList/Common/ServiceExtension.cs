using TodoList.Repository.Concrete;
using TodoList.Repository.Interface;
using TodoList.Services.Interface;
using TodoList.Services.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace TodoList.Common
{
    public static class ServiceExtension
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ITodoRepository, TodoRepository>();
            services.AddScoped<ITodoService, TodoService>();

            return services;
        }
    }
}
