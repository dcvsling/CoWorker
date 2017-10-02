using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using CoWorker.DependencyInjection.Abstractions;

namespace CoWorker.DependencyInjection.Decorator
{
    public class QueryDecorator<TEntity> : IQueryHandler<TEntity>
        where TEntity : class
    {
        private readonly IQueryable<TEntity> _query;

        protected QueryDecorator(IQueryable<TEntity> query)
        {
            this._query = query;
        }
        public IEnumerable<TResult> Handle<TResult>(Func<IQueryable<TEntity>, IEnumerable<TResult>> query)
            => query(_query);
    }
}