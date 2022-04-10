namespace Comandante.Tests.Commands
{
    public class CreateUserCommand : ICommand<CreateUserCommand, long>
    {
        public CreateUserCommand(string userName)
        {
            UserName = userName;
        }

        public string UserName { get; }
    }
}