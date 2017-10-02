using CoWorker.DependencyInjection.Abstractions;

namespace CoWorker.DependencyInjection.Decorator
{
    public interface IProcessHandler<TCommand> : ICommandHandler<TCommand>,IQueryHandler<TCommand> where TCommand : class
    {
        //void Execute(TCommand cmd);
        //IEnumerable<TResult> Query<TResult>(IQueryable<TCommand> query);
    }
}
