using Microsoft.WindowsAzure.MediaServices.Client;
using System.Threading.Tasks;

namespace CoWorker.Azure.Media.Factory
{
    public interface IChannelFactory
    {
        Task<IChannel> CreateAsync(string name);
    }
}