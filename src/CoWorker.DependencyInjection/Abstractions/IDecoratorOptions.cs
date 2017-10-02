using System;

namespace CoWorker.DependencyInjection.Abstractions
{
    public interface IDecoratorOptions<T> where T : class
    {
        void Decorate(String name, T t, Action<T> next);
        void Decorate(T t, Action<T> next);
    }
}