using Microsoft.Extensions.Options;
using System.Linq;
namespace CoWorker.Net.Proxy
{
    using System.Net.Http;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using System.Threading.Tasks;
    using System;
    using System.Threading;

    public class ProxyMiddleware
    {
        private readonly IOptionsSnapshot<ProxyOptions> _options;

        public ProxyMiddleware(IOptionsSnapshot<ProxyOptions> options)
        {
            this._options = options;
        }

        private Task CreateInvoker(HttpContext context,RequestDelegate request)
        {
            var options = _options.Get(context.Request.Path.Value.Split('/').FirstOrDefault());
            return options.RoutePrefix == string.Empty ? request(context) : new ProxyHandler(options).Invoke(context);
        }

        public RequestDelegate Middleware(RequestDelegate request)
            => ctx => CreateInvoker(ctx, request);
    }
}
