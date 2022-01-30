using System.Threading;
using System.Threading.Tasks;

namespace Comandante.Tests.Queries
{
    public class GetUserQueryHandler : IQueryHandler<GetUserQuery, User>
    {
        public Task<User> Handle(GetUserQuery query, CancellationToken cancellationToken)
        {
            return Task.FromResult(new User(42, "The one"));
        }
    }
}