using BuildingBlocks.DependencyInjectionTest.Mocks;
using BuildingBlocks.DependencyInjectionTest.Mocks.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.DependencyInjectionTest
{
    public class DependencyInjectionTest(IServiceProvider provider)
    {
        [Fact]
        public void TestNonEnumerableAndNonKeyed()
        {
            Assert.Equal(nameof(TestSingletonClass), provider.GetRequiredService<ITestSingletonClass>().ImplementationName);
            Assert.Equal(nameof(TestSingletonAssembly), provider.GetRequiredService<ITestSingletonAssembly>().ImplementationName);
        }

        [Fact]
        public void TestNonEnumerableAndKeyed()
        {
            Assert.Equal(nameof(TestKeyedSingletonClass), provider.GetRequiredKeyedService<ITestKeyedSingleton>(nameof(TestKeyedSingletonClass)).ImplementationName);
            Assert.Equal(nameof(TestKeyedSingletonAssembly), provider.GetRequiredKeyedService<ITestKeyedSingleton>(nameof(TestKeyedSingletonAssembly)).ImplementationName);
        }

        [Fact]
        public void TestEnumerableAndNonKeyed()
        {
            var list = provider.GetServices<ITestEnumerableSingleton>();
            Assert.Equal(2, list.Count());
            Assert.True(list.All(item => item.ImplementationName == nameof(TestEnumerableSingletonClass)
                || item.ImplementationName == nameof(TestEnumerableSingletonAssembly)));
        }

        [Fact]
        public void TestEnumerableAndKeyed()
        {
            var list1 = provider.GetKeyedServices<ITestKeyedEnumerableSingleton>(nameof(TestKeyedEnumerableSingletonAssembly));
            Assert.Single(list1);
            Assert.True(list1.All(item => item.ImplementationName == nameof(TestKeyedEnumerableSingletonAssembly)));
            var list2 = provider.GetKeyedServices<ITestKeyedEnumerableSingleton>(nameof(TestKeyedEnumerableSingletonClass));
            Assert.Single(list2);
            Assert.True(list2.All(item => item.ImplementationName == nameof(TestKeyedEnumerableSingletonClass)));
        }
    }
}
