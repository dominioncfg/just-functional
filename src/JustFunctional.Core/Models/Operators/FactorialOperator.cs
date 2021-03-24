using System.Collections.Generic;
namespace JustFunctional.Core
{
    public class FactorialOperator : Operator
    {
        public FactorialOperator() : base(
                                            name: "Factorial",
                                            rawToken: ConfigurationConstants.AsString.Factorial,
                                            precedence: ConfigurationConstants.Precedences.GeneralPurposeFunctions,
                                            type: OperatorType.Unary,
                                            associativity: Associativity.Right
                                         )
        { }
        public override decimal Calculate(List<Operand> operands, IEvaluationContext context)
        {
            return Maths.Factorial((uint)ResolveIfVariableOperand(operands[0], context));
        }
    }
}