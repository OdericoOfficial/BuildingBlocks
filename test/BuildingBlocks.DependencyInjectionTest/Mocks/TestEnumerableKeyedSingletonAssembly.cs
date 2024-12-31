using BuildingBlocks.DependencyInjectionTest.Mocks.Abstractions;

namespace BuildingBlocks.DependencyInjectionTest.Mocks
{
    internal class TestKeyedEnumerableSingletonAssembly : ITestKeyedEnumerableSingleton
    {
        public string ImplementationName 
            => nameof(TestKeyedEnumerableSingletonAssembly);
    }
}
