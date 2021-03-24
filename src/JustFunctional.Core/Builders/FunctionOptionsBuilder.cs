namespace JustFunctional.Core
{
    public class FunctionOptionsBuilder
    {
        private IVariablesProvider? _variablesProvider;
        private IEvaluatorFactory? _evaluatorFactory;
        private ITokenProviderBuilder? tokenProviderBuilder;

        public FunctionOptionsBuilder WithVariablesProvider(IVariablesProvider? variablesProvider)
        {
            _variablesProvider = variablesProvider;
            return this;
        }
        public FunctionOptionsBuilder WithEvaluator(IEvaluatorFactory evaluatorFactory)
        {
            _evaluatorFactory = evaluatorFactory;
            return this;
        }

        public FunctionOptionsBuilder WithTokenProvider(ITokenProviderBuilder tokensProvider)
        {
            tokenProviderBuilder = tokensProvider;
            return this;
        }

        public FunctionOptions Build()
        {
            var evaluator = _evaluatorFactory ?? ConfigurationConstants.Options.CompiledDefault.EvaluatorProvider;
            var tokensProviderFactory = tokenProviderBuilder?.Build() ?? ConfigurationConstants.Options.CompiledDefault.TokensProvider;

            return new FunctionOptions(evaluator, tokensProviderFactory, _variablesProvider);
        }
    }
}