using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
namespace JustFunctional.Core
{
    public class CustomizableTokensProvider : ITokensProvider
    {
        private readonly ReadOnlyCollection<Operator> _operators;
        private readonly ReadOnlyCollection<Constant> _constants;

        public CustomizableTokensProvider(IEnumerable<Operator> operators, IEnumerable<Constant> constants)
        {
            if (!operators.Any())
                throw new ArgumentException("You need to supply at least one operator.", nameof(operators));

            CheckForRepeatedOperatorsAndThrowExceptionIfAny(operators);
            CheckForRepeatedConstantsAndThrowExceptionIfAny(constants);

            _operators = operators.ToList().AsReadOnly();
            _constants = constants.ToList().AsReadOnly();
        }

        private void CheckForRepeatedOperatorsAndThrowExceptionIfAny(IEnumerable<Operator> operators)
        {
            var checkedOperators = new HashSet<string>();
            foreach (var op in operators)
            {
                bool added = checkedOperators.Add(op.RawToken);
                if (!added)
                    throw new ArgumentException($"There are operators with the same token {op.RawToken}", nameof(operators));
            }
        }

        private void CheckForRepeatedConstantsAndThrowExceptionIfAny(IEnumerable<Constant> constants)
        {
            var checkedContants = new HashSet<string>();
            foreach (var c in constants)
            {
                bool added = checkedContants.Add(c.RawToken);
                if (!added)
                    throw new ArgumentException($"There are contants with the same token {c.RawToken}", nameof(constants));
            }
        }

        public IEnumerable<Operator> GetAvailableOperators() => _operators;
        public IEnumerable<Constant> GetAvailableConstants() => _constants;
    }
}