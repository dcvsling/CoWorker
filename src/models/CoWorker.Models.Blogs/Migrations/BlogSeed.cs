using CoWorker.Models.Blog;
using System;
using Microsoft.EntityFrameworkCore.Internal;
using System.Threading.Tasks;

namespace CoWorker.Models.Blogs.Migrations
{
    public class BlogSeed
    {
        private readonly IBlogFactory _factory;

        public BlogSeed(IBlogFactory factory)
        {
            this._factory = factory;
        }

        async public Task CreateSeed()
        {
            var rootId = Guid.Empty;
            var post = new Post()
            {
                Title = string.Empty,
                Source = string.Empty,
                Content = string.Empty,
                Description = string.Empty,
                Creator = "System",
                Modifier = "System",
                ModifyDate = DateTimeOffset.MinValue,
                CreateDate = DateTimeOffset.MinValue,
            };

            var state = new PostState()
            {
                Owner = new User() { Id = new Guid(), Email = string.Empty },
                Level = 0,
                EndDate = DateTime.MaxValue,
                StartDate = DateTime.MinValue
            };
            await _factory.Post(state.SetPost(post));
        }
    }
}
