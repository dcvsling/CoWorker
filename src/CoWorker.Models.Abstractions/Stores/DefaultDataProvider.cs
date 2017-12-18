using System;

namespace CoWorker.Models.Abstractions.Stores
{
    public class DefaultDataProvider<T> : IDataProvider<T> where T : class
    {
        private readonly Func<T> _factory;
        private T _value;
        public DefaultDataProvider(string name, Func<T> factory)
        {
            _factory =  factory;
            Name = name;
            _value = default;
        }

        public DefaultDataProvider(string name, T certificate)
        {
            this.Name = name;
            this._value = certificate;
            this._factory = () => _value;
        }

        public string Name { get; }

        public T Value => _value ?? GetValue();

        private T GetValue()
        {
            _value = _factory();
            return _value;
        }
    }
}
