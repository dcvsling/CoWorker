
namespace CoWorker.DependencyInjection
{
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using System;

    public static class ServiceProviderExtensions
    {
        public static IHostingEnvironment GetHostingEnvironment(this IServiceProvider provider)
            => provider.GetService<IHostingEnvironment>();
        
        public static IConfiguration GetConfiguration(this IServiceProvider provider)
            => provider.GetService<IConfiguration>();

        public static ILogger<T> GetLogger<T>(this IServiceProvider provider)
            => provider.GetService<ILogger<T>>();
    }
}
