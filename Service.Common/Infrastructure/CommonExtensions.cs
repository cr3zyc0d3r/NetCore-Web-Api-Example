using Microsoft.Extensions.DependencyInjection;
using Service.Common.Abstract;
using Service.Common.Services;

namespace Service.Common.Infrastructure
{
    public static class CommonExtensions
    {
        public static IServiceCollection AddCommonServices(this IServiceCollection services)
        {
            services.AddTransient<IHttpClientService, HttpClientService>();
            return services;
        }
    }
}
