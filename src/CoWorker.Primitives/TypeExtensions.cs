using System.Linq;
using System;
using System.Collections.Generic;

namespace CoWorker.Primitives
{

    public static class TypeExtensions
    {
        public static T As<T>(this object obj)
            => obj is T result ? result : default;
        public static string RemovePostfixName(this Type type)
         => type.Name.RemoveLastPart(x => Char.IsUpper(x));
        public static string RemoveLastPart(
            this string str,
            Func<char, bool> predicate,
            bool ignorehead = true)
            {
                var mark = str.MarkPoint(predicate, ignorehead);
                return str.Substring(0, str.MarkPoint(predicate, ignorehead).LastOrDefault());
            }
        public static IEnumerable<int> MarkPoint<T>(
             this IEnumerable<T> ts,
             Func<T, bool> predicate,
             bool ignorehead)
             => ts.MarkPoint((t, i) => predicate(t), ignorehead);
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
            foreach (var t in seq)
            {
                if (predicate(t, i))
                    yield return i;
                i++;
            }
            yield break;
        }

    }
}
