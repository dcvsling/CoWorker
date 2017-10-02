using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using CoWorker.DependencyInjection.Abstractions;

namespace CoWorker.DependencyInjection.Decorator
{
    public class QueryHandler<TEntity> : IQueryHandler<TEntity>
        where TEntity : class
    {
        private readonly IQueryable<TEntity> _query;

        protected QueryHandler(IQueryable<TEntity> query)
        {
            this._query = query;
        }
        public IEnumerable<TResult> Handle<TResult>(Func<IQueryable<TEntity>,IEnumerable<TResult>> query)
            => query(_query);
    }
}