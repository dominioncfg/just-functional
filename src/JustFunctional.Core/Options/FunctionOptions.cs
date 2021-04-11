namespace JustFunctional.Core
{
    public class FunctionOptions
    {
        public IEvaluatorFactory EvaluatorProvider { get; }
        public IVariablesProvider? VariablesProvider { get; }
        public ITokensProvider TokensProvider { get; }
        public ICultureProvider CultureProvider { get; }

        public FunctionOptions(IEvaluatorFactory evaluatorProvider, ITokensProvider tokensProvider,
                               IVariablesProvider? variablesProvider, ICultureProvider cultureProvider)
        {
            EvaluatorProvider = evaluatorProvider;
            VariablesProvider = variablesProvider;
            TokensProvider = tokensProvider;
            CultureProvider = cultureProvider;
        }
    }
}