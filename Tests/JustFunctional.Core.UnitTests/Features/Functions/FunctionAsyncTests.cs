using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JustFunctional.Core.UnitTests.Features.Functions
{
    public abstract class FunctionAsyncTests : FunctionsTestBase
    {
        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_ASYNC)]
        public async Task CanEvaluateSingleTime()
        {
            string func = "(X*4)^2";
            var sut = GivenFunction(func);

            var result = await sut.EvaluateAsync(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = 3 }));

            result.Should().Be(144);
        }

        [Fact]
        [Trait(UnitTestTraitCategories.Function.CATEGORY_NAME, UnitTestTraitCategories.Function.FUNCTION_ASYNC)]
        public async Task CanEvaluateMultipleTimes()
        {
            int rangeLimit = 100;
            string func = "(X*4)^2";
            var sut = GivenFunction(func);
            var expectedResult = Enumerable.Range(1, rangeLimit).Select(x => (x * 4) * (x * 4));

            var tasks = Enumerable.Range(1, rangeLimit).Select(x => sut.EvaluateAsync(new EvaluationContext(new Dictionary<string, decimal>() { ["X"] = x })));
            await Task.WhenAll(tasks);

            var actualResult = tasks.Select(x => x.Result).ToList();
            actualResult.Should().Equal(expectedResult);
        }

    }
    public class CompiledFunctionAsyncTests : FunctionAsyncTests
    {
        public override Function GivenFunction(string func) => CompiledFunctionTestFixtureFactory.GivenFunction(func);
    }
    public class JustInTimeFunctionAsyncTests : FunctionAsyncTests
    {
        public override Function GivenFunction(string func) => JustInTimeFunctionTestFixtureFactory.GivenFunction(func);
    }
}