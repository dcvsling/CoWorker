using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
namespace CoWorker.Builder
{
    //using Builder = ConfigureAggregateDelegate<IServiceCollection, ConfigureDelegate<IServiceCollection>>;
    //using Executor = ConfigureExecuteDelegate<IServiceCollection>;
    //using Member = ConfigureDelegate<IServiceCollection>;
    //public delegate TBuilder ConfigureDelegate<TBuilder>(TBuilder builder);
    //public delegate TBuilder ConfigureExecuteDelegate<TBuilder>(TBuilder builder, WebHostBuilderContext context);
    //public delegate ConfigureExecuteDelegate<TBuilder> ConfigureAggregateDelegate<TBuilder, TMember>(params ConfigureDelegate<TMember>[] members);
    //public delegate ConfigureDelegate<TBuilder> ConfigureLazyDelegate<TBuilder>(ConfigureExecuteDelegate<TBuilder> executor);
    //public static class Environment
    //{
    //    public static Builder Dev
    //        => mem => (srv, ctx) => ctx.HostingEnvironment.IsDevelopment() ? mem.Aggregate(srv, (seed, next) => next(seed)) : srv;
    //    public static Builder Prod
    //        => mem => (srv, ctx) => ctx.HostingEnvironment.IsProduction() ? mem.Aggregate(srv, (seed, next) => next(seed)) : srv;
    //    public static Builder Default
    //        => mem => (srv, ctx) => mem.Aggregate(srv, (seed, next) => next(seed));

    //    public static Executor IsDev(this Executor builder, params Member[] members)
    //        => (srv, ctx) => Dev(members)(builder(srv, ctx), ctx);
    //    public static Executor IsProd(this Executor builder, params Member[] members)
    //        => (srv, ctx) => Prod(members)(builder(srv, ctx), ctx);
    //}
}