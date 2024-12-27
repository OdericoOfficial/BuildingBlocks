using BuildingBlocks.DependencyInjection.Attributes;
using BuildingBlocks.DependencyInjectionTest.Mocks;
using BuildingBlocks.DependencyInjectionTest.Mocks.Abstractions;

[assembly: Singleton<ITestSingletonAssembly, TestSingletonAssembly>]
[assembly: Singleton<ITestKeyedSingletonAssembly, TestKeyedSingletonAssembly>(nameof(TestKeyedSingletonAssembly))]
[assembly: Singleton<ITestKeyedEnumerableSingletonAssembly, TestKeyedEnumerableSingletonAssembly>(nameof(TestKeyedEnumerableSingletonAssembly), true)]
[assembly: Singleton<ITestEnumerableSingletonAssembly, TestEnumerableSingletonAssembly>(isEnumerable: true)]
[assembly: HostedService<TestHostedServiceAssembly>]