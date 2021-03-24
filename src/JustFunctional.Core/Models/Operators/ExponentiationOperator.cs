using System;
using System.Collections.Generic;
namespace JustFunctional.Core
{
    public class ExponentiationOperator : Operator
    {
        public ExponentiationOperator() : base(
                                                name: "Exponentiation",
                                                rawToken: ConfigurationConstants.AsString.Exponentiation,
                                                precedence: ConfigurationConstants.Precedences.ExponentiationAndRoots,
                                                type: OperatorType.Binary,
                                                associativity: Associativity.Right
                                             )
        { }

        public override decimal Calculate(List<Operand> operands, IEvaluationContext context)
        {
            return (decimal)Math.Pow((double)ResolveIfVariableOperand(operands[0], context), (double)ResolveIfVariableOperand(operands[1], context));
        }
    }
}