using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using System.Net.Cache;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using System.Collections.Generic;

namespace CoWorker.Models.Abstractions.Filters
{
    public class PipeStartupFilter : IStartupFilter
    {
        private readonly IEnumerable<IApplicationFilter> _apps;

        public PipeStartupFilter(IEnumerable<IApplicationFilter> apps)
        {
            _apps = apps.OrderBy(x => x.Level);
        }

        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
            => app => _apps.Aggregate(next ?? EndOfNext(), AggregateFilter)(app);

        private Action<IApplicationBuilder> AggregateFilter(
            Action<IApplicationBuilder> next,
            IApplicationFilter filter)
            => ap => filter.Configure(ap, next);

        private Action<IApplicationBuilder> EndOfNext()
            => app => app.Use(next => ctx => Task.Run(() => ctx.Response.Redirect($"/#{ctx.Request.Path}")));
    }
}