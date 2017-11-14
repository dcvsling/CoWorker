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
    public interface IExceptionHandler : INamedHandler
    {

    }

    public static class ExceptionHandlerExtensions
    {
        public static IServiceCollection AddExceptionHandler(this IServiceCollection services)
        {
            services.TryAddSingleton<IPostConfigureOptions<ExceptionHandlerOptions>, ExceptionHandlerConfigureOptions>();
            services.TryAddSingleton<IExceptionHandler, DeveloperExceptionHandler>();
            services.TryAddSingleton<IExceptionHandler, ProductionExceptionHandler>();
            return services;
        }

        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder app)
            => app.UseExceptionHandler(
                app.ApplicationServices
                    .GetService<IOptionsSnapshot<ExceptionHandlerOptions>>()
                    .Get(app.ApplicationServices.GetService<IHostingEnvironment>().EnvironmentName));
    }
}
