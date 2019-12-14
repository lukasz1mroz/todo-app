using System;
using System.Threading.Tasks;
using EntityFrameworkExercise.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkExercise.Models
{
    public class LoggerService
    {
        private readonly RequestDelegate next;
        private ILogger<LoggerService> logger;

        public LoggerService(RequestDelegate next, ILogger<LoggerService> logger)
        {
            this.next = next;
            this.logger = logger;

        }

        public async Task Invoke(HttpContext httpContext, ApplicationContext AppContext)
        {
            var httpPath = httpContext.Request.Path.ToString();
            var httpMethod = httpContext.Request.Method;
            var httpContent = httpContext.Request.Body.ToString();
            var date = DateTime.Now;

            await next.Invoke(httpContext);

            var httpRespCode = httpContext.Response.StatusCode;
            string logInformation = $"Path: {httpPath}, Method: {httpMethod}, Content: {httpContent},  Date: {date}, Response status code: {httpRespCode}";
            if (httpRespCode == 200)
            {
                AppContext.Logs.Add(new Log(httpPath, httpMethod, httpContent, "information"));
                logger.LogInformation(logInformation);
            }
            else
            {
                AppContext.Logs.Add(new Log(httpPath, httpMethod, httpContent, "error"));
                logger.LogError(logInformation);
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class LoggerServiceExtensions
    {
        public static IApplicationBuilder UseLoggerService(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoggerService>();
        }
    }
}
