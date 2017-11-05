using Microsoft.Extensions.Internal;

using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Hosting;

namespace CoWorker.Models.Blogs
{
    using CoWorker.Models.Blog;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Configuration;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Internal;

    public class BlogHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((ctx, srv) =>
            {
                srv.AddBlog()
                .AddSingleton(p => new DbContextPool<BlogDbContext>(p.GetService<DbContextOptions<BlogDbContext>>()))
                .AddDbContextPool<BlogDbContext>(options =>
                    options.EnableSensitiveDataLogging(true).UseSqlServer(ctx.Configuration.GetConnectionString("esport-asia-db")));
                srv.TryAddSingleton<ISystemClock,SystemClock>();
            });
        }
    }
}
