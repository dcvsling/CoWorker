namespace System.Collections.Generic
{
    using System;
    using System.Linq;
    public static class CollectionHelper
    {
        public static IEnumerable<TValue> Match<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Func<TKey, bool> predicate)
            => dictionary.Where(x => predicate(x.Key)).Select(x => x.Value);

        public static IEnumerable<TValue> Match<T,TKey, TValue>(this IEnumerable<T> sequences,IDictionary<TKey, TValue> dictionary, Func<TKey, bool> predicate)
            => dictionary.Where(x => predicate(x.Key)).Select(x => x.Value);

        public static IEnumerable<TValue> Match<T, TKey, TValue>(this IEnumerable<T> sequences, Func<T, TKey> source, Func<T, TKey, bool> predicate, Func<TKey, TValue> result)
            => sequences.Select(x => (source: x, key: source(x)))
                        .Where(x => predicate(x.source, x.key))
                        .Select(x => result(x.key));

		public static IDictionary<TKey, TValue> AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> map, TKey key, TValue value)
		{
			if (!map.ContainsKey(key))
				map.Add(key, value);
			return map;
		}

        public static T Get<T>(this IDictionary map, object key)
            => map.Contains(key) && map[key] is T ? (T)map[key] : default(T);
		public static TValue Get<TKey,TValue>(this IDictionary<TKey, TValue> map, TKey key)
			=> map.ContainsKey(key) ? map[key] : default(TValue);

		public static bool HasAllAndEqual<TKey, TValue>(this IDictionary<TKey, TValue> left, IDictionary<TKey, TValue> right)
            => left.Join(right, x => x.Key, x => x.Key, (x, y) => x.Equals(y))
                .Aggregate((x, y) => x && y);

        public static bool FullEqual<TKey,TValue>(this IDictionary<TKey,TValue> left,IDictionary<TKey,TValue> right)
            => left.Keys.SequenceEqual(right.Keys) && left.Join(right, x => x.Key, x => x.Key, (x, y) => x.Equals(y))
                .Aggregate((x, y) => x && y);

		public static bool Exist<TKey, TValue>(this IDictionary<TKey, TValue> map, TKey key = default, TValue value = default)
			=> map.ContainsKey(key) && map[key].Equals(value);

        public static void TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> map, TKey key = default, TValue value = default)
        {
            if (!map.ContainsKey(key))
                map.Add(key, value);
            else
                map[key] = value;
        }
    }
}
