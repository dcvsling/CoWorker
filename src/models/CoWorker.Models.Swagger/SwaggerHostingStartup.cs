using Microsoft.AspNetCore.Builder;
using CoWorker.LightMvc.Swagger;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;

namespace CoWorker.Models.Swagger
{
    public class SwaggerHostingStartup : IHostingStartup
    {
        void IHostingStartup.Configure(IWebHostBuilder builder)
            => builder.ConfigureServices(srv => srv
                .AddMvcCore()
                    .AddApiExplorer()
                    .AddControllersAsServices()
                    .AddJsonFormatters(o => o.Initialize())
                    .AddJsonOptions(o => o.SerializerSettings.Initialize())
                    .AddMvcOptions(o => {
                        o.AllowEmptyInputInBodyModelBinding = true;
                        o.RequireHttpsPermanent = true;
                        o.SslPort = 443;
                    })
                    .Services
                .AddSwagger()
                .AddSingleton<IStartupFilter,SwaggerStartupFilter>()
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
