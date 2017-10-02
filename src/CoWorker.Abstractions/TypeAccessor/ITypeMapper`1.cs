
namespace CoWorker.Abstractions.TypeAccessor
{
    using System;


    public interface ITypeMapper<T>
    {
        TValue Get<TValue>(string name);
        void Set(String name, object val);
    }
}