using CoWorker.DependencyInjection.Configuration;
using System.Linq;
using CoWorker.DependencyInjection.Decorator;
using Microsoft.AspNetCore.Hosting;
using CoWorker.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;
using CoWorker.DependencyInjection.Factory;
using Moq;
using Microsoft.Extensions.Configuration;
using CoWorker.DependencyInjection.Abstractions;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace CoWorker.DependencyInjection.Tests
{
    public class ConfigurationTests
    {
        [Fact]
        public void test_configuration()
        {
            using (var provider = TestModel.GetProvider())
            {
                var config = provider.GetRequiredService<IConfiguration>();
                Assert.Equal("esport-asia-db", config.GetSection("datasource:name").Value);
            }
        }
        [Fact]
        public void test_ConfigurationConfigureOptions()
        {
            using (var provider = TestModel.GetProvider(
                srv => srv.AddConfiguration(
                    c => c.AddInMemoryCollection(new Dictionary<string, string>()
                    {
                        [$"{typeof(DataSource).Namespace.Replace(".",":")}:{nameof(DataSource)}"] = "esport-asia-db"
                    }))
                ))
            {
                var config = new ConfigurationConfigureOptions<DataSource>(provider.GetService<IConfiguration>(),null);
                var actual = new DataSource();
                config.PostConfigure(string.Empty,actual);
                Assert.Equal("esport-asia-db", actual.Name);
            }
        }

        [Fact]
        public void test_ObjectConfigure()
        {
            using (var provider = TestModel.GetProvider(
                srv => srv.AddConfiguration(
                    c => c.AddInMemoryCollection(new Dictionary<string, string>()
                    {
                        [$"{typeof(DataSource).Namespace}:{nameof(DataSource)}"] = "esport-asia-db"
                    }))
                ))
            {
                var configs = provider.GetServices<IObjectConfigure<DataSource>>();
                var actual = configs.Aggregate(
                    new DataSource(),
                    (seed, next) => next.Configure("esport-asia-db", seed));
                Assert.Equal("esport-asia-db", actual.Name);
            }
        }



        public class DataSource
        {
            public string Name { get; set; }
            public string Provider { get; set; }
            public string ConnStr { get; set; }
        }
    }
}
