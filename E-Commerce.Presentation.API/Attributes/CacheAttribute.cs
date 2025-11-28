using E_Commerce.Service.Abstraction;
using E_Commerce.Serviece.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace E_Commerce.Persistence.Attributes
{
    internal class CacheAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IServiceManager>().CacheService;

            var cacheKey = await GetCacheKey(context.HttpContext.Request);

            var resultFromCache = await cacheService.GetAsync(cacheKey);
            if(!string.IsNullOrEmpty(resultFromCache))
            {
                var response = new ContentResult 
                { 
                    Content = resultFromCache, 
                    ContentType = "application/json",
                    StatusCode = 200 // OK
                };
                context.Result = response;
                return;

            }

            var executedContext = await next();
            if(executedContext.Result is OkObjectResult okObjectResult)
            {
                await cacheService.SetAsync(cacheKey, okObjectResult.Value!, TimeSpan.FromMinutes(10));
            }
        }

        private async Task<string> GetCacheKey(HttpRequest request)
        {
            var key = new StringBuilder();
            key.Append($"{request.Path}");

            foreach(var item in request.Query.OrderBy(x => x.Key))
            {
                key.Append($"|{item.Key}-{item.Value}");
            }
            return key.ToString();
        }
    }
}
