using APPC.Models.Common;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace APPC.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Unhandled exception occurred for {Method} {Path}.", httpContext.Request.Method,httpContext.Request.Path);
                await HandleExceptionAsync(httpContext, ex, httpContext.RequestServices.GetRequiredService<IHostEnvironment>());
            }
        }

        private static Task HandleExceptionAsync(HttpContext httpContext,Exception exception, IHostEnvironment env)
        {
            httpContext.Request.ContentType = "application/json";

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            string message = env.IsDevelopment()
                            ? exception.Message
                            : "Internal Server Error. Please try again later.";

            var result = ServiceResult<object>.Fail(StatusCodes.Status500InternalServerError, message);

            // Message => Internal Server Error. Please try again later.
            return httpContext.Response.WriteAsJsonAsync(result);
        }

    }
}
