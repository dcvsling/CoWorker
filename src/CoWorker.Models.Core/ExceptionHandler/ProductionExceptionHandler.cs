using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Diagnostics;
using CoWorker.Primitives;

namespace CoWorker.Models.Core.ExceptionHandler
{
    public class ProductionExceptionHandler : IExceptionHandler
    {
        public RequestDelegate Handler
            => context =>
            {
                var feature = context.Features.Get<IExceptionHandlerPathFeature>();
                var msg = JsonConvert.SerializeObject(
                    new
                    {
                        context.Response.StatusCode,
                        feature.Path,
                        feature.Error
                    }, new JsonSerializerSettings().Initialize());
                context.RequestServices.GetService<ILogger<ProductionExceptionHandler>>().LogError(msg);
                return context.Response.WriteAsync(new
                {
                    context.Response.StatusCode,
                    feature.Path
                }.ToJson());
            };

        public string Name => EnvironmentName.Production;
    }
}
