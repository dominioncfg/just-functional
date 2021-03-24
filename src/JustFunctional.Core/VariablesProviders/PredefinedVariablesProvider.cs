using System.Linq;
using System.Collections.Generic;
namespace JustFunctional.Core
{
    public class PredefinedVariablesProvider : IVariablesProvider
    {
        private readonly List<Variable> _variables;

        public PredefinedVariablesProvider(IEnumerable<string> variables)
        {
            this._variables = variables.Select(x => new Variable(x)).ToList();
        }
        public IEnumerable<Variable> GetRegisteredVariables() => _variables;
    }
}