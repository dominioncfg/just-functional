using FluentAssertions;
using System.Collections.Generic;
using Xunit;
namespace JustFunctional.Core.UnitTests.Features.Functions
{
    public abstract class MultiplyOperatorTests : FunctionsTestBase
    {
        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERTATORS_MULTIPLY)]
        public void CanEvaluateSingleMultiply()
        {
            string func = "X*3";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 2 }));

            result.Should().Be(6);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERTATORS_MULTIPLY)]
        public void CanEvaluateSeveralMultiply()
        {
            string func = "X*3*2";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 4 }));

            result.Should().Be(24);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERTATORS_MULTIPLY)]
        public void MultiplyComesFirstWhenIsBetweenBracketsEvenWhenAnotherOperationWithHigherPrecedenceIsBefore()
        {
            string func = "2^(X*4)";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 1 }));

            result.Should().Be(16);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERTATORS_MULTIPLY)]
        public void BracketsAreEvaluatedBeforeMultiply()
        {
            string func = "3*(X-2)";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 7 }));

            result.Should().Be(15);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERTATORS_MULTIPLY)]
        public void MultiplyIsEvaluatedBeforeRest()
        {
            string func = "6-X*3";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 2 }));

            result.Should().Be(0);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERTATORS_MULTIPLY)]
        public void MultiplyIsEvaluatedBeforeSum()
        {
            string func = "6+X*3";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 2 }));

            result.Should().Be(12);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERTATORS_MULTIPLY)]
        public void DivideIsEvaluatedBefore()
        {
            string func = "9/X*3";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 9 }));

            result.Should().Be(3);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERTATORS_MULTIPLY)]
        public void ExponentialIsEvaluatedBefore()
        {
            string func = "6*X^3";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 2 }));

            result.Should().Be(48);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERTATORS_MULTIPLY)]
        public void SquareRootIsEvaluatedBefore()
        {
            string func = "6*sqrt X";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 9 }));

            result.Should().Be(18);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERTATORS_MULTIPLY)]
        public void CubeRootIsEvaluatedBefore()
        {
            string func = "6*cbr X";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 27 }));

            result.Should().Be(18);
        }
    }

    public class CompiledMultiplyOperatorTests : MultiplyOperatorTests
    {
        public override Function GivenFunction(string func) => CompiledFunctionTestFixtureFactory.GivenFunction(func);
    }

    public class JustInTimeMultiplyOperatorTests : MultiplyOperatorTests
    {
        public override Function GivenFunction(string func) => JustInTimeFunctionTestFixtureFactory.GivenFunction(func);
    }
}