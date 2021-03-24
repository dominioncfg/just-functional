using BenchmarkDotNet.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JustFunctional.Core.Benchmarks
{
    [MemoryDiagnoser]
    public class DiferentCompilersBenchmarks
    {
        [Params(1, 10, 100, 1000, 10000, 100000)]
        public int NumberOfTimesToEvaluate;

        private const string expression = "X*2+5-1+X/3";
        private Function compiledFunction;
        private Function justInTimeInterpretedFunction;

        [GlobalSetup]
        public void GlobalSetup()
        {
            var compiledFactory = FunctionFactoryBuilder.ConfigureFactory(options =>
                options
                .WithCompiledEvaluator()
                .WithDefaultsTokenProvider()
                .WithEvaluationContextVariablesProvider()
            );
            var interpreterFactory = FunctionFactoryBuilder.ConfigureFactory(options =>
                options
                .WithJustInTimeEvalutator()
                .WithDefaultsTokenProvider()
                .WithEvaluationContextVariablesProvider()
            );
            compiledFunction = compiledFactory.Create(expression);
            justInTimeInterpretedFunction = compiledFactory.Create(expression);
        }


        [Benchmark(Baseline = true)]
        public decimal InterpretedFunction() => EvaluateInRange(justInTimeInterpretedFunction);

        [Benchmark]
        public decimal CompiledFunction() => EvaluateInRange(compiledFunction);


        [Benchmark]
        public decimal AsyncParallelInterpretedFunction()
        {
            EvaluateFunctionTPLAsync(justInTimeInterpretedFunction);
            return 0;
        }


        [Benchmark]
        public decimal AsyncParallelCompiledFunction()
        {
            EvaluateFunctionTPLAsync(compiledFunction);
            return 0;
        }

        [Benchmark]
        public decimal AsyncTasksInterpretedFunction()
        {
            EvaluateFunctionTPLAsync(justInTimeInterpretedFunction);
            return 0;
        }

        [Benchmark]
        public async Task<decimal> AsyncTasksCompiledFunction()
        {
            await EvaluateFunctionAsync(compiledFunction);
            return 0;
        }

        private void EvaluateFunctionTPLAsync(Function function)
        {
            ParallelOptions parallelOptions = new ParallelOptions();
            parallelOptions.MaxDegreeOfParallelism = 6;
            Parallel.ForEach(Enumerable.Range(1, NumberOfTimesToEvaluate), parallelOptions, (i) =>
            {
                var temp = function.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = i }));
            });
        }

        private async Task EvaluateFunctionAsync(Function function)
        {
            var tasks = Enumerable.Range(1, NumberOfTimesToEvaluate).Select(x => function.EvaluateAsync(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = x })));
            await Task.WhenAll(tasks);
        }

        private decimal EvaluateInRange(Function func)
        {
            for (var i = 1; i <= NumberOfTimesToEvaluate; i++)
            {
                var temp = func.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = i }));
            }
            return 0;
        }
    }
}
