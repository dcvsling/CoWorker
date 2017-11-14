using Microsoft.Extensions.Options;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Configuration;

namespace CoWorker.Models.Security.OAuth.Twitch
{

    public class OAuthOptionsConfigureOptions : IPostConfigureOptions<OAuthOptions>
    {
        private readonly IConfiguration _config;

        public OAuthOptionsConfigureOptions(IConfiguration config)
        {
            _config = config;
        }
        public void PostConfigure(string name, OAuthOptions options)
        {
            var section = _config.GetSection($"oauth:{name}");
            if(!section.Exists()) return;
            section.Bind(options);
        }
    }
}