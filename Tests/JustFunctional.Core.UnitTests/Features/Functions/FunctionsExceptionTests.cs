using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace JustFunctional.Core.UnitTests.Features.Functions
{
    public abstract class FunctionsExceptionTests : FunctionsTestBase
    {
        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_EXCEPTIONS)]
        public void ThrowsExceptionWhenIncorrectFormulaAtTheEnd()
        {
            string func = "(X*2)+adadds";
            var sut = GivenFunction(func);

            Func<decimal> act = () => sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 8 }));

            act.Should().Throw<SyntaxErrorInExpressionException>();
        }


        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_EXCEPTIONS)]
        public void ThrowsExceptionWhenIncorrectFormulaMinusUnaryBeforeAnotherOperator()
        {
            string func = "-*5";
            var sut = GivenFunction(func);

            Func<decimal> act = () => sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 8 }));

            act.Should().Throw<MissingOperandException>();
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_EXCEPTIONS)]
        public void ThrowsExceptionWhenIncorrectFormulaBeforeAnotherOperator()
        {
            string func = "5+";
            var sut = GivenFunction(func);

            Func<decimal> act = () => sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 8 }));

            act.Should().Throw<MissingOperandException>();
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_EXCEPTIONS)]
        public void ThrowsExceptionWhenIncorrectFormulaBeforeAnotherOperator1()
        {
            string func = "5X";
            var sut = GivenFunction(func);

            Func<decimal> act = () => sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 8 }));

            act.Should().Throw<MissingOperatorException>();
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_EXCEPTIONS)]
        public void ThrowsExceptionWhenIncorrectFormulaBeforeAnotherOperator2()
        {
            string func = "4+5)";
            var sut = GivenFunction(func);

            Func<decimal> act = () => sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 8 }));

            act.Should().Throw<MissingOperatorException>();
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_EXCEPTIONS)]
        public void ThrowsExceptionWhenVariableIsNotSupplied()
        {
            string func = "X";
            var sut = GivenFunction(func);

            Func<decimal> act = () => sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["Y"] = 8 }));

            act.Should().Throw<SyntaxErrorInExpressionException>();
        }

       
    }
    public class CompiledFunctionsExceptionTests : FunctionsExceptionTests
    {
        public override Function GivenFunction(string func) => CompiledFunctionTestFixtureFactory.GivenFunction(func);
    }
    public class JustInTimeFunctionsExceptionTests : FunctionsExceptionTests
    {
        public override Function GivenFunction(string func) => JustInTimeFunctionTestFixtureFactory.GivenFunction(func);
    }
}