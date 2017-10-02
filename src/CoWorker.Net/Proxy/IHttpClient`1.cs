using Microsoft.Extensions.Logging;
using System.Net.Http;
namespace CoWorker.Net.Proxy
{
    using System.Threading.Tasks;
    using System;
    using System.Threading;

    public interface IHttpClient : IDisposable
    {
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken);
    }

}
