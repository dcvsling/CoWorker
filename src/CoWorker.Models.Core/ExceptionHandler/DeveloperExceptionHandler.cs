using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Diagnostics;
using CoWorker.Primitives;

namespace CoWorker.Models.Core.ExceptionHandler
{
    public class DeveloperExceptionHandler : IExceptionHandler
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
                return context.Response.WriteAsync(msg);
            };

        public string Name => EnvironmentName.Development;
    }
}
