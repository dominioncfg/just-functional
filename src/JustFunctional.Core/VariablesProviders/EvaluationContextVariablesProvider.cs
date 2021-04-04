using System.Collections.Generic;
using System.Linq;
namespace JustFunctional.Core
{
    public class EvaluationContextVariablesProvider : IVariablesProvider
    {
        private readonly IEvaluationContext _context;

        public EvaluationContextVariablesProvider(IEvaluationContext context)
        {
            _context = context;
        }
        public IEnumerable<Variable> GetRegisteredVariables() => _context.GetAllVariablesNames().Select(varName => new Variable(varName));
    }
}