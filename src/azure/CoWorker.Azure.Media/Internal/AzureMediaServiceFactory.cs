using Microsoft.WindowsAzure.MediaServices.Client;
using System;
using Microsoft.Extensions.Options;
using MediaService.Core;

namespace CoWorker.Azure.Media.Internal
{
    public class AzureMediaServiceFactory : IAzureMediaServiceFactory
    {
        private AMSOptions options;
        public AzureMediaServiceFactory(IOptions<AMSOptions> options)
        {
            this.options = options.Value;
        }

        public CloudMediaContext Create()
            => new CloudMediaContext(
                new Uri(options.REST),
                new AzureAdTokenProvider(
                    new AzureAdTokenCredentials(
                        options.Domain,
                        new AzureAdClientSymmetricKey(options.ClientId,options.ClientSecret),
                        AzureEnvironments.AzureCloudEnvironment))
                );

    }
}
