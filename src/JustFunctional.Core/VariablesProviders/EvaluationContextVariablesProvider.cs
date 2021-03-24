using System.Linq;
using System.Collections.Generic;
namespace JustFunctional.Core
{
    public class EvaluationContextVariablesProvider : IVariablesProvider
    {
        private readonly IEvaluationContext _context;

        public EvaluationContextVariablesProvider(IEvaluationContext context)
        {
            this._context = context;
        }
        public IEnumerable<Variable> GetRegisteredVariables()
        {
            return _context.GetAllVariablesNames().Select(varName=> new Variable(varName));
        }
    }
}