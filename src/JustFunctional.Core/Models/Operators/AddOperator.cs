using System.Collections.Generic;
namespace JustFunctional.Core
{
    public class AddOperator : Operator
    {
        public AddOperator() : base(
                                     name: "Add",
                                     rawToken: ConfigurationConstants.AsString.Add,
                                     precedence: ConfigurationConstants.Precedences.AddSubstract,
                                     type: OperatorType.Binary,
                                     associativity: Associativity.Left
                                   )
        { }

        public override decimal Calculate(List<Operand> operands, IEvaluationContext context)
        {
            return ResolveIfVariableOperand(operands[0], context) + ResolveIfVariableOperand(operands[1], context);
        }
    }
}