using System.Linq;
using CoWorker.Models.Blog;

namespace CoWorker.Models.Models.Blog.Query
{

    public class GetAll : IBlogQuery
    {
        public void Get(IQueryable<PostRelated> query)
        { }
    }
}