using CoWorker.Builder;
using CoWorker.Models.Abstractions.Filters;
using CoWorker.Models.Security.KeyVault;
using Microsoft.AspNetCore.Hosting;

namespace CoWorker.Models.Security
{
    public class SecurityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(
                    (ctx, b) => new KeyVaultConfigurationBuilder().Build(b))
                .ConfigureServices(
                    (ctx, srv) => srv.AddCookieAuth()
                        .AddOAuthWithConfiguration(ctx)
                    .AddAppPipe<SecurityApplicationFilter>());
        }
    }
}
