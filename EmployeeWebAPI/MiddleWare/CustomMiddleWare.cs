using EmployeeWebAPI.MiddleWare.Models;
using Newtonsoft.Json;
using Serilog;

namespace EmployeeWebAPI.MiddleWare
{
    public class CustomMiddleWare
    {
        private readonly RequestDelegate _next;
        private string requestBody;
        public CustomMiddleWare(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            await ReadRequestBody(httpContext);
        }

        private async Task ReadRequestBody(HttpContext httpContext)
        {
            try
            {
                //read request body
                httpContext.Request.EnableBuffering();
                using (var reader = new StreamReader(httpContext.Request.Body, encoding: System.Text.Encoding.UTF8, detectEncodingFromByteOrderMarks: false,
                    bufferSize: 1024, leaveOpen: true))
                {

                    requestBody = await reader.ReadToEndAsync();
                    httpContext.Request.Body.Position = 0;
                }

                Log.Information("Called From Custom Middleware");
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                LogException(httpContext, ex);

            }
        }

        private void LogException(HttpContext httpContext, Exception ex)
        {
            ExceptionModel exceptionModel = new ExceptionModel();
            exceptionModel.HttpMethod = httpContext.Request.Method;
            exceptionModel.QueryStringValue = httpContext.Request.QueryString.Value;
            exceptionModel.RequestBody = requestBody;
            exceptionModel.ExceptionMessage = ex.Message;
            exceptionModel.ExceptionStackTrace = ex.StackTrace;
            exceptionModel.InnerException = ex.InnerException?.Message;
            exceptionModel.InnerExceptionStackTrace = ex.InnerException?.StackTrace;
            Log.Error("Exception in Application      " + JsonConvert.SerializeObject(exceptionModel));
        }
    }
        public static class MiddleWareExtenstion
        {
            public static IApplicationBuilder UseCustomMiddleWare(this IApplicationBuilder builder)
            {

                return builder.UseMiddleware<CustomMiddleWare>();
            }

        }
    
}
