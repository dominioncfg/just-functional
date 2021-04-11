using System.Collections.Generic;
namespace JustFunctional.Core
{
    public class MinusUnaryOperator : Operator
    {
        public MinusUnaryOperator() : base(
                                            name: "MinusUnary",
                                            rawToken: ConfigurationConstants.AsString.MinusUnary,
                                            precedence: ConfigurationConstants.Precedences.UnaryMultiply,
                                            type: OperatorType.Unary,
                                            associativity: Associativity.Right
                                        )
        { }
        public override decimal Calculate(List<Operand> operands, IEvaluationContext context) => ResolveIfVariableOperand(operands[0], context) * -1;
    }
}