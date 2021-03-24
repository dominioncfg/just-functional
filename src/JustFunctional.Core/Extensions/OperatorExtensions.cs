namespace JustFunctional.Core
{
    public static class OperatorExtensions
    {
        internal static bool IsOpeningBracket(this Operator @operator) => @operator.RawToken == ConfigurationConstants.AsString.OpeningBracket;
        internal static bool IsClosingBracket(this Operator @operator) => @operator.RawToken == ConfigurationConstants.AsString.ClosingBracket;
    }
}