using CoWorker.Models.Abstractions;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore.Internal;

namespace CoWorker.Models.IdentityServer.Repository
{
    public class ConfigurationRepository<TModel> : DatabaseRepository<ConfigurationDbContext,TModel> where TModel : class, new() {
        public ConfigurationRepository(DbContextPool<ConfigurationDbContext> pool) : base(pool)
        {
        }
    }
}
