
using System.Threading.Tasks;

namespace CoWorker.Models.Abstractions
{
    public interface IRepository<TModel> where TModel : class, new()
    {
        Task Add(TModel model);
        Task Remove(TModel model);
        Task Update(TModel model);
        Task<TModel> Find(object id);
    }
}
