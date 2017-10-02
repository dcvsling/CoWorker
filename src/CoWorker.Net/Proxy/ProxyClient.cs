namespace CoWorker.Net.Proxy
{
    using Microsoft.Extensions.Logging;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System;
    using System.Threading;

    public class ProxyClient : IDisposable
    {
        private readonly ILogger<ProxyClient> _logger;
        private readonly IHttpClient _client;
        public ProxyClient(ILogger<ProxyClient> logger, IHttpClient client)
        {
            _logger = logger;
            _client = client;
        }

        public void Dispose() => this._client.Dispose();

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            => _client.SendAsync(request, cancellationToken);
    }
}
