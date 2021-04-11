using System;
using System.Collections.Generic;
using System.Linq;
namespace JustFunctional.Core
{
    public class DelegateOperator : Operator
    {
        private readonly Func<List<decimal>, decimal> _operation;

        public DelegateOperator(string name, string rawToken, int precedence,
                                OperatorType type, Associativity associativity, Func<List<decimal>, decimal> operation)
                                : base(name, rawToken, precedence, type, associativity)

        {
            this._operation = operation;
        }

        public override decimal Calculate(List<Operand> operands, IEvaluationContext context)
        {
            var resolvedOperands = operands.Select(o => ResolveIfVariableOperand(o, context)).ToList();
            return _operation(resolvedOperands);
        }
        
    }
}