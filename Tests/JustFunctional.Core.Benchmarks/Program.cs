using BenchmarkDotNet.Running;
namespace JustFunctional.Core.Benchmarks
{
    public class Program
    {
        public static void Main() => _ = BenchmarkRunner.Run<DiferentCompilersBenchmarks>();
    }
}
