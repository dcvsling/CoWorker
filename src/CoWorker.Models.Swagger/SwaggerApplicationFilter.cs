using CoWorker.LightMvc.Swagger;
using Microsoft.AspNetCore.Builder;
using System;
using CoWorker.Models.Abstractions.Filters;

namespace CoWorker.Models.Swagger
{

    public class SwaggerApplicationFilter : IApplicationFilter
    {
        public string Name => nameof(SwaggerApplicationFilter);

        public int Level => 20;

        public void Configure(IApplicationBuilder app, Action<IApplicationBuilder> next)
            => app.Next(next)
                .UseSwaggerWithUI();
    }
}
