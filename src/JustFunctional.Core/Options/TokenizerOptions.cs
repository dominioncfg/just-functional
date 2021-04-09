namespace JustFunctional.Core
{
    public class TokenizerOptions
    {
        public ITokensProvider TokensProvider { get; }
        public IVariablesProvider VariablesProvider { get; }
        public ICultureProvider CultureProvider { get; }

        public TokenizerOptions(ITokensProvider tokensProvider, IVariablesProvider variablesProvider, ICultureProvider cultureProvider)
        {
            TokensProvider = tokensProvider;
            VariablesProvider = variablesProvider;
            CultureProvider = cultureProvider;
        }
    }
}