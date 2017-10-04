using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace CoWorker.Builder
{
    using System;
    using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
    using CoWorker.Security.OAuth.Twitch;
    using Microsoft.AspNetCore.Authentication.Facebook;
    using CoWorker.DependencyInjection.Configuration;
    using Microsoft.AspNetCore.Authentication.Google;
    using CoWorker.Security.OAuth;
    using Microsoft.Extensions.Options;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Authentication;
    using CoWorker.Security.Authentication;
    using Microsoft.AspNetCore.Authentication.OAuth;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using System.Threading.Tasks;
    using System.Security.Claims;
    using System.Linq;

    public static class ServiceCollectionExtensions
	{
        public static AuthenticationBuilder AddCookieAuth(this IServiceCollection services)
            => services.AddSingleton<IPostConfigureOptions<GoogleOptions>, ConfigurationConfigureOptions<GoogleOptions>>()
                .AddSingleton<IPostConfigureOptions<FacebookOptions>, ConfigurationConfigureOptions<FacebookOptions>>()
                .AddSingleton<IPostConfigureOptions<TwitchOptions>, ConfigurationConfigureOptions<TwitchOptions>>()
                .AddSingleton<IPostConfigureOptions<MicrosoftAccountOptions>, ConfigurationConfigureOptions<MicrosoftAccountOptions>>()
                .AddSingleton<IPostConfigureOptions<CookieAuthenticationOptions>, ConfigurationConfigureOptions<CookieAuthenticationOptions>>()
                .AddClaimBaseAuthorization()
                .AddAuthentication(o =>
                    {
                        o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                        o.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                        o.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    })
                    .AddCookie(o =>
                    {
                        o.LoginPath = "/auth/login";
                        o.LogoutPath = "/auth/logout";
                        o.AccessDeniedPath = "/";
                        o.ClaimsIssuer = "https://CoWorker.idv";
                        o.ExpireTimeSpan = new TimeSpan(1, 0, 0);
                    });
        
        public static IServiceCollection AddClaimBaseAuthorization(this IServiceCollection services)
            => services.AddSingleton<IClaimsTransformation, AdditionalClaimTransformation>()
                .AddClaimsProvider<DefaultClaimProvider>()
                .AddSingleton<IConfigureOptions<OAuthOptions>,OAuthOptionsConfigureOptions>();

        public static IServiceCollection AddClaimsProvider<TProvider>(this IServiceCollection services)
            where TProvider : class,IClaimProvider
            => services.AddSingleton<IClaimProvider, TProvider>();
    }

    public class AuthenticationMiddleware
    {
        private readonly IHostingEnvironment _env;
        private readonly ILogger<AuthenticationMiddleware> _logger;

        public AuthenticationMiddleware(IHostingEnvironment env,ILogger<AuthenticationMiddleware> logger)
        {
            this._env = env;
            this._logger = logger;
        }

        async public Task Invoke(HttpContext context,Task next)
        {
            if (_env.IsProduction())
            {
                context.Response.Redirect("/");
                return;
            }
            if (!context.User.Claims.Any())
            {
                await context.ChallengeAsync("Google");
                _logger.LogInformation("challenge for swagger");
                return;
            }
            if (!context.User.Identity.IsAuthenticated)
            {
                await context.SignInAsync(context.User);
                _logger.LogInformation($"signin {context.User.FindFirst(ClaimTypes.Email).Value} for swagger ui");
            }
            _logger.LogInformation($"{context.User.FindFirst(ClaimTypes.Email).Value} enter swagger ui");
            await next;
        }

        public RequestDelegate Middleware(RequestDelegate next)
            => ctx => Invoke(ctx, next(ctx));
    }

    public class AuthenticationEvent
    {
        public RequestDelegate OnUnAuthentication { get; }
        public RequestDelegate OnSignInBefore { get; }
        public RequestDelegate OnSignInAfter { get; }
    }
}
