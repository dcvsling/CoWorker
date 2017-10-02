using Microsoft.WindowsAzure.MediaServices.Client;

namespace MediaService.Core
{
    public interface IAzureMediaServiceFactory
    {
        CloudMediaContext Create();
    }
}