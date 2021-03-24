using System;
using System.Collections.Generic;
namespace JustFunctional.Core
{
    public class CubeRootOperator : Operator
    {
        public CubeRootOperator() : base(
                                            name: "CubeRoot",
                                            rawToken: ConfigurationConstants.AsString.CubeRoot,
                                            precedence: ConfigurationConstants.Precedences.ExponentiationAndRoots,
                                            type: OperatorType.Unary,
                                            associativity: Associativity.Right
                                        )
        { }

        public override decimal Calculate(List<Operand> operands, IEvaluationContext context)
        {
            double exp = (double)1 / (double)3;
            return (decimal)Math.Pow((double)ResolveIfVariableOperand(operands[0], context), exp);
        }
    }
}