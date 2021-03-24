namespace JustFunctional.Core.UnitTests
{
    public class CompiledFunctionTestFixtureFactory
    {
        private static readonly IFunctionFactory _factory = FunctionFactoryBuilder.ConfigureFactory(options =>
        {
            options
                .WithEvaluationContextVariablesProvider()
                .WithDefaultsTokenProvider()
                .WithCompiledEvaluator();
        });
        public static Function GivenFunction(string expression)
        {
            return _factory.Create(expression);
        }
    }
}