using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;

namespace CoWorker.Builder
{
    using Microsoft.Extensions.Options;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Authentication;
    using CoWorker.Models.Security.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using System.Web;

    public static partial class ServiceCollectionExtensions
	{
        public static AuthenticationBuilder AddCookieAuth(this IServiceCollection services)
            => services
                .AddSingleton<IHttpContextAccessor,HttpContextAccessor>()
                .AddClaimBaseAuthorize()
                .AddAuthentication(o =>
                    {
                        o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                        o.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                        o.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    })
                    .AddCookie(o =>
                    {
                        o.ExpireTimeSpan = new TimeSpan(1, 0, 0);
                        o.Events.OnRedirectToReturnUrl =
                            ctx => Task.Run(() => ctx.Response.Redirect(
                                HttpUtility.HtmlDecode(ctx.Request.Query[ctx.Options.ReturnUrlParameter])));
                    });

        public static IServiceCollection AddClaimBaseAuthorize(this IServiceCollection services)
            => services.AddSingleton<IClaimsTransformation, AdditionalClaimTransformation>()
                .AddClaimsProvider<DefaultClaimProvider>();

        public static IServiceCollection AddClaimsProvider<TProvider>(this IServiceCollection services)
            where TProvider : class,IClaimProvider
            => services.AddSingleton<IClaimProvider, TProvider>();
    }
}
