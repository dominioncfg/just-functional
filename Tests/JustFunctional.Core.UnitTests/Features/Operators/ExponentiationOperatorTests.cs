using FluentAssertions;
using System.Collections.Generic;
using Xunit;
namespace JustFunctional.Core.UnitTests.Features.Functions
{
    public abstract class ExponentiationOperatorTests : FunctionsTestBase
    {
        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_EXPONENTIATION)]
        public void CanEvaluateSingleExponentiation()
        {
            string func = "2^X";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 5 }));

            result.Should().Be(32);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_EXPONENTIATION)]
        public void CanEvaluateMultipleExponentiation()
        {
            string func = "2^3^X";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 2 }));

            result.Should().Be(512);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_EXPONENTIATION)]
        public void ExponentiationHasPrecedenceOverSumAndSubstract()
        {
            string func = "2^X+6-2";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 6 }));

            result.Should().Be(68);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_EXPONENTIATION)]
        public void ExponentiationHasPrecedenceOverMultiplicationAndDivide()
        {
            string func = "X^3*4/2";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 3 }));

            result.Should().Be(54);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_EXPONENTIATION)]
        public void BracketsAreEvaluatedBeforeExponentiation()
        {
            string func = "2^(X+4)";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 2 }));

            result.Should().Be(64);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_EXPONENTIATION)]
        public void ExponentiationHasLessPrecedenceThanLogarithm()
        {
            string func = "3^logX";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 100 }));

            result.Should().Be(9);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_EXPONENTIATION)]
        public void ExponentiationHasTheSamePrecedenceThanSquareRoot()
        {
            string func = "X^sqrt64";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 2 }));

            result.Should().Be(256);
        }
    }
    public class CompiledFunctionsExponentiationOperatorTests : ExponentiationOperatorTests
    {
        public override Function GivenFunction(string func) => CompiledFunctionTestFixtureFactory.GivenFunction(func);
    }
    public class JustInTimeFunctionsExponentiationOperatorTests : ExponentiationOperatorTests
    {
        public override Function GivenFunction(string func) => JustInTimeFunctionTestFixtureFactory.GivenFunction(func);
    }
}