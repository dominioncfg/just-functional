using System.Collections.Generic;
using System.Linq;
namespace JustFunctional.Core
{
    internal class PostfixExpressionCompiler
    {
        private static readonly Operand placeHolderOperand = new(1);

        private readonly ITokenizer _tokenizer;

        public PostfixExpressionCompiler(string expression, ITokensProvider tokensProvider, IVariablesProvider variablesProvider, ICultureProvider cultureProvider)
        {
            _tokenizer = new Tokenizer(expression, new TokenizerOptions(tokensProvider, variablesProvider, cultureProvider));
        }

        public List<IToken> CompileExpression()
        {
            Stack<Operator> remainingOperators = new();
            var operands = new Stack<Operand>();

            List<IToken> output = new();

            _tokenizer.Reset();
            IToken? token;
            while ((token = _tokenizer.GetNextToken()).IsNotEndOfFile())
            {
                switch (token)
                {
                    case Operand operand:
                        operands.Push(operand);
                        output.Add(operand);
                        break;

                    case Operator @operator when @operator.IsOpeningBracket():
                        remainingOperators.Push(@operator);
                        break;

                    case Operator @operator when @operator.IsClosingBracket():
                        DequeAllOperatorsUntilNextOpeningBracketAndPutThemIntoOutput(remainingOperators, operands, output);
                        break;

                    case Operator @operator:
                        if (remainingOperators.Any())
                            DequeAnyOperatorUntilOneWithSamePrecedenceIsFoundAndPutThemIntoOutput(remainingOperators, operands, @operator, output);

                        remainingOperators.Push(@operator);
                        break;
                    default:
                        throw new SyntaxErrorInExpressionException("The tokenizer has returned an invalid token");
                }
            }

            DequeAnyLeftOperatorAndPutItIntoOutput(remainingOperators, operands, output);

            if (operands.Count > 1)
            {
                string tokenOperands = string.Join(',', operands.Select(x => x.RawToken));
                throw new MissingOperatorException($"Missing operator to resolve this operands: {tokenOperands}.");
            }

            return output;
        }

        private static void DequeAllOperatorsUntilNextOpeningBracketAndPutThemIntoOutput(Stack<Operator> remainingOperators, Stack<Operand> operands, List<IToken> output)
        {
            while (remainingOperators.Any() && !remainingOperators.Peek().IsOpeningBracket())
            {
                var nextOperator = ValidateAndGetOperatorInQueue(remainingOperators, operands);
                output.Add(nextOperator);
            }

            if (!remainingOperators.Any())
                throw new MissingOperatorException($"There is a '{ConfigurationConstants.AsString.ClosingBracket}' without corresponding '{ConfigurationConstants.AsString.OpeningBracket}'");

            remainingOperators.Pop();
        }

        private static void DequeAnyOperatorUntilOneWithSamePrecedenceIsFoundAndPutThemIntoOutput(Stack<Operator> remainingOperators, Stack<Operand> operands, Operator currentOperator, List<IToken> output)
        {
            while (remainingOperators.TryPeek(out var prevOperator))
            {
                bool prevOperatorHasHigherPrecedence = prevOperator.Precedence > currentOperator.Precedence;
                bool prevOperatorHasEqualPrecedenceButLeftAssociativity = prevOperator.Precedence == currentOperator.Precedence && prevOperator.Associativity != Associativity.Right;
                bool needsToBeResolved = prevOperatorHasHigherPrecedence || prevOperatorHasEqualPrecedenceButLeftAssociativity;

                if (!needsToBeResolved) break;

                var nextOperator = ValidateAndGetOperatorInQueue(remainingOperators, operands);
                output.Add(nextOperator);
            }
        }
        private static void DequeAnyLeftOperatorAndPutItIntoOutput(Stack<Operator> remainingOperators, Stack<Operand> operands, List<IToken> output)
        {
            while (remainingOperators.Any())
            {
                var nextOperator = ValidateAndGetOperatorInQueue(remainingOperators, operands);
                output.Add(nextOperator);
            }
        }

        private static Operator ValidateAndGetOperatorInQueue(Stack<Operator> operators, Stack<Operand> operands)
        {
            var resolveOperator = operators.Pop();

            if(resolveOperator.IsOpeningBracket())
                throw new MissingOperatorException($"There is a '{ConfigurationConstants.AsString.OpeningBracket}' without corresponding '{ConfigurationConstants.AsString.ClosingBracket}'");
           
            _ = DequeAndGetOperandsForOperator(operands, resolveOperator);

            operands.Push(placeHolderOperand);

            return resolveOperator;
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
                throw new MissingOperandException(errorMessage);
            }

            return operandsNeededToResolveOperator;
        }       
    }
}