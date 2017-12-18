using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace CoWorker.Models.Abstractions
{
    public class DatabaseRepository<TContext, TModel> : IRepository<TModel>
        where TContext : DbContext
        where TModel : class, new()
    {
        private readonly DbContextPool<TContext> _pool;

        public DatabaseRepository(DbContextPool<TContext> pool)
        {
            _pool = pool;
        }

        async private Task Using(Func<DbSet<TModel>, Task> func)
        {
            using (var context = _pool.Rent())
            {
                await func(context.Set<TModel>());
            }
        }

        async private Task<T> Return<T>(Func<DbSet<TModel>,Func<Task<T>,Task>,Task> func)
        {
            T result = default;
            await Using(async set => await func(set,async x => result = await x));
            return result;
        }

        public Task Add(TModel model)
            => Using(set => set.AddAsync(model));

        public Task<TModel> Find(object id)
            => Return<TModel>((set, cb) => cb(set.FindAsync(id)));

        public Task Remove(TModel model)
            => Using(set => Task.Run(() => set.Remove(model)));

        public Task Update(TModel model)
            => Using(set => Task.Run(() => set.Update(model)));
    }
}
