using System.Web;
using Microsoft.AspNetCore.Authorization;

namespace CoWorker.Models.Security.Authentication
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Hosting;
    using System.Threading.Tasks;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Authentication;
    using System;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.Extensions.Options;

    [Controller]
    [Route("auth")]
    public class AuthorizationController //DevSkim: ignore DS184626
    {
        private IAuthenticationSchemeProvider schemas;
        private readonly IHostingEnvironment _environment;
        private readonly IHttpContextAccessor _accessor;
        private readonly CookieAuthenticationOptions _options;

        public AuthorizationController(
            IOptions<CookieAuthenticationOptions> options,
            IHostingEnvironment environment,
            IAuthenticationSchemeProvider schemas,
            IHttpContextAccessor accessor)
        {
            this.schemas = schemas;
            this._environment = environment;
            this._accessor = accessor;
            this._options = options.Value;
        }
        [HttpGet]
        public Task<IEnumerable<AuthenticationScheme>> Get()
            => Task.Run(schemas.GetAllSchemesAsync)
                .ContinueWith(t => t.Result
                    .Where(x => !string.IsNullOrEmpty(x.DisplayName))
                    .Select(x =>
                    new AuthenticationScheme()
                    {
                        Url = $"{_accessor.HttpContext.Request.Path.ToString()}/{x.DisplayName}",
                        Image = $"{_accessor.HttpContext.Request.Path.ToString()}/{x.DisplayName}".Replace("auth", "rsc"),
                        DisplayName = x.DisplayName,
                        Scheme = x.Name
                    }));

        [HttpPost("{scheme}")]
        async public Task Post([FromRoute] string scheme)
        {
            var context = _accessor.HttpContext;
            if(string.IsNullOrEmpty(scheme))
            {
                await context.ChallengeAsync();
                return;
            }
            if (context.User.Identity.IsAuthenticated)
                await Logout();
            var schemes = await schemas.GetAllSchemesAsync();
            var currentScheme = schemes.FirstOrDefault(x => x.Name.Equals(scheme, StringComparison.OrdinalIgnoreCase))
                ?.Name
                ?? (await schemas.GetDefaultChallengeSchemeAsync()).Name;
            var properties =new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = true
                };

            await context.ChallengeAsync(currentScheme,properties);
        }

        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        [HttpGet("user")]
        public Task<Dictionary<string, string>> User()
            => Task.FromResult(_accessor.HttpContext.User.Claims
                .ToDictionary(x => x.Type, x => x.Value));

        [HttpGet("login")]
        async public Task Login()
        {
            var context = _accessor.HttpContext;
            if (!context.User.Identity.IsAuthenticated) await context.ChallengeAsync();
            await _accessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                context.User,
                new AuthenticationProperties {
                    AllowRefresh = true,
                    IsPersistent = true,
                    RedirectUri = HttpUtility.HtmlDecode(context.Request.Query[_options.ReturnUrlParameter])
                });
        }

        [HttpGet("logout")]
        async public Task Logout()
        {
            var context = _accessor.HttpContext;
            await context.SignOutAsync(
                  CookieAuthenticationDefaults.AuthenticationScheme,
                  new AuthenticationProperties
                  {
                      RedirectUri = HttpUtility.HtmlDecode(context.Request.Query[_options.ReturnUrlParameter])
                  });
        }
    }
}
