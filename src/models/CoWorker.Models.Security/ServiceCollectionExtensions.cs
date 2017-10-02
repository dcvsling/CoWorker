using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;

namespace CoWorker.Builder
{
    using CoWorker.Models.Security.OAuth.Twitch;
    using Microsoft.AspNetCore.Authentication.Facebook;
    using CoWorker.DependencyInjection.Configuration;
    using Microsoft.AspNetCore.Authentication.Google;
    using CoWorker.Models.Security.OAuth;
    using Microsoft.Extensions.Options;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Authentication;
    using CoWorker.Models.Security.Authentication;
    using Microsoft.AspNetCore.Authentication.OAuth;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using System.Net;
    using System.Web;

    public static class ServiceCollectionExtensions
	{
        public static AuthenticationBuilder AddCookieAuth(this IServiceCollection services)
            => services
                .AddSingleton<IHttpContextAccessor,HttpContextAccessor>()
                .AddSingleton<IPostConfigureOptions<GoogleOptions>, ConfigurationConfigureOptions<GoogleOptions>>()
                .AddSingleton<IPostConfigureOptions<FacebookOptions>, ConfigurationConfigureOptions<FacebookOptions>>()
                .AddSingleton<IPostConfigureOptions<TwitchOptions>, ConfigurationConfigureOptions<TwitchOptions>>()
                .AddSingleton<IPostConfigureOptions<CookieAuthenticationOptions>, ConfigurationConfigureOptions<CookieAuthenticationOptions>>()
                .AddClaimBaseAuthorize()
                .AddAuthentication(o =>
                    {
                        o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                        o.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                        o.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    })
                    .AddCookie(o =>
                    {
                        //o.LoginPath = "/auth/login";
                        //o.LogoutPath = "/auth/logout";
                        o.ExpireTimeSpan = new TimeSpan(1, 0, 0);
                        o.Events.OnRedirectToReturnUrl =
                            ctx => Task.Run(() => ctx.Response.Redirect(
                                HttpUtility.HtmlDecode(ctx.Request.Query[ctx.Options.ReturnUrlParameter])));
                    });

        public static IServiceCollection AddClaimBaseAuthorize(this IServiceCollection services)
            => services.AddSingleton<IClaimsTransformation, AdditionalClaimTransformation>()
                .AddClaimsProvider<DefaultClaimProvider>()
                .AddSingleton<IConfigureOptions<OAuthOptions>,OAuthOptionsConfigureOptions>();

        public static IServiceCollection AddClaimsProvider<TProvider>(this IServiceCollection services)
            where TProvider : class,IClaimProvider
            => services.AddSingleton<IClaimProvider, TProvider>();

    }
}
