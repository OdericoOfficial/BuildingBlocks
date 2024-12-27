using BuildingBlocks.DependencyInjection.Attributes;
using BuildingBlocks.DependencyInjectionTest.Mocks.Abstractions;

namespace BuildingBlocks.DependencyInjectionTest.Mocks
{
    [Singleton<ITestSingletonClass>]
    internal class TestSingletonClass : ITestSingletonClass
    {
        public string ImplementationName 
            => nameof(TestSingletonClass);
    }
}
