using System;
using System.Collections.Generic;
namespace JustFunctional.Core
{
    public class SineOperator : Operator
    {
        public SineOperator() : base(
                                        name: "Sine",
                                        rawToken: ConfigurationConstants.AsString.Sine,
                                        precedence: ConfigurationConstants.Precedences.GeneralPurposeFunctions,
                                        type: OperatorType.Unary,
                                        associativity: Associativity.Right
                                    )
        { }

        public override decimal Calculate(List<Operand> operands, IEvaluationContext context) => (decimal)Math.Sin((double)ResolveIfVariableOperand(operands[0], context));
    }
}