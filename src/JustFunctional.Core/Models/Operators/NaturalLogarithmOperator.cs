using System;
using System.Collections.Generic;
namespace JustFunctional.Core
{
    public class NaturalLogarithmOperator : Operator
    {
        public NaturalLogarithmOperator() : base(
                                                    name: "NaturalLogarithm",
                                                    rawToken: ConfigurationConstants.AsString.NaturalLogarithm,
                                                    precedence: ConfigurationConstants.Precedences.GeneralPurposeFunctions,
                                                    type: OperatorType.Unary,
                                                    associativity: Associativity.Right
                                                )
        { }

        public override decimal Calculate(List<Operand> operands, IEvaluationContext context)
        {
            return (decimal)Math.Log((double)ResolveIfVariableOperand(operands[0], context));
        }
    }
}