using System.Threading;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Microsoft.Extensions.DependencyInjection;

namespace Comandante.Benchmarks
{
    [MemoryDiagnoser]
    [SimpleJob(RuntimeMoniker.Net50)]
    [SimpleJob(RuntimeMoniker.Net60)]
    public class ComandanteBenchmark
    {
        private IQueryDispatcher _queryDispatcher;
        
        [GlobalSetup]
        public void Setup()
        {
            var conf = new ServiceCollection();
            conf.AddComandate(typeof(PingQuery).Assembly);
            var sp = conf.BuildServiceProvider();

            _queryDispatcher = sp.GetRequiredService<IQueryDispatcher>();
        }

        [Benchmark]
        public async Task<Pong> HandleQuery()
        {
            var result = await _queryDispatcher.Dispatch(new PingQuery(), CancellationToken.None);
            return result;
        }
    }
}