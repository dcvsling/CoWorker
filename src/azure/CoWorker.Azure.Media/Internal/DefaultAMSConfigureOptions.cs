using MediaService.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CoWorker.Azure.Media.Internal
{
    public class DefaultAMSConfigureOptions : ConfigureOptions<AMSOptions>
    {
        public DefaultAMSConfigureOptions(IConfiguration config) :
            base(options => config.GetSection("AMS").Bind(options))
        { }
    }
}
