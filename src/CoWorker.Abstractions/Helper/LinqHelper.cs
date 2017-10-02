namespace System.Linq
{
	using System.Collections.Generic;

	public static class LinqExtensions {
        
        public static IEnumerable<T> Expect<T>(this IEnumerable<T> seq)
            => seq.Where(x => x != null);

        public static void Each<T>(this IEnumerable<T> seq, Action<T> action) => seq.ToList().ForEach(action);
        public static void Join<TSource, TJoin, TKey>(
            this IEnumerable<TSource> seq,
            IEnumerable<TJoin> jseq,
            Func<TSource, TKey> selector,
            Func<TJoin, TKey> jselector,
            Action<TSource, TJoin> each)
            => seq.Join(jseq, selector, jselector, (x, y) => (x, y))
                .Each(x => each(x.Item1, x.Item2));

        public static IEnumerable<T> Append<T>(this IOptional<T> left, T right)
            => Enumerable.Empty<T>().Append(left.Value).Append(right);

        public static IEnumerable<T> Do<T>(this IEnumerable<T> seq, Action<T> action)
            => seq.Select(
                x => {
                    action(x);
                    return x;
                });

        public static IEnumerable<T> Do<T>(this IEnumerable<T> seq, Func<T,object> action)
            => seq.Select(
                x => {
                    action(x);
                    return x;
                });

        public static IEnumerable<int> MarkPoint<T>(
            this IEnumerable<T> ts, 
            Func<T, int, bool> predicate,
            bool ignorehead)
        {
            var i = 0;
            var seq = ts;
            if (ignorehead)
            {
                i = 1;
                seq = seq.Skip(1);
            }
            foreach(var t in seq)
            {
                if (predicate(t,i))
                    yield return i;
                i++;
            }
            yield break;
        }
        public static IEnumerable<int> MarkPoint<T>(
            this IEnumerable<T> ts, 
            Func<T, bool> predicate, 
            bool ignorehead)
            => ts.MarkPoint((t, i) => predicate(t),ignorehead);
        
        public static TSeed Aggregate<T,TSeed>(this IEnumerable<T> seq,TSeed seed,Action<TSeed,T> action)
            => seq.Aggregate(
                seed, 
                (sd, next) => {
                     action(sd, next);
                     return sd;
                 });

        public static IEnumerable<TNext> AsEnumerable<T,TNext>(this T t,Func<T,TNext> selector)
        {
            var current = selector(t);
            while(current != null)
            {
                yield return current;
            }
        }
    }
}
