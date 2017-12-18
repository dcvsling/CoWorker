using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.EntityFrameworkCore
{
    public static class DefaultConfig
    {
        public static Action<IServiceProvider, DbContextOptionsBuilder> ConfigureDbContextOptionsBuilder(string name, string migrationAsm)
            => (provider, builder) =>
                builder.UseSqlServer(provider.GetService<IConfiguration>().GetConnectionString(name), b => b.MigrationsAssembly(migrationAsm));
    }
}
