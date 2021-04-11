namespace JustFunctional.Core
{
    public static class CharsExtensions
    {
        public static bool IsDigit(this char c) => char.IsDigit(c);

        public static bool IsSpace(this char c) => c.Equals(ConfigurationConstants.AsChar.Space);

        public static bool IsIdentifierStartCharacter(this char c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || c == '_' || c == '@' || char.IsLetter(c);
        }

        public static bool IsIdentifierPartCharacter(this char c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || c == '_' || (c >= '0' && c <= '9') || char.IsLetter(c);
        }
    }
}