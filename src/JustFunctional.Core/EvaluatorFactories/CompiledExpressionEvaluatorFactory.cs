namespace JustFunctional.Core
{
    public class CompiledExpressionEvaluatorFactory : IEvaluatorFactory
    {
        public IEvaluator CreateFor(string expression, ITokensProvider tokensProvider)
        {
            return new PostfixCompiledExpressionEvaluator(expression, tokensProvider);
        }
    }
}