using System.Threading;
using System.Threading.Tasks;

namespace Comandante
{
    /// <summary>
    /// Send a single command to be handled by a single command handler.
    /// </summary>
    public interface ICommandDispatcher
    {
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
        Task<TCommandResult> Dispatch<TCommand, TCommandResult>(TCommand command, CancellationToken cancellationToken)
            where TCommand: ICommand<TCommandResult>;
    }
}