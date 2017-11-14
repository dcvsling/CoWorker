using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using System;
using CoWorker.Models.Abstractions.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace CoWorker.Models.Mvc
{
    public class MvcHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices(
                (ctx, srv) => srv.AddMvcCore()
                    .AddApiExplorer()
                    .AddControllersAsServices()
                    .AddJsonFormatters()
                    .Services
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
