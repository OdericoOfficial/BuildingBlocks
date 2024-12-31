using BuildingBlocks.DependencyInjection.Attributes;
using BuildingBlocks.DependencyInjectionTest.Mocks.Abstractions;

namespace BuildingBlocks.DependencyInjectionTest.Mocks
{
    [KeyedEnumerableSingleton<ITestKeyedEnumerableSingleton>(nameof(TestKeyedEnumerableSingletonClass))]
    internal class TestKeyedEnumerableSingletonClass : ITestKeyedEnumerableSingleton
    {
        public string ImplementationName 
            => nameof(TestKeyedEnumerableSingletonClass);
    }
}
