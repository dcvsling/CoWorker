using System.Linq;
namespace System.Dynamic
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public class Queryable<T> : IQueryable<T>
    {
        private readonly IQueryProvider _provider;

        public Queryable(IQueryProvider provider)
        {
            this._provider = provider;
            this.ElementType = typeof(T);
            this.Provider = new EnumerableQuery<T>(Expression);
        }

        public Type ElementType { get; }

        public Expression Expression { get; }

        public IQueryProvider Provider { get; }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();
        public IEnumerator<T> GetEnumerator()
            => new Enumerator<T>(_provider.Execute<T>(this.Expression));

        internal class Enumerator<TElement> : IEnumerator<TElement> where TElement : T
        {
            public TElement Current { get; private set; }

            Object IEnumerator.Current => this.Current;

            internal Enumerator(TElement obj)
            {
                Current = obj;
            }

            public Boolean MoveNext() => false;
            public void Reset() { }
            public void Dispose()
            {
                (Current as IDisposable)?.Dispose();
                Current = default;
            }
        }
    }
}
