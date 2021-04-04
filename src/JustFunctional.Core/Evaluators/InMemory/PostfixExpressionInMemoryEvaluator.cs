using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace JustFunctional.Core
{
    public class PostfixExpressionInMemoryEvaluator : IEvaluator
    {
        private readonly string _expression;
        private readonly ITokensProvider _tokensProvider;

        public PostfixExpressionInMemoryEvaluator(string expression, ITokensProvider tokensProvider)
        {
            _expression = expression;
            _tokensProvider = tokensProvider;
        }
        public async Task<decimal> EvaluateAsync(IEvaluationContext context, IVariablesProvider variablesProvider) => await Task.Run(() => Evaluate(context, variablesProvider)).ConfigureAwait(false);
        public decimal Evaluate(IEvaluationContext context, IVariablesProvider variablesProvider)
        {
            var tokenizer = new Tokenizer(_expression, _tokensProvider, variablesProvider);
            var operands = new Stack<Operand>();
            var operators = new Stack<Operator>();

            IToken? token;
            while ((token = tokenizer.GetNextToken()).IsNotEndOfFile())
            {
                switch (token)
                {
                    case Operand operand:
                        operands.Push(operand);
                        break;

                    case Operator @operator when @operator.IsOpeningBracket():
                        operators.Push(@operator);
                        break;

                    case Operator @operator when @operator.IsClosingBracket():
                        EvaluateAllOperatorsUntilNextOpeningBracket(operators, operands, context);
                        break;

                    case Operator @operator:
                        if (operators.Any())
                        {
                            EvaluateAnyOperatorUntilOneWithSamePrecedenceIsFound(operators, operands, @operator, context);
                        }
                        operators.Push(@operator);
                        break;
                    default:
                        throw new SyntaxErrorInExpressionException("The tokenizer has returned an invalid token");
                }
            }

            EvaluateAnyPendingOperator(operators, operands, context);

            decimal value = DequeeAndResolveLastOperand(operands, context);
            return value;
        }
        private static void EvaluateAllOperatorsUntilNextOpeningBracket(Stack<Operator> operators, Stack<Operand> operands, IEvaluationContext context)
        {
            while (operators.Any() && operators.Peek().RawToken != ConfigurationConstants.AsString.OpeningBracket)
                ResolverNextOperatorInQueue(operators, operands, context);

            if (!operators.Any())
                throw new MissingOperatorException($"There is a '{ConfigurationConstants.AsString.ClosingBracket}' without corresponding '{ConfigurationConstants.AsString.OpeningBracket}'");

            operators.Pop();
        }
        private static void EvaluateAnyOperatorUntilOneWithSamePrecedenceIsFound(Stack<Operator> operators, Stack<Operand> operands, Operator currentOperator, IEvaluationContext context)
        {
            while (operators.TryPeek(out var prevOperator))
            {
                bool prevOperatorHasHigherPrecedence = prevOperator.Precedence > currentOperator.Precedence;
                bool prevOperatorHasEqualPrecedenceButLeftAssociativity = prevOperator.Precedence == currentOperator.Precedence && prevOperator.Associativity != Associativity.Right;
                bool needsToBeResolved = prevOperatorHasHigherPrecedence || prevOperatorHasEqualPrecedenceButLeftAssociativity;

                if (!needsToBeResolved) break;

                ResolverNextOperatorInQueue(operators, operands, context);
            }
        }
        private static void EvaluateAnyPendingOperator(Stack<Operator> operators, Stack<Operand> operands, IEvaluationContext context)
        {
            while (operators.Any())
                ResolverNextOperatorInQueue(operators, operands, context);
        }
        private static void ResolverNextOperatorInQueue(Stack<Operator> operators, Stack<Operand> operands, IEvaluationContext context)
        {
            var resolveOperator = operators.Pop();

            var operandsNeededToResolveOperator = DequeAndGetOperandsForOperator(operands, resolveOperator);

            decimal result = resolveOperator.Calculate(operandsNeededToResolveOperator, context);
            Operand o = new(result);
            operands.Push(o);
        }
        private static List<Operand> DequeAndGetOperandsForOperator(Stack<Operand> operands, Operator resolveOperator)
        {
            var operandsNeededToResolveOperator = new List<Operand>();

            int neededOperandsCount = (int)resolveOperator.Type;
            while (operands.TryPeek(out var operand) && operandsNeededToResolveOperator.Count < neededOperandsCount)
            {
                operands.Pop();
                operandsNeededToResolveOperator.Insert(0, operand);
            }

            if (operandsNeededToResolveOperator.Count < neededOperandsCount)
            {
                string tokenOperands = string.Join(',', operandsNeededToResolveOperator.Select(x => x.RawToken));
                string errorMessage = $"Could not resolve operator '{resolveOperator.RawToken}' because he needs {neededOperandsCount} but found only {operandsNeededToResolveOperator.Count}.";
                errorMessage += $"Operands found were: {tokenOperands}";
                throw new MissingOperandException();
            }
            return operandsNeededToResolveOperator;
        }
        private static decimal DequeeAndResolveLastOperand(Stack<Operand> operands, IEvaluationContext context)
        {
            if (operands.Count > 1)
            {
                string tokenOperands = string.Join(',', operands.Select(x => x.RawToken));
                throw new MissingOperatorException($"Missing operator to resolve this operands: {tokenOperands}.");
            }

            var lastOperand = operands.Pop();
            decimal value = lastOperand is Variable ? context.ResolveVariable(lastOperand.RawToken) : lastOperand.Value;
            return value;
        }
    }
}