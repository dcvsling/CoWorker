using Microsoft.AspNetCore.ApplicationInsights.HostingStartup;
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using CoWorker.Models.Core;
using Microsoft.Extensions.PlatformAbstractions;
using System.Threading.Tasks;

[assembly: HostingStartup(typeof(ApplicationInsightsHostingStartup))]
[assembly: HostingStartup(typeof(BootstrappingHostingStartup))]

namespace CoWorker.MainSite
{
    public static class Program
    {
        async public static Task Main(string[] args)
        {
            await BuildWebHost(args).StartAsync();
            Console.Read();
        }

        public static IWebHost BuildWebHost(string[] args)
            => WebHost.CreateDefaultBuilder(args)
                .UseSetting(WebHostDefaults.ApplicationKey, PlatformServices.Default.Application.ApplicationName)
                .Build();

    }
}
