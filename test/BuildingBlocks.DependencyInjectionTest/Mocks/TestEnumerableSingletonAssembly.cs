using BuildingBlocks.DependencyInjectionTest.Mocks.Abstractions;

namespace BuildingBlocks.DependencyInjectionTest.Mocks
{
    internal class TestEnumerableSingletonAssembly : ITestEnumerableSingleton
    {
        public string ImplementationName 
            => nameof(TestEnumerableSingletonAssembly);
    }
}
