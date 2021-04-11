using System;
namespace JustFunctional.Core
{
    public class DelegateFunctionFactory : IFunctionFactory
    {
        private readonly Func<string, Function> _setupAction;

        public DelegateFunctionFactory(Func<string, Function> setupAction)
        {
            _setupAction = setupAction;
        }
        public Function Create(string expression) => _setupAction(expression);
    }
}