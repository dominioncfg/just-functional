using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Xunit;

namespace JustFunctional.Core.UnitTests.Features.Functions
{
    public class FactoryFunctionTests
    {
        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_FACTORY)]
        public void CanEvaluateWithDefaultConfiguration()
        {
            string func = "X+2";
            var sut = CreateWithSettings(func, option => { });

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 3 }));

            result.Should().Be(5);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_FACTORY)]
        public void CanEvaluateWithPredefinedVariables()
        {
            string func = "X+2";
            var sut = CreateWithSettings(func, option => option.WithPredefinedVariables("X"));

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 3 }));

            result.Should().Be(5);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_FACTORY)]
        public void ThrowsWhenUsingPredefinedVariablesButThereAreMoreVariablesEvenIfTheVariableIsPassed()
        {
            string func = "X+Y+2";
            var sut = CreateWithSettings(func, option => option.WithPredefinedVariables("X"));

            Action action = () => sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 3, ["Y"] = 4 }));

            action.Should().Throw<SyntaxErrorInExpressionException>();
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_FACTORY)]
        public void CanEvaluateCustomDefinedOperator()
        {
            string func = "X+2";
            var sut = CreateWithSettings(func, option =>
                      option.WithCustomTokenProvider(provider =>
                        provider
                            .ClearOperators()
                            .ClearConstants()
                            .WithOperator(new AddOperator())
                        )
                     );

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 3 }));

            result.Should().Be(5);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_FACTORY)]
        public void CanEvaluateMultipleCustomOperatorsWhenTheyAreDefined()
        {
            string func = "X+2*X";
            var sut = CreateWithSettings(func, option =>
                      option.WithCustomTokenProvider(provider =>
                        provider
                            .ClearOperators()
                            .ClearConstants()
                            .WithOperators(new AddOperator(), new MultiplyOperator())
                        )
                     );

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 3 }));

            result.Should().Be(9);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_FACTORY)]
        public void ThrowsExceptionWhenUndefinedOperator()
        {
            string func = "X-2";
            var sut = CreateWithSettings(func, option =>
                      option.WithCustomTokenProvider(provider =>
                        provider
                            .ClearOperators()
                            .ClearConstants()
                            .WithOperators(new AddOperator(), new MultiplyOperator())
                        )
                     );

            Action action = () => sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 3 }));

            action.Should().Throw<SyntaxErrorInExpressionException>();
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_FACTORY)]
        public void ThrowsExceptionWhenNoOperatorsAreSupplied()
        {
            string func = "X-2";
            Action action = () => CreateWithSettings(func, option =>
                     option.WithCustomTokenProvider(provider =>
                       provider
                           .ClearOperators()
                           .ClearConstants()
                       )
                     );

            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_FACTORY)]
        public void ThrowsExceptionWhenOperatorsAreRepeated()
        {
            string func = "X-2";
            Action action = () => CreateWithSettings(func, option =>
                     option.WithCustomTokenProvider(provider =>
                       provider
                           .ClearOperators()
                           .ClearConstants()
                           .WithOperators(new AddOperator(), new AddOperator())
                       )
                     );

            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_FACTORY)]
        public void CanEvaluateCustomConstants()
        {
            string func = "X+c";
            var sut = CreateWithSettings(func, option =>
                      option.WithCustomTokenProvider(provider =>
                        provider
                            .ClearConstants()
                            .WithConstant(new Constant("c", 1))
                        )
                     );

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 3 }));

            result.Should().Be(4);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_FACTORY)]
        public void ThrowsExceptionWhenRepeatedConstant()
        {
            string func = "X+c";
            Action action = () => CreateWithSettings(func, option =>
                      option.WithCustomTokenProvider(provider =>
                        provider
                            .ClearConstants()
                            .WithConstant(new Constant("c", 1))
                            .WithConstant(new Constant("c", 2))
                        )
                     );

            action.Should().Throw<ArgumentException>();
        }

        [Theory]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_FACTORY)]
        [InlineData("es-ES", ",")]
        [InlineData("en-US", ".")]
        public async Task CanEvaluateWithDifferentsCultures(string culture, string separator)
        {
            string func = $"(X*2)+2{separator}1";
            decimal result = 0M;
            await Task.Run(() => {
                CultureInfo.CurrentCulture = new CultureInfo(culture);
                var sut = CreateWithSettings(func, options => options.WithSystemProvidedCulture());

                result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 8 }));
            });

            result.Should().Be(18.1M);
        }

        private static Function CreateWithSettings(string expression, Action<FunctionOptionsBuilder> setupAction)
        {
            var factory = FunctionFactoryBuilder.ConfigureFactory(setupAction);
            return factory.Create(expression);
        }
    }
}
