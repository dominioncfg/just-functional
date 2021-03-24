using System.Threading.Tasks;
namespace JustFunctional.Core
{
    public interface IEvaluator
    {
        decimal Evaluate(IEvaluationContext context, IVariablesProvider variablesProvider);
        Task<decimal> EvaluateAsync(IEvaluationContext context, IVariablesProvider variablesProvider);
    }
}