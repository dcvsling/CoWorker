
namespace CoWorker.DependencyInjection.Abstractions
{
    using Microsoft.Extensions.DependencyInjection;
    using System;

    public interface IInternalServiceProvider<T> : IServiceProvider,IDisposable
    {
        IServiceProvider Clone();
        void AddTo(IServiceCollection services);
    }
}