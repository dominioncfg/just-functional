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
        public override decimal Calculate(List<Operand> operands, IEvaluationContext context) => Factorial((uint)ResolveIfVariableOperand(operands[0], context));


        private static uint Factorial(uint Ceiling)
        {
            uint fact = 1;
            for (uint i = 1; i <= Ceiling; i++)
            {
                fact *= i;
            }
            return fact;
        }
    }
}