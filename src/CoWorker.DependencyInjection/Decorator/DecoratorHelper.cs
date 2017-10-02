using CoWorker.DependencyInjection.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Linq;

namespace CoWorker.DependencyInjection.Decorator
{
    public static class DecoratorHelper
    {
        public static IServiceCollection Decorate<T>(
            this IServiceCollection services,
            string name,
            Func<T, T> config)
            where T : class
            => services.AddTransient<IDecoratorOptions<T>>(p => new DecoratorOptions<T>(name, config));

        public static IServiceCollection AddCommandHandler<THandler, TCommand>(this IServiceCollection services)
            where THandler : class,ICommandHandler<TCommand>
            where TCommand : class
            => services.AddSingleton<ICommandHandler<TCommand>, THandler>();

        public static IServiceCollection AddCommandDecorator<THandler, TDecorator, TCommand>(this IServiceCollection services)
            where THandler : class, ICommandHandler<TCommand>
            where TCommand : class
            where TDecorator : CommandDecorator<THandler, TCommand>
        {
            var srv = services.First(x => x.ServiceType == typeof(ICommandHandler<TCommand>));
            services.Replace(ServiceDescriptor
                .Singleton(
                    typeof(ICommandHandler<TCommand>),
                    srv.ImplementationFactory
                        ?? (p => ActivatorUtilities.GetServiceOrCreateInstance(p,srv.ImplementationType))));
            return services;
        }

        public static IServiceCollection AddHandler(this IServiceCollection services)
            => services.AddSingleton<IProcessHandler, ProcessHandler>()
                .AddSingleton(typeof(IQueryHandler<>),typeof(QueryHandler<>));
    }

    public interface IProcessHandler
    {
        IProcessHandler<TCommand> GetHandler<TCommand>() where TCommand : class;
    }

    public class ProcessHandler : IProcessHandler
    {
        private readonly IServiceProvider _provider;

        public ProcessHandler(IServiceProvider provider)
        {
            this._provider = provider;
        }
        public IProcessHandler<TCommand> GetHandler<TCommand>() where TCommand : class
        {
            return new ProcessHandler<TCommand>(
                _provider.GetService<ICommandHandler<TCommand>>(),
                _provider.GetService<IQueryHandler<TCommand>>()
                );
        }
    }
}
