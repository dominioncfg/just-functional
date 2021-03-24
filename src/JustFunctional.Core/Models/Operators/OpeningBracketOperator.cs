using System;
using System.Collections.Generic;
namespace JustFunctional.Core
{
    internal class OpeningBracketOperator : Operator
    {
        public OpeningBracketOperator() : base(
                                                 name: $"OpeningBracket",
                                                 rawToken: ConfigurationConstants.AsString.OpeningBracket,
                                                 precedence: ConfigurationConstants.Precedences.Brackets,
                                                 type: OperatorType.Unary,
                                                 associativity: Associativity.Left
                                                )
        { }
        public override decimal Calculate(List<Operand> operands, IEvaluationContext context) =>
            throw new InvalidOperationException("'(' is an special operator.");
    }
}