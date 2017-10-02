using CoWorker.Abstractions.Cache;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Microsoft.Extensions.DependencyModel;

namespace CoWorker.Abstractions.TypeStore
{
    public class TypeStoreManager : ITypeStore
    {
        private readonly IDictionary<Func<Type, bool>, Type> _typeCache;
        private readonly IEnumerable<AssemblyName> _names;
        private readonly IOptionsCache<Assembly> _cache;

        public TypeStoreManager(IOptionsCache<Assembly> cache)
        {
            _names = DependencyContext.Default.GetDefaultAssemblyNames();
            _cache = cache;
            _typeCache = new Dictionary<Func<Type, bool>, Type>();
        }

        public IEnumerable<Type> List
            => _names.Select(x => _cache.GetOrAdd(x.Name, x.Load))
                .SelectMany(x => x.ExportedTypes);

        public Type Find(Func<Type, bool> predicate)
            => _typeCache.TryGetValue(predicate, out var result)
                ? result ?? FindAndCache(predicate)
                : FindAndCache(predicate);

        private Type FindAndCache(Func<Type, bool> predicate)
        {
            var result = List.FirstOrDefault(predicate);
            _typeCache.Add(predicate, result);
            return result;
        }
    }
}