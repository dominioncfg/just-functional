using System.Collections.Generic;
namespace JustFunctional.Core
{
    public static class CharsExtensions
    {
        private static readonly HashSet<char> _digits = new() { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        public static bool IsADigit(this char c) => _digits.Contains(c);

        public static bool IsSpace(this char c) => c.Equals(ConfigurationConstants.AsChar.Space);
    }
}