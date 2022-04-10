namespace Comandante
{
    /// <summary>
    /// Represents a command
    /// </summary>
    /// <typeparam name="TCommand">A command payload type</typeparam>
    /// <typeparam name="TCommandResult">A command result</typeparam>
    public interface ICommand<TCommand, out TCommandResult> where TCommand : ICommand<TCommand, TCommandResult>
    {
    }
}