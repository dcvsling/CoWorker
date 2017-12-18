using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CoWorker.Models.Abstractions;
using System.Linq;

namespace CoWorker.Models.Mvc
{

    [Controller]
    [Route("[controller]/[model]s")]
    public class QueryController<TModel> where TModel : class,new()
    {
        private readonly IReadonlyRepository<TModel> _repo;

        public QueryController(IReadonlyRepository<TModel> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public Task<IQueryable<TModel>> Get()
            => Task.Run(() => _repo.Query());
        [HttpGet("id")]
        public Task<TModel> Get(object id)
            => _repo.Find(id);
    }
}
