using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace CoWorker.Models.IdentityServer0
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection services)
            => services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .Services;
    }
    //public static class ServiceCollectionExtensions
    //{
    //    public static IServiceCollection AddIdentityService(this IServiceCollection services)
    //        => services.AddOptions()
    //            .AddIdentityServer()
    //            .AddConfigurationStore(o=>o.ConfigureDbContext(new DbContextOptionsBuilder()))
    //            .AddOperationalStore()
    //            .AddSigningCredentialWithConfiguration()
    //            .AddCorsPolicyServiceWithConfiguration()
    //            .AddClientStoreWithConfiguration()
    //            .AddResourceStoreWithConfiguration()
    //            .AddJwtBearerClientAuthentication()
    //            .Services;

    //    public static IIdentityServerBuilder AddCorsPolicyServiceWithConfiguration(this IIdentityServerBuilder builder)
    //    {
    //        builder.AddCorsPolicyService<DefaultCorsPolicyService>()
    //            .Services
    //            .AddSingleton<IPostConfigureOptions<DefaultCorsPolicyService>, CorsPolicyServiceConfigureOptions>();
    //        return builder;
    //    }

    //    public static IIdentityServerBuilder AddSigningCredentialWithConfiguration(this IIdentityServerBuilder builder)
    //    {
    //        //builder.Services.AddDataProtection().UseCustomCryptographicAlgorithms(
    //        //    new ManagedAuthenticatedEncryptorConfiguration()
    //        //    {
    //        //        EncryptionAlgorithmType = typeof(RSA),
    //        //        ValidationAlgorithmType = typeof(SHA256)
    //        //    }).Services.AddSingleton(p => p.GetService<IAuthenticatedEncryptor>());

    //        builder.Services
    //            .AddSingleton<RSAKeyStore>()
    //            .AddSingleton<ISigningCredentialStore,DefaultSigningCredentialsStore>()
    //            .AddSingleton<IValidationKeysStore, DefaultValidationKeysStore>()
    //            .AddSingleton(p => new SigningCredentials(p.GetService<SecurityKey>(),"RS256"))
    //            .AddSingleton<SecurityKey>(p => p.GetService<RSAKeyStore>().Create());
    //        return builder;
    //    }

    //    public static IIdentityServerBuilder AddClientStoreWithConfiguration(this IIdentityServerBuilder builder)
    //    {
    //        builder.AddClientStore<ClientStore>()
    //            .Services
    //            .AddSingleton<IPostConfigureOptions<Client>,ClientConfigureOptions>();
    //        return builder;
    //    }

    //    public static IIdentityServerBuilder AddResourceStoreWithConfiguration(this IIdentityServerBuilder builder)
    //    {
    //        builder.AddResourceStore<ResourceStore>()
    //            .Services
    //            .AddScoped<IConfigureOptions<IdentityServer4.Models.Resources>,ResourcesConfigureOptions>()
    //            .AddSingleton<IdentityServer4.Models.Resources>()
    //            .AddSingleton<IdentityResource,IdentityResources.Address>()
    //            .AddSingleton<IdentityResource, IdentityResources.Email>()
    //            .AddSingleton<IdentityResource, IdentityResources.OpenId>()
    //            .AddSingleton<IdentityResource, IdentityResources.Phone>()
    //            .AddSingleton<IdentityResource, IdentityResources.Profile>();

    //        return builder;
    //    }
    //}
}
