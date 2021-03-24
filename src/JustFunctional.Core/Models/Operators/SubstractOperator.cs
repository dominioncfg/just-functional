using System.Collections.Generic;
namespace JustFunctional.Core
{
    public class SubstractOperator : Operator
    {
        public SubstractOperator() : base(
                                            name: "Substract",
                                            rawToken: ConfigurationConstants.AsString.Substract,
                                            precedence: ConfigurationConstants.Precedences.AddSubstract,
                                            type: OperatorType.Binary,
                                            associativity: Associativity.Left
                                         )
        { }

        public override decimal Calculate(List<Operand> operands, IEvaluationContext context)
        {
            return ResolveIfVariableOperand(operands[0], context) - ResolveIfVariableOperand(operands[1], context);
        }
    }
}