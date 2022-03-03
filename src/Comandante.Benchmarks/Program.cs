using BenchmarkDotNet.Running;

namespace Comandante.Benchmarks
{
    class Program
    {
        static void Main(string[] args) => 
            BenchmarkRunner.Run<ComandanteBenchmark>(null, args);
    }
}