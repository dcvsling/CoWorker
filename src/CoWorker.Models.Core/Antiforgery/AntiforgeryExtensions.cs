using Microsoft.AspNetCore.Builder;

using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Antiforgery;
namespace CoWorker.Builder
{
    using CoWorker.Models.Core.Antiforgery;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using System;

    public static class AntiforgeryExtensions
    {
        public static IServiceCollection AddAntiforgeryMiddleware(this IServiceCollection services, Action<AntiforgeryOptions> config = null)
        {
            services.TryAddSingleton<IConfigureOptions<AntiforgeryOptions>, DefaultAntiforgeryConfigureOptions>();
            services.TryAddSingleton<AntiforgeryMiddleware>();
            services.AddAntiforgery(config ?? Helper.Empty<AntiforgeryOptions>());
            return services;
        }

        public static IApplicationBuilder UseAntiforgeryMiddleware(this IApplicationBuilder app)
            => app.Use(app.ApplicationServices.GetRequiredService<AntiforgeryMiddleware>().Middleware);
    }
}