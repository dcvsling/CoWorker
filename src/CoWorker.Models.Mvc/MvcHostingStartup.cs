using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

[assembly: HostingStartup(typeof(CoWorker.Models.Mvc.MvcHostingStartup))]
namespace CoWorker.Models.Mvc
{
    public class MvcHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices(
                (ctx, srv) => srv
                    .AddMvcCore()
                    .AddMvcCoreDesignTime<MvcDesignTimeBuilder>()
                    .Services
                    .AddAppPipe<MvcApplicationFilter>()
                    .AddSingleton<IConfigureOptions<FileServerOptions>>(p => new ConfigureOptions<FileServerOptions>(o =>
                    {
                        o.EnableDefaultFiles = true;
                        o.RequestPath = string.Empty;
                        o.EnableDirectoryBrowsing = false;
                        o.FileProvider = p.GetService<IHostingEnvironment>().WebRootFileProvider;
                        o.StaticFileOptions.ServeUnknownFileTypes = true;
                    })));
        }
    }
}
