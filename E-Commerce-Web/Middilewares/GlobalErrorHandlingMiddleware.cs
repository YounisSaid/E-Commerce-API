using E_commerce.Domain.Exceptions.BadRequest;
using E_commerce.Domain.Exceptions.NotFound;
using E_commerce.Domain.Exceptions.UnAuthorized;
using E_Commerce.Shared.ErrorModels;

namespace E_Commerce_Web.Middilewares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
                if(context.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    context.Response.ContentType = "application/json";
                    var response = new ErrorDetails
                    {
                        StatusCode = context.Response.StatusCode,
                        ErrorMessage = $"The requested Endpoint {context.Request.Path} was not found."
                    };
                    await context.Response.WriteAsJsonAsync(response);      
                }
            }
            catch(Exception ex)
            {
                context.Response.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    BadRequestException => StatusCodes.Status400BadRequest,
                    UnAuthorizedException => StatusCodes.Status400BadRequest,
                    _ => StatusCodes.Status500InternalServerError
                };

                context.Response.ContentType = "application/json";

                var response = new ErrorDetails
                {
                    StatusCode = context.Response.StatusCode,
                    ErrorMessage = ex.Message
                };

                await context.Response.WriteAsJsonAsync(response);

            }
        }
    }
}
