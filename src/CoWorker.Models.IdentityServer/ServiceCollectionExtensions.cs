using IdentityServer4.EntityFramework.Options;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using CoWorker.Models.IdentityServer.Repository;

namespace CoWorker.Models.IdentityServer
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigDefault(Action<Action<IServiceProvider, DbContextOptionsBuilder>> callback)
            => callback(DefaultConfig.ConfigureDbContextOptionsBuilder("coworker-db", typeof(ServiceCollectionExtensions).Assembly.GetName().Name));
        public static IServiceCollection AddIdentityService(this IServiceCollection services)
            => services.AddOptions()
                .AddIdentityServer(new IdentityServerOptionsConfigureOptions().Configure)
                .AddConfigurationStore<ConfigurationDbContext>(o => ConfigDefault(action => o.ResolveDbContextOptions = action))
                .AddOperationalStore<PersistedGrantDbContext>(o => ConfigDefault(action => o.ResolveDbContextOptions = action))
                .AddDefaultEndpoints()
                .AddDefaultSecretParsers()
                .AddDefaultSecretValidators()
                .AddDeveloperSigningCredential()
                .AddJwtBearerClientAuthentication()
                .AddValidators()
                .Services
                .AddSingleton<ApplicationPart,ControllerModelMetadata>()
                .AddRepositoryDefinition<ConfigurationDbContext>(typeof(ConfigurationRepository<>))
                .AddRepositoryDefinition<PersistedGrantDbContext>(typeof(PersistedGrantRepository<>));
    }
}
