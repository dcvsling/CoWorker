using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace CoWorker.Models.Abstractions.Filters
{

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAppPipe<TApplicationPipe>(this IServiceCollection services)
            where TApplicationPipe : class,IApplicationFilter
        {
            services.AddSingleton<IApplicationFilter, TApplicationPipe>()
                .TryAddSingleton<IStartupFilter,PipeStartupFilter>();

            return services;
        }
    }
}
