using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CoWorker.Net.Proxy
{

    public static class ProxyExtensions
    {
        public static IServiceCollection AddProxy(this IServiceCollection services,Action<ProxyOptions> config)
            => services.AddTransient<IHttpClient, DefaultHttpClient>()
                .AddSingleton<ProxyHandler>()
                .AddTransient<ProxyClient>()
                .AddTransient<ProxyMiddleware>()
                .AddSingleton<IConfigureOptions<ProxyOptions>>(p => new ConfigureOptions<ProxyOptions>(o => config(o)));

        public static IApplicationBuilder UsePrefixRouteProxy(this IApplicationBuilder app)
            => app.Use(next => app.ApplicationServices.GetRequiredService<ProxyMiddleware>().Middleware(next));
    }

}