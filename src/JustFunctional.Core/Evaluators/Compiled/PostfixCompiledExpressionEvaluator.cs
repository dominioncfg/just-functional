using System.Collections.Generic;
using System.Threading.Tasks;
namespace JustFunctional.Core
{
    public class PostfixCompiledExpressionEvaluator : ICompiledEvaluator
    {
        private List<IToken> _compiledExpression;
        private readonly PostfixExpressionEvaluator _evaluator;
        private readonly string _expression;
        private readonly ITokensProvider _tokensProvider;
        private readonly ICultureProvider _cultureProvider;
        private readonly object _compileLock = new();

        private bool IsCompiled { get; set; }

        public PostfixCompiledExpressionEvaluator(string expression, ITokensProvider tokensProvider,ICultureProvider cultureProvider)
        {
            _expression = expression;
            _tokensProvider = tokensProvider;
            _cultureProvider = cultureProvider;
            _evaluator = new PostfixExpressionEvaluator();
            _compiledExpression = new List<IToken>();
        }

        public void Compile(IVariablesProvider variablesProvider)
        {
            if (IsCompiled) return;
            lock (_compileLock)
            {
                if (IsCompiled) return;
                var compiler = new PostfixExpressionCompiler(_expression, _tokensProvider, variablesProvider, _cultureProvider);
                _compiledExpression = compiler.CompileExpression();
                IsCompiled = true;
            }
        }

        public async Task<decimal> EvaluateAsync(IEvaluationContext context, IVariablesProvider variablesProvider) => await Task.Run(() => Evaluate(context, variablesProvider)).ConfigureAwait(false);
        public decimal Evaluate(IEvaluationContext context, IVariablesProvider variablesProvider)
        {
            if (!IsCompiled) Compile(variablesProvider);
            return _evaluator.Evaluate(_compiledExpression, context);
        }
    }
}