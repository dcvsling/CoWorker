using Microsoft.AspNetCore.Mvc.Infrastructure;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using CoWorker.LightMvc.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;

namespace CoWorker.Models.Swagger
{

    public class SwaggerStartupFilter : IStartupFilter
    {
        Action<IApplicationBuilder> IStartupFilter.Configure(Action<IApplicationBuilder> next)
           => app => Configure(app, next);

        public void Configure(IApplicationBuilder app, Action<IApplicationBuilder> next)
        {
            var logger = app.ApplicationServices.GetService<ILogger<SwaggerStartupFilter>>();
            logger.LogInformation($"begin {nameof(SwaggerStartupFilter)} application builder ");
            Configure(app,
                logger,
                app.ApplicationServices.GetService<IHostingEnvironment>(),
                next);
            logger.LogInformation($"end {nameof(SwaggerStartupFilter)} application builder ");
        }

        public void Configure(
            IApplicationBuilder app,
            ILogger<SwaggerStartupFilter> logger,
            IHostingEnvironment env,
            Action<IApplicationBuilder> next)
        {
            app.Use(req => async ctx => {
                var items = ctx.RequestServices.GetService<IActionDescriptorCollectionProvider>()
                    .ActionDescriptors
                    .Items;
               await req(ctx);
            });
            next(app);
         
            app.UseFileServer(app.ApplicationServices.GetService<IOptions<FileServerOptions>>().Value)
                .UseMvc()
                .Use(req => async ctx =>
                {
                    if (env.IsProduction())
                    {
                        ctx.Response.Redirect("/");
                        return;
                    }
                    if (!ctx.User.Claims.Any())
                    {
                        await ctx.ChallengeAsync("Google");
                        logger.LogInformation("challenge for swagger");
                        return;
                    }
                    if (!ctx.User.Identity.IsAuthenticated)
                    {
                        await ctx.SignInAsync(ctx.User);
                        logger.LogInformation($"signiin {ctx.User.FindFirst(ClaimTypes.Email).Value} for swagger");
                    }
                    logger.LogInformation($"{ctx.User.FindFirst(ClaimTypes.Email).Value} enter swagger ui");
                    await req(ctx);
                })
                .UseSwaggerWithUI();
        }
    }
}
