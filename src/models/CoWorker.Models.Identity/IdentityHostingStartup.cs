using System;
using IdentitySamples.Controllers;
using IdentitySample.Controllers;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.AspNetCore.Identity;
using CoWorker.Models.Identity.Internal;

namespace CoWorker.Models.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {

            builder.ConfigureServices(
                (ctx, srv) => srv
                    .AddIdentityCore<User>(Helper.Empty<IdentityOptions>())
                        .AddDefaultTokenProviders()
                        .Services
                    .AddSingleton<IConfigureOptions<IdentityOptions>,IdentityOptionsConfigureOptions>()
                    .AddScoped<AccountController>()
                    .AddScoped<ManageController>()
                    .AddSingleton(p => new DbContextPool<IdentityDbContext>(p.GetService<DbContextOptions<IdentityDbContext>>()))
                    .AddDbContextPool<IdentityDbContext>(
                        options => options.UseSqlServer(ctx.Configuration.GetConnectionString("*"))));
        }
    }
}
