namespace CoWorker.Builder
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Rewrite;
    using Microsoft.AspNetCore.Server.Kestrel.Core;
    using Microsoft.Extensions.DependencyInjection;
    using System.Net;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHttpsRedirect(this IServiceCollection services)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            services.Configure<RewriteOptions>(o => o.AddRedirectToHttpsPermanent());
            return services;
        }

        public static IServiceCollection AddKestrelHttps(this IServiceCollection services)
        {
            services.Configure<KestrelServerOptions>(
                o => o.Listen(new IPEndPoint(
                    IPAddress.Loopback, 443),
                    l => l.UseHttps("sslkey.pfx", "1")
                ));
            return services;
        }
        public static IWebHostBuilder AddHostingStartup<THostingStartup>(this IWebHostBuilder builder) where THostingStartup : IHostingStartup, new()
        {
            new THostingStartup().Configure(builder);
            return builder;
        }
    }
}