using Microsoft.Extensions.Options;
using IdentityServer4.EntityFramework.Options;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace CoWorker.Models.IdentityServer.Configurations
{
    public class ConfigurationStoreOptionsConfigureOptions : IPostConfigureOptions<ConfigurationStoreOptions>
    {
        private readonly IConfiguration _config;

        public ConfigurationStoreOptionsConfigureOptions(IConfiguration config)
        {
            _config = config;
        }
        public void PostConfigure(string name, ConfigurationStoreOptions options)
            => options.ConfigureDbContext = ConfigureDbContext;

        private void ConfigureDbContext(DbContextOptionsBuilder builder)
            => builder.UseSqlServer(_config.GetConnectionString(nameof(ConfigurationDbContext)),
                s => s.UseRelationalNulls().MigrationsAssembly($"{Assembly.GetEntryAssembly().FullName}.Migrations"));
    }
}
