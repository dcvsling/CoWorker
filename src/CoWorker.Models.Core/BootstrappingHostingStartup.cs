using CoWorker.Models.Core.ExceptionHandler;
using Microsoft.AspNetCore.Builder;


namespace CoWorker.Models.Core
{
    using System.Linq;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Logging;
    using CoWorker.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.IO;
    using CoWorker.Models.Core.Https;
    using CoWorker.Models.Abstractions.Filters;

    public class BootstrappingHostingStartup : IHostingStartup
    {
        void IHostingStartup.Configure(IWebHostBuilder builder)
        {
            builder.CaptureStartupErrors(true)
                .ConfigureAppConfiguration(ConfigureAppConfiguration)
                .ConfigureLogging(ConfigureLogging)
                .ConfigureServices(ConfigureService)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseWebRoot("wwwroot")
                .UseIISIntegration()
                .Configure(Helper.Empty<IApplicationBuilder>());
        }

        public void ConfigureAppConfiguration(WebHostBuilderContext context, IConfigurationBuilder builder)
        {
            context.Configuration = builder
                .AddEnvironmentVariables()
                .AddInMemoryCollection(context.Configuration.AsEnumerable())
                .Build();
        }

        public void ConfigureLogging(WebHostBuilderContext context, ILoggingBuilder builder)
            => builder.AddAzureWebAppDiagnostics();

        public void ConfigureService(WebHostBuilderContext context, IServiceCollection services)
        {
            services.AddOptions()
                    .AddHttps()
                    .AddExceptionHandler()
                    .AddAntiforgeryMiddleware()
                    .AddAppPipe<CoreApplicationFilter>()
                    .AddSingleton(services);

            Helper.InitDefaultJsonSetting();
        }
    }

}
