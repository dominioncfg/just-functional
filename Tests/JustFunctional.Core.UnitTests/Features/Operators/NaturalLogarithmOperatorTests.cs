using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;
namespace JustFunctional.Core.UnitTests.Features.Functions
{
    public abstract class NaturalLogarithmOperatorTests : FunctionsTestBase
    {
        private readonly decimal Euler = (decimal)Math.E;
        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_NATURAL_LOGARITHM)]
        public void CanEvaluateSingleLogarithm()
        {
            string func = "lnX";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = (Euler * Euler) }));

            result.Should().Be(2);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_NATURAL_LOGARITHM)]
        public void LogarithmHasPrecedenceOverSumAndSubstract()
        {
            string func = "lnX+12-3";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = (Euler * Euler) }));

            result.Should().Be(11);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_NATURAL_LOGARITHM)]
        public void LogarithmHasPrecedenceOverMultiplicationAndDivide()
        {
            string func = "lnX*10/4";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = (Euler * Euler) }));

            result.Should().Be(5);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_NATURAL_LOGARITHM)]
        public void BracketsAreEvaluatedBeforeLogarithm()
        {
            string func = "ln(X+1)";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = Euler * Euler - 1 }));

            result.Should().Be(2);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_NATURAL_LOGARITHM)]
        public void LogarithmHasPrecedenceOverExponentiation()
        {
            string func = "3^lnX";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = Euler * Euler }));

            result.Should().Be(9);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_NATURAL_LOGARITHM)]
        public void LogarithmHasTheSamePrecedenceThanModule()
        {
            string func = "lnmodX";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = -Euler * Euler }));

            result.Should().Be(2);
        }
    }

    public class CompiledFunctionsNaturalLogarithmOperatorTests : NaturalLogarithmOperatorTests
    {
        public override Function GivenFunction(string func) => CompiledFunctionTestFixtureFactory.GivenFunction(func);
    }
    public class JustInTimeFunctionsNaturalLogarithmOperatorTests : NaturalLogarithmOperatorTests
    {
        public override Function GivenFunction(string func) => JustInTimeFunctionTestFixtureFactory.GivenFunction(func);
    }
}