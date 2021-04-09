using System.Collections.Generic;
namespace JustFunctional.Core
{
    public class EvaluationContext : IEvaluationContext
    {
        private readonly Dictionary<string, decimal> _variables = new();
        public EvaluationContext() : this(new Dictionary<string, decimal>()) { }
        public EvaluationContext(Dictionary<string, decimal> variables)
        {
            _variables = variables ?? new Dictionary<string, decimal>();
        }
        public decimal ResolveVariable(string identifier)
        {
            string errorMessage = $"The variable '{identifier} does not exist in the current context'";
            return _variables.ContainsKey(identifier) ? _variables[identifier] : throw new VariableUndefinedException(errorMessage);
        }
        public void RegisterVariable(string identifier, decimal value) => _variables[identifier] = value;
        public IEnumerable<string> GetAllVariablesNames() => _variables.Keys;
    }
}