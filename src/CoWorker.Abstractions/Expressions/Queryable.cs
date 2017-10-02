using System.Linq;
namespace System.Dynamic
{
    using System.Collections;
    using System.Linq.Expressions;

    public class Queryable : IQueryable
    {
        public Queryable(IQueryProvider provider,Type type, Expression exp = default)
        {
            this.Provider = provider;
            this.ElementType = type;
            this.Expression = exp;
        }
        public Type ElementType { get; }

        public Expression Expression { get; }

        public IQueryProvider Provider { get; }

        public IEnumerator GetEnumerator()
        {
            var result = Provider.Execute(Expression);
            return result is IEnumerable seq
                ? seq.GetEnumerator()
                : new Enumerator(result);
        }

        internal class Enumerator : IEnumerator
        {
            internal Enumerator(object obj) => this.Current = obj;
            public Object Current { get; }

            public Boolean MoveNext() => false;
            public void Reset() { }
        }
    }
}
