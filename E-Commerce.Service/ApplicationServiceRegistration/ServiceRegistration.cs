using E_Commerce.Service.Abstraction;
using E_Commerce.Service.MappingProfiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace E_Commerce.Service.ApplicationServiceRegistration
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add AutoMapper profiles
            services.AddAutoMapper(x => x.AddProfile(new ProductProfile(configuration)));
            services.AddAutoMapper(x => x.AddProfile(new BasketProfile()));
            // Register service implementations
            //services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IServiceManager, ServiceManger>();
            return services;
        }
    }
}
