using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace JustFunctional.Core.UnitTests.Features.Functions
{
    public class FactoryFunctionTryCreateTests
    {

        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_FACTORY_TRY_CREATE)]
        public void Can_Create_Returns_Valid_Function_Simple_Function()
        {
            string func = "X+2";
            var sut = CreateWithSettings(option => option.WithVariablesProvider(new PredefinedVariablesProvider("X")));

            var result = sut.TryCreate(func);
            result.Should().NotBeNull();

            result.Success.Should().BeTrue();
            result.Errors.Should().BeEmpty();
            result.Function.Should().NotBeNull();

            decimal evaluateResult = result.Function.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 3 }));
            evaluateResult.Should().Be(5);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_FACTORY_TRY_CREATE)]
        public void Can_Create_Returns_Valid_Function_When_Function_Has_Parenthesis()
        {
            string func = "(X+2)*4";
            var sut = CreateWithSettings(option => option.WithVariablesProvider(new PredefinedVariablesProvider("X")));

            var result = sut.TryCreate(func);
            result.Should().NotBeNull();

            result.Success.Should().BeTrue();
            result.Errors.Should().BeEmpty();
            result.Function.Should().NotBeNull();

            decimal evaluateResult = result.Function.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 3 }));
            evaluateResult.Should().Be(20);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_FACTORY_TRY_CREATE)]
        public void Can_Create_Returns_Invalid_Function_When_Missing_Operand()
        {
            string func = "X+2+";
            var sut = CreateWithSettings(option => option.WithVariablesProvider(new PredefinedVariablesProvider("X")));

            var result = sut.TryCreate(func);
            result.Should().NotBeNull();

            result.Success.Should().BeFalse();
            result.Errors.Should().NotBeEmpty();
            result.Function.Should().BeNull();
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_FACTORY_TRY_CREATE)]
        public void Can_Create_Returns_Invalid_Function_When_Missing_Operator()
        {
            string func = "5X";
            var sut = CreateWithSettings(option => option.WithVariablesProvider(new PredefinedVariablesProvider("X")));

            var result = sut.TryCreate(func);
            result.Should().NotBeNull();

            result.Success.Should().BeFalse();
            result.Errors.Should().NotBeEmpty();
            result.Function.Should().BeNull();
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_FACTORY_TRY_CREATE)]
        public void Can_Create_Returns_Invalid_Function_When_Missing_Closing_Parenthesis()
        {
            string func = "(X+2";
            var sut = CreateWithSettings(option => option.WithVariablesProvider(new PredefinedVariablesProvider("X")));

            var result = sut.TryCreate(func);
            result.Should().NotBeNull();

            result.Success.Should().BeFalse();
            result.Errors.Should().NotBeEmpty();
            result.Function.Should().BeNull();
        }


        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_FACTORY_TRY_CREATE)]
        public void Can_Create_Returns_Invalid_Function_When_Missing_Opening_Parenthesis()
        {
            string func = "X+2)";
            var sut = CreateWithSettings(option => option.WithVariablesProvider(new PredefinedVariablesProvider("X")));

            var result = sut.TryCreate(func);
            result.Should().NotBeNull();

            result.Success.Should().BeFalse();
            result.Errors.Should().NotBeEmpty();
            result.Function.Should().BeNull();
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_FACTORY_TRY_CREATE)]
        public void Can_Create_Returns_Invalid_Function_When_Unknown_Variable()
        {
            string func = "Y+2";
            var sut = CreateWithSettings(option => option.WithVariablesProvider(new PredefinedVariablesProvider("X")));

            var result = sut.TryCreate(func);
            result.Should().NotBeNull();

            result.Success.Should().BeFalse();
            result.Errors.Should().NotBeEmpty();
            result.Function.Should().BeNull();
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_FACTORY_TRY_CREATE)]
        public void EvaluationContextVariablesProvider_Can_Create_Returns_Valid_Function_Simple_Function()
        {
            string func = "X+2";
            var sut = CreateWithSettings(option => option.WithEvaluationContextVariablesProvider());

            var result = sut.TryCreate(func, new PredefinedVariablesProvider("X"));
            result.Should().NotBeNull();

            result.Success.Should().BeTrue();
            result.Errors.Should().BeEmpty();
            result.Function.Should().NotBeNull();

            decimal evaluateResult = result.Function.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 3 }));
            evaluateResult.Should().Be(5);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_FACTORY_TRY_CREATE)]
        public void EvaluationContextVariablesProvider_Can_Create_Returns_Invalid_Function_When_Unknown_Variable()
        {
            string func = "Y+2";
            var sut = CreateWithSettings(option => option.WithEvaluationContextVariablesProvider());

            var result = sut.TryCreate(func, new PredefinedVariablesProvider("X"));
            result.Should().NotBeNull();

            result.Success.Should().BeFalse();
            result.Errors.Should().NotBeEmpty();
            result.Function.Should().BeNull();
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_FACTORY_TRY_CREATE)]
        public void EvaluationContextVariablesProvider_Throws_Exception_When_Variabled_Are_Not_Supplied()
        {
            string func = "Y+2";
            var sut = CreateWithSettings(option => option.WithEvaluationContextVariablesProvider());

            Func<TryCreateFunctionResult> act = () => sut.TryCreate(func);

            act.Should().Throw<InvalidOperationException>();
        }

        private static IFunctionFactory CreateWithSettings(Action<FunctionOptionsBuilder> setupAction)
        {
            return FunctionFactoryBuilder.ConfigureFactory(setupAction);
        }
    }
}
