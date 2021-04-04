namespace JustFunctional.Core
{
    public class JustInTimeEvaluatorFactory : IEvaluatorFactory
    {
        public IEvaluator CreateFor(string expression, ITokensProvider tokensProvider) => new PostfixExpressionInMemoryEvaluator(expression, tokensProvider);
    }
}
