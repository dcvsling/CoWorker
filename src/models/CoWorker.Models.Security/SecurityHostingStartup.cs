using CoWorker.Builder;
using CoWorker.Models.Security.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore.Internal;

namespace CoWorker.Models.Security
{
    public class SecurityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices(
                (ctx, srv) => srv.AddCookieAuth()
                        .AddGoogle()
                        .AddFacebook()
                        .AddTwitch()
                    .Services
                    .AddSingleton(p => new DbContextPool<IdentityDbContext>(p.GetService<DbContextOptions<IdentityDbContext>>()))
                    .AddDbContextPool<IdentityDbContext>(
                        options => options.UseSqlServer(ctx.Configuration.GetConnectionString("*")))
                    .AddSingleton<IStartupFilter,SecurityStartupFilter>());
        }
    }
}
