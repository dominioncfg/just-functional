using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;
namespace JustFunctional.Core.UnitTests.Features.Functions
{
    public abstract class SineOperatorTests : FunctionsTestBase
    {
        private const decimal NinetyDegreesInRad = (decimal)Math.PI / 2;
        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_SINE)]
        public void CanEvaluateSingleSine()
        {
            string func = "sinX";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 0 }));

            result.Should().Be(0);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_SINE)]
        public void CanEvaluateMultipleSine()
        {
            string func = "sinsinX";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 0 }));

            result.Should().Be(0);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_SINE)]
        public void SineHasPrecedenceOverSumAndSubstract()
        {
            string func = "sinX+6-2";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 0 }));

            result.Should().Be(4);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_SINE)]
        public void SineHasPrecedenceOverMultiplicationAndDivide()
        {
            string func = "sinX*8/4";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = NinetyDegreesInRad }));

            result.Should().Be(2);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_SINE)]
        public void BracketsAreEvaluatedBeforeSine()
        {
            string func = "sin(X+40)";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = -40 }));

            result.Should().Be(0);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_SINE)]
        public void SineHasPrecedenceOverExponentiation()
        {
            string func = "3^sinX";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 0 }));

            result.Should().Be(1);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_SINE)]
        public void SineHasTheSamePrecedenceThanModule()
        {
            string func = "sinmodX";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = -NinetyDegreesInRad }));

            result.Should().Be(1);
        }
    }
    public class CompiledFunctionsSineOperatorTests : SineOperatorTests
    {
        public override Function GivenFunction(string func) => CompiledFunctionTestFixtureFactory.GivenFunction(func);
    }
    public class JustInTimeFunctionsSineOperatorTests : SineOperatorTests
    {
        public override Function GivenFunction(string func) => JustInTimeFunctionTestFixtureFactory.GivenFunction(func);
    }
}