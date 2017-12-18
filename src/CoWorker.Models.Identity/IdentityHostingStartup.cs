using CoWorker.Models.Identity.Repository;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

[assembly: HostingStartup(typeof(CoWorker.Models.Identity.IdentityHostingStartup))]

namespace CoWorker.Models.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices(
                (ctx, srv) => srv
                    .AddSingleton<IApplicationPartTypeProvider,ControllerModelMetadata>()
                    .AddIdentityCore<User>(Helper.Empty<IdentityOptions>())
                        .AddUserStore<UserOnlyStore<User,UserDbContext,Guid>>()
                        .AddDefaultTokenProviders()
                        .Services
                    .AddRepositoryDefinition<UserDbContext>(typeof(UserRepository<>)));
        }
    }
}
