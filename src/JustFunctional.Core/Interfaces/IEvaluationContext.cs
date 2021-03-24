using System.Collections.Generic;
namespace JustFunctional.Core
{
    public interface IEvaluationContext
    {
        public decimal ResolveVariable(string identifier);
        public IEnumerable<string> GetAllVariablesNames();
    }
}