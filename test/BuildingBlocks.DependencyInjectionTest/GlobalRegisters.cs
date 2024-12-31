using BuildingBlocks.DependencyInjection.Attributes;
using BuildingBlocks.DependencyInjectionTest.Mocks;
using BuildingBlocks.DependencyInjectionTest.Mocks.Abstractions;

[assembly: Singleton<ITestSingletonAssembly, TestSingletonAssembly>]
[assembly: KeyedSingleton<ITestKeyedSingleton, TestKeyedSingletonAssembly>(nameof(TestKeyedSingletonAssembly))]
[assembly: KeyedEnumerableSingleton<ITestKeyedEnumerableSingleton, TestKeyedEnumerableSingletonAssembly>(nameof(TestKeyedEnumerableSingletonAssembly))]
[assembly: EnumerableSingleton<ITestEnumerableSingleton, TestEnumerableSingletonAssembly>]
[assembly: HostedService<TestHostedServiceAssembly>]