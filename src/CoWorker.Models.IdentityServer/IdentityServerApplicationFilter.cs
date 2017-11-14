using Microsoft.AspNetCore.Builder;
using System;
using CoWorker.Models.Abstractions.Filters;

namespace CoWorker.Models.IdentityServer
{

    public class IdentityServerApplicationFilter : IApplicationFilter
    {
        public string Name => nameof(IdentityServerApplicationFilter);

        public int Level => 25;

        public void Configure(IApplicationBuilder app, Action<IApplicationBuilder> next)
            => app.UseIdentityServer()
                .Next(next);
    }
}
