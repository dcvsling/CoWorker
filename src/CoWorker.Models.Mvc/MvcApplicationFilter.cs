using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using System;
using CoWorker.Models.Abstractions.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace CoWorker.Models.Mvc
{

    public class MvcApplicationFilter : IApplicationFilter
    {
        public string Name => nameof(MvcApplicationFilter);

        public int Level => 50;

        public void Configure(IApplicationBuilder app, Action<IApplicationBuilder> next)
            => app.Next(next)
                .UseStaticFiles()
                .UseMvc();
    }
}
