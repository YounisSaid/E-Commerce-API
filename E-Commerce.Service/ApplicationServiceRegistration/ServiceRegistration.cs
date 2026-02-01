using E_Commerce.Service.Abstraction;
using E_Commerce.Service.MappingProfiles;
using E_Commerce.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace E_Commerce.Service.ApplicationServiceRegistration
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMappingProfiles(configuration);
            services.AddCoreServices();
            services.AddJwtAuthentication(configuration);

            return services;
        }

        private static void AddMappingProfiles(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile(new ProductProfile(configuration));
                cfg.AddProfile(new BasketProfile());
                cfg.AddProfile(new OrderProfile());
                cfg.AddProfile(new AuthProfile());
            });
        }

        private static void AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManger>();
        }

        private static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSection = configuration.GetSection("jwtOptions");
            services.Configure<JwtOptions>(jwtSection);

            var jwtOptions = jwtSection.Get<JwtOptions>();
            if (jwtOptions == null) return;

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Bearer";
                options.DefaultChallengeScheme = "Bearer";
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = CreateTokenValidationParameters(jwtOptions);
            });
        }

        private static TokenValidationParameters CreateTokenValidationParameters(JwtOptions jwtOptions)
        {
            return new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidAudience = jwtOptions.Audience,
                ValidateIssuer = true,
                ValidIssuer = jwtOptions.Issuer,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecurityKey))
            };
        }
    }
}