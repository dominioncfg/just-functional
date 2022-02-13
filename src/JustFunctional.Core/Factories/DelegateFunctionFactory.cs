using System;

namespace JustFunctional.Core
{
    public class DelegateFunctionFactory : IFunctionFactory
    {
        private readonly FunctionOptions _options;

        public DelegateFunctionFactory(FunctionOptions options)
        {
            _options = options;
        }
        public Function Create(string expression) => new(expression, _options);

        public TryCreateFunctionResult TryCreate(string expression)
        {
            const string txt = "If you are using EvaluationContextVariablesProvider you need to provide the variables";
            var varProvider = _options.VariablesProvider ?? throw new InvalidOperationException(txt);
            return TryCreate(expression, varProvider);
        }

        public TryCreateFunctionResult TryCreate(string expression, IVariablesProvider _variablesProvider)
        {
            try
            {
                var compiler = new PostfixCompiledExpressionEvaluator(expression, _options.TokensProvider, _options.CultureProvider);
                compiler.Compile(_variablesProvider);
                return TryCreateFunctionResult.Successful(new Function(expression,_options));
            }
            catch (Exception ex)
            {
                return TryCreateFunctionResult.Failure(new System.Collections.Generic.List<string>() { ex.Message });
            }
        }
    }
}