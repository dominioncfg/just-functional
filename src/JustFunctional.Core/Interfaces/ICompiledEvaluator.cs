namespace JustFunctional.Core
{
    public interface ICompiledEvaluator: IEvaluator
    {
        void Compile(IVariablesProvider variablesProvider);
    }
}