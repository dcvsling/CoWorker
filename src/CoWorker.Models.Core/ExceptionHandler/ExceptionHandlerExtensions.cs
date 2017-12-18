using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using CoWorker.Models.Abstractions;
using CoWorker.Primitives;
using CoWorker.Models.Abstractions.Filters;

namespace CoWorker.Models.Core.ExceptionHandler
{

    public static class ExceptionHandlerExtensions
    {
        public static IServiceCollection AddExceptionHandler(this IServiceCollection services)
        {
            services.TryAddSingleton<IConfigureOptions<ExceptionHandlerOptions>, ExceptionHandlerConfigureOptions>();
            services.TryAddSingleton<IExceptionHandler, DeveloperExceptionHandler>();
            services.TryAddSingleton<IExceptionHandler, ProductionExceptionHandler>();
            return services;
        }

        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder app)
            => app.UseExceptionHandler(
                app.ApplicationServices
                    .GetService<IOptionsMonitor<ExceptionHandlerOptions>>().Get(app.ApplicationServices.GetService<IHostingEnvironment>().EnvironmentName));
    }
}
