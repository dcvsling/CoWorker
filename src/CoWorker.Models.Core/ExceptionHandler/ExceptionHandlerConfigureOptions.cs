using Microsoft.AspNetCore.Hosting;
using System.Linq;
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace CoWorker.Models.Core.ExceptionHandler
{

    public class ExceptionHandlerConfigureOptions : IConfigureNamedOptions<ExceptionHandlerOptions>
    {
        private readonly IEnumerable<IExceptionHandler> _handlers;
        private readonly IOptionsMonitorCache<IExceptionHandler> _cache;
        private readonly IHostingEnvironment _env;

        public ExceptionHandlerConfigureOptions(
            IEnumerable<IExceptionHandler> handlers,
            IOptionsMonitorCache<IExceptionHandler> cache,
            IHostingEnvironment env) {
            _handlers = handlers;
            _cache = cache;
            _env = env;
        }

        public void Configure(ExceptionHandlerOptions options)
            => Configure(_env.EnvironmentName, options);

        public void Configure(string name, ExceptionHandlerOptions options)
        {
            options.ExceptionHandler = _cache.GetOrAdd(name, CreateHandler).Handler;

            IExceptionHandler CreateHandler()
                => _handlers.FirstOrDefault(x => x.Name == name)
                    ?? _handlers.First(x => x.Name == Helper.EmptyString);
        }
    }
}
