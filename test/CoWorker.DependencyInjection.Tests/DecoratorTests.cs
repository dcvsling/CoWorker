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

    public class DecoratorTests
    {
        [Fact]
        public void test_decorator()
        {
            using (var provider = TestModel.GetProvider(
                srv => srv.Decorate<TestModel.A>(string.Empty, x => new TestModel.DecoratorA(x))))
            {
                var factory = provider.GetService<IObjectFactory<TestModel.A>>();
                var value = factory.Create(string.Empty);
                var actual = value.Result;
                Assert.True(actual);
            }
        }
    }
}
