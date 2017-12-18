using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using CoWorker.Models.Abstractions;
using CoWorker.Primitives;
using CoWorker.Models.Abstractions.Filters;

namespace CoWorker.Models.Core.ExceptionHandler
{
    public interface IExceptionHandler : INamedHandler
    {

    }
}
