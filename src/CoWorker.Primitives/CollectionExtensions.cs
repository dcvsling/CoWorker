using System.Linq;
using System;
using System.Collections.Generic;

namespace CoWorker.Primitives
{

    public static class CollectionExtensions
    {
        public static void TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> map, TKey key = default, TValue value = default)
        {
            if (!map.ContainsKey(key))
                map.Add(key, value);
            else
                map[key] = value;
        }
    }
}
