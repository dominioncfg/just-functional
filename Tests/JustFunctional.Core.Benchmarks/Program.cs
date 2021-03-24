using BenchmarkDotNet.Running;
namespace JustFunctional.Core.Benchmarks
{
    class Program
    {
        static void Main()
        {
            _ = BenchmarkRunner.Run<DiferentCompilersBenchmarks>();
        }
    }
}
