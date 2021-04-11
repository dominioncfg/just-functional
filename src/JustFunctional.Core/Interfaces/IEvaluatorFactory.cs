namespace JustFunctional.Core
{
    public interface IEvaluatorFactory
    {
        IEvaluator CreateFor(string expression, EvaluatorOptions evaluatorOptions);
    }
}