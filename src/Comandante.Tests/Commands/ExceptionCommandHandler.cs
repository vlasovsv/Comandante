using System;
using System.Threading;
using System.Threading.Tasks;

namespace Comandante.Tests.Commands
{
    public class ExceptionCommandHandler : ICommandHandler<ExceptionCommand, long>
    {
        public Task<long> Handle(ExceptionCommand command, CancellationToken cancellationToken)
        {
            throw new CommandException();
        }
    }

    public class ExceptionCommand : ICommand<long>
    {
        
    }

    public class CommandException : Exception
    {
        
    }
}