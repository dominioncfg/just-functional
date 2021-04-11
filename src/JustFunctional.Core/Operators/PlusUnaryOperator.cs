using System.Collections.Generic;
namespace JustFunctional.Core
{

    public class PlusUnaryOperator : Operator
    {
        public PlusUnaryOperator() : base(
                                            name: "PlusUnary",
                                            rawToken: ConfigurationConstants.AsString.PlusUnary,
                                            precedence: ConfigurationConstants.Precedences.UnaryMultiply,
                                            type: OperatorType.Unary,
                                            associativity: Associativity.Right
                                        )
        { }

        public override decimal Calculate(List<Operand> operands, IEvaluationContext context) => ResolveIfVariableOperand(operands[0], context) * 1;
    }
}