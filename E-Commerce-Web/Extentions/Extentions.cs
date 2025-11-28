using E_commerce.Domain.Contracts;
using E_Commerce.Persistence;
using E_Commerce.Service.ApplicationServiceRegistration;
using E_Commerce.Shared.ErrorModels.Validations;
using E_Commerce_Web.Middilewares;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Web.Extentions
{
    public static class Extentions
    {
        public static IServiceCollection AddAllServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddWebServices();
            services.AddApplicationServices(configuration);
            services.AddInfrastructureServices(configuration);
            services.ConfigureAPIBehaviorOptions();
            return services;
        }

        private static IServiceCollection ConfigureAPIBehaviorOptions(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(config =>
            {
                config.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(m => m.Value.Errors.Any())
                                                         .Select(m => new ValidationErrors
                                                         {
                                                             Field = m.Key,
                                                             Errors = m.Value.Errors.Select(E => E.ErrorMessage)
                                                         });

                    var response = new ValidationErrorsResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(response);

                };

            });
            return services;
        }

        private static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddControllers();
            return services;
        }

        public static async Task<WebApplication> ConfigureMiddlewareAsync(this WebApplication app)
        {
            await app.SeedData();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseGlobalErrorHandler();
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            
            return app;
        }

        private static async Task<WebApplication> SeedData(this WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var initializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await initializer.InitializeAsync();
            await initializer.InitializeIdentityAsync();
            return app;
        }
        private static WebApplication UseGlobalErrorHandler(this WebApplication app)
        {
            app.UseMiddleware<GlobalErrorHandlingMiddleware>();
            return app;
        }
    }
}
