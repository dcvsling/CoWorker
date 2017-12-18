using System.Security.Cryptography.X509Certificates;
namespace CoWorker.Models.Core.Https
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Rewrite;
    using Microsoft.AspNetCore.Server.Kestrel.Core;
    using CoWorker.Models.Abstractions.Stores;
    using Microsoft.Extensions.DependencyInjection;
    using System.Net;

    public static class HttpsExtensions
    {
        public static IServiceCollection AddHttps(this IServiceCollection services)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            services.Configure<RewriteOptions>(o => o.AddRedirectToHttpsPermanent());
            services.AddCertificate("sslkey.pfx","1");
            services.Configure<KestrelServerOptions>(
                o => {
                    o.Listen(new IPEndPoint(IPAddress.Loopback, 80));
                    o.Listen(new IPEndPoint(IPAddress.Loopback, 443),l => l.UseHttps(o.ApplicationServices.GetService<IStore<X509Certificate2>>().Get("sslkey")));
                });
            return services;
        }
    }
}