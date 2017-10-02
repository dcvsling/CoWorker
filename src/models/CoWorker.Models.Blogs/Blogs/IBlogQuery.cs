using System.Linq;
using CoWorker.Models.Blog;

namespace CoWorker.Models.Models.Blog.Query
{
    public interface IBlogQuery
    {
        void Get(IQueryable<PostRelated> context);
    }
}