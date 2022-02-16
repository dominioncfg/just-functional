using FluentAssertions;
using System.Collections.Generic;
using Xunit;
namespace JustFunctional.Core.UnitTests.Features.Functions
{
    public abstract class CubeRootOperatorTests : FunctionsTestBase
    {
        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_CUBE_ROOT)]
        public void CanEvaluateSingleCubeRoot()
        {
            string func = "cbrX";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 27 }));

            result.Should().Be(3);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_CUBE_ROOT)]
        public void CanEvaluateMultipleCubeRoot()
        {
            string func = "cbrcbrX";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 512 }));

            result.Should().Be(2);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_CUBE_ROOT)]
        public void CubeRootmHasPrecedenceOverSumAndSubstract()
        {
            string func = "cbrX+10-2";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 27 }));

            result.Should().Be(11);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_CUBE_ROOT)]
        public void CubeRootHasPrecedenceOverMultiplicationAndDivide()
        {
            string func = "cbrX*4/2";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 27 }));

            result.Should().Be(6);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_CUBE_ROOT)]
        public void BracketsAreEvaluatedBeforeCubeRoot()
        {
            string func = "cbr(X+4)";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 60 }));

            result.Should().Be(4);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_CUBE_ROOT)]
        public void CubeRootHasLessPrecedenceThanLogarithm()
        {
            string func = "cbrlogX";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 100000000 }));

            result.Should().Be(2);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_CUBE_ROOT)]
        public void CubeRootTheSamePrecedenceThanExponentiation()
        {
            string func = "cbrX^2";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 125 }));

            result.Should().Be(25);
        }
    }
    public class CompiledFunctionsCubeRootOperatorTests : CubeRootOperatorTests
    {
        public override Function GivenFunction(string func) => CompiledFunctionTestFixtureFactory.GivenFunction(func);
    }
    public class JustInTimeFunctionsCubeRootOperatorTests : CubeRootOperatorTests
    {
        public override Function GivenFunction(string func) => JustInTimeFunctionTestFixtureFactory.GivenFunction(func);
    }
}