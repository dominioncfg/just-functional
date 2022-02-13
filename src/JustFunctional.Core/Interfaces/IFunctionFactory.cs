namespace JustFunctional.Core
{
    public interface IFunctionFactory
    {
        Function Create(string expression);
        public TryCreateFunctionResult TryCreate(string expression);
        public TryCreateFunctionResult TryCreate(string expression, IVariablesProvider _variablesProvider);
    }
}