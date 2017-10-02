using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.MediaServices.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CoWorker.Azure.Media.Repository
{

    public class LocatorRepository : IRepository<ILocator>
    {
        private readonly CloudBaseCollection<ILocator> _locator;

        public LocatorRepository(CloudBaseCollection<ILocator> locator)
        {
            this._locator = locator;
        }
        public Task<ILocator> CreateAsync(String name) => throw new NotImplementedException();
        public Task DeleteAsync(String name) => throw new NotImplementedException();
        public Task<ILocator> FindAsync(String name) => throw new NotImplementedException();
        public IEnumerable<ILocator> Query(Expression<Func<ILocator, Boolean>> predicate) => throw new NotImplementedException();
        public Task UpdateAsync(String name) => throw new NotImplementedException();
    }
}
