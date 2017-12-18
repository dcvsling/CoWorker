using CoWorker.Models.Abstractions;
using System;
using CoWorker.Models.Abstractions.Filters;
using CoWorker.Models.Abstractions.Stores;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationFilter(this IServiceCollection services)
            => services.AddSingleton<IStartupFilter, PipeStartupFilter>();

        public static IServiceCollection AddAppPipe<TApplicationPipe>(this IServiceCollection services)
            where TApplicationPipe : class,IApplicationFilter
            => services.AddSingleton<IApplicationFilter, TApplicationPipe>();

        public static IServiceCollection AddStores(this IServiceCollection services)
            => services.AddSingleton(typeof(IStore<>), typeof(Store<>));

        public static IServiceCollection AddRepositoryDefinition<TContext>(this IServiceCollection services, Type repoType)
            where TContext : DbContext
        {
            services.TryAddSingleton(typeof(IRepository<>), repoType);
            services.AddDbContextPool<TContext>(DefaultConfig.ConfigureDbContextOptionsBuilder("coworker-db", repoType.Assembly.GetName().Name));
            return services;
        }
    }
}
