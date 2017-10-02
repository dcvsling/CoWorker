using System.Linq;
using System.Collections;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using CoWorker.Diagnostics;
using Xunit;
using System;

namespace CoWorker.Logging.Tests
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.TestHost;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var host = WebHost.CreateDefaultBuilder()
                .Configure(app =>
                {
                    app.Properties.Add("_tasks", new Dictionary<string, TaskRunnerFactory>());
                    var builder = new TaskRunnerBuilder<HttpContext>("test",app);
                    var t1 = builder.New("1").Use(next => ctx => ctx.Response.WriteAsync("1")).Build(x => x);
                    var t2 = builder.New("2").Use(next => ctx => ctx.Response.WriteAsync("2")).Build(x => x);
                    var t3 = builder.New("3")
                        .Use(next => ctx => ctx.Response.WriteAsync("3"))
                        .Use(next => ctx => ctx.Response.WriteAsync("4"))
                        .Build(x => x);
                    var map = app.Properties["_tasks"] as IDictionary<string, TaskRunnerFactory>;
                    app.Use(next => ctx =>
                    {
                        var tasks = ctx.Request.Path.Value.Split('/').Select(x => map[x].Create());
                        return Task.Run(() => tasks.Each(x => x()));
                    });
                });
            var server = new TestServer(host);
            server.CreateRequest("/1/2/3").GetAsync()
                .ContinueWith(t => t.Result.Content.ReadAsStringAsync()
                .ContinueWith(ts => Assert.Equal("1234", ts.Result)));

            server.CreateRequest("/2/3/1").GetAsync()
                .ContinueWith(t => t.Result.Content.ReadAsStringAsync()
                .ContinueWith(ts => Assert.Equal("2341", ts.Result)));

            server.CreateRequest("/2/3/1/3/2/1").GetAsync()
                .ContinueWith(t => t.Result.Content.ReadAsStringAsync()
                .ContinueWith(ts => Assert.Equal("23413421", ts.Result)));
        }
    }
}
