namespace JustFunctional.Core
{
    public class JustInTimeEvaluatorFactory : IEvaluatorFactory
    {
        public IEvaluator CreateFor(string expression, ITokensProvider tokensProvider)
        {
            return new PostfixExpressionInMemoryEvaluator(expression, tokensProvider);
        }
    }
}
