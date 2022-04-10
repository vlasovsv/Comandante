using System.Threading;
using System.Threading.Tasks;

namespace Comandante.Tests.Commands
{
    public class CommandDecorator<TCommand, TCommandResult> : ICommandHandler<TCommand, TCommandResult>
        where TCommand : ICommand<TCommand, TCommandResult>
    {
        private readonly ICommandHandler<TCommand, TCommandResult> _decoratee;

        public CommandDecorator(ICommandHandler<TCommand, TCommandResult> decoratee)
        {
            _decoratee = decoratee;
        }
        
        public Task<TCommandResult> Handle(TCommand command, CancellationToken cancellationToken)
        {
            return _decoratee.Handle(command, cancellationToken);
        }
    }
}