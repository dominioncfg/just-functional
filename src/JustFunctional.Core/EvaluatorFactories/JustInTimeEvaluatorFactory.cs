namespace JustFunctional.Core
{
    public class JustInTimeEvaluatorFactory : IEvaluatorFactory
    {
        public IEvaluator CreateFor(string expression, EvaluatorOptions evaluatorOptions) => new PostfixExpressionInMemoryEvaluator(expression, evaluatorOptions.TokensProvider, evaluatorOptions.CultureProvider);
    }
}
