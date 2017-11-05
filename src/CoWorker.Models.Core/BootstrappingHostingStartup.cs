using Microsoft.AspNetCore.Builder;


namespace CoWorker.Models.Core
{
    using System.Linq;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Logging;
    using CoWorker.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.PlatformAbstractions;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.IO;
    using CoWorker.Primitives;

    public class BootstrappingHostingStartup : IHostingStartup
    {
        void IHostingStartup.Configure(IWebHostBuilder builder)
        {
            new HostingStartupProvider()
                .HostingStartups
                .Each(x => x.Configure(builder));

            builder.CaptureStartupErrors(true)
                .ConfigureAppConfiguration(ConfigureAppConfiguration)
                .ConfigureLogging(ConfigureLogging)
                .ConfigureServices(ConfigureService)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseWebRoot("wwwroot")
                //.RunIf(
                //    () => builder.GetSetting(WebHostDefaults.EnvironmentKey) == "Development",
                //    srv => srv.AddKestrelHttps())
                .UseSetting(WebHostDefaults.DetailedErrorsKey, "true")
                .Configure(Helper.Empty<IApplicationBuilder>());
        }

        public void ConfigureAppConfiguration(WebHostBuilderContext context, IConfigurationBuilder builder)
        {
            context.Configuration = builder
                .AddEnvironmentVariables()
                .AddInMemoryCollection(context.Configuration.AsEnumerable()).Build();
            var config = context.Configuration.GetSection("keyvault");
            context.Configuration = builder.AddAzureKeyVault(
                        $"https://{config.GetValue<string>("name")}.vault.azure.net/",
                        config.GetValue<string>("clientid"),
                        config.GetValue<string>("clientsecret"))
                .Build();
        }

        public void ConfigureLogging(WebHostBuilderContext context, ILoggingBuilder builder)
            => builder.SetMinimumLevel(LogLevel.Trace)
                .AddConfiguration(context.Configuration)
                .AddAzureWebAppDiagnostics();

        public void ConfigureService(WebHostBuilderContext context, IServiceCollection services)
        {
            services.AddOptions()
                    .AddElm()
                    .AddHttpsRedirect()
                    .AddAntiforgeryMiddleware()
                    .AddSingleton<IStartupFilter, StartupFilterBase>()
                    .AddSingleton(services);

            if (context.HostingEnvironment.IsDevelopment())
                services.AddKestrelHttps();

            Helper.InitDefaultJsonSetting();
        }
    }

}
