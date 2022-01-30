namespace Comandante.Tests.Commands
{
    public class CreateUserCommand : ICommand<long>
    {
        public CreateUserCommand(string userName)
        {
            UserName = userName;
        }

        public string UserName { get; }
    }
}