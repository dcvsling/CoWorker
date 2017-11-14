using Microsoft.Extensions.Internal;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CoWorker.Models.Blog
{
    using System;
    using Microsoft.AspNetCore.Http;
    using System.Threading.Tasks;
    using CoWorker.Models.Models.Blog.Query;
    using Microsoft.EntityFrameworkCore.Internal;
    using System.Collections.Generic;
    using CoWorker.Models.Blogs;

    public class BlogFactory : IBlogFactory
    {
        public static readonly DateTime MAX_DATE = new DateTime(2100, 12, 31);
        public static readonly DateTime MIN_DATE = new DateTime(1900, 1, 1);
        private readonly DbContextPool<BlogDbContext> _context;
        private readonly IHttpContextAccessor _accessor;
        private readonly ISystemClock _clock;
        public BlogFactory(
            DbContextPool<BlogDbContext> context,
            IHttpContextAccessor accessor,
            ISystemClock clock)
        {
            this._accessor = accessor;
            this._context = context;
            this._clock = clock;
        }

        async private Task Using(Func<BlogDbContext,Task> func)
        {
            using (var ctx = _context.Rent())
            {
                await Task.Run(() => func(ctx));
            }
        }

        async private Task<IEnumerable<T>> Query<T>(Func<BlogDbContext,Task<IEnumerable<T>>> func)
        {
            using (var context = _context.Rent())
            {
                return await func(context);
            }
        }

        async public Task<Post> Post(PostState state)
        {
            using (var ctx = _context.Rent())
            {
                var post = state.GetPost();
                var parentId = state.ParentId;
                await ctx.AddAsync(post);
                var related = (state
                    ?? new PostState() {
                        EndDate = BlogDefault.MAX_DATE ,
                        StartDate = BlogDefault.MAX_DATE })
                    .CreateRelated();
                await ctx.AddAsync(related);
                related.Current = post;
                var parentPost = ctx.Set<Post>().Find(parentId);
                ctx.Attach(parentPost).State = EntityState.Unchanged;
                var queryRelated = ctx.Set<PostRelated>().Include(p => p.Current);
                var parent = await (queryRelated.FirstOrDefaultAsync(r => r.Current != null && r.Current.Id == parentPost.Id)
                    ?? queryRelated.AsNoTracking().FirstOrDefaultAsync(r => r.Current.Id == Guid.Empty));
                parent.Posts = parent.Posts ?? new List<PostRelated>();
                parent.Posts.Add(related);
                await ctx.SaveChangesAsync();
                return post;
            }
        }

        async public Task<object> Query(IBlogQuery query)
            => (await Query(async ctx => (await UseQuery(ctx,query)).AsEnumerable()))
                .ToFormat()
                .ToArray();



        async private Task<IEnumerable<PostRelated>> UseQuery(BlogDbContext context,IBlogQuery query)
        {
            var source = context.Set<PostRelated>().Include(p => p.Current)
                .Where(x => x.Current.Id == Guid.Empty || (x.EndDate >= _clock.UtcNow.DateTime && x.StartDate <= _clock.UtcNow.DateTime));
            query.Get(source);
            var result = await source.ToListAsync();
            return result.AsEnumerable();
        }


    }
}
