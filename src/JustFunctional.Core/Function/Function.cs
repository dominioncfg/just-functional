using System.Threading.Tasks;
namespace JustFunctional.Core
{
    public class Function : IEvaluable
    {
        private readonly IEvaluator _evaluator;
        private readonly IVariablesProvider? _variablesProvider;
        public string Expression { get; }

        public Function(string expression) : this(expression, ConfigurationConstants.Options.CompiledDefault) { }

        public Function(string expression, FunctionOptions options)
        {
            var useTokensProvider = options.TokensProvider;
            Expression = expression;
            _variablesProvider = options.VariablesProvider;
            _evaluator = options.EvaluatorProvider.CreateFor(expression, useTokensProvider);
        }

        public override string ToString() => Expression;
        public decimal Evaluate(IEvaluationContext context)
        {
            var variablesProvider = _variablesProvider ?? new EvaluationContextVariablesProvider(context);
            return _evaluator.Evaluate(context, variablesProvider);
        }

        public async Task<decimal> EvaluateAsync(IEvaluationContext context)
        {
            var variablesProvider = _variablesProvider ?? new EvaluationContextVariablesProvider(context);
            return await _evaluator.EvaluateAsync(context, variablesProvider).ConfigureAwait(false);
        }
    }
}