using System.Threading;
using System.Threading.Tasks;

namespace Comandante.Benchmarks
{
    public class PingQueryHandler : IQueryHandler<PingQuery, Pong>
    {
        public Task<Pong> Handle(PingQuery query, CancellationToken cancellationToken)
        {
            return Task.FromResult(new Pong());
        }
    }
}