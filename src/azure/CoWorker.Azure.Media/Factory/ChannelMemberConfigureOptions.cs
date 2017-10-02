using System.Linq;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.MediaServices.Client;
using System.Collections.Generic;
using CoWorker.Abstractions.Cache;

namespace CoWorker.Azure.Media.Factory
{
    public class ChannelMemberConfigureOptions : IConfigureOptions<ChannelCreationOptions>
    {
        private readonly IConfiguration _config;
        private readonly IEnumerable<IConfigureOptions<ChannelCreationOptions>> _options;
        private readonly IServiceProvider _provider;
        private readonly IOptionsCache<Action<ChannelCreationOptions>> _cache;

        public ChannelMemberConfigureOptions(
            IServiceProvider provider,
            IEnumerable<IChannelConfigureOptions> options,
            IOptionsCache<Action<ChannelCreationOptions>> cache)
        {
            _options = options;
            _provider = provider;
            _cache = cache;
        }
        public void Configure(System.String name, ChannelCreationOptions options)
        {
            name = name ?? string.Empty;
            _cache.GetOrAdd(name,
                () => _options.Select(x => x is IConfigureNamedOptions<ChannelCreationOptions> c 
                        ? o => c.Configure(name,o)
                        : new Action<ChannelCreationOptions>(o => x.Configure(o)))
                    .Aggregate(Aggregate))
                    (options);
        }

        public void Configure(ChannelCreationOptions options)
            => Configure(string.Empty, options);

        public Action<ChannelCreationOptions> Aggregate(Action<ChannelCreationOptions> left , Action<ChannelCreationOptions> right)
            => o =>
            {
                left(o);
                right(o);
            };
    }
}