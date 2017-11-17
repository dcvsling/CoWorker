using CoWorker.Primitives;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Microsoft.Extensions.Options;
using IdentityServer4.Stores;
using System.Collections.Generic;
using IdentityServer4.Models;
using System.Threading.Tasks;
namespace CoWorker.Models.IdentityServer.Resources
{

    public class ResourceStore : IResourceStore
    {
        private readonly AppResources _resources;
        private readonly IOptionsSnapshot<AppResources> _snapshot;

        public ResourceStore(IOptionsSnapshot<AppResources> snapshot)
        {
            _snapshot = snapshot;
        }
        public Task<ApiResource> FindApiResourceAsync(string name)
            => Task.FromResult(_resources.ApiResources.FirstOrDefault(x => x.Name == name));

        public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
            => Task.FromResult(_resources.ApiResources.AsEnumerable());

        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
            => Task.FromResult(_resources.IdentityResources.AsEnumerable());

        public Task<IdentityServer4.Models.Resources> GetAllResourcesAsync()
            => Task.FromResult(_resources.As<IdentityServer4.Models.Resources>());
    }
}
