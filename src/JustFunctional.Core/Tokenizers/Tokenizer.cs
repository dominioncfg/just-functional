using System.Globalization;
using System.Linq;
namespace JustFunctional.Core
{
    internal class Tokenizer : ITokenizer
    {
        private readonly OperatorReadOnlyCollection _registeredOperators;
        private readonly OperandReadOnlyCollection _registeredConstants;
        private readonly OperandReadOnlyCollection _registeredVariables;
        private readonly ITokenReader<char> _expressionEnumerator;

        private char CurrentChar => _expressionEnumerator.Current;
        private IToken? _lastToken;

        public Tokenizer(string expression, ITokensProvider tokensProvider, IVariablesProvider variablesProvider)
        {
            var allOperators = ConfigurationConstants.Operators.IntrinsicOperators.Concat(tokensProvider.GetAvailableOperators());
            _expressionEnumerator = new CharTokenReaderEnumerator(expression);
            _registeredOperators = new OperatorReadOnlyCollection(allOperators);
            _registeredConstants = new OperandReadOnlyCollection(tokensProvider.GetAvailableConstants());
            _registeredVariables = new OperandReadOnlyCollection(variablesProvider.GetRegisteredVariables());
        }

        private bool MoveNext() => _expressionEnumerator.MoveNext();
        public void Reset() => _expressionEnumerator.Reset();
        private Operator TryGetRegisteredOperator(string token) => _registeredOperators.GetOperatorOrDefault(token);
        private Operand TryGetRegisteredOperand(string token) => _registeredConstants.GetOperandOrDefault(token);
        private Operand TryGetRegisteredVariable(string token) => _registeredVariables.GetOperandOrDefault(token);

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

            bool isMinusUnary = IsMinusUnary(CurrentChar, _lastToken);
            if (isMinusUnary)
            {
                MoveNext();
            }

            if (CurrentChar.IsADigit())
            {
                var operand = GetNextNumberAndMoveIterator(_expressionEnumerator, isMinusUnary);
                _lastToken = operand;
                return operand;
            }

            int currentLookupLength = 1;
            string token = string.Empty;
            do
            {
                token += CurrentChar.ToString();
                var nextToken = TryGetRegisteredOperator(token) ?? TryGetRegisteredOperand(token) ?? (IToken)TryGetRegisteredVariable(token);

                if (nextToken is null) continue;

                var thereIsOperatorAfterMinusUnary = nextToken is Operator && isMinusUnary;
                if (thereIsOperatorAfterMinusUnary)
                {
                    throw new MissingOperandException($"Expected operand after '{ConfigurationConstants.AsString.MinusUnary}' found '{nextToken.GetValue()}'.");
                }

                var thereIsAVaribaleAfterMinusUnary = nextToken is Variable && isMinusUnary;
                if (thereIsAVaribaleAfterMinusUnary)
                {
                    throw new NotSupportedException($"A variable after  '{ConfigurationConstants.AsString.MinusUnary}' is not supported in this version.");
                }

                _lastToken = nextToken;
                return nextToken;
            }
            while (MoveNext() && currentLookupLength < ConfigurationConstants.MaxLengthForOperatorsAndConstants);

            throw new SyntaxErrorInExpressionException($"Syntax error after '{_lastToken?.GetValue()}', at position {_expressionEnumerator.CurrentIndex + 1}. Expected operator/operand.");
        }

        private static bool IsMinusUnary(char currentChar, IToken? prevToken)
        {
            var isMinusSign = currentChar == ConfigurationConstants.AsChar.MinusUnary;
            if (!isMinusSign) return false;

            var isAtTheBegining = prevToken is null;
            if (isAtTheBegining) return true;

            var @operator = prevToken as Operator;
            var isAnyOperatorExceptClosingBracket = @operator is not null &&
                                                        @operator.RawToken != ConfigurationConstants.AsString.ClosingBracket;
            return isAnyOperatorExceptClosingBracket;
        }
        private static Operand GetNextNumberAndMoveIterator(ITokenReader<char> iterator, bool negate = false)
        {
            string token = iterator.Current.ToString();
            while (iterator.MoveNext())
            {
                if (iterator.Current.IsADigit() || iterator.Current == ConfigurationConstants.DecimalPlacesSeparator)
                {
                    token += iterator.Current.ToString();
                }
                else
                {
                    iterator.MovePrevious();
                    break;
                }
            }
            
            var ci = new CultureInfo("en-US");
            var tokenValue = decimal.Parse(token, NumberStyles.Float, ci);
            if (negate) tokenValue *= -1;

            var operand = new Operand(tokenValue);
            return operand;
        }
    }
}