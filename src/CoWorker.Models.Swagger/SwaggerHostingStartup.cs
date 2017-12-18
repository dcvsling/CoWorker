using Microsoft.AspNetCore.Builder;
using CoWorker.LightMvc.Swagger;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using CoWorker.Primitives;
using CoWorker.Models.Abstractions.Filters;

[assembly: HostingStartup(typeof(CoWorker.Models.Swagger.SwaggerHostingStartup))]
namespace CoWorker.Models.Swagger
{
    public class SwaggerHostingStartup : IHostingStartup
    {
        void IHostingStartup.Configure(IWebHostBuilder builder)
            => builder.ConfigureServices(srv => srv
                .AddSwagger()
                .AddAppPipe<SwaggerApplicationFilter>());
    }

}
