using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using CoWorker.Models.Abstractions.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CoWorker.Models.Mvc
{
    public class MvcApplicationFilter : IApplicationFilter
    {
        public string Name => nameof(MvcApplicationFilter);

        public int Level => 50;

        public void Configure(IApplicationBuilder app, Action<IApplicationBuilder> next)
            => app.Next(next)
                .UseMvc(app.ApplicationServices.GetService<IConfigureOptions<IRouteBuilder>>().Configure)
                .UseFileServer();
    }

    public class RouteBuilderConfigureOptions : IConfigureOptions<IRouteBuilder>
    {
        public void Configure(IRouteBuilder options)
        {
            options.MapAreaRoute("mnt", "mnt", "{mnt}/{model}");
        }
    }
}
