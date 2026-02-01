using E_Commerce_Web.Extentions;

namespace E_Commerce_Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddAllServices(builder.Configuration);
            var app = builder.Build();
            await app.ConfigureMiddlewareAsync();
            app.Run();
        }
    }
}
