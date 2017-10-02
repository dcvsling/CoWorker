using Microsoft.Extensions.Logging;
using System.Net.Http;
namespace CoWorker.Net.Proxy
{
    using System.Threading.Tasks;
    using System;
    using System.Threading;

    public class DefaultHttpClient : IHttpClient
    {
        private HttpClient _client;
        public DefaultHttpClient()
        {
            _client = new HttpClient();
        }

        public void Dispose()
        {
            _client.Dispose();
            _client = null;
        }
        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            => _client.SendAsync(request, cancellationToken);
    }

}
