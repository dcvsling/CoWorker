using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CoWorker.Models.Security.OAuth
{

    public class OAuthOptionsConfigureOptions : IConfigureNamedOptions<OAuthOptions>
    {
        private readonly IConfiguration _config;

        public OAuthOptionsConfigureOptions(IConfiguration config)
        {
            this._config = config;
        }
        public virtual void Configure(System.String name, OAuthOptions options)
            => _config.GetSection(name).Bind(options);
        public virtual void Configure(OAuthOptions options)
            => Configure(string.Empty, options);
    }
}
