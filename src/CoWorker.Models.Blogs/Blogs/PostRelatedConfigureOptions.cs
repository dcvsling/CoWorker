using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace CoWorker.Models.Blog
{
    using System;
    using Microsoft.AspNetCore.Http;
    using System.Collections.Generic;
    using CoWorker.Models.Blogs;

    public class PostRelatedConfigureOptions : IConfigureNamedOptions<PostRelated>
    {
        private readonly IHttpContextAccessor _accessor;

        public PostRelatedConfigureOptions(IHttpContextAccessor accessor)
        {
            this._accessor = accessor;
        }
        public void Configure(String name, PostRelated options)
        {
            options.StartDate = BlogDefault.MAX_DATE;
            options.EndDate = BlogDefault.MIN_DATE;
            options.Level = 0;
            options.Owner = new User { Id = Guid.Empty, Email = _accessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value };
            //options.Parent = new PostRelated { Current = new Post { Id = Guid.Parse(name) } };
            options.Posts = new List<PostRelated>();
        }
        public void Configure(PostRelated options)
            => Configure(Guid.Empty.ToString(), options);
    }
}
