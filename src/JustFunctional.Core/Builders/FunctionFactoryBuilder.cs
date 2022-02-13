using System;
namespace JustFunctional.Core
{
    public static class FunctionFactoryBuilder
    {
        public static IFunctionFactory ConfigureFactory(Action<FunctionOptionsBuilder>? setupAction = null)
        {
            var options = new FunctionOptionsBuilder();
            setupAction?.Invoke(options);
            var fOptions = options.Build();
            return new DelegateFunctionFactory(fOptions);
        }
    }
}