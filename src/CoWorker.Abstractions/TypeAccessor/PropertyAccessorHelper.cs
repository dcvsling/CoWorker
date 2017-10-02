using System;

namespace CoWorker.Abstractions.TypeAccessor
{
    public static class PropertyAccessorHelper
    {
        public static T Get<T>(this IAccessor accessor)
            => accessor.Get().As<T>();
        public static T Get<T>(this IPropertyAccessor accessor,object obj)
            => accessor.Get(obj).As<T>();
    }
}