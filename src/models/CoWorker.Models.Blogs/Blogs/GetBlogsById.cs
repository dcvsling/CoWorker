
namespace CoWorker.Models.Models.Blog.Query
{
    using System;
    using System.Linq;
    using CoWorker.Models.Blog;

    public class GetBlogsById : IBlogQuery
    {
        private readonly Guid _id;

        public GetBlogsById(Guid id)
        {
            _id = id;
        }

        public void Get(IQueryable<PostRelated> query)
            => query.Where(related => related.Current.Id == _id);
    }
}