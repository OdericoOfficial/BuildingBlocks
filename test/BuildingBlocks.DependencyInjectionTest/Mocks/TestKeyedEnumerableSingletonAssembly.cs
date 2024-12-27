using BuildingBlocks.DependencyInjectionTest.Mocks.Abstractions;

namespace BuildingBlocks.DependencyInjectionTest.Mocks
{
    internal class TestKeyedEnumerableSingletonAssembly : ITestKeyedEnumerableSingletonAssembly
    {
        public string ImplementationName 
            => nameof(TestKeyedEnumerableSingletonAssembly);
    }
}
