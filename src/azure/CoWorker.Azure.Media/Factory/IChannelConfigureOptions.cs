using Microsoft.WindowsAzure.Storage;
using System.Net;
using System.Collections;
using System.Linq;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.MediaServices.Client;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CoWorker.Azure.Media.Factory
{
    public interface IChannelConfigureOptions : IConfigureNamedOptions<ChannelCreationOptions>
    {
        void Configure(string name, ChannelCreationOptions options);
    }
}