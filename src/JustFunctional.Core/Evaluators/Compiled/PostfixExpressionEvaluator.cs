using System.Collections.Generic;
using System.Linq;
namespace JustFunctional.Core
{
    internal class PostfixExpressionEvaluator
    {
        public decimal Evaluate(List<IToken> compiledExpression, IEvaluationContext context)
        {
            Stack<Operand> operands = new();

            foreach (var currentToken in compiledExpression)
            {
                switch (currentToken)
                {
                    case Operand operand:
                        operands.Push(operand);
                        break;
                    case Operator @operator:
                        DequeeOperandsAndEvaluateOperator(operands, @operator, context);
                        break;
                    default:
                        throw new SyntaxErrorInExpressionException("The compiler has returned an invalid token");
                }             
            }           
            decimal value = DequeeAndEvaluateLastOperand(operands, context);
            return value;
        }
        private static void DequeeOperandsAndEvaluateOperator(Stack<Operand> operands, Operator @operator, IEvaluationContext context)
        {
            List<Operand> operandsForOperator = DequeAndGetOperandsForOperator(operands, @operator);
            decimal result = @operator.Calculate(operandsForOperator, context);
            operands.Push(new Operand(result));
        }
        private static List<Operand> DequeAndGetOperandsForOperator(Stack<Operand> operands, Operator @operator)
        {
            var operandsNeededToForOperator = new List<Operand>();

            var neededOperandsCount = (int)@operator.Type;
            while (operands.TryPeek(out Operand operand) && operandsNeededToForOperator.Count < neededOperandsCount)
            {
                operands.Pop();
                operandsNeededToForOperator.Insert(0, operand);
            }

            if (operandsNeededToForOperator.Count < neededOperandsCount)
            {
                var tokenOperands = string.Join(',', operandsNeededToForOperator.Select(x => x.RawToken));
                string errorMessage = $"Could not resolve operator '{@operator.RawToken}' because he needs {neededOperandsCount} but found only {operandsNeededToForOperator.Count}.";
                errorMessage += $"Operands found were: {tokenOperands}";
                throw new MissingOperandException();
            }
            return operandsNeededToForOperator;
        }
        private static decimal DequeeAndEvaluateLastOperand(Stack<Operand> operands, IEvaluationContext context)
        {
            if (operands.Count > 1)
            {
                var tokenOperands = string.Join(',', operands.Select(x => x.RawToken));
                throw new MissingOperatorException($"Missing operator to resolve this operands: {tokenOperands}.");
            }

            var lastOperand = operands.Pop();
            var value = lastOperand is Variable ? context.ResolveVariable(lastOperand.RawToken) : lastOperand.Value;
            return value;
        }
    }
}