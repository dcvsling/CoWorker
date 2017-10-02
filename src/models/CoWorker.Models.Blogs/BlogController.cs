using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
namespace CoWorker.Models.Blog
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using System;
    using CoWorker.Models.Models.Blog.Query;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using CoWorker.Models.Blogs.Migrations;

    [Controller]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Route("api/blog")]
    public class BlogController //DevSkim: ignore DS184626
    {
        private readonly IBlogFactory _factory;
        private readonly IHttpContextAccessor _accessor;
        private readonly BlogSeed _seed;
        private readonly IOptionsFactory<PostRelated> _related;

        public BlogController(IBlogFactory factory,IHttpContextAccessor accessor,BlogSeed seed,IOptionsFactory<PostRelated> related)
        {
            _factory = factory;
            this._accessor = accessor;
            this._seed = seed;
            this._related = related;
        }

        async public Task<IActionResult> Get<TQuery>() where TQuery : IBlogQuery, new()
            => new OkObjectResult(await _factory.Query(new TQuery()));

        async protected virtual Task<IActionResult> Get<TQuery>(TQuery query) where TQuery : IBlogQuery
            => new OkObjectResult(await _factory.Query(query));

        [HttpGet]
        public Task<IActionResult> Get()
            => Get<GetAll>();

        [HttpGet("{id}")]
        public Task<IActionResult> Get([FromRoute] Guid id)
            => Get(new GetBlogsById(id));

        [HttpGet("news")]
        public Task<IActionResult> News()
            => Get<News>();
        [HttpPost]
        async public Task<IActionResult> Post([FromBody] Post post)
            => await Post(new PostState() { ParentId = new Guid() }.SetPost(post));
        [HttpPost("{parentId}")]
        async public Task<IActionResult> Post([FromBody] PostState state,[FromRoute] Guid parentId)
        {
            state.ParentId = parentId;
            var post = await _factory.Post(state);
            return new CreatedResult($"{_accessor.HttpContext.Request.Path}/{post.Id}", post);
        }

        [HttpPost("seed")]
        async public Task<IActionResult> Seed()
        {
            await _seed.CreateSeed();
            return new CreatedResult(_accessor.HttpContext.Request.Path,default);
        }
    }
}
