using System.Collections.Generic;
namespace JustFunctional.Core
{
    public class MultiplyOperator : Operator
    {
        public MultiplyOperator() : base(
                                            name: "Multiply",
                                            rawToken: ConfigurationConstants.AsString.Multiply,
                                            precedence: ConfigurationConstants.Precedences.MultiplyDivide,
                                            type: OperatorType.Binary,
                                            associativity: Associativity.Left
                                        )
        { }

        public override decimal Calculate(List<Operand> operands, IEvaluationContext context) => ResolveIfVariableOperand(operands[0], context) * ResolveIfVariableOperand(operands[1], context);
    }
}