using Microsoft.AspNetCore.Hosting;
namespace CoWorker.Builder
{
    using CoWorker.Net.FileUpload;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using System.Net;
    using Microsoft.AspNetCore.Server.Kestrel.Core;
    using Microsoft.AspNetCore.Rewrite;

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
                    l => l.UseHttps("ssl.pfx", "1")));
            return services;
        }
    }
}