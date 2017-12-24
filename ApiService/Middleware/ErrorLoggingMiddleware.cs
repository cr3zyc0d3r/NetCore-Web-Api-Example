using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Serilog;
using Serilog.Events;

namespace ApiService.Middleware
{
    public class ErrorLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        private static readonly ILogger _logger = Log.ForContext<ErrorLoggingMiddleware>();
        const string LogTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode}";

        public ErrorLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);

                var statusCode = httpContext.Response?.StatusCode;
                var level = statusCode > 499 ? LogEventLevel.Error : LogEventLevel.Information;

                var log = level == LogEventLevel.Error ? HandleException(httpContext) : _logger;
                log.Write(level, LogTemplate, httpContext.Request.Method, httpContext.Request.Path, statusCode);
            }
            catch (Exception ex) when (LogException(httpContext, ex)) { }

        }

        static bool LogException(HttpContext httpContext, Exception ex)
        {
            HandleException(httpContext)
                .Error(ex, LogTemplate, httpContext.Request.Method, httpContext.Request.Path, 500);

            return false;
        }

        static ILogger HandleException(HttpContext context)
        {
            var request = context.Request;

            var result = Log
                .ForContext("RequestHeaders", request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()), destructureObjects: true)
                .ForContext("RequestHost", request.Host)
                .ForContext("RequestProtocol", request.Protocol);

            if (request.HasFormContentType)
                result = result.ForContext("RequestForm", request.Form.ToDictionary(v => v.Key, v => v.Value.ToString()));

            return result;
        }
    }


    public static class ErrorLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorLoggingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorLoggingMiddleware>();
        }
    }
}
