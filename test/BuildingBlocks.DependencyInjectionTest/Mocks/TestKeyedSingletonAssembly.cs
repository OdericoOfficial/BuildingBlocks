using BuildingBlocks.DependencyInjectionTest.Mocks.Abstractions;

namespace BuildingBlocks.DependencyInjectionTest.Mocks
{
    internal class TestKeyedSingletonAssembly : ITestKeyedSingletonAssembly
    {
        public string ImplementationName 
            => nameof(TestKeyedSingletonAssembly);
    }
}
