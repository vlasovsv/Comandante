using System.Threading;
using System.Threading.Tasks;

namespace Comandante.Tests.Commands
{
    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, long>
    {
        public Task<long> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            return Task.FromResult(42l);
        }
    }
}