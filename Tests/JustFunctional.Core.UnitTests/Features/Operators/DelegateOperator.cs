using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
namespace JustFunctional.Core.UnitTests.Features.Functions
{
    public class DelegateOperatorTests
    {
        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERTATORS_DIVIDE)]
        public void CanUseDelegateOperator()
        {
            var @operator = new DelegateOperator("OP", "Sum", 100, OperatorType.Binary, Associativity.Left, (List<decimal> operands) => operands.Sum());
            string func = "X Sum 3";
            var sut = CreateWithSettings(func, options => options.WithCustomTokenProvider(tokens => tokens.WithOperator(@operator)));
            var result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 6 }));
            result.Should().Be(9);
        }

        private Function CreateWithSettings(string expression, Action<FunctionOptionsBuilder> setupAction)
        {
            var factory = FunctionFactoryBuilder.ConfigureFactory(setupAction);
            return factory.Create(expression);
        }
    }


}