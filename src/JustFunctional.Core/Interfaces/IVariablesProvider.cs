using System.Collections.Generic;
namespace JustFunctional.Core
{
    public interface IVariablesProvider
    {
        IEnumerable<Variable> GetRegisteredVariables();
    }
}