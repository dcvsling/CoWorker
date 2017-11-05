using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Antiforgery;

namespace CoWorker.Models.Core.Antiforgery
{
    using System;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using System.Linq;
    using System.Threading.Tasks;
    using CoWorker.Primitives;

    public class AntiforgeryMiddleware
	{
        private string[] IgnoreValidMethod = { "GET", "HEAD", "TRACE", "OPTIONS" };
        private readonly IAntiforgery _antiforgery;
        private readonly ILogger<AntiforgeryMiddleware> _logger;
        private readonly AntiforgeryOptions _options;
        private readonly IHostingEnvironment _env;
        private IEqualityComparer<string> equaltyComparer
            => EqualityComparer<string>.Create(
                obj => obj.GetHashCode(),
                (x, y) => x.ToLower().Equals(y.ToLower(),
                    StringComparison.OrdinalIgnoreCase));

        public AntiforgeryMiddleware(
            IHostingEnvironment env,
            IAntiforgery antiforgery,
            ILogger<AntiforgeryMiddleware> logger,
            IOptions<AntiforgeryOptions> options)
        {
            _antiforgery = antiforgery;
            _logger = logger;
            _options = options.Value;
            _env = env;
        }

        async public Task Invoke(HttpContext context)
        {
            var isValid = ShouldValidate(context) ? ValidToken : Helper.Default<HttpContext,Task>(Task.CompletedTask);
            var isGenerateOrValid = ShouldGenerate(context) ? GenerateToken : isValid;
            await isGenerateOrValid(context);
        }
        public RequestDelegate Middleware(RequestDelegate next)
            => async ctx =>
            {

                await Invoke(ctx);
                await (ctx.Response.StatusCode == 400 ? Task.CompletedTask : next(ctx));
                //await next(ctx);
            };

        private bool ShouldGenerate(HttpContext context)
            => !context.Request.Cookies.Any(x => x.Key == _options.Cookie.Name);

        private bool ShouldValidate(HttpContext context)
            => !IgnoreValidMethod.Contains(
                    context.Request.Method,
                    equaltyComparer);

        private Task GenerateToken(HttpContext context)
        {
            var tokens = _antiforgery.GetAndStoreTokens(context);
            context.Response.Cookies.Append(_options.Cookie.Name, tokens.RequestToken, _options.Cookie.Build(context));
            return Task.CompletedTask;
        }

        private Task ValidToken(HttpContext context)
            => _antiforgery.ValidateRequestAsync(context);
    }
}