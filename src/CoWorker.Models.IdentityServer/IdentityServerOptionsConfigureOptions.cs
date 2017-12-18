using Microsoft.Extensions.Options;
using IdentityServer4.Configuration;
using IdentityServer4.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace CoWorker.Models.IdentityServer
{

    public class IdentityServerOptionsConfigureOptions : IConfigureOptions<IdentityServerOptions>
    {
        public void Configure(IdentityServerOptions options)
        {
            options.Discovery.ShowApiScopes = options.Discovery.ShowClaims = options.Discovery.ShowEndpoints = options.Discovery.ShowExtensionGrantTypes = options.Discovery.ShowGrantTypes
                = options.Discovery.ShowIdentityScopes = options.Discovery.ShowKeySet = options.Discovery.ShowResponseModes = options.Discovery.ShowResponseTypes = options.Discovery.ShowTokenEndpointAuthenticationMethods = true;
          
            options.Events.RaiseErrorEvents = options.Events.RaiseFailureEvents = options.Events.RaiseInformationEvents = options.Events.RaiseSuccessEvents = true;
            options.IssuerUri = "https://localhost";
            options.PublicOrigin = "https://localhost";
        }
    }
}
