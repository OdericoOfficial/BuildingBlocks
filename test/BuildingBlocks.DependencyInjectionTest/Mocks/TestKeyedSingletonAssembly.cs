using BuildingBlocks.DependencyInjectionTest.Mocks.Abstractions;

namespace BuildingBlocks.DependencyInjectionTest.Mocks
{
    internal class TestKeyedSingletonAssembly : ITestKeyedSingleton
    {
        public string ImplementationName 
            => nameof(TestKeyedSingletonAssembly);
    }
}
