
namespace CoWorker.Azure.Media.Controllers
{
    using System.Threading.Tasks;
    using CoWorker.Azure.Media.Repository;
    using Microsoft.WindowsAzure.MediaServices.Client;
    using Microsoft.AspNetCore.Mvc;
    using System;

    public class StreamingEndPointController : BaseController
    {
        private readonly IRepository<IStreamingEndpoint> _repo;
        private readonly StreamingEndpointBaseCollection _stream;

        public StreamingEndPointController(StreamingEndpointBaseCollection stream,IRepository<IStreamingEndpoint> repo)
        {

            this._repo = repo;
            this._stream = stream;
        }

        [HttpGet]
        public Task<IActionResult> Get()
              => Ok(() => _repo.Query(x => true));

        [HttpGet("{name}")]
        public Task<IActionResult> Get(string name)
            => Ok(_repo.FindAsync(name));

        [HttpDelete]
        public Task<IActionResult> Delete(string name)
            => NoContent(_repo.DeleteAsync(name));

        [HttpPut]
        public Task<IActionResult> Put(string name)
            => NoContent(_repo.UpdateAsync(name));
        
        [HttpPost]
        public Task<IActionResult> Post(string name = null)
            => Created(_stream.CreateAsync(new StreamingEndpointCreationOptions(name ?? Guid.NewGuid().ToString(), 1)));
    }
}
