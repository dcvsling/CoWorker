namespace CoWorker.Builder
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyModel;
    using System;
    using System.Linq;
    using System.Runtime.Loader;
    using CoWorker.Primitives;
    public static class HttpsExtensions
    {
        public static IWebHostBuilder AddHostingStartup<THostingStartup>(this IWebHostBuilder builder) where THostingStartup : IHostingStartup, new()
        {
            new THostingStartup().Configure(builder);
            return builder;
        }

        public static IWebHostBuilder AddAllStartup(this IWebHostBuilder builder)
            => DependencyContext.Default.GetDefaultAssemblyNames()
                .Select(AssemblyLoadContext.Default.LoadFromAssemblyName)
                .Select(x => x.GetCustomAttributesData().FirstOrDefault(y => y.AttributeType == typeof(HostingStartupAttribute))?.ConstructorArguments.First().Value)
                .Where(x => x is Type type)
                .Cast<Type>()
                .Select(x => Activator.CreateInstance(x) as IHostingStartup)
                .Aggregate(builder, (seed, next) => next.Configure(seed));
    }
}