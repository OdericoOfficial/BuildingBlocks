using BuildingBlocks.DependencyInjection.Attributes;
using BuildingBlocks.DependencyInjectionTest.Mocks.Abstractions;

namespace BuildingBlocks.DependencyInjectionTest.Mocks
{
    [Singleton<ITestKeyedEnumerableSingletonClass>(nameof(TestKeyedEnumerableSingletonClass), true)]
    internal class TestKeyedEnumerableSingletonClass : ITestKeyedEnumerableSingletonClass
    {
        public string ImplementationName 
            => nameof(TestKeyedEnumerableSingletonClass);
    }
}
