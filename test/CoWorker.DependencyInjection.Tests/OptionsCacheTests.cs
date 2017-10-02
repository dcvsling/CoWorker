using Microsoft.Extensions.DependencyInjection;
using Xunit;
using CoWorker.DependencyInjection.Factory;

namespace CoWorker.DependencyInjection.Tests
{
    public class OptionsCacheTests
    {
       
        [Fact]
        public void test_default_work()
        {
            using(var provider = TestModel.GetProvider())
            {
                var expect = provider.GetRequiredService<IOptionsCache<TestModel>>();
                Assert.NotNull(expect.Get());
            }

        }
        [Fact]
        public void test_twice_are_same()
        {
            using (var provider = TestModel.GetProvider())
            {
                var expect = provider.GetRequiredService<IOptionsCache<TestModel>>();
                var actual = provider.GetRequiredService<IOptionsCache<TestModel>>();
                Assert.Same(expect.Get(), actual.Get());
            }
        }   
    }
}
