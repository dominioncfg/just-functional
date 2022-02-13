using System.Collections.Generic;
using System.Linq;
namespace JustFunctional.Core
{
    public class PredefinedVariablesProvider : IVariablesProvider
    {
        private readonly List<Variable> _variables;

        public PredefinedVariablesProvider(params string[] variables)
        {
            _variables = variables.Select(x => new Variable(x)).ToList();
        }
        public IEnumerable<Variable> GetRegisteredVariables() => _variables;
    }
}