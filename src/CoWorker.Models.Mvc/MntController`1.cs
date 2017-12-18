using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CoWorker.Models.Abstractions;

namespace CoWorker.Models.Mvc
{
    [Controller]
    [Area("mnt")]
    [Route("[model]s")]
    public class MntController<TModel> where TModel : class,new()
    {
        private readonly IRepository<TModel> _repo;
        public MntController(IRepository<TModel> repo)
        {
            _repo = repo;
        }

        [HttpGet("{id}")]
        public Task<TModel> Get([FromRoute] object id)
            => _repo.Find(id);

        [HttpPost]
        public Task<IActionResult> Post([FromBody] TModel model)
            => _repo.Add(model)
                .ContinueWith(t => new CreatedResult(string.Empty, model) as IActionResult);

        [HttpPut]
        public Task<IActionResult> Put([FromBody] TModel model)
            => _repo.Update(model)
                .ContinueWith(t => new OkResult() as IActionResult);
        [HttpDelete]
        public Task<IActionResult> Delete([FromRoute] object id)
            => _repo.Find(id).ContinueWith(t => _repo.Remove(t.Result))
                .ContinueWith(t => new OkResult() as IActionResult);
    }
}
