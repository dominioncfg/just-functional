using System;
using System.Collections.Generic;
namespace JustFunctional.Core
{
    public class SquareRootOperator : Operator
    {
        public SquareRootOperator() : base(
                                            name: "SquareRoot",
                                            rawToken: ConfigurationConstants.AsString.SquareRoot,
                                            precedence: ConfigurationConstants.Precedences.ExponentiationAndRoots,
                                            type: OperatorType.Unary,
                                            associativity: Associativity.Right
                                          )
        { }

        public override decimal Calculate(List<Operand> operands, IEvaluationContext context)
        {
            return (decimal)Math.Sqrt((double)ResolveIfVariableOperand(operands[0], context));
        }
    }
}