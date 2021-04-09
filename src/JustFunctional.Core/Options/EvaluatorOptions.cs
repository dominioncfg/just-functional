namespace JustFunctional.Core
{
    public class EvaluatorOptions
    {
        public ITokensProvider TokensProvider { get; }
        public ICultureProvider CultureProvider { get; }

        public EvaluatorOptions(ITokensProvider tokensProvider, ICultureProvider cultureProvider)
        {
            TokensProvider = tokensProvider;
            CultureProvider = cultureProvider;
        }
    }
}