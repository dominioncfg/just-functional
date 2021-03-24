namespace JustFunctional.Core
{
    public static class TokenExtensions
    {
        internal static bool IsNotEndOfFile(this IToken token) => !(token is Operand o && o.RawToken == ConfigurationConstants.AsString.EndOfFile);
    }
}