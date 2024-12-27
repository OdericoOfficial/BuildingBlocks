using BuildingBlocks.DependencyInjection.Attributes;
using BuildingBlocks.DependencyInjectionTest.Mocks.Abstractions;

namespace BuildingBlocks.DependencyInjectionTest.Mocks
{
    [Singleton<ITestEnumerableSingletonClass>(isEnumerable: true)]
    internal class TestEnumerableSingletonClass : ITestEnumerableSingletonClass
    {
        public string ImplementationName 
            => nameof(TestEnumerableSingletonClass);
    }
}
