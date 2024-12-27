using BuildingBlocks.DependencyInjection.Attributes;
using BuildingBlocks.DependencyInjectionTest.Mocks.Abstractions;

namespace BuildingBlocks.DependencyInjectionTest.Mocks
{
    [Singleton<ITestKeyedSingletonClass>(nameof(TestKeyedSingletonClass))]
    internal class TestKeyedSingletonClass : ITestKeyedSingletonClass
    {
        public string ImplementationName 
            => nameof(TestKeyedSingletonClass);
    }
}
