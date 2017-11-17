using IdentityModel;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Microsoft.Extensions.Options;
using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Text;
using IdentityServer4.Models;
using System.Threading.Tasks;
using System.Collections;

namespace CoWorker.Models.IdentityServer.Resources
{
    using Resources = IdentityServer4.Models.Resources;
    public class ResourcesConfigureOptions : IConfigureOptions<Resources>
    {
        private readonly IOptionsSnapshot<ApiResource> _api;
        private readonly IOptionsSnapshot<IdentityResource> _identity;
        private readonly IConfiguration _config;
        private readonly IEnumerable<IdentityResource> _identities;
        private readonly IEnumerable<ApiResource> _apis;

        public ResourcesConfigureOptions(
            IOptionsSnapshot<ApiResource> api,
            IOptionsSnapshot<IdentityResource> identity,
            IEnumerable<ApiResource> apis,
            IEnumerable<IdentityResource> identities,
            IConfiguration config)
        {
            _api = api;
            _identity = identity;
            _config = config;
            _identities = identities;
            _apis = apis;
        }
        public void Configure(Resources options)
        {
            _config.GetSection("identityserver:resources:api")
                .GetChildren()
                .Select(x => x.Get<ApiResource>() ?? _api.Get(x.Get<string>()))
                .ToList()
                .ForEach(options.ApiResources.Add);

            _config.GetSection("identityserver:resources:identity")
                .GetChildren()
                .Select(x => _identities.FirstOrDefault(y => y.Name.Equals( x.Value,StringComparison.OrdinalIgnoreCase)))
                .ToList()
                .ForEach(options.IdentityResources.Add);
        }
    }
}
