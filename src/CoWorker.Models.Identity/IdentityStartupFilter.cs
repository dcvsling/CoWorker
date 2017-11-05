using IdentitySamples.Controllers;
using IdentitySample.Controllers;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.AspNetCore.Identity;
using CoWorker.Models.Identity.Internal;
using Microsoft.AspNetCore.Builder;
using System;

namespace CoWorker.Models.Identity
{
    public class IdentityStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
            => next;
    }
}
