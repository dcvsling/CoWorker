using System.Collections;
using Microsoft.AspNetCore.Server.WebListener;
using System.Linq;
using Microsoft.AspNetCore.Server.HttpSys;
namespace CoWorker.Models.Core.Https
{
    using CoWorker.Primitives;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Rewrite;
    using Microsoft.AspNetCore.Server.Kestrel.Core;
    using Microsoft.Extensions.DependencyInjection;
    using System.Net;

    public static class HttpsExtensions
    {
        public static IServiceCollection AddHttps(this IServiceCollection services)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            services.Configure<RewriteOptions>(o => o.AddRedirectToHttpsPermanent());
            services.Configure<KestrelServerOptions>(
                o => o.Listen(new IPEndPoint(
                    IPAddress.Loopback, 443),
                    l => l.UseHttps("sslkey.pfx", "1")
                ));
            services.PostConfigureAll<HttpSysOptions>(o => o.UrlPrefixes.ToHttps());
            services.PostConfigureAll<WebListenerOptions>(o => o.ListenerSettings.UrlPrefixes.ToHttps());
            return services;
        }

        internal static void ToHttps(this UrlPrefixCollection urls)
        {
            var https = urls
                .Select(x => UrlPrefix.Create($"{x.Scheme.Replace("http:", "https:")}")) //DevSkim: ignore DS137138
                .ToArray();
            urls.Clear();
            https.Each(x => urls.Add(x));
        }
        public static void ToHttps(this Microsoft.Net.Http.Server.UrlPrefixCollection urls)
        {
            var https = urls
                .Select(x => Microsoft.Net.Http.Server.UrlPrefix.Create($"{x.Scheme.Replace("http:", "https:")}")) //DevSkim: ignore DS137138
                .ToArray();
            urls.Clear();
            https.Each(x => urls.Add(x));
        }
    }
}