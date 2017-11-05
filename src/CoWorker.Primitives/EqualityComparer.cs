
using System;
using System.Collections.Generic;


namespace CoWorker.Primitives
{
    public class EqualityComparer<T> : IEqualityComparer<T>
    {
        public static IEqualityComparer<T> Create(Func<T, int> gethash)
            => new EqualityComparer<T>((x, y) => x.GetHashCode().Equals(y.GetHashCode()), gethash);
        public static IEqualityComparer<T> Create(Func<T, T, bool> equals)
            => new EqualityComparer<T>(equals, x => x.GetHashCode());
        public static IEqualityComparer<T> Create(Func<T, int> gethash, Func<T, T, bool> equals)
            => new EqualityComparer<T>(equals, gethash);

        public Boolean Equals(T x, T y) => _equals(x, y);
        public Int32 GetHashCode(T obj) => _gethash(obj);

        private readonly Func<T,int> _gethash;
        private readonly Func<T,T,bool> _equals;

        private EqualityComparer(Func<T,T,bool> equals,Func<T,int> gethash)
        {
            _gethash = gethash;
            _equals = equals;
        }
    }
}
