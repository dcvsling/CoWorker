using System;
using System.Threading.Tasks;
using CoWorker.Models.Models.Blog.Query;
using System.Collections.Generic;

namespace CoWorker.Models.Blog
{
    public interface IBlogFactory
    {
        Task<Object> Query(IBlogQuery query);
        Task<Post> Post(PostState state);
    }
}