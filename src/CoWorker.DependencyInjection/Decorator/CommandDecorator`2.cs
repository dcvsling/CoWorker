using CoWorker.DependencyInjection.Abstractions;
using System.Linq;

namespace CoWorker.DependencyInjection.Decorator
{

    public abstract class CommandDecorator<THandler, TCommand> : ICommandHandler<TCommand>
        where THandler : class, ICommandHandler<TCommand>
        where TCommand : class
    {
        private readonly THandler _handler;

        protected CommandDecorator(THandler handler)
        {
            this._handler = handler;
        }
        void ICommandHandler<TCommand>.Handle(TCommand cmd)
        {
            this.Handle(cmd);
            _handler.Handle(cmd);
        }

        public abstract void Handle(TCommand command);
    }
}