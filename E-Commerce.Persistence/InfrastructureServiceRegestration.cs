using E_commerce.Domain.Contracts;
using E_commerce.Domain.Entites.Identity;
using E_Commerce.Persistence.Context;
using E_Commerce.Persistence.DbInitializers;
using E_Commerce.Persistence.Identity.Context;
using E_Commerce.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace E_Commerce.Persistence
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddDbContext<IdentityStoreDbContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("RedisIdentityConnection"));
            });
            // Identity Services
            services.AddIdentityService();
            services.AddScoped<IDbInitializer, DbInitializer>();
          
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<ICacheRepository, CacheRepository>();

            services.AddSingleton<IConnectionMultiplexer>((opt) =>
            {
                return ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection"));
            });
            return services;
        }

        public static IServiceCollection AddIdentityService(this IServiceCollection services)
        {
             services.AddIdentityCore<AppUser>(opt =>
            {
                opt.User.RequireUniqueEmail = true;

            }).AddRoles<IdentityRole>()
               .AddEntityFrameworkStores<IdentityStoreDbContext>();
            return services;

        }
    }
}
