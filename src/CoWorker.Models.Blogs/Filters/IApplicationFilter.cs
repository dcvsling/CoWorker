using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.AspNetCore.Builder;
using CoWorker.Primitives;

namespace CoWorker.Models.Abstractions.Filters
{
    public interface IApplicationFilter : IName,ILevel
    {
        void Configure(IApplicationBuilder app, Action<IApplicationBuilder> next);
    }
}
