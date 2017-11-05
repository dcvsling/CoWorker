
namespace CoWorker.Models.Models.Blog.Query
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using CoWorker.Models.Blog;

    public class News : GetBlogByTitle
    {
        public const string NEWS = "News";

        public News() : base(NEWS) { }
    }
}
