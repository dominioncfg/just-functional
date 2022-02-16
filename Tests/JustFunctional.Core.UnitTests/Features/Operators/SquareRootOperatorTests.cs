using FluentAssertions;
using System.Collections.Generic;
using Xunit;
namespace JustFunctional.Core.UnitTests.Features.Functions
{
    public abstract class SquareRootOperatorTests : FunctionsTestBase
    {
        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_SQUARE_ROOT)]
        public void CanEvaluateSingleSquareRoot()
        {
            string func = "sqrtX";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 144 }));

            result.Should().Be(12);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_SQUARE_ROOT)]
        public void CanEvaluateMultipleSquareRoot()
        {
            string func = "sqrtsqrtX";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 81 }));

            result.Should().Be(3);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_SQUARE_ROOT)]
        public void SquareRootmHasPrecedenceOverSumAndSubstract()
        {
            string func = "sqrtX+10-2";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 144 }));

            result.Should().Be(20);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_SQUARE_ROOT)]
        public void SquareRootHasPrecedenceOverMultiplicationAndDivide()
        {
            string func = "sqrtX*4/2";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 144 }));

            result.Should().Be(24);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_SQUARE_ROOT)]
        public void BracketsAreEvaluatedBeforeSquareRoot()
        {
            string func = "sqrt(X+40)";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 60 }));

            result.Should().Be(10);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_SQUARE_ROOT)]
        public void SquareRootHasLessPrecedenceThanLogarithm()
        {
            string func = "sqrtlogX";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 10000 }));

            result.Should().Be(2);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_SQUARE_ROOT)]
        public void SquareRootTheSamePrecedenceThanExponentiation()
        {
            string func = "sqrtX^3";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 4 }));

            result.Should().Be(8);
        }
    }
    public class CompiledFunctionsSquareRootOperatorTests : SquareRootOperatorTests
    {
        public override Function GivenFunction(string func) => CompiledFunctionTestFixtureFactory.GivenFunction(func);
    }
    public class JustInTimeFunctionsSquareRootOperatorTests : SquareRootOperatorTests
    {
        public override Function GivenFunction(string func) => JustInTimeFunctionTestFixtureFactory.GivenFunction(func);
    }
}