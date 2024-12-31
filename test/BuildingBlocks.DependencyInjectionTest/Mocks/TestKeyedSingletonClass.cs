using BuildingBlocks.DependencyInjection.Attributes;
using BuildingBlocks.DependencyInjectionTest.Mocks.Abstractions;

namespace BuildingBlocks.DependencyInjectionTest.Mocks
{
    [KeyedSingleton<ITestKeyedSingleton>(nameof(TestKeyedSingletonClass))]
    internal class TestKeyedSingletonClass : ITestKeyedSingleton
    {
        public string ImplementationName 
            => nameof(TestKeyedSingletonClass);
    }
}
