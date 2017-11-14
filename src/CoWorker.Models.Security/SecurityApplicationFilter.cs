using System;
using Microsoft.AspNetCore.Builder;
using CoWorker.Models.Abstractions.Filters;

namespace CoWorker.Models.Security
{

    public class SecurityApplicationFilter : IApplicationFilter
    {
        public string Name => nameof(SecurityApplicationFilter);

        public int Level => 30;

        public void Configure(IApplicationBuilder app, Action<IApplicationBuilder> next)
            => app.UseAuthentication()
                .Next(next);

    }
}
