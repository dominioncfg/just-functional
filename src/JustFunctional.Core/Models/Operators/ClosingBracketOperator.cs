using System;
using System.Collections.Generic;
namespace JustFunctional.Core
{
    internal class ClosingBracketOperator : Operator
    {
        public ClosingBracketOperator() : base(
                                                 name: "ClosingBracket",
                                                 rawToken: ConfigurationConstants.AsString.ClosingBracket,
                                                 precedence: ConfigurationConstants.Precedences.Brackets,
                                                 type: OperatorType.Unary,
                                                 associativity: Associativity.Left
                                               )
        { }

        public override decimal Calculate(List<Operand> operands, IEvaluationContext context) =>
            throw new InvalidOperationException("')' is an special operator.");
    }
}