using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;
namespace JustFunctional.Core.UnitTests.Features.Functions
{
    public abstract class CosineOperatorTests : FunctionsTestBase
    {
        private const decimal NinetyDegreesInRad = (decimal)Math.PI / 2;
        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_COSINE)]
        public void CanEvaluateSingleCosine()
        {
            string func = "cosX";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 0 }));

            result.Should().Be(1);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_COSINE)]
        public void CanEvaluateMultipleCosine()
        {
            string func = "coscosX";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = NinetyDegreesInRad }));

            result.Should().Be(1);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_COSINE)]
        public void CosineHasPrecedenceOverSumAndSubstract()
        {
            string func = "cosX+6-2";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 0 }));

            result.Should().Be(5);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_COSINE)]
        public void CosineHasPrecedenceOverMultiplicationAndDivide()
        {
            string func = "cosX*8/4";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 0 }));

            result.Should().Be(2);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_COSINE)]
        public void BracketsAreEvaluatedBeforeCosine()
        {
            string func = "cos(X+40)";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = -40 }));

            result.Should().Be(1);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_COSINE)]
        public void CosineHasPrecedenceOverExponentiation()
        {
            string func = "3^cosX";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = NinetyDegreesInRad }));

            result.Should().Be(1);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_COSINE)]
        public void CosineHasTheSamePrecedenceThanModule()
        {
            string func = "cosmodX";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = -NinetyDegreesInRad }));

            result.Should().BeApproximately(0, 0.001m);
        }
    }
    public class CompiledFunctionsSineCosineOperatorTests : CosineOperatorTests
    {
        public override Function GivenFunction(string func) => CompiledFunctionTestFixtureFactory.GivenFunction(func);
    }
    public class JustInTimeFunctionsCosineOperatorTests : CosineOperatorTests
    {
        public override Function GivenFunction(string func) => JustInTimeFunctionTestFixtureFactory.GivenFunction(func);
    }
}