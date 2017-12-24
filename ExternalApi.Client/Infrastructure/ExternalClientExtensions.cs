using Microsoft.Extensions.DependencyInjection;
using ExternalApi.Client.Abstract;
using ExternalApi.Client.Client;

namespace ExternalApi.Client.Infrastructure
{
    public static class ExternalClientExtensions
    {
        public static IServiceCollection AddExternalAPiClient(this IServiceCollection services)
        {
            services.AddTransient<IExternalClient, ExternalClient>();
            return services;
        }
    }
}
