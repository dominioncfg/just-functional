using System;
using System.Collections.Generic;
namespace JustFunctional.Core
{
    public class CommonLogarithmOperator : Operator
    {
        public CommonLogarithmOperator() : base(
                                                 name: "CommonLogarithm",
                                                 rawToken: ConfigurationConstants.AsString.CommonLogarithm,
                                                 precedence: ConfigurationConstants.Precedences.GeneralPurposeFunctions,
                                                 type: OperatorType.Unary,
                                                 associativity: Associativity.Right
                                                )
        { }

        public override decimal Calculate(List<Operand> operands, IEvaluationContext context) => (decimal)Math.Log10((double)ResolveIfVariableOperand(operands[0], context));
    }
}