using System.Collections.Generic;
using System.Linq;
namespace JustFunctional.Core
{
    internal class PostfixExpressionCompiler
    {
        private readonly ITokenizer _tokenizer;
        public PostfixExpressionCompiler(string expression, ITokensProvider tokensProvider, IVariablesProvider variablesProvider)
        {
            _tokenizer = new Tokenizer(expression,tokensProvider, variablesProvider);
        }
        public List<IToken> CompileExpression()
        {
            Stack<Operator> remainingOperators = new();
            List<IToken> output = new();

            _tokenizer.Reset();
            IToken? token;
            while ((token = _tokenizer.GetNextToken()).IsNotEndOfFile())
            {
                switch (token)
                {
                    case Operand operand:
                        output.Add(operand);
                        break;

                    case Operator @operator when @operator.IsOpeningBracket():
                        remainingOperators.Push(@operator);
                        break;

                    case Operator @operator when @operator.IsClosingBracket():
                        DequeAllOperatorsUntilNextOpeningBracketAndPutThemIntoOutput(remainingOperators, output);
                        break;

                    case Operator @operator:
                        if (remainingOperators.Any())
                            DequeAnyOperatorUntilOneWithSamePrecedenceIsFoundAndPutThemIntoOutput(remainingOperators, @operator, output);

                        remainingOperators.Push(@operator);
                        break;
                    default:
                        throw new SyntaxErrorInExpressionException("The tokenizer has returned an invalid token");
                }
            }

            DequeAnyLeftOperatorAndPutItIntoOutput(remainingOperators, output);

            return output;
        }
        private static void DequeAllOperatorsUntilNextOpeningBracketAndPutThemIntoOutput(Stack<Operator> remainingOperators, List<IToken> output)
        {
            while (remainingOperators.Peek().RawToken != ConfigurationConstants.AsString.OpeningBracket && remainingOperators.Any())
            {
                output.Add(remainingOperators.Pop());
            }

            if (!remainingOperators.Any())
                throw new MissingOperatorException($"There is a '{ConfigurationConstants.AsString.ClosingBracket}' without corresponding '{ConfigurationConstants.AsString.OpeningBracket}'");

            remainingOperators.Pop();
        }
        private static void DequeAnyOperatorUntilOneWithSamePrecedenceIsFoundAndPutThemIntoOutput(Stack<Operator> remainingOperators, Operator currentOperator, List<IToken> output)
        {
            while (remainingOperators.TryPeek(out Operator prevOperator))
            {
                var prevOperatorHasHigherPrecedence = prevOperator.Precedence > currentOperator.Precedence;
                var prevOperatorHasEqualPrecedenceButLeftAssociativity = prevOperator.Precedence == currentOperator.Precedence && prevOperator.Associativity != Associativity.Right;
                var needsToBeResolved = prevOperatorHasHigherPrecedence || prevOperatorHasEqualPrecedenceButLeftAssociativity;

                if (!needsToBeResolved) break;

                output.Add(remainingOperators.Pop());
            }
        }
        private static void DequeAnyLeftOperatorAndPutItIntoOutput(Stack<Operator> remainingOperators, List<IToken> output)
        {
            while (remainingOperators.TryPeek(out _))
                output.Add(remainingOperators.Pop());
        }
    }
}