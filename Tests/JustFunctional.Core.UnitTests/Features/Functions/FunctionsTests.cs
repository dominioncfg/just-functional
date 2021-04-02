using FluentAssertions;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Xunit;

namespace JustFunctional.Core.UnitTests.Features.Functions
{
    public abstract class FunctionsTests: FunctionsTestBase
    {
        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_EVALUATE)]
        public void CanEvaluateConstantFunction()
        {
            string func = "2";
            var sut = GivenFunction(func);

            var result = sut.Evaluate(new EvaluationContext());

            result.Should().Be(2);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_EVALUATE)]
        public void CanEvaluateLinearFunction()
        {
            string func = $"X";
            var sut = GivenFunction(func);

            var result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 2 }));

            result.Should().Be(2);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_EVALUATE)]
        public void CanEvaluateSumFunction()
        {
            string func = "X+2";
            var sut = GivenFunction(func);

            var result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 3 }));

            result.Should().Be(5);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_EVALUATE)]
        public void CanEvaluateSubstractFunction()
        {
            string func = "X-2";
            var sut = GivenFunction(func);

            var result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 6 }));

            result.Should().Be(4);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_EVALUATE)]
        public void CanEvaluateMultiplyFunction()
        {
            string func = "X*2";
            var sut = GivenFunction(func);

            var result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 4 }));

            result.Should().Be(8);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_EVALUATE)]
        public void CanEvaluateDivideFunction()
        {
            string func = "X/2";
            var sut = GivenFunction(func);

            var result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 8 }));

            result.Should().Be(4);
        }

        [Theory]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_EVALUATE)]
        [InlineData(-40)]
        [InlineData(-4)]
        [InlineData(20)]
        [InlineData(25)]
        [InlineData(35)]
        [InlineData(929)]
        [InlineData(9387)]
        public void CanEvaluateComplexFunction(decimal x)
        {
            string funcExpression = "X*2+4-2/2";
            static decimal func(decimal x) => x * 2 + 4 - 2 / 2;
            var sut = GivenFunction(funcExpression);
            var result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = x }));
            result.Should().Be(func(x));
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_EVALUATE)]
        public void CanEvaluateFunctionWithSpacesAtTheBegining()
        {
            string func = " 5+X";
            var sut = GivenFunction(func);

            var result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 8 }));

            result.Should().Be(13);
        }
        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_EVALUATE)]
        public void CanEvaluateFunctionWithSpacesInTheMiddle()
        {
            string func = "5 + X";
            var sut = GivenFunction(func);

            var result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 8 }));

            result.Should().Be(13);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_EVALUATE)]
        public void CanEvaluateFunctionWithSpacesAtTheEnd()
        {
            string func = "5+X ";
            var sut = GivenFunction(func);

            var result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 8 }));

            result.Should().Be(13);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_EVALUATE)]
        public void CanBeEvaluateMultipleTimes()
        {
            string func = "X+4";
            var sut = GivenFunction(func);

            var result1 = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 3 }));
            var result2 = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 5 }));

            result1.Should().Be(7);
            result2.Should().Be(9);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_EVALUATE)]
        public void CanEvaluateParenteshis()
        {
            string func = "(X+4)*2";
            var sut = GivenFunction(func);

            var result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 3 }));

            result.Should().Be(14);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_EVALUATE)]
        public void CanEvaluateFunctionStartingWithNegativeNumberAtTheBegining()
        {
            string func = "-5+X";
            var sut = GivenFunction(func);

            var result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 8 }));

            result.Should().Be(3);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_EVALUATE)]
        public void CanEvaluateFunctionWithNegativeNumberAfterOperator()
        {
            string func = "X*-5";
            var sut = GivenFunction(func);

            var result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 8 }));

            result.Should().Be(-40);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_EVALUATE)]
        public void CanEvaluateFunctionWithMinusAfterBracket()
        {
            string func = "(X*2)-5";
            var sut = GivenFunction(func);

            var result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 8 }));

            result.Should().Be(11);
        }


        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_EVALUATE)]
        public void CanEvaluateFunctionWithOperandWithDecimalDigits()
        {
            string func = "(X*2)+2.1";
            var sut = GivenFunction(func);

            var result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 8 }));

            result.Should().Be(18.1M);
        }

        [Theory]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_EVALUATE)]
        [InlineData("es-ES")]
        [InlineData("pt-PT")]
        [InlineData("pt-BR")]
        [InlineData("tr-TR")]
        [InlineData("en-US")]
        [InlineData("en-UK")]
        public void CanEvaluateFunctionWithOperandWithDecimalDigitsWithCulture(string culture)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);

            string func = "(X*2)+2.1";
            var sut = GivenFunction(func);

            var result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 8 }));

            result.Should().Be(18.1M);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_EVALUATE)]
        public void CanEvaluateMultipleVariableFunction()
        {
            string func = $"X+Y";
            var sut = GivenFunction(func);

            var result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 2, ["Y"] = 3 }));

            result.Should().Be(5);
        }
    }
    public class CompiledFunctionsTests : FunctionsTests
    {
        public override Function GivenFunction(string func) => CompiledFunctionTestFixtureFactory.GivenFunction(func);
    }
    public class JustInTimeFunctionsTests : FunctionsTests
    {
        public override Function GivenFunction(string func) => JustInTimeFunctionTestFixtureFactory.GivenFunction(func);
    }
}