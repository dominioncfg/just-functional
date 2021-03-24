using System;
using System.Collections.Generic;
namespace JustFunctional.Core
{
    public class ModuleOperator : Operator
    {
        public ModuleOperator() : base(
                                        name: "Module",
                                        rawToken: ConfigurationConstants.AsString.Module,
                                        precedence: ConfigurationConstants.Precedences.GeneralPurposeFunctions,
                                        type: OperatorType.Unary,
                                        associativity: Associativity.Right
                                       )
        { }

        public override decimal Calculate(List<Operand> operands, IEvaluationContext context)
        {
            return (decimal)Math.Abs((double)ResolveIfVariableOperand(operands[0], context));
        }
    }
}