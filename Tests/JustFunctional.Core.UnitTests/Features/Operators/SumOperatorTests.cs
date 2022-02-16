using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace JustFunctional.Core.UnitTests.Features.Functions
{
    public abstract class SumOperatorsTests : FunctionsTestBase
    {
        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERTATORS_SUM)]
        public void CanEvaluateSingleSum()
        {
            string func = "X+3";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 1 }));

            result.Should().Be(4);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERTATORS_SUM)]
        public void CanEvaluateMultipleSum()
        {
            string func = "X+3+2";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 1 }));

            result.Should().Be(6);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERTATORS_SUM)]
        public void SumComesFirstWhenIsBetweenBracketsEvenWhenAnotherOperationWithHigherPrecedenceIsBefore()
        {
            string func = "2*(X+3)";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 1 }));

            result.Should().Be(8);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERTATORS_SUM)]
        public void BracketsAreEvaluatedBeforeSum()
        {
            string func = "3+(X-2)";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 7 }));

            result.Should().Be(8);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERTATORS_SUM)]
        public void RestIsEvaluatedBeforeSumWhenSumIsAfter()
        {
            string func = "6-X+3";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 1 }));

            result.Should().Be(8);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERTATORS_SUM)]
        public void MultiplyIsEvaluatedBeforeSum()
        {
            string func = "6+X*3";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 3 }));

            result.Should().Be(15);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERTATORS_SUM)]
        public void DivideIsEvaluatedBeforeSum()
        {
            string func = "6+X/3";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 9 }));

            result.Should().Be(9);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERTATORS_SUM)]
        public void ExponentialIsEvaluatedBeforeSum()
        {
            string func = "6+X^3";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 2 }));

            result.Should().Be(14);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERTATORS_SUM)]
        public void SquareRootIsEvaluatedBeforeSum()
        {
            string func = "6+sqrt X";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 9 }));

            result.Should().Be(9);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERTATORS_SUM)]
        public void CubeRootIsEvaluatedBeforeSum()
        {
            string func = "6+cbr X";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 27 }));

            result.Should().Be(9);
        }
    }
    public class CompiledSumOperatorsTests : SumOperatorsTests
    {
        public override Function GivenFunction(string func) => CompiledFunctionTestFixtureFactory.GivenFunction(func);
    }
    public class JustInTimeSumOperatorsTests : SumOperatorsTests
    {
        public override Function GivenFunction(string func) => JustInTimeFunctionTestFixtureFactory.GivenFunction(func);
    }
}
