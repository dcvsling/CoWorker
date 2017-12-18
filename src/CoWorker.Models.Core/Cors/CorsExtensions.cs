using Microsoft.AspNetCore.Hosting;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Cors.Infrastructure;
using CoWorker.Primitives;

namespace CoWorker.Models.Core.Cors
{
    public static class CorsExtensions
    {
        public static IServiceCollection AddCorsWithConfiguration(this IServiceCollection services)
        {
            services.AddCors()
                .AddSingleton<IConfigureOptions<CorsOptions>, CorsConfigureOptions>();

            return services;
        }

        public static IApplicationBuilder UseCors(this IApplicationBuilder app)
            => app.UseCors(app.ApplicationServices.GetService<IOptionsMonitor<CorsOptions>>().Get(app.ApplicationServices.GetService<IHostingEnvironment>().EnvironmentName).DefaultPolicyName);
    }
}
