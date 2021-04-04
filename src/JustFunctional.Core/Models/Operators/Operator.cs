using System.Collections.Generic;
namespace JustFunctional.Core
{
    public abstract class Operator : IToken
    {
        public string Name { get; }
        public string RawToken { get; }
        public int Precedence { get; }
        public OperatorType Type { get; }
        public Associativity Associativity { get; }
        public string GetValue() => RawToken;

        public Operator(string name, string rawToken, int precedence, OperatorType type, Associativity associativity)
        {
            Name = name;
            RawToken = rawToken;
            Precedence = precedence;
            Type = type;
            Associativity = associativity;
        }

        public abstract decimal Calculate(List<Operand> operands, IEvaluationContext context);
        protected decimal ResolveIfVariableOperand(Operand operand, IEvaluationContext context) => operand is Variable ? context.ResolveVariable(operand.RawToken) : operand.Value;
    }
}