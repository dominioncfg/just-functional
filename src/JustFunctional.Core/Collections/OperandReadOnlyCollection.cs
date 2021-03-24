using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
namespace JustFunctional.Core
{
    internal class OperandReadOnlyCollection
    {
        private readonly ReadOnlyDictionary<string, Operand> _operands;
        public OperandReadOnlyCollection(IEnumerable<Operand> operands)
        {
            _operands = new ReadOnlyDictionary<string, Operand>(operands.ToDictionary(o => o.RawToken, o => o));
        }
        public Operand GetOperandOrDefault(string token)
        {
            _operands.TryGetValue(token, out Operand o);
            return o;
        }
    }
}