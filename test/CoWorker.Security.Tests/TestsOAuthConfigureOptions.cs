using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using CoWorker.Builder;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Xunit;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.TestHost;
using CoWorker.DependencyInjection.Configuration;

namespace CoWorker.Security.Tests
{
    public class TestsOAuthConfigureOptions
    {
        [Fact]
        async public Task test_configureOptions()
        {
            var host = WebHost.CreateDefaultBuilder()
                .ConfigureServices(srv => srv
                    .AddSingleton(p => p.GetService<WebHostBuilderContext>().Configuration)
                    .AddSingleton(typeof(IConfigureOptions<>), typeof(ConfigurationConfigureOptions<>))
                    .AddSingleton<IPostConfigureOptions<GoogleOptions>,ConfigurationConfigureOptions<GoogleOptions>>()
                    .AddOptions()
                    .AddAuthentication()
                        .AddGoogle())
                .Configure(app => app.Use(
                    next => ctx =>
                    {
                        var google = new GoogleOptions();
                        var ss = ctx.RequestServices.GetServices<IPostConfigureOptions<GoogleOptions>>();

                        ctx.Response.WriteAsync(ctx.RequestServices.GetService<IOptionsSnapshot<GoogleOptions>>().Get(string.Empty).ClientId);
                        ctx.Response.WriteAsync(ctx.RequestServices.GetService<IOptionsSnapshot<GoogleOptions>>().Get("Google").ClientId);
                        ctx.Response.WriteAsync(ctx.RequestServices.GetService<IOptionsFactory<GoogleOptions>>().Create(string.Empty).ClientId);
                        ctx.Response.WriteAsync(ctx.RequestServices.GetService<IOptionsFactory<GoogleOptions>>().Create("Google").ClientId);
                        return Task.CompletedTask;
                    }));


            var result = await new TestServer(host).CreateRequest("/").GetAsync();
            var actual = await result.Content.ReadAsStringAsync();
            Assert.NotEqual(string.Empty, actual);
        }
    }
}
