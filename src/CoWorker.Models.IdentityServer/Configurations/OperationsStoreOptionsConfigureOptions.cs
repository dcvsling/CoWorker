using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.Extensions.Options;
using IdentityServer4.EntityFramework.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CoWorker.Models.IdentityServer.Configurations
{
    public class OperationsStoreOptionsConfigureOptions : IPostConfigureOptions<OperationalStoreOptions>
    {
        private readonly IConfiguration _config;

        public OperationsStoreOptionsConfigureOptions(IConfiguration config)
        {
            _config = config;
        }
        public void PostConfigure(string name, OperationalStoreOptions options)
            => options.ConfigureDbContext = ConfigureDbContext;

        private void ConfigureDbContext(DbContextOptionsBuilder builder)
            => builder.UseSqlServer(_config.GetConnectionString(nameof(PersistedGrantDbContext)),
                s => s.UseRelationalNulls().MigrationsAssembly($"{Assembly.GetEntryAssembly().FullName}.Migrations"));
    }
}
