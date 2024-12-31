using BuildingBlocks.DependencyInjectionTest.Mocks.Abstractions;

namespace BuildingBlocks.DependencyInjectionTest.Mocks
{
    internal class TestSingletonAssembly : ITestSingletonAssembly
    {
        public string ImplementationName 
            => nameof(TestSingletonAssembly);
    }
}
