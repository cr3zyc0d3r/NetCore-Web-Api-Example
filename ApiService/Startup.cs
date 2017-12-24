using System;
using ApiService.Middleware;
using ExternalApi.Client.Config;
using ExternalApi.Client.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.Common.Infrastructure;

namespace ApiService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddXmlDataContractSerializerFormatters();

            services.AddCommonServices();
            services.AddExternalAPiClient();

            services.Configure<ExternalClientConfig>(options => Configuration.GetSection("ExternalApi").Bind(options));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseExceptionHandler();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseErrorLoggingMiddleware();
            app.UseHttpFromHeaderMiddleware();

            app.UseMvc();
        }
    }
}
