using BuildingBlocks.DependencyInjectionTest.Mocks.Abstractions;

namespace BuildingBlocks.DependencyInjectionTest.Mocks
{
    internal class TestEnumerableSingletonAssembly : ITestEnumerableSingletonAssembly
    {
        public string ImplementationName 
            => nameof(TestEnumerableSingletonAssembly);
    }
}
