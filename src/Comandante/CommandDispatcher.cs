using System;
using System.Threading;
using System.Threading.Tasks;

namespace Comandante
{
    /// <summary>
    /// Default command dispatcher implementation
    /// </summary>
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceFactory _serviceFactory;

        /// <summary>
        /// Creates a new command dispatcher
        /// </summary>
        /// <param name="serviceFactory">A service factory</param>
        public CommandDispatcher(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        /// <inheritdoc cref="ICommandDispatcher.Dispatch{TCommand,TCommandResult}"/>
        public Task<TCommandResult> Dispatch<TCommand, TCommandResult>(
            ICommand<TCommand, TCommandResult> command,
            CancellationToken cancellationToken
        ) where TCommand : ICommand<TCommand, TCommandResult>
        {
            if (!(command is TCommand concreteCommand))
            {
                throw new ArgumentException($"Command must be an instance of {typeof(TCommand)}");
            }

            var handler = _serviceFactory.GetService<ICommandHandler<TCommand, TCommandResult>>();
            
            if (handler is null)
                throw new ComandanteException(
                    $"Handler was not found for command of type {typeof(TCommand)}. Register your handlers with the container.");

            return handler.Handle(concreteCommand, cancellationToken);
        }
    }
}