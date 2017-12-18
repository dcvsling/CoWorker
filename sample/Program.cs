using CoWorker.Builder;
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using Microsoft.Extensions.PlatformAbstractions;
using System.Threading.Tasks;

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
                .UseApplicationInsights()
                .AddAllStartup()
                .UseSetting(WebHostDefaults.ApplicationKey, PlatformServices.Default.Application.ApplicationName)
                .Build();
    }
}
