using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
namespace JustFunctional.Core
{
    internal class OperatorReadOnlyCollection
    {
        private readonly ReadOnlyDictionary<string, Operator> _operators;
        public OperatorReadOnlyCollection(IEnumerable<Operator> operators)
        {
            _operators = new ReadOnlyDictionary<string, Operator>(operators.ToDictionary(o => o.RawToken, o => o));
        }
        public Operator? GetOperatorOrDefault(string token)
        {
            _operators.TryGetValue(token, out var o);
            return o;
        }
    }
}