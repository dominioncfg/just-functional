namespace JustFunctional.Core
{
    public class CompiledExpressionEvaluatorFactory : IEvaluatorFactory
    {
        public IEvaluator CreateFor(string expression, EvaluatorOptions evaluatorOptions) => new PostfixCompiledExpressionEvaluator(expression, evaluatorOptions.TokensProvider,evaluatorOptions.CultureProvider);
    }
}