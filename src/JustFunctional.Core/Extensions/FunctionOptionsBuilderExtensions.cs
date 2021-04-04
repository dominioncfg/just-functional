using System;
namespace JustFunctional.Core
{
    public static class FunctionOptionsBuilderExtensions
    {
        public static FunctionOptionsBuilder WithEvaluationContextVariablesProvider(this FunctionOptionsBuilder builder) => builder.WithVariablesProvider(default);

        public static FunctionOptionsBuilder WithPredefinedVariables(this FunctionOptionsBuilder builder, params string[] variables) => builder.WithVariablesProvider(new PredefinedVariablesProvider(variables));

        public static FunctionOptionsBuilder WithJustInTimeEvalutator(this FunctionOptionsBuilder builder) => builder.WithEvaluator(new JustInTimeEvaluatorFactory());

        public static FunctionOptionsBuilder WithCompiledEvaluator(this FunctionOptionsBuilder builder) => builder.WithEvaluator(new CompiledExpressionEvaluatorFactory());

        public static FunctionOptionsBuilder WithDefaultsTokenProvider(this FunctionOptionsBuilder builder) => builder.WithTokenProvider(new DefaultTokenProviderBuilder());

        public static FunctionOptionsBuilder WithCustomTokenProvider(this FunctionOptionsBuilder builder, Action<CustomizableTokenProviderBuilder> setupAction)
        {
            var tokenBuilder = new CustomizableTokenProviderBuilder();
            setupAction?.Invoke(tokenBuilder);
            return builder.WithTokenProvider(tokenBuilder);
        }
    }
}