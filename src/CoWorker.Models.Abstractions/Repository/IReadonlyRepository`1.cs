using System.Linq;
using System.Linq.Expressions;

using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace CoWorker.Models.Abstractions
{

    public interface IReadonlyRepository<TModel> where TModel : class, new()
    {
        IQueryable<TModel> Query();
        Task<TModel> Find(object id);
    }
}
