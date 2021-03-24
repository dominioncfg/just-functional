using System.Collections.Generic;
namespace JustFunctional.Core
{
    public class CustomizableTokenProviderBuilder : ITokenProviderBuilder
    {
        private readonly List<Operator> _operators = new();
        private readonly List<Constant> _constants = new();

        public CustomizableTokenProviderBuilder()
        {
            var baseProvider = new DefaultTokensProvider();
            _operators.AddRange(baseProvider.GetAvailableOperators());
            _constants.AddRange(baseProvider.GetAvailableConstants());
        }

        public CustomizableTokenProviderBuilder ClearOperators()
        {
            _operators.Clear();
            return this;
        }

        public CustomizableTokenProviderBuilder ClearConstants()
        {
            _constants.Clear();
            return this;
        }

        public CustomizableTokenProviderBuilder WithOperator(Operator @operator)
        {
            _operators.Add(@operator);
            return this;
        }

        public CustomizableTokenProviderBuilder WithOperators(params Operator[] operators)
        {
            _operators.AddRange(operators);
            return this;
        }

        public CustomizableTokenProviderBuilder WithConstant(Constant constant)
        {
            _constants.Add(constant);
            return this;
        }

        public CustomizableTokenProviderBuilder WithConstant(params Constant[] constants)
        {
            _constants.AddRange(constants);
            return this;
        }
        public ITokensProvider Build() => new CustomizableTokensProvider(_operators, _constants);
    }
}