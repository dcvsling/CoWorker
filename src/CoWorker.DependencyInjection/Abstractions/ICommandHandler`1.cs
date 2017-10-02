using System.Linq;

namespace CoWorker.DependencyInjection.Abstractions
{

    public interface ICommandHandler<TCommand> where TCommand : class
    {
        void Handle(TCommand cmd);
    }
}