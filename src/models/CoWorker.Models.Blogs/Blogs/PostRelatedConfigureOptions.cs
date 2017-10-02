using CoWorker.EntityFramework;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Internal;
using Microsoft.AspNetCore.Mvc.Formatters.Internal;
using System.Security.Claims;
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

    public class PostRelatedConfigureOptions : IConfigureNamedOptions<PostRelated>
    {
        private readonly IHttpContextAccessor _accessor;

        public PostRelatedConfigureOptions(IHttpContextAccessor accessor)
        {
            this._accessor = accessor;
        }
        public void Configure(String name, PostRelated options)
        {
            options.StartDate = EntityFrameworkDefault.MAX_DATE;
            options.EndDate = EntityFrameworkDefault.MIN_DATE;
            options.Level = 0;
            options.Owner = new User { Id = Guid.Empty, Email = _accessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value };
            //options.Parent = new PostRelated { Current = new Post { Id = Guid.Parse(name) } };
            options.Posts = new List<PostRelated>();
        }
        public void Configure(PostRelated options)
            => Configure(Guid.Empty.ToString(), options);
    }
}
