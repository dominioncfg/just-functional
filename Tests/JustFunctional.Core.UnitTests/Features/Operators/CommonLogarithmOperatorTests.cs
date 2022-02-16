using FluentAssertions;
using System.Collections.Generic;
using Xunit;
namespace JustFunctional.Core.UnitTests.Features.Functions
{
    public abstract class CommonLogarithmOperatorTests : FunctionsTestBase
    {
        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_COMMON_LOGARITHM)]
        public void CanEvaluateSingleLogarithm()
        {
            string func = "logX";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 10 }));

            result.Should().Be(1);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_COMMON_LOGARITHM)]
        public void CanEvaluateMultipleLogarithm()
        {
            string func = "loglogX";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 10 }));

            result.Should().Be(0);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_COMMON_LOGARITHM)]
        public void LogarithmHasPrecedenceOverSumAndSubstract()
        {
            string func = "4+1-logX+6";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 100 }));

            result.Should().Be(9);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_COMMON_LOGARITHM)]
        public void LogarithmHasPrecedenceOverMultiplicationAndDivide()
        {
            string func = "4*3/logX+2";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 100 }));

            result.Should().Be(8);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_COMMON_LOGARITHM)]
        public void BracketsAreEvaluatedBeforeLogarithm()
        {
            string func = "log(X+40)";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 60 }));

            result.Should().Be(2);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_COMMON_LOGARITHM)]
        public void LogarithmHasPrecedenceOverExponentiation()
        {
            string func = "3^logX";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 100 }));

            result.Should().Be(9);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_COMMON_LOGARITHM)]
        public void LogarithmHasTheSamePrecedenceThanModule()
        {
            string func = "logmodX";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = -100 }));

            result.Should().Be(2);
        }
    }
    public class CompiledFunctionsCommonLogarithmOperatorTests : CommonLogarithmOperatorTests
    {
        public override Function GivenFunction(string func) => CompiledFunctionTestFixtureFactory.GivenFunction(func);
    }
    public class JustInTimeFunctionsCommonLogarithmOperatorTests : CommonLogarithmOperatorTests
    {
        public override Function GivenFunction(string func) => JustInTimeFunctionTestFixtureFactory.GivenFunction(func);
    }
}