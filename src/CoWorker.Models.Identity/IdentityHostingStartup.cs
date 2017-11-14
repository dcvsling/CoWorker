using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using IdentitySamples.Controllers;
using IdentitySample.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.AspNetCore.Identity;

namespace CoWorker.Models.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {

            builder.ConfigureServices(
                (ctx, srv) => srv
                    .AddIdentityCore<User>(Helper.Empty<IdentityOptions>())
                        .AddEntityFrameworkStores<UserDbContext>()
                        .AddUserStore<UserOnlyStore<User,UserDbContext,Guid>>()
                        .AddDefaultTokenProviders()
                        .Services
                    .AddScoped<AccountController>()
                    .AddScoped<ManageController>()
                    .AddSingleton(p => new DbContextPool<UserDbContext>(p.GetService<DbContextOptions<UserDbContext>>()))
                    .AddDbContextPool<UserDbContext>(options => options.UseSqlServer(ctx.Configuration.GetConnectionString(nameof(UserDbContext)))));
        }
    }
}
