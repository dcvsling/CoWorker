using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using CoWorker.Models.Abstractions;

[assembly: HostingStartup(typeof(CoWorker.Models.IdentityServer.IdentityServerHostingStartup))]
namespace CoWorker.Models.IdentityServer
{

    public class IdentityServerHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices(ConfigureServices);
        }

        private void ConfigureServices(IServiceCollection services)
            => services
                .AddIdentityService()
                .AddAppPipe<IdentityServerApplicationFilter>();
    }
}
