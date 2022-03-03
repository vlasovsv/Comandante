using System;
using System.Reflection;
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
        /// <typeparam name="TCommandResult">A command result</typeparam>
        /// <returns>
        /// Returns a task that represents a command operation. The task result contains a command handler response.
        /// </returns>
        public async Task<TCommandResult> Dispatch<TCommandResult>(ICommand<TCommandResult> command, CancellationToken cancellationToken)
        {
            if (command is null)
                throw new ArgumentException("Command cannot be null");

            var commandType = command.GetType();
            var handlerType = typeof(ICommandHandler<,>).MakeGenericType(commandType, typeof(TCommandResult));

            var handler = _serviceFactory.GetService(handlerType);
            
            if (handler is null)
                throw new ComandanteException(
                    $"Handler was not found for command of type {commandType}. Register your handlers with the container.");
            
            var magicMethod = handlerType.GetMethod("Handle");

            try
            {
                var invocationResult = await (Task<TCommandResult>)magicMethod.Invoke(
                    handler, new object[] { command, cancellationToken });

                return invocationResult;
            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException ?? e;
            }
        }
    }
}