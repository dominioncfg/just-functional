namespace JustFunctional.Core
{
    public class FunctionOptions
    {
        public IEvaluatorFactory EvaluatorProvider { get; }
        public IVariablesProvider? VariablesProvider { get; }
        public ITokensProvider TokensProvider { get; }

        public FunctionOptions(IEvaluatorFactory evaluatorProvider, ITokensProvider tokensProvider, IVariablesProvider? variablesProvider)
        {
            EvaluatorProvider = evaluatorProvider;
            VariablesProvider = variablesProvider;
            TokensProvider = tokensProvider;
        }
    }
}