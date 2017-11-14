using System;
using Microsoft.AspNetCore.Builder;
using CoWorker.Builder;
using CoWorker.Models.Core.ExceptionHandler;
using CoWorker.Models.Core.Cors;
using CoWorker.Models.Abstractions.Filters;

namespace CoWorker.Models.Core
{

    public class CoreApplicationFilter : IApplicationFilter
    {
        public string Name => nameof(CoreApplicationFilter);

        public int Level => 10;

        public void Configure(IApplicationBuilder app, Action<IApplicationBuilder> next)
            => app.UseErrorHandler()
                .UseAntiforgeryMiddleware()
                .UseRewriter()
                .UseCors()
                .Next(next)
                .UseStatusCodePages();
    }
}
