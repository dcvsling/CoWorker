using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.OAuth;
using CoWorker.Primitives;
using System.Collections;
using System;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Microsoft.AspNetCore.Hosting;

namespace CoWorker.Builder
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Authentication;
    using System.Collections.Generic;

    public delegate T Middleware<T>(T target);

    public class OAuthBuilder
    {
        public OAuthBuilder(AuthenticationBuilder builder, WebHostBuilderContext context)
        {
            Context = context;
            Builder = builder;
            Services = builder.Services;
        }

        public WebHostBuilderContext Context { get; }
        public AuthenticationBuilder Builder { get; }
        public IServiceCollection Services { get; }

        public IServiceCollection Build()
        {
            Context.Configuration.GetSection("oauth")
                .GetChildren()
                .Join(OAuthConfigures,
                    x => x.Key.ToLower(),
                    x => x.Key,
                    (x, y) => OAuthConfigures[x.Key])
                .Each(x => x(Builder));
            return Services;
        }

        private IDictionary<string, Middleware<AuthenticationBuilder>> OAuthConfigures
            = new Dictionary<string, Middleware<AuthenticationBuilder>>
            {
                ["google"] = GoogleExtensions.AddGoogle,
                ["microsoftaccount"] = MicrosoftAccountExtensions.AddMicrosoftAccount,
                ["twitter"] = TwitterExtensions.AddTwitter,
                ["facebook"] = FacebookAuthenticationOptionsExtensions.AddFacebook,
            };
    }

    public class OAuthSource
    {
        public string Name { get; set; }
        public Middleware<AuthenticationBuilder> Builder { get; set; }
    }
}
