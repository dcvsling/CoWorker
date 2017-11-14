using System.Linq;
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace CoWorker.Models.Core.ExceptionHandler
{

    public class ExceptionHandlerConfigureOptions : IPostConfigureOptions<ExceptionHandlerOptions>
    {
        private readonly IEnumerable<IExceptionHandler> _handlers;
        private readonly IOptionsMonitorCache<IExceptionHandler> _cache;

        public ExceptionHandlerConfigureOptions(IEnumerable<IExceptionHandler> handlers,IOptionsMonitorCache<IExceptionHandler> cache) {
            _handlers = handlers;
            _cache = cache;
        }

        public void PostConfigure(string name, ExceptionHandlerOptions options)
        {
            options.ExceptionHandler = _cache.GetOrAdd(name,CreateHandler).Handler;

            IExceptionHandler CreateHandler()
                => _handlers.FirstOrDefault(x => x.Name == name)
                    ?? _handlers.First(x => x.Name == Helper.EmptyString);
        }
    }
}
