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
        
        /// <summary>
        /// Asynchronously dispatches a command to a single command handler
        /// </summary>
        /// <param name="command">A command</param>
        /// <param name="cancellationToken">A cancellation token</param>
        /// <typeparam name="TCommand">A command type</typeparam>
        /// <typeparam name="TCommandResult">A command result</typeparam>
        /// <returns>
        /// Returns a task that represents a command operation. The task result contains a command handler response.
        /// </returns>
        public Task<TCommandResult> Dispatch<TCommand, TCommandResult>(TCommand command, 
            CancellationToken cancellationToken)
            where TCommand: ICommand<TCommandResult>
        {
            var handler = _serviceFactory.GetService<ICommandHandler<TCommand, TCommandResult>>();

            return handler.Handle(command, cancellationToken);
        }
    }
}