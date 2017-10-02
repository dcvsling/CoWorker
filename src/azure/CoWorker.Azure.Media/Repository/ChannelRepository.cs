using System.Net;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.MediaServices.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace CoWorker.Azure.Media.Repository
{

    public class ChannelRepository : IRepository<IChannel>
    {
        private readonly ChannelBaseCollection _channel;
        private readonly IOptionsSnapshot<ChannelCreationOptions> _snapshot;

        public ChannelRepository(
            ChannelBaseCollection channel,
            IOptionsSnapshot<ChannelCreationOptions> snapshot)
        {
            _channel = channel;
            _snapshot = snapshot;
        }

        private Task<IChannel> FindByName(string name)
            => Task.Run(() => _channel.ToArray().FirstOrDefault(x => x.Name == name));
        public Task<IChannel> CreateAsync(string name)
            => _channel.CreateAsync(_snapshot.Get(name));

        public IEnumerable<IChannel> Query(Expression<Func<IChannel, bool>> predicate)
            => _channel.Where(predicate).ToArray();
        public Task<IChannel> FindAsync(String name)
            => FindByName(name);
        async public Task UpdateAsync(string name)
        {
            var channel = await FindByName(name);
            await (((ChannelState.Stopped | ChannelState.Stopping) & channel.State) == 0
                ? channel.StartAsync()
                : channel.StopAsync());
        }
        public Task DeleteAsync(String name)
            => Task.Run(async () =>
            {
                var channel = await FindByName(name);
                if (ChannelState.Stopped != channel.State) await channel.StopAsync();
                await channel.DeleteAsync();
            });
    }
}
