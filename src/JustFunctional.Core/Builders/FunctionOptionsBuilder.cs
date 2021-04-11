namespace JustFunctional.Core
{
    public class FunctionOptionsBuilder
    {
        private IVariablesProvider? _variablesProvider;
        private IEvaluatorFactory? _evaluatorFactory;
        private ITokenProviderBuilder? tokenProviderBuilder;
        private ICultureProvider? _cultureProvider;
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

        public FunctionOptionsBuilder WithCultureProvider(ICultureProvider cultureProvider)
        {
            _cultureProvider = cultureProvider;
            return this;
        }

        public FunctionOptions Build()
        {
            var evaluator = _evaluatorFactory ?? ConfigurationConstants.Options.CompiledDefault.EvaluatorProvider;
            var tokensProviderFactory = tokenProviderBuilder?.Build() ?? ConfigurationConstants.Options.CompiledDefault.TokensProvider;
            var cultureProvider = _cultureProvider ?? ConfigurationConstants.Options.CompiledDefault.CultureProvider;

            return new FunctionOptions(evaluator, tokensProviderFactory, _variablesProvider, cultureProvider);
        }
    }
}