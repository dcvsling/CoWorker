using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace CoWorker.DependencyInjection.Abstractions
{
    public interface IQueryHandler<TEntity> where TEntity : class
    {
        IEnumerable<TResult> Handle<TResult>(Func<IQueryable<TEntity>, IEnumerable<TResult>> query);
    }
}