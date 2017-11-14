using System.Linq;
using CoWorker.Models.Security.OAuth.Twitch;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.OAuth;
using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;

namespace CoWorker.Builder
{
    using CoWorker.Models.Security.OAuth;
    using CoWorker.Primitives;
    using Microsoft.AspNetCore.Hosting;

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
        //public static IServiceCollection AddAuth(this IServiceCollection services, params ConfigureDelegate<AuthenticationBuilder>[] oauths)
        //    => oauths.Aggregate(services.AddAuthentication(), (seed, next) => next(seed)).Services;


        public static IServiceCollection AddOAuthWithConfiguration(this AuthenticationBuilder builder, WebHostBuilderContext context)
            => new OAuthBuilder(builder, context).Build();

    }
}
