using System.Collections.Generic;
namespace JustFunctional.Core
{
    public interface ITokensProvider
    {
        IEnumerable<Operator> GetAvailableOperators();
        IEnumerable<Constant> GetAvailableConstants();
    }
}