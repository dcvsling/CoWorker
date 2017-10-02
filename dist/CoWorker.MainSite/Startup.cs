
using CoWorker.Builder;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using System;
using System.IO;

namespace CoWorker.MainSite
{
    public class HostStartup : IHostingStartup
    {
        public static IWebHostBuilder Initialize(IWebHostBuilder builder)
        {
            (new HostStartup() as IHostingStartup).Configure(builder);

            return builder;
        }

        public void ConfigureAppConfiguration(WebHostBuilderContext context, IConfigurationBuilder builder)
        {
            context.Configuration = builder
                .AddEnvironmentVariables()
                .AddInMemoryCollection(context.Configuration.AsEnumerable())
                .Build();
        }

        public void ConfigureLogging(WebHostBuilderContext context, ILoggingBuilder builder)
            => builder.SetMinimumLevel(LogLevel.Trace)
                .AddConfiguration(context.Configuration)
                .AddAzureWebAppDiagnostics();

        public void ConfigureService(WebHostBuilderContext context, IServiceCollection services)
        {
            if (context.HostingEnvironment.IsDevelopment())
                services.AddKestrelHttps()
                    .ConfigureApplicationCookie(o => o.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest)
                    .ConfigureExternalCookie(o => o.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest);
            Helper.InitDefaultJsonSetting();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            
        }

        void IHostingStartup.Configure(IWebHostBuilder builder)
        {
            builder.CaptureStartupErrors(true)
                .UseSetting(WebHostDefaults.ApplicationKey, PlatformServices.Default.Application.ApplicationName)
                .ConfigureAppConfiguration(ConfigureAppConfiguration)
                .ConfigureLogging(ConfigureLogging)
                .ConfigureServices(ConfigureService)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseWebRoot("wwwroot")
                .UseSetting(WebHostDefaults.DetailedErrorsKey, "true")
                .UseStartup<HostStartup>();
        }
    }
}
