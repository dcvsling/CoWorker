using System.Linq;
namespace System.Dynamic
{
    using System.Linq.Expressions;

    public class QueryProvider : IQueryProvider
    {
        private readonly IQueryProvider _provider;
        private readonly ExpressionVisitor _visitor;

        private QueryProvider(QueryProvider provider)
        {
            this._provider = provider;
            this._visitor = provider._visitor;
        }

        public QueryProvider(IQueryProvider provider,ExpressionVisitor visitor = default)
        {
            this._provider = provider;
            this._visitor = visitor;
        }
        public IQueryable CreateQuery(Expression expression)
        {
            var result = _provider.CreateQuery(expression);
            return new Queryable(new QueryProvider(result.Provider), result.ElementType, result.Expression);
        }
        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            var result = _provider.CreateQuery<TElement>(expression);
            return new Queryable<TElement>(result.Provider);
        }
        public Object Execute(Expression expression)
        {
            _visitor.Visit(expression);
            return _provider.Execute(expression);
        }
        public TResult Execute<TResult>(Expression expression)
        {
            _visitor.Visit(expression);
            return _provider.Execute<TResult>(expression);
        }
    }
}
