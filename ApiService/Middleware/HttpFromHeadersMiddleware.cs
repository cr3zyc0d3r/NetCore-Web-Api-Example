using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace ApiService.Middleware
{
    public class HttpFromHeadersMiddleware
    {
        private readonly RequestDelegate _next;

        private static readonly ILogger _logger = Log.ForContext<ErrorLoggingMiddleware>();
        const string LogTemplate = "Header FROM added to HTTP {RequestMethod} {RequestPath}";

        public HttpFromHeadersMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            httpContext.Request.Headers.TryGetValue("From", out var userEmail);

            if (string.IsNullOrWhiteSpace(userEmail))
            {
                httpContext.Request.Headers.Add("From", "automatically added");
                _logger.Information(LogTemplate, httpContext.Request.Method, httpContext.Request.Path);
            }

            await _next(httpContext);
        }

    }

    public static class HttpFromHeaderMiddlewareExtensions
    {
        public static IApplicationBuilder UseHttpFromHeaderMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HttpFromHeadersMiddleware>();
        }
    }
}
