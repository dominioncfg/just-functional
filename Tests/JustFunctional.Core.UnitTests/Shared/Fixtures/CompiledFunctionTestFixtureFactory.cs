﻿namespace JustFunctional.Core.UnitTests
{
    public class CompiledFunctionTestFixtureFactory
    {
        private static readonly IFunctionFactory _factory = FunctionFactoryBuilder.ConfigureFactory(options => {
            options
                .WithEvaluationContextVariablesProvider()
                .WithDefaultsTokenProvider()
                .WithCompiledEvaluator()
                .WithSystemProvidedCulture();                
        });
        public static Function GivenFunction(string expression) => _factory.Create(expression);
    }
}