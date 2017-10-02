using Microsoft.EntityFrameworkCore.Design.Internal;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using CoWorker.EntityFramework.Abstractions;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using Microsoft.AspNetCore.TestHost;
using TwitchAdapter.Models.Blog.Query;
using CoWorker.Models.Blog;
using CoWorker.Models.Blogs;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Moq;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Hosting;
namespace CoWorker.EntityFramework.Tests
{
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;
    using CoWorker.Builder;
    using System.Threading.Tasks;
    using CoWorker.EntityFramework.Internal;
    using System.IO;
    using CoWorker.DependencyInjection.Factory;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.AspNetCore;

    public class RepositoryProviderTests
    {
        [Fact]
        async public Task test_dbcontext()
        {
            var builder = WebHost.CreateDefaultBuilder().ConfigureAppConfiguration((ctx, c) => ctx.HostingEnvironment.EnvironmentName = EnvironmentName.Development)
                .ConfigureServices(srv => srv.AddDefaultService().AddEntityFramework()
                    .AddBlog()
                    .AddSingleton<IDesignTimeDbContextFactory<DbContext<PostRelated>>,BlogDbContextFactory>())
                .Configure(app => app.Use(
                    next => async ctx => {
                        var context = ctx.RequestServices.GetService<DbContext<PostRelated>>();
                        var factory = ctx.RequestServices.GetService<IBlogFactory>();
                        var result = factory.GetBlogs(new GetAll());
                        }));

            var client = new TestServer(builder).CreateClient();
            var res = await client.GetAsync("/");
        }
    }

    public class BlogDbContextFactory : IDesignTimeDbContextFactory<DbContext<PostRelated>>
    {
        private readonly IModelBuilderFactory _factory;
        public BlogDbContextFactory(IModelBuilderFactory factory)
        {
            this._factory = factory;
        }
        public DbContext<PostRelated> CreateDbContext(System.String[] args)
        {
            var builder = new DbContextOptionsBuilder();
            var modelbuilder = _factory.Create();
            builder.UseInternalServiceProvider(InternalService().BuildServiceProvider());

            new BlogModelBuilderConfigureOptions().Configure(modelbuilder);
            builder.UseModel(modelbuilder.Model);
            return new DbContext<PostRelated>(builder.Options);
        }

        private IServiceCollection InternalService()
        {
            var srv = new ServiceCollection().AddLogging().AddMemoryCache().AddConfiguration();
            new SqlServerDesignTimeServices().ConfigureDesignTimeServices(
                srv.AddEntityFrameworkSqlServer()
                    .AddScaffolding(new OperationReporter(new OperationReportHandler())));
            return srv;
        }
    }
}