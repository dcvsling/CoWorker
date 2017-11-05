using CoWorker.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

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
                    .AddSingleton<IStartupFilter,SecurityStartupFilter>());
        }
    }
}
