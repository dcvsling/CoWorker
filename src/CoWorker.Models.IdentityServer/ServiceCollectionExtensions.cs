using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.DataProtection;
using CoWorker.Models.IdentityServer.Resources;
using Microsoft.Extensions.Options;
using IdentityServer4.Services;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using CoWorker.Models.IdentityServer.Crypto;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using CoWorker.Models.IdentityServer.ClientStores;
using CoWorker.Models.IdentityServer.Cors;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;

namespace CoWorker.Models.IdentityServer
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection services)
            => services.AddOptions()
                .AddIdentityServer()
                .AddSigningCredentialWithConfiguration()
                .AddCorsPolicyServiceWithConfiguration()
                .AddClientStoreWithConfiguration()
                .AddResourceStoreWithConfiguration()
                .AddJwtBearerClientAuthentication()
                .Services;

        public static IIdentityServerBuilder AddCorsPolicyServiceWithConfiguration(this IIdentityServerBuilder builder)
        {
            builder.AddCorsPolicyService<DefaultCorsPolicyService>()
                .Services
                .AddSingleton<IPostConfigureOptions<DefaultCorsPolicyService>, CorsPolicyServiceConfigureOptions>();
            return builder;
        }

        public static IIdentityServerBuilder AddSigningCredentialWithConfiguration(this IIdentityServerBuilder builder)
        {
            builder.Services
                .AddSingleton<RSAKeyStore>()
                .AddSingleton<ISigningCredentialStore,DefaultSigningCredentialsStore>()
                .AddSingleton<IValidationKeysStore, DefaultValidationKeysStore>()
                .AddSingleton(p => new SigningCredentials(p.GetService<SecurityKey>(),"RS256"))
                .AddSingleton<SecurityKey>(p => p.GetService<RSAKeyStore>().Create());
            return builder;
        }

        public static IIdentityServerBuilder AddClientStoreWithConfiguration(this IIdentityServerBuilder builder)
        {
            builder.AddClientStore<ClientStore>()
                .Services
                .AddSingleton<IPostConfigureOptions<Client>,ClientConfigureOptions>();
            return builder;
        }

        public static IIdentityServerBuilder AddResourceStoreWithConfiguration(this IIdentityServerBuilder builder)
        {
            builder.AddResourceStore<ResourceStore>()
                .Services
                .AddScoped<IConfigureOptions<IdentityServer4.Models.Resources>,ResourcesConfigureOptions>()
                .AddSingleton<IdentityServer4.Models.Resources>()
                .AddSingleton<IdentityResource,IdentityResources.Address>()
                .AddSingleton<IdentityResource, IdentityResources.Email>()
                .AddSingleton<IdentityResource, IdentityResources.OpenId>()
                .AddSingleton<IdentityResource, IdentityResources.Phone>()
                .AddSingleton<IdentityResource, IdentityResources.Profile>();

            return builder;
        }
    }
}
