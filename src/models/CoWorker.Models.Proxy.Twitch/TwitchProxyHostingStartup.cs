using Microsoft.Extensions.DependencyInjection;
using CoWorker.Net.Proxy;
using Microsoft.AspNetCore.Hosting;
using System;

namespace CoWorker.Models.Proxy.Twitch
{
    public class TwitchProxyHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices(
                (ctx, srv) => srv.AddProxy(o =>
                {
                    o.RoutePrefix = "tch";
                    o.HostUrl = "https://api.twitch.tv/kraken";
                }))
                .Configure(app => app.UsePrefixRouteProxy());
        }
    }
}
