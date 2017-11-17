using System;
using System.Collections.Generic;
using System.Text;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;

namespace CoWorker.Models.IdentityServer.Internal
{
    public class ResourcesServiceDbContext : ConfigurationDbContext
    {
        public ResourcesServiceDbContext(DbContextOptions<ConfigurationDbContext> options, ConfigurationStoreOptions storeOptions) : base(options, storeOptions)
        {
        }
    }

    public class IdentityDbContext : PersistedGrantDbContext
    {
        public IdentityDbContext(DbContextOptions<PersistedGrantDbContext> options, OperationalStoreOptions storeOptions) : base(options, storeOptions)
        {
        }
    }
}
