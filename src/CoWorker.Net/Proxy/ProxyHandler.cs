using System.Linq;
namespace CoWorker.Net.Proxy
{
    using System.Net.Http;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using System.Threading.Tasks;
    using System;
    using System.Threading;

    public class ProxyHandler
    {
        private readonly ProxyOptions _options;

        public ProxyHandler(ProxyOptions options)
        {
            this._options = options;
        }

        private string GetProxyHost(HttpContext context)
            => $"{_options.HostUrl}" +
                  $"{context.Request.Path.Value.Replace(_options.RoutePrefix, string.Empty)}" +
                  $"{context.Request.QueryString.Value}";

        private HttpRequestMessage CreateRequest(HttpContext context)
            =>  new HttpRequestMessage(new HttpMethod(context.Request.Method), GetProxyHost(context));

        async public Task Invoke(HttpContext context)
        {
            using (var http = context.RequestServices.GetService<ProxyClient>())
            {
                var client = await http.SendAsync(CreateRequest(context), CancellationToken.None);
                context.Response.StatusCode = (int)client.StatusCode;
                await context.Response.WriteAsync(await client.Content.ReadAsStringAsync(), CancellationToken.None);
            }
        }
    }
}
