using CoWorker.DependencyInjection.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CoWorker.DependencyInjection.Decorator
{

    public class DecoratorOptions<T> : IDecoratorOptions<T> where T : class
    {
        private readonly Func<T,T> _config;
        private readonly string _name;

        public DecoratorOptions(string name,Func<T,T> config)
        {
            _config = config;
            _name = name;
        }

        public void Decorate(string name,T t,Action<T> next)
            => Helper.If<T>(
                x => name == _name ,
                x => next(_config(x)) ,
                x => next(x))(t);

        public void Decorate(T t, Action<T> next)
            => Decorate(string.Empty, t, next);
    }
}
