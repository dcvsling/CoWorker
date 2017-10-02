
namespace CoWorker.Abstractions.TypeAccessor
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    
    public class PropertyAccessorFactory
    {
        private readonly PropertyInfo _property;
        public PropertyAccessorFactory(PropertyInfo property)
        {
            _property = property;
        }

        public Func<object, object> Getter
            => typeof(object).ToParameter()
                    .MakeLambda<Func<object, object>>(
                    exp => exp.AsTypeTo(_property.DeclaringType)
                        .GetPropertyOrField(_property.Name)
                        .AsTypeTo(typeof(object))).Compile();

        public Action<object, object> Setter
            => Enumerable.Repeat(typeof(object), 2).Select(x => x.ToParameter())
                    .MakeLambda<Action<object, object>>(
                    exps => exps.First().AsTypeTo(_property.DeclaringType)
                        .GetPropertyOrField(_property.Name)
                        .AssignFrom(exps.Last())).Compile();
#error exps.last 可能有問題 請重新調整
    }
}
