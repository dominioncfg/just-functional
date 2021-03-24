using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace JustFunctional.Core.UnitTests.Features.Functions
{
    public abstract class SubstractOperatorTests : FunctionsTestBase
    {
        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERTATORS_SUBSTRACT)]
        public void CanEvaluateSingleSubstract()
        {
            string func = "X-3";
            var sut = GivenFunction(func);

            var result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 10 }));

            result.Should().Be(7);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERTATORS_SUBSTRACT)]
        public void CanEvaluateMultipleSubstract()
        {
            string func = "X-3-2";
            var sut = GivenFunction(func);

            var result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 9 }));

            result.Should().Be(4);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERTATORS_SUBSTRACT)]
        public void SubstractComesFirstWhenIsBetweenBracketsEvenWhenAnotherOperationWithHigherPrecedenceIsBefore()
        {
            string func = "2*(X-3)";
            var sut = GivenFunction(func);

            var result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 6 }));

            result.Should().Be(6);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERTATORS_SUBSTRACT)]
        public void BracketsAreEvaluatedBeforeSubstract()
        {
            string func = "3+(X-2)";
            var sut = GivenFunction(func);

            var result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 7 }));

            result.Should().Be(8);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERTATORS_SUBSTRACT)]
        public void SumIsEvaluatedBeforeSubstractWhenSubstractIsAfter()
        {
            string func = "6+X-3";
            var sut = GivenFunction(func);

            var result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 1 }));

            result.Should().Be(4);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERTATORS_SUBSTRACT)]
        public void MultiplyIsEvaluatedBeforeSubstract()
        {
            string func = "6-X*3";
            var sut = GivenFunction(func);

            var result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 2 }));

            result.Should().Be(0);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERTATORS_SUBSTRACT)]
        public void DivideIsEvaluatedBeforeSubstract()
        {
            string func = "16-X/3";
            var sut = GivenFunction(func);

            var result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 9 }));

            result.Should().Be(13);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERTATORS_SUBSTRACT)]
        public void ExponentialIsEvaluatedBeforeSubstract()
        {
            string func = "18-X^3";
            var sut = GivenFunction(func);

            var result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 2 }));

            result.Should().Be(10);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERTATORS_SUBSTRACT)]
        public void SquareRootIsEvaluatedBeforeSubstract()
        {
            string func = "6-sqrt X";
            var sut = GivenFunction(func);

            var result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 9 }));

            result.Should().Be(3);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERTATORS_SUBSTRACT)]
        public void CubeRootIsEvaluatedBeforeSubstract()
        {
            string func = "6-cbr X";
            var sut = GivenFunction(func);

            var result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 27 }));

            result.Should().Be(3);
        }
    }
    public class CompiledSubstractOperatorTests : SubstractOperatorTests
    {
        public override Function GivenFunction(string func) => CompiledFunctionTestFixtureFactory.GivenFunction(func);
    }
    public class JustInTimeSubstractOperatorTests : SubstractOperatorTests
    {
        public override Function GivenFunction(string func) => JustInTimeFunctionTestFixtureFactory.GivenFunction(func);
    }
}
