using System.Security.Claims;

using System.Threading;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.MediaServices.Client;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.IO;
using CoWorker.Net.FileUpload;
using CoWorker.Azure.Media.Repository;
using CoWorker.Azure.Media.Internal;

namespace CoWorker.Azure.Media.Controllers
{
    [Route("api/asset")]
    public class AssetController : BaseController
    {
        private readonly FileUploadHandler _upload;
        private readonly IRepository<IAsset> _repo;

        public AssetController(IRepository<IAsset> repo, FileUploadHandler upload)
        {
            _upload = upload;
            _repo = repo;
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
        [DisableFormValueModelBinding]
        public Task<IActionResult> Post()
            => Created(_upload.Upload(Path.GetTempFileName()));

        [HttpPost("{name}")]
        [DisableFormValueModelBinding]
        public async Task<IActionResult> Post([FromRoute]string name,[FromBody] List<IFormFile> files)
        {
            var email = HttpContext.User.FindFirst(ClaimTypes.Email).Value;
            var assets = await _repo.FindAsync(email)
                ?? await _repo.CreateAsync(email);
            var assetfile = await assets.AssetFiles.CreateAsync(name,CancellationToken.None);

            long size = files.Sum(f => f.Length);

            var filePath = Path.GetTempFileName();

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                        await assetfile.UploadAsync(
                            filePath, 
                            new BlobTransferClient(), 
                            assets.Locators.FirstOrDefault(x => x.Path == filePath), 
                            CancellationToken.None);
                    }
                }
            }

            return new OkObjectResult(new { count = files.Count, size, filePath });
        }
    }
}
