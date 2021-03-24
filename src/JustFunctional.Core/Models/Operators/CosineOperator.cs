using System;
using System.Collections.Generic;
namespace JustFunctional.Core
{
    public class CosineOperator : Operator
    {
        public CosineOperator() : base(
                                        name: "Cosine",
                                        rawToken: ConfigurationConstants.AsString.Cosine,
                                        precedence: ConfigurationConstants.Precedences.GeneralPurposeFunctions,
                                        type: OperatorType.Unary,
                                        associativity: Associativity.Right
                                      )
        { }

        public override decimal Calculate(List<Operand> operands, IEvaluationContext context)
        {
            return (decimal)Math.Cos((double)ResolveIfVariableOperand(operands[0], context));
        }
    }
}