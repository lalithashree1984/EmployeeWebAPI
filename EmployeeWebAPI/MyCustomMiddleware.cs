using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace EmployeeWebAPI
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class MyCustomMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public MyCustomMiddleware(RequestDelegate next,ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger= loggerFactory.CreateLogger<MyCustomMiddleware>();
            _logger.LogInformation("Starting the Middleware");
        }

        public async Task Invoke(HttpContext httpContext)
        {
            _logger.LogInformation("Executed the Middleware");
            await _next.Invoke(httpContext);
           
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MyCustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyCustomMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyCustomMiddleware>();
        }
    }
}
