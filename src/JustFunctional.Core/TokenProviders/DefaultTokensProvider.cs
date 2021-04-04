using System;
using System.Collections.Generic;
namespace JustFunctional.Core
{
    public class DefaultTokensProvider : ITokensProvider
    {
        public IEnumerable<Operator> GetAvailableOperators() => new Operator[]
        {
            new AddOperator(),
            new SubstractOperator(),
            new MultiplyOperator(),
            new DivideOperator(),
            new ExponentiationOperator(),
            new SquareRootOperator(),
            new CubeRootOperator(),
            new NaturalLogarithmOperator(),
            new CommonLogarithmOperator(),
            new SineOperator(),
            new CosineOperator(),
            new ModuleOperator(),
            new FactorialOperator(),
        };

        public IEnumerable<Constant> GetAvailableConstants() => new[]
        {
            new Constant("e",(decimal)Math.E),
            new Constant("pi",(decimal)Math.PI),
        };
    }
}