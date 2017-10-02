using System;
using System.Collections.Generic;

namespace CoWorker.Abstractions.TypeStore
{
    public interface ITypeStore
    {
        IEnumerable<Type> List { get; }
        Type Find(Func<Type, Boolean> predicate);
    }
}