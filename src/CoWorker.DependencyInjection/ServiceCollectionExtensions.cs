using Microsoft.AspNetCore.Hosting;
using System.Linq;
using CoWorker.DependencyInjection.Abstractions;

namespace CoWorker.Builder
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using CoWorker.DependencyInjection.Configuration;

    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddConfiguration(
            this IServiceCollection services,
            Action<IConfigurationBuilder> builder = default)
        {
            services.TryAddSingleton(typeof(IConfigureOptions<>), typeof(ConfigurationConfigureOptions<>));

            if(services.Any(x => x.ServiceType == typeof(WebHostBuilderContext)))
            {
                services.TryAddSingleton(p => p.GetRequiredService<WebHostBuilderContext>().Configuration);
            }
            else
            {
                services.TryAddSingleton<IConfigurationBuilder, ConfigurationBuilder>();
                services.TryAddSingleton<IConfiguration, Configuration>();
            }
            return services.AddConfigurationConfigureOptions(builder);
        }

        public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration config)
            => services.Replace(ServiceDescriptor.Singleton(typeof(IConfiguration),config));

        private static IServiceCollection AddConfigurationConfigureOptions(
            this IServiceCollection services,
            Action<IConfigurationBuilder> config)
        {
            if (config == null) return services;
            services.AddSingleton<IConfigurationBuilderConfigureOptions>(
                   p => new ConfigurationBuilderConfigureOptions(config));
            return services;
        }
    }
}