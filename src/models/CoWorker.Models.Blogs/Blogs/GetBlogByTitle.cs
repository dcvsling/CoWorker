
namespace CoWorker.Models.Models.Blog.Query
{
    using System;
    using System.Linq;
    using CoWorker.Models.Blog;

    public class GetBlogByTitle : IBlogQuery
    {
        private readonly string _title;

        public GetBlogByTitle(string title)
        {
            _title = title;
        }
        public void Get(IQueryable<PostRelated> query)
            => query.Where(related => related.Current.Title == _title)
                  .Where(related => related.StartDate <= DateTime.Now)
                  .Where(related => related.EndDate >= DateTime.Now)
                  .Take(1);
    }
}
