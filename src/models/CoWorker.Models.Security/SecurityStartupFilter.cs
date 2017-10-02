using CoWorker.Builder;
using CoWorker.Models.Security.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

namespace CoWorker.Models.Security
{

    public class SecurityStartupFilter : IStartupFilter
    {
        Action<IApplicationBuilder> IStartupFilter.Configure(Action<IApplicationBuilder> next)
            => app => Configure(app, next);

        public void Configure(IApplicationBuilder app, Action<IApplicationBuilder> next)
        {
            var logger = app.ApplicationServices.GetService<ILogger<SecurityStartupFilter>>();
            logger.LogInformation($"begin {nameof(SecurityStartupFilter)} application builder ");
            Configure(app,
                logger,
                app.ApplicationServices.GetService<IHostingEnvironment>(),
                next);
            logger.LogInformation($"end {nameof(SecurityStartupFilter)} application builder ");
        }

        public void Configure(
            IApplicationBuilder app,
            ILogger<SecurityStartupFilter> logger,
            IHostingEnvironment env,
            Action<IApplicationBuilder> next)
        {
            app.UseAuthentication();
            next(app);
        }
    }
}
