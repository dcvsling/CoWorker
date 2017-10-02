using System.Linq;
using CoWorker.Security.OAuth.Twitch;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.OAuth;
using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;

namespace CoWorker.Builder
{
    using CoWorker.Security.OAuth;
    public static class OAuthBuilderExtensions
    {
        public static AuthenticationBuilder AddOAuth<TOptions>(this AuthenticationBuilder services)
            where TOptions : OAuthOptions, new()
            => services.AddOAuth<TOptions, OAuthHandler<TOptions>>();

        public static AuthenticationBuilder AddOAuth<TOptions, THandler>(
            this AuthenticationBuilder services,
            string name = Helper.EmptyString,
            Action<TOptions> config = null)
            where TOptions : OAuthOptions, new()
            where THandler : OAuthHandler<TOptions>
        {
            name = name ?? typeof(TOptions).RemovePostfixName();
            return  services.AddRemoteScheme<TOptions, THandler>(name, name, config ?? Helper.Empty<TOptions>());
        }

        public static AuthenticationBuilder AddTwitch(this AuthenticationBuilder builder)
            => builder.AddOAuth<TwitchOptions, TwitchHandler>(TwitchOptions.AuthenticationScheme);

        //public static IServiceCollection AddAuth(this IServiceCollection services, params ConfigureDelegate<AuthenticationBuilder>[] oauths)
        //    => oauths.Aggregate(services.AddAuthentication(), (seed, next) => next(seed)).Services;
    }
}
