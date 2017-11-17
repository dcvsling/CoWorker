
using CoWorker.Models.Abstractions.Filters;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace CoWorker.Models.IdentityServer
{

    public class IdentityServerHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices(ConfigureServices);
        }

        private void ConfigureServices(IServiceCollection services)
            => services.AddIdentityServer().Services
                .AddAppPipe<IdentityServerApplicationFilter>();
    }
}
