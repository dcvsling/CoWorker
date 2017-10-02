
namespace CoWorker.Abstractions.TypeAccessor
{
    using System.Reflection;
    using System;

    public class PropertyAccessor : IPropertyAccessor
    {
        private readonly PropertyAccessorFactory _factory;
        private Action<object, object> _setter;
        private Func<object, object> _getter;
        private readonly PropertyInfo _property;

        public Type DeclareType => _property.DeclaringType;
        public string PropertyName => _property.Name;
        public PropertyAccessor(PropertyInfo property)
        {
            _property = property ?? throw new ArgumentException(nameof(property));
            _factory = new PropertyAccessorFactory(property);
        }

        public Object Get(Object obj)
        {
            _getter = _getter ?? _factory.Getter;
            return _getter(obj);
        }
        public void Set(Object obj, Object val)
        {
            _setter = _setter ?? _factory.Setter;
            _setter(obj, val);
        }
    }
}