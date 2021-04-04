namespace JustFunctional.Core
{
    public class CompiledExpressionEvaluatorFactory : IEvaluatorFactory
    {
        public IEvaluator CreateFor(string expression, ITokensProvider tokensProvider) => new PostfixCompiledExpressionEvaluator(expression, tokensProvider);
    }
}