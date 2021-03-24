using System.Threading.Tasks;
namespace JustFunctional.Core
{
    public interface IEvaluable
    {
        decimal Evaluate(IEvaluationContext context);
        Task<decimal> EvaluateAsync(IEvaluationContext context);
    }
}