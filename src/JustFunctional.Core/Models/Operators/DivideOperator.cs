using System.Collections.Generic;
namespace JustFunctional.Core
{
    public class DivideOperator : Operator
    {
        public DivideOperator() : base(
                                        name: "Divide",
                                        rawToken: ConfigurationConstants.AsString.Divide,
                                        precedence: ConfigurationConstants.Precedences.MultiplyDivide,
                                        type: OperatorType.Binary,
                                        associativity: Associativity.Left
                                      )
        { }

        public override decimal Calculate(List<Operand> operands, IEvaluationContext context)
        {
            return ResolveIfVariableOperand(operands[0], context) / ResolveIfVariableOperand(operands[1], context);
        }
    }
}