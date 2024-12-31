using BuildingBlocks.DependencyInjection.Attributes;
using BuildingBlocks.DependencyInjectionTest.Mocks.Abstractions;

namespace BuildingBlocks.DependencyInjectionTest.Mocks
{
    [EnumerableSingleton<ITestEnumerableSingleton>]
    internal class TestEnumerableSingletonClass : ITestEnumerableSingleton
    {
        public string ImplementationName 
            => nameof(TestEnumerableSingletonClass);
    }
}
