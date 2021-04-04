namespace JustFunctional.Core.UnitTests
{
    public class JustInTimeFunctionTestFixtureFactory
    {
        private static readonly IFunctionFactory _factory = FunctionFactoryBuilder.ConfigureFactory(options => {
            options
                .WithDefaultsTokenProvider()
                .WithEvaluationContextVariablesProvider()
                .WithJustInTimeEvalutator();
        });
        public static Function GivenFunction(string expression) => _factory.Create(expression);
    }
}