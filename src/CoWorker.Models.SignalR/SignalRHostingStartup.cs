using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.SignalR;
using CoWorker.Models.Abstractions.Filters;
using CoWorker.Primitives;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using MsgPack.Serialization;
using Microsoft.AspNetCore.Builder;
using System;

[assembly:HostingStartup(typeof(CoWorker.Models.SignalR.SignalRHostingStartup))]

namespace CoWorker.Models.SignalR
{
    public class SignalRHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices(
                (ctx, srv) => srv
                    .AddSignalR(o => o.JsonSerializerSettings.Initialize())
                    .Services
                    .AddAppPipe<SignalRApplicationFilter>());
        }
    }

    public class SignalRApplicationFilter : IApplicationFilter
    {
        public string Name => nameof(SignalRApplicationFilter);

        public int Level => 100;

        public void Configure(IApplicationBuilder app, Action<IApplicationBuilder> next)
        {
            next(app);
            app.ApplicationServices.GetService<ILogger<SignalRApplicationFilter>>().LogInformation("Log signalr");
            app.UseSignalR(b => b.MapHub<Hub>("chat"));
        }
    }
}
