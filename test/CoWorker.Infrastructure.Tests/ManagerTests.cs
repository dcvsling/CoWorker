using CoWorker.Infrastructure.Cache;

namespace CoWorker.Infrastructure.TypeStore
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyModel;
    using System;
    using System.Linq;
    using System.Reflection;
    using Xunit;

    public class ManagerTests
    {
        [Fact]
        public void test_Manager()
        {
            var manager = new TypeStoreManager(new DictionaryCache<Assembly>());
            var diff = DependencyContext.Default.GetDefaultAssemblyNames()
                .SelectMany(x => x.Load().ExportedTypes)
                .Except(manager.List);
            Assert.False(diff.Any());
        }
    }
}
