using BuildingBlocks.SourceGeneratorsTest.Helpers.Abstractions;

namespace BuildingBlocks.SourceGeneratorsTest.DependencyInjection
{
    public class Test(IDependencyInjectionVerifyHelper helper)
    {
        [Fact]
        public Task TestDependencyInjectionAsync()
            => helper.VerifyAsync(RangeProvider.GetSources());
    }
}