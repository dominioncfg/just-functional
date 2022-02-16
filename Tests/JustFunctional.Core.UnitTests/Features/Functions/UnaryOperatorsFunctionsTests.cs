using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace JustFunctional.Core.UnitTests.Features.Functions
{
    public abstract class UnaryOperatorsFunctionsTests : FunctionsTestBase
    {
        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_UNARY_OPERATORS)]
        public void CanEvaluateFunctionStartingWithNegativeNumberAtTheBegining()
        {
            string func = "-5+X";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 8 }));

            result.Should().Be(3);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_UNARY_OPERATORS)]
        public void CanEvaluateFunctionWithNegativeNumberAfterOperator()
        {
            string func = "X*-5";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 8 }));

            result.Should().Be(-40);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_UNARY_OPERATORS)]
        public void CanEvaluateFunctionWithSubstractAfterBracket()
        {
            string func = "(X*2)-5";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 8 }));

            result.Should().Be(11);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_UNARY_OPERATORS)]
        public void CanEvaluateFunctionStartingWithPositiveNumberAtTheBegining()
        {
            string func = "+5+X";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 8 }));

            result.Should().Be(13);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_UNARY_OPERATORS)]
        public void CanEvaluateFunctionWithPositiveNumberAfterOperator()
        {
            string func = "X*+5";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 8 }));

            result.Should().Be(40);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_UNARY_OPERATORS)]
        public void CanEvaluateFunctionWithAdditionAfterBracket()
        {
            string func = "(X*2)+5";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 8 }));

            result.Should().Be(21);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_UNARY_OPERATORS)]
        public void CanEvaluateFunctionWithNegativeVariable()
        {
            string func = "-X";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 8 }));

            result.Should().Be(-8);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_UNARY_OPERATORS)]
        public void CanEvaluateFunctionWithNegativeVariableAndAfterAnOperation()
        {
            string func = "-X+5";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 8 }));

            result.Should().Be(-3);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_UNARY_OPERATORS)]
        public void CanEvaluateFunctionWithNegativeVariableAfterOperator()
        {
            string func = "3*-X";
            var sut = GivenFunction(func);
            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 8 }));

            result.Should().Be(-24);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_UNARY_OPERATORS)]
        public void CanEvaluateFunctionWithSubstractAfterBrackets()
        {
            string func = "(4*3)-X";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 8 }));

            result.Should().Be(4);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_UNARY_OPERATORS)]
        public void CanEvaluateFunctionWithPositiveVariable()
        {
            string func = "+X";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 8 }));

            result.Should().Be(8);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_UNARY_OPERATORS)]
        public void CanEvaluateFunctionWithPositiveVariableAndAfterAnOperation()
        {
            string func = "+X-5";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 8 }));

            result.Should().Be(3);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_UNARY_OPERATORS)]
        public void CanEvaluateFunctionWithPositiveVariableAfterOperator()
        {
            string func = "3*+X";
            var sut = GivenFunction(func);
            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 8 }));

            result.Should().Be(24);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_UNARY_OPERATORS)]
        public void CanEvaluateFunctionWithAdditionVariableAfterBrackets()
        {
            string func = "(4*3)+X";
            var sut = GivenFunction(func);

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 8 }));

            result.Should().Be(20);
        }
    }

    public class CompiledUnaryOperatorsFunctionsTests : UnaryOperatorsFunctionsTests
    {
        public override Function GivenFunction(string func) => CompiledFunctionTestFixtureFactory.GivenFunction(func);
    }
    public class JustInTimeUnaryOperatorsFunctionsTests : UnaryOperatorsFunctionsTests
    {
        public override Function GivenFunction(string func) => JustInTimeFunctionTestFixtureFactory.GivenFunction(func);
    }
}