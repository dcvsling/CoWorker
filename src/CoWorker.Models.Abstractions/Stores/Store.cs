using System.Linq;
using System.Collections.Generic;

namespace CoWorker.Models.Abstractions.Stores
{
    public class Store<T> : IStore<T> where T : class
    {
        private IDictionary<string, IDataProvider<T>> map;
        public Store(IEnumerable<IDataProvider<T>> stores)
        {
            this.map = stores.ToDictionary(x => x.Name, x => x);
        }
        public T Get(string name) => map.ContainsKey(name) ? map[name].Value : default;
    }
}
