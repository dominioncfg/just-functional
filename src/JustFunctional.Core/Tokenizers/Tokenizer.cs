using System;
using System.Globalization;
using System.Linq;
namespace JustFunctional.Core
{
    public class Tokenizer : ITokenizer
    {
        private readonly OperatorReadOnlyCollection _registeredOperators;
        private readonly OperandReadOnlyCollection _registeredConstants;
        private readonly OperandReadOnlyCollection _registeredVariables;
        private readonly ITokenReader<char> _expressionEnumerator;
        private readonly CultureInfo _culture;

        private char CurrentChar => _expressionEnumerator.Current;
        private IToken? _lastToken;

        public Tokenizer(string expression, TokenizerOptions options)
        {
            _culture = options.CultureProvider.GetCulture();
            var tokensProvider = options.TokensProvider;
            var variablesProvider = options.VariablesProvider;

            var allOperators = ConfigurationConstants.Operators.IntrinsicOperators.Concat(tokensProvider.GetAvailableOperators());
            _expressionEnumerator = new CharTokenReaderEnumerator(expression);
            _registeredOperators = new OperatorReadOnlyCollection(allOperators);
            _registeredConstants = new OperandReadOnlyCollection(tokensProvider.GetAvailableConstants());
            _registeredVariables = new OperandReadOnlyCollection(variablesProvider.GetRegisteredVariables());
        }

        private bool MoveNext() => _expressionEnumerator.MoveNext();

        public void Reset() => _expressionEnumerator.Reset();

        private IToken? GetRegisterdOperandOrDefault(string token)
        {
            return _registeredConstants.GetOperandOrDefault(token) ?? (_registeredVariables.GetOperandOrDefault(token) as IToken);
        }

        private IToken? GetRegisteredOperatorOrDefault(string token) => _registeredOperators.GetOperatorOrDefault(token);

        public IToken GetNextToken()
        {
            bool eof = true;
            while (MoveNext())
            {
                if (CurrentChar.IsSpace()) continue;
                eof = false;
                break;
            }
            if (eof)
            {
                Reset();
                return ConfigurationConstants.Tokens.EndOfFile;
            }

            bool isUnaryOperator = IsUnaryOperator(CurrentChar, _lastToken);
            if (isUnaryOperator)
            {
                return GetUnaryOperator(CurrentChar);
            }

            if (CurrentChar.IsDigit())
            {
                var operand = GetNextNumberAndMoveIterator(_expressionEnumerator, _culture);
                _lastToken = operand;
                return operand;
            }

            string token = string.Empty;
            bool isIdentifier = true;
            do
            {
                IToken? nextToken = null;
                token += CurrentChar.ToString();
                bool keepLookingForOperators = token.Length <= ConfigurationConstants.MaxLengthForOperators;
                isIdentifier = IsStillIdentifier(token, CurrentChar, isIdentifier);

                if (isIdentifier)
                {
                    nextToken = GetRegisterdOperandOrDefault(token);
                }

                if (nextToken is null && keepLookingForOperators)
                {
                    nextToken = GetRegisteredOperatorOrDefault(token);
                }

                if (!isIdentifier && !keepLookingForOperators)
                {
                    break;
                }

                if (nextToken is null) continue;

                _lastToken = nextToken;
                return nextToken;
            }
            while (MoveNext());

            throw new SyntaxErrorInExpressionException($"Syntax error after '{_lastToken?.GetValue()}', at position {_expressionEnumerator.CurrentIndex + 1}. Expected operator/operand.");
        }

        private static bool IsUnaryOperator(char currentChar, IToken? prevToken)
        {
            bool isMinusSign = currentChar == ConfigurationConstants.AsChar.MinusUnary || currentChar == ConfigurationConstants.AsChar.PlusUnary;
            if (!isMinusSign) return false;

            bool isAtTheBegining = prevToken is null;
            if (isAtTheBegining) return true;

            bool isAnyOperatorExceptClosingBracket = prevToken is Operator @operator && !@operator.IsClosingBracket();
            return isAnyOperatorExceptClosingBracket;
        }

        private static Operator GetUnaryOperator(char currentChar)
        {
            return currentChar switch {
                ConfigurationConstants.AsChar.MinusUnary => ConfigurationConstants.Operators.MinusUnary,
                ConfigurationConstants.AsChar.PlusUnary => ConfigurationConstants.Operators.PlusUnary,
                _ => throw new ArgumentException($"'{currentChar}' is not allowed."),
            };
        }

        private static Operand GetNextNumberAndMoveIterator(ITokenReader<char> iterator, CultureInfo culture)
        {
            string token = iterator.Current.ToString();

            char decimalSeparator = culture.NumberFormat.NumberDecimalSeparator[0];
            while (iterator.MoveNext())
            {
                char current = iterator.Current;
                if (current.IsDigit() || current == decimalSeparator)
                {
                    token += current.ToString();
                }
                else
                {
                    iterator.MovePrevious();
                    break;
                }
            }

            decimal tokenValue = decimal.Parse(token, NumberStyles.Float, culture);

            var operand = new Operand(tokenValue);
            return operand;
        }

        private static bool IsStillIdentifier(string token, char currentChar, bool isIdentifier)
        {
            if (token.Length == 1)
            {
                return currentChar.IsIdentifierStartCharacter();
            }
            return isIdentifier && currentChar.IsIdentifierPartCharacter();
        }
    }
}