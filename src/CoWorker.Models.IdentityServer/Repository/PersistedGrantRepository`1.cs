using CoWorker.Models.Abstractions;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore.Internal;

namespace CoWorker.Models.IdentityServer.Repository
{
    public class PersistedGrantRepository<TModel> : DatabaseRepository<PersistedGrantDbContext, TModel> where TModel : class, new()
    {
        public PersistedGrantRepository(DbContextPool<PersistedGrantDbContext> pool) : base(pool)
        {
        }
    }
}
