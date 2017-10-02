using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.MediaServices.Client;
using System.Threading.Tasks;

namespace CoWorker.Azure.Media.Factory
{

    public class ChannelFactory : IChannelFactory
    {
        private readonly CloudMediaContext _media;
        private readonly IOptionsSnapshot<ChannelCreationOptions> _snapshot;

        public ChannelFactory(CloudMediaContext media,IOptionsSnapshot<ChannelCreationOptions> snapshot)
        {
            _media = media;
            _snapshot = snapshot;
        }

        public Task<IChannel> CreateAsync(string name)
            => _media.Channels.CreateAsync(_snapshot.Get(name));
    }
}