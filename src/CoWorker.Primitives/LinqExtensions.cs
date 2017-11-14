using System.Linq;
using System;
using System.Collections.Generic;

namespace CoWorker.Primitives
{
    public static class LinqExtensions
    {
        public static void Each<T>(this IEnumerable<T> seq,Action<T> action)
            => seq.ToList().ForEach(action);

        public static TAccumulate Aggregate<TSource, TAccumulate>(this IEnumerable<TSource> seq, TAccumulate acumulate, Action<TAccumulate, TSource> action)
            => seq.Aggregate(
                acumulate,
                (seed, next) =>
                 {
                     action(seed, next);
                     return seed;
                 });

    }
}
