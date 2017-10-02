namespace CoWorker.Builder
{
    using CoWorker.DependencyInjection.Abstractions;
    using Microsoft.Extensions.DependencyInjection;
	using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection TryAddServices<TMark>(
            this IServiceCollection services,
            Action<IServiceCollection> config)
            where TMark : class
            => services.Any(x => x.ServiceType == typeof(TMark))
                ? services
                : services.AddAndMark<TMark>(config);

        private static IServiceCollection AddAndMark<TMark>(
            this IServiceCollection services,
            Action<IServiceCollection> config)
            where TMark : class
        {
            config(services);
            services.AddSingleton<TMark>();
            return services;
        }

        public static T Create<T>(this Action<T> builder, Func<T> factory)
            where T : class
        {
            var result = factory();
            builder(result);
            return result;
        }
        public static T Create<T>(this Action<T> builder)
            where T : class,new()
        {
            var result = new T();
            builder(result);
            return result;
        }

        public static TService Create<TService,TImplement>(
            this Action<TService> builder,
            Func<TImplement> factory)
            where TService : class
            where TImplement : class,TService,new()
        {
            var result = factory();
            builder(result);
            return result;
        }

        internal static Action<IServiceCollection> LazyCopyTo(this ICollection<ServiceDescriptor> services)
            => seq => services.Each(seq.Add);

        public static T RunIf<T>(this T services, Func<bool> predicate, Action<T> config)
        {
            (predicate() ? config : Helper.Empty<T>()).Invoke(services);
            return services;
        }

        public static T ConfigureBy<T>(this T t,Action<T> config)
        {
            config(t);
            return t;
        }
    }
}