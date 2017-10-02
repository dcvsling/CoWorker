
using Microsoft.AspNetCore.Hosting;
using CoWorker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;

namespace CoWorker.DependencyInjection.Tests
{
    public class TestModel
    {
        internal static ServiceProvider GetProvider(Action<IServiceCollection> config = null)
        {
            var services = new ServiceCollection()
                 .AddDefaultService()
                 .AddSingleton(p => Mock.Of<IHostingEnvironment>(
                     env => env.EnvironmentName == EnvironmentName.Development));
            config?.Invoke(services);
            return services.BuildServiceProvider();
        }
        public class A { public bool Result { get; set; } = false; }
        public class DecoratorA : A
        {
            private readonly A _a;

            public DecoratorA(A a)
            {
                this._a = a;
                this.Result = true;
            }
        }
    }
}
