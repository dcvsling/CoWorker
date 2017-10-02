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
    public class DefaultChannelMemberOptions : IChannelConfigureOptions
    {
        public void Configure(String name, ChannelCreationOptions options)
            => Configure(options);
        public void Configure(ChannelCreationOptions options)
        {
            Configure(options.Input);
            Configure(options.Output);
            Configure(options.Preview);
            Configure(options.Encoding);
            Configure(options.CrossSiteAccessPolicies);
            Configure(options.Slate);
        }
        
        protected virtual void Configure(CrossSiteAccessPolicies cros)
        {
        }

        protected virtual void Configure(ChannelOutput output)
        {
            output.Hls = new ChannelOutputHls() { FragmentsPerSegment = 30 };
        }
        protected virtual void Configure(ChannelInput input)
        {
            input.AccessControl = input.AccessControl
                ?? new ChannelAccessControl();
            Configure(input.AccessControl);
            input.KeyFrameInterval = new TimeSpan(2000);
            input.StreamingProtocol = StreamingProtocol.RTMP;
        }
        protected virtual void Configure(ChannelPreview preview)
        {
            preview.AccessControl = new ChannelAccessControl();
            Configure(preview.AccessControl);
        }

        protected virtual void Configure(ChannelAccessControl control)
        {
            control.IPAllowList = control.IPAllowList ?? new List<IPRange>();
            control.IPAllowList.Add(
                new IPRange()
                {
                    Name = "default",
                    Address = IPAddress.Any,
                    SubnetPrefixLength = 32
                });
        }

        protected virtual void Configure(ChannelEncoding encoding)
        {
            encoding.SystemPreset = "Default720p";
        }

        protected virtual void Configure(ChannelSlate slate)
        {
            slate.InsertSlateOnAdMarker = false;
        }
    }
}