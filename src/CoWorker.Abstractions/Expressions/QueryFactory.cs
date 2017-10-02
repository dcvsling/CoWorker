namespace System.Dynamic
{
    using System.Linq;
    using System.Linq.Expressions;

    public class QueryFactory
    {
        public IQueryable<T> Create<T>(IQueryProvider provider = null)
        {
            provider = provider ?? new EnumerableQuery<T>(Enumerable.Empty<T>());
            return new Queryable<T>(provider);
        }

        public IQueryable<T> Create<T>(ExpressionVisitor visitor = null)
        {
            var provider = new QueryProvider(new EnumerableQuery<T>(Enumerable.Empty<T>()), visitor);
            return new Queryable<T>(provider);
        }
    }
}
