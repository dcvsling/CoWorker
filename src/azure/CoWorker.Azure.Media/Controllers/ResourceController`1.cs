using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.MediaServices.Client;
using System;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Collections.Generic;
using CoWorker.Azure.Media.Repository;

namespace CoWorker.Azure.Media.Controllers
{
    [Route("api/[resource]")]
    public class AzureResourceController<TResource> : BaseController 
        where TResource : BaseCollection<TResource>
    {
        private readonly IRepository<TResource> _repo;

        public AzureResourceController(IRepository<TResource> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public Task<IActionResult> Get()
            => Ok(() => _repo.Query(x => true));

        [HttpGet("{name}")]
        public Task<IActionResult> Get(string name)
            => Ok(_repo.FindAsync(name));

        [HttpPost]
        
        public Task<IActionResult> Post(string name)
            => Created(_repo.CreateAsync(name));

        [HttpDelete]
        
        public Task<IActionResult> Delete(string name)
            => NoContent(_repo.DeleteAsync(name));

        [HttpPut]
        
        public Task<IActionResult> Put(string name)
            => NoContent(_repo.UpdateAsync(name));
    }
}
