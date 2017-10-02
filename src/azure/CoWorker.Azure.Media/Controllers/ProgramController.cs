using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.MediaServices.Client;
using MediaService.Core;
using CoWorker.Azure.Media.Repository;

namespace CoWorker.Azure.Media.Controllers
{
    [Authorize]
    [Route("api/program")]
    public class ProgramController : BaseController
    {
        private readonly IRepository<IProgram> _repo;
        private readonly IRepository<IChannel> _channelRepo;
        private readonly string _channelName;

        public ProgramController(IRepository<IProgram> repo)
        {
            _repo = repo;
        }
        private IRepository<IProgram> SetChannel(string channel)
            => _repo is ProgramRepository newRepo ? newRepo.With(channel) : _repo;
        [HttpGet]
        public Task<IActionResult> Get()
         => Ok(() => _repo.Query(x => true));

        [HttpGet("{name}")]
        public Task<IActionResult> Get(string name,string channel)
            => Ok(SetChannel(channel).FindAsync(name));

        [HttpPost]
        
        public Task<IActionResult> Post(string name, string channel)
            => Created(SetChannel(channel).CreateAsync(name));

        [HttpDelete]
        
        public Task<IActionResult> Delete(string name, string channel)
            => NoContent(SetChannel(channel).DeleteAsync(name));

        [HttpPut]
        
        public Task<IActionResult> Put(string name, string channel)
            => NoContent(SetChannel(channel).UpdateAsync(name));
    }
}
