using FluentAssertions;
using System.Collections.Generic;
using Xunit;
namespace JustFunctional.Core.UnitTests.Features.Functions
{
    public abstract class ModuleOperatorTests : FunctionsTestBase
    {
        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_MODULE)]
        public void CanEvaluateSingleModule()
        {
            string func = "modX";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = -10 }));

            result.Should().Be(10);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_MODULE)]
        public void CanEvaluateMultipleModules()
        {
            string func = "modmodX";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = -10 }));

            result.Should().Be(10);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_MODULE)]
        public void ModuleHasPrecedenceOverSumAndSubstract()
        {
            string func = "modX+6";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = -10 }));

            result.Should().Be(16);
        }
        
        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_MODULE)]
        public void ModuleHasPrecedenceOverMultiplicationAndDivide()
        {
            string func = "modX*2/4";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = -10 }));

            result.Should().Be(5);
        }
        
        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_MODULE)]
        public void BracketsAreEvaluatedBeforeModule()
        {
            string func = "mod(X+40)";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = -60 }));

            result.Should().Be(20);
        }
        
        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_MODULE)]
        public void ModuleHasPrecedenceOverExponentiation()
        {
            string func = "3^modX";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = -2 }));

            result.Should().Be(9);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Opertators.CATEGORY_NAME, UnitTestTraitCategories.Opertators.OPERATORS_MODULE)]
        public void ModuleHasTheSamePrecedenceThanLog()
        {
            string func = "logmodX";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = -100 }));

            result.Should().Be(2);
        }
    }
    public class CompiledFunctionsModuleOperatorTests : ModuleOperatorTests
    {
        public override Function GivenFunction(string func) => CompiledFunctionTestFixtureFactory.GivenFunction(func);
    }
    public class JustInTimeFunctionsModuleOperatorTests : ModuleOperatorTests
    {
        public override Function GivenFunction(string func) => JustInTimeFunctionTestFixtureFactory.GivenFunction(func);
    }
}