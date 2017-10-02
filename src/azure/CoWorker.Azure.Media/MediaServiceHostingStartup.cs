using CoWorker.Azure.Media.Controllers;
using MediaService.Core;

namespace CoWorker.Azure.Media
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Mvc.Infrastructure;

    public class MediaServiceHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((ctx, srv) =>
            {
                srv.AddOptions()
                    .AddAzureMediaService()
                    .AddAMSController();
            });
        }
    }
}
