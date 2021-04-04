using System.Collections;
using System.Collections.ObjectModel;
namespace JustFunctional.Core
{
    internal class CharTokenReaderEnumerator : ITokenReader<char>
    {
        private readonly ReadOnlyCollection<char> _expression;
        private int _currentCharIndex;
        public int CurrentIndex => _currentCharIndex;
        public char Current => _expression[_currentCharIndex];
        object IEnumerator.Current => Current;
        public CharTokenReaderEnumerator(string expression)
        {
            _currentCharIndex = -1;
            _expression = new ReadOnlyCollection<char>(expression.ToCharArray());
        }
        public void Reset() => _currentCharIndex = -1;
        public bool MoveNext()
        {
            if (_expression.Count <= _currentCharIndex + 1) return false;
            _currentCharIndex++;
            return true;
        }
        public bool MovePrevious()
        {
            if (_expression.Count < 0) return false;
            _currentCharIndex--;
            return true;
        }
        public void Dispose() { }
    }
}