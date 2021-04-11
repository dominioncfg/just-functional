using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace JustFunctional.Core.UnitTests.Features.Functions
{
    public class FunctionsIdentifiersTests
    {
        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_IDENTIFIERS)]
        public void CanEvaluateFunctionWithLongVariableName()
        {
            string func = "SuperMegaVariable+3";
            var sut = CreateWithSettings(func, option => { });

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["SuperMegaVariable"] = 2 }));

            result.Should().Be(5);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_IDENTIFIERS)]
        public void CanEvaluateFunctionsWithTwoVariablesStartingWithTheSameName()
        {
            string func = "X1+X2";
            var sut = CreateWithSettings(func, option => { });

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X1"] = 3, ["X2"] = 2 }));

            result.Should().Be(5);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_IDENTIFIERS)]
        public void CanEvaluateFunctionsWithNameStartingWithUnderscore()
        {
            string func = "_a1+9";
            var sut = CreateWithSettings(func, option => { });

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["_a1"] = 3 }));

            result.Should().Be(12);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_IDENTIFIERS)]
        public void CanEvaluateFunctionsWithOperatorWithLengthOfFourChars()
        {
            string func = "_a1 AddO 9";
            var @operator = new DelegateOperator("TestAdd", "AddO", 1, OperatorType.Binary,
                                                Associativity.Left, (operators) => operators.Sum());

            var sut = CreateWithSettings(func, option =>
                option
                    .WithCustomTokenProvider(tokenOptions =>
                        tokenOptions
                        .WithOperator(@operator)
                    )
            );

            decimal result = sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["_a1"] = 3 }));

            result.Should().Be(12);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_IDENTIFIERS)]
        public void ThrowsExceptionWhenEvaluatingFunctionsWithOperatorsWithMoreThanFourCharsOfLength()
        {
            string func = "X AddOp 9";
            var @operator = new DelegateOperator("TestAdd", "AddOp", 1, OperatorType.Binary,
                                                Associativity.Left, (operators) => operators.Sum());

            var sut = CreateWithSettings(func, option =>
                option
                    .WithCustomTokenProvider(tokenOptions =>
                        tokenOptions
                        .WithOperator(@operator)
                    )
            );
            Func<decimal> act = () => sut.Evaluate(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 3 })); 

            act.Should().Throw<SyntaxErrorInExpressionException>();          
        }


        private Function CreateWithSettings(string expression, Action<FunctionOptionsBuilder> setupAction)
        {
            var factory = FunctionFactoryBuilder.ConfigureFactory(setupAction);
            return factory.Create(expression);
        }
    }
}
