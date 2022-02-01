using System.Threading;
using System.Threading.Tasks;

namespace Comandante
{
    /// <summary>
    /// A command handler to process commands
    /// </summary>
    /// <typeparam name="TCommand">A command type</typeparam>
    /// <typeparam name="TCommandResult">A command type result</typeparam>
    public interface ICommandHandler<in TCommand, TCommandResult>
        where TCommand : ICommand<TCommandResult>
    {
        /// <summary>
        /// Asynchronously handles a single command
        /// </summary>
        /// <param name="command">A command</param>
        /// <param name="cancellationToken">A cancellation token</param>
        /// <returns>
        /// Returns a task that represents a command operation. The task result contains the command result
        /// </returns>
        Task<TCommandResult> Handle(TCommand command, CancellationToken cancellationToken);
    }
}