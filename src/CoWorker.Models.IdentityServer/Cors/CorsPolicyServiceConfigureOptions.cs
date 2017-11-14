using System.Linq;
using Microsoft.Extensions.Options;
using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace CoWorker.Models.IdentityServer.Cors
{
    public class CorsPolicyServiceConfigureOptions : IPostConfigureOptions<DefaultCorsPolicyService>
    {
        private readonly IConfiguration _config;

        public CorsPolicyServiceConfigureOptions(IConfiguration config)
        {
            _config = config;
        }
        public void PostConfigure(string name, DefaultCorsPolicyService options)
        {
            var section = _config.GetSection("identityserver:cors");
            options.AllowAll = !section.Exists() || !section.GetChildren().Any();
            options.AllowedOrigins = section.GetChildren().Select(x => x.Value).ToList();
        }
    }
}
