using BuildingBlocks.SourceGeneratorsTest.Helpers.Abstractions;

namespace BuildingBlocks.SourceGeneratorsTest
{
    public class DependencyInjectionTest(IDependencyInjectionVerifyHelper helper)
    {
        private static readonly string _interfaceSource = @"namespace DependencyInjectionTest.Abstractions
{
    public interface IDITest { }

    public interface IDITest<T1, T2> : IDITest { }
}";

        private static readonly string _inheritedClassSource = @"using Microsoft.Extensions.DependencyInjection.Attributes;
using DependencyInjectionTest.Abstractions;

namespace DependencyInjectionTest
{
    [Singleton(typeof(IDITest))]
    [Singleton(typeof(IDITest), key: nameof(DITest))]
    [Singleton(typeof(IDITest), isEnumerable: true)]
    [Singleton(typeof(IDITest), key: nameof(DITest), isEnumerable: true)]
    [Singleton(typeof(IDITest<string, int>))]
    [Singleton(typeof(IDITest<string, int>), key: nameof(DITest))]
    [Singleton(typeof(IDITest<string, int>), isEnumerable: true)]
    [Singleton(typeof(IDITest<string, int>), key: nameof(DITest), isEnumerable: true)]
    [Singleton<IDITest>]
    [Singleton<IDITest>(key: nameof(DITest))]
    [Singleton<IDITest>(isEnumerable: true)]
    [Singleton<IDITest>(key: nameof(DITest), isEnumerable: true)]
    [Singleton<IDITest<string, int>>]
    [Singleton<IDITest<string, int>>(key: nameof(DITest))]
    [Singleton<IDITest<string, int>>(isEnumerable: true)]
    [Singleton<IDITest<string, int>>(key: nameof(DITest), isEnumerable: true)]
    [Scoped(typeof(IDITest))]
    [Scoped(typeof(IDITest), key: nameof(DITest))]
    [Scoped(typeof(IDITest), isEnumerable: true)]
    [Scoped(typeof(IDITest), key: nameof(DITest), isEnumerable: true)]
    [Scoped(typeof(IDITest<string, int>))]
    [Scoped(typeof(IDITest<string, int>), key: nameof(DITest))]
    [Scoped(typeof(IDITest<string, int>), isEnumerable: true)]
    [Scoped(typeof(IDITest<string, int>), key: nameof(DITest), isEnumerable: true)]
    [Scoped<IDITest>]
    [Scoped<IDITest>(key: nameof(DITest))]
    [Scoped<IDITest>(isEnumerable: true)]
    [Scoped<IDITest>(key: nameof(DITest), isEnumerable: true)]
    [Scoped<IDITest<string, int>>]
    [Scoped<IDITest<string, int>>(key: nameof(DITest))]
    [Scoped<IDITest<string, int>>(isEnumerable: true)]
    [Scoped<IDITest<string, int>>(key: nameof(DITest), isEnumerable: true)]
    [Transient(typeof(IDITest))]
    [Transient(typeof(IDITest), key: nameof(DITest))]
    [Transient(typeof(IDITest), isEnumerable: true)]
    [Transient(typeof(IDITest), key: nameof(DITest), isEnumerable: true)]
    [Transient(typeof(IDITest<string, int>))]
    [Transient(typeof(IDITest<string, int>), key: nameof(DITest))]
    [Transient(typeof(IDITest<string, int>), isEnumerable: true)]
    [Transient(typeof(IDITest<string, int>), key: nameof(DITest), isEnumerable: true)]
    [Transient<IDITest>]
    [Transient<IDITest>(key: nameof(DITest))]
    [Transient<IDITest>(isEnumerable: true)]
    [Transient<IDITest>(key: nameof(DITest), isEnumerable: true)]
    [Transient<IDITest<string, int>>]
    [Transient<IDITest<string, int>>(key: nameof(DITest))]
    [Transient<IDITest<string, int>>(isEnumerable: true)]
    [Transient<IDITest<string, int>>(key: nameof(DITest), isEnumerable: true)]
    internal class DITest : IDITest, IDITest<string, int> { }

    [Singleton(typeof(IDITest<,>))]
    [Singleton(typeof(IDITest<,>), key: nameof(DITest))]
    [Singleton(typeof(IDITest<,>), isEnumerable: true)]
    [Singleton(typeof(IDITest<,>), key: nameof(DITest), isEnumerable: true)]
    [Scoped(typeof(IDITest<,>))]
    [Scoped(typeof(IDITest<,>), key: nameof(DITest))]
    [Scoped(typeof(IDITest<,>), isEnumerable: true)]
    [Scoped(typeof(IDITest<,>), key: nameof(DITest), isEnumerable: true)]
    [Transient(typeof(IDITest<,>))]
    [Transient(typeof(IDITest<,>), key: nameof(DITest))]
    [Transient(typeof(IDITest<,>), isEnumerable: true)]
    [Transient(typeof(IDITest<,>), key: nameof(DITest), isEnumerable: true)]
    internal class DITest<T1, T2> : IDITest, IDITest<T1, T2> { }
}";

        private static readonly string _classSource = @"using Microsoft.Extensions.DependencyInjection.Attributes;

namespace DependencyInjectionTest
{
    [Singleton]
    [Singleton(key: nameof(EmptyDITest))]
    [Singleton(isEnumerable: true)]
    [Singleton(key: nameof(EmptyDITest), isEnumerable: true)]
    [Scoped]
    [Scoped(key: nameof(EmptyDITest))]
    [Scoped(isEnumerable: true)]
    [Scoped(key: nameof(EmptyDITest), isEnumerable: true)]
    [Transient]
    [Transient(key: nameof(EmptyDITest))]
    [Transient(isEnumerable: true)]
    [Transient(key: nameof(EmptyDITest), isEnumerable: true)]
    internal class EmptyDITest { }

    [Singleton]
    [Singleton(key: nameof(EmptyDITest))]
    [Singleton(isEnumerable: true)]
    [Singleton(key: nameof(EmptyDITest), isEnumerable: true)]
    [Scoped]
    [Scoped(key: nameof(EmptyDITest))]
    [Scoped(isEnumerable: true)]
    [Scoped(key: nameof(EmptyDITest), isEnumerable: true)]
    [Transient]
    [Transient(key: nameof(EmptyDITest))]
    [Transient(isEnumerable: true)]
    [Transient(key: nameof(EmptyDITest), isEnumerable: true)]
    internal class EmptyDITest<T1, T2> { }
}";

        private static readonly string _hostedClassSource = @"using Microsoft.Extensions.DependencyInjection.Attributes;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DependencyInjectionTest
{
    [HostedService]
    internal class DITestHostedService : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            {
                throw new NotImplementedException();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            {
                throw new NotImplementedException();
            }
        }
    }
}";

        private static readonly string _assemblyAttributes = $@"using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Attributes;
using DependencyInjectionTest.Abstractions;
using DependencyInjectionTest;

[assembly: HostedService<DITestHostedService>]
{GetZeroTypeArgumentsAssemblyAttributes("Singleton")}
{GetOneTypeArgumentsAssemblyAttributes("Singleton")}
{GetTwoTypeArgumentsAssemblyAttributes("Singleton")}
{GetZeroTypeArgumentsAssemblyAttributes("Scoped")}
{GetOneTypeArgumentsAssemblyAttributes("Scoped")}
{GetTwoTypeArgumentsAssemblyAttributes("Scoped")}
{GetZeroTypeArgumentsAssemblyAttributes("Transient")}
{GetOneTypeArgumentsAssemblyAttributes("Transient")}
{GetTwoTypeArgumentsAssemblyAttributes("Transient")}";

        private static readonly IEnumerable<string> _sources
            = [_interfaceSource, _inheritedClassSource, _classSource,
                _hostedClassSource, _assemblyAttributes];

        [Fact]
        public Task DITestDependencyInjectionAsync()
            => helper.VerifyAsync(_sources);

        private static string GetZeroTypeArgumentsAssemblyAttributes(string prefix)
            => $@"[assembly: {prefix}(typeof(EmptyDITest))]
[assembly: {prefix}(typeof(EmptyDITest<string, int>))]
[assembly: {prefix}(typeof(EmptyDITest<,>))]
[assembly: {prefix}(typeof(EmptyDITest), key: nameof(EmptyDITest))]
[assembly: {prefix}(typeof(EmptyDITest<string, int>), key: nameof(EmptyDITest))]
[assembly: {prefix}(typeof(EmptyDITest<,>), key: nameof(EmptyDITest))]
[assembly: {prefix}(typeof(IDITest), typeof(DITest))]
[assembly: {prefix}(typeof(IDITest<string, int>), typeof(DITest))]
[assembly: {prefix}(typeof(IDITest), typeof(DITest<string, int>))]
[assembly: {prefix}(typeof(IDITest<string, int>), typeof(DITest<string, int>))]
[assembly: {prefix}(typeof(IDITest<,>), typeof(DITest<,>))]
[assembly: {prefix}(typeof(IDITest), typeof(DITest), nameof(DITest))]
[assembly: {prefix}(typeof(IDITest<string, int>), typeof(DITest), nameof(DITest))]
[assembly: {prefix}(typeof(IDITest), typeof(DITest<string, int>), nameof(DITest))]
[assembly: {prefix}(typeof(IDITest<string, int>), typeof(DITest<string, int>), nameof(DITest))]
[assembly: {prefix}(typeof(IDITest<,>), typeof(DITest<,>), nameof(DITest))]
[assembly: {prefix}(typeof(IDITest), typeof(DITest), isEnumerable: true)]
[assembly: {prefix}(typeof(IDITest<string, int>), typeof(DITest), isEnumerable: true)]
[assembly: {prefix}(typeof(IDITest), typeof(DITest<string, int>), isEnumerable: true)]
[assembly: {prefix}(typeof(IDITest<string, int>), typeof(DITest<string, int>), isEnumerable: true)]
[assembly: {prefix}(typeof(IDITest<,>), typeof(DITest<,>), isEnumerable: true)]
[assembly: {prefix}(typeof(IDITest), typeof(DITest), nameof(DITest), true)]
[assembly: {prefix}(typeof(IDITest<string, int>), typeof(DITest), nameof(DITest), true)]
[assembly: {prefix}(typeof(IDITest), typeof(DITest<string, int>), nameof(DITest), true)]
[assembly: {prefix}(typeof(IDITest<string, int>), typeof(DITest<string, int>), nameof(DITest), true)]
[assembly: {prefix}(typeof(IDITest<,>), typeof(DITest<,>), nameof(DITest), true)]";

        private static string GetOneTypeArgumentsAssemblyAttributes(string prefix)
            => $@"[assembly: {prefix}<EmptyDITest>]
[assembly: {prefix}<EmptyDITest<string, int>>]
[assembly: {prefix}<EmptyDITest>(key: nameof(EmptyDITest))]
[assembly: {prefix}<EmptyDITest<string, int>>(key: nameof(EmptyDITest))]
[assembly: {prefix}<IDITest>(typeof(DITest))]
[assembly: {prefix}<IDITest<string, int>>(typeof(DITest))]
[assembly: {prefix}<IDITest>(typeof(DITest<string, int>))]
[assembly: {prefix}<IDITest<string, int>>(typeof(DITest<string, int>))]
[assembly: {prefix}<IDITest>(typeof(DITest), nameof(DITest))]
[assembly: {prefix}<IDITest<string, int>>(typeof(DITest), nameof(DITest))]
[assembly: {prefix}<IDITest>(typeof(DITest<string, int>), nameof(DITest))]
[assembly: {prefix}<IDITest<string, int>>(typeof(DITest<string, int>), nameof(DITest))]
[assembly: {prefix}<IDITest>(typeof(DITest), isEnumerable: true)]
[assembly: {prefix}<IDITest<string, int>>(typeof(DITest), isEnumerable: true)]
[assembly: {prefix}<IDITest>(typeof(DITest<string, int>), isEnumerable: true)]
[assembly: {prefix}<IDITest<string, int>>(typeof(DITest<string, int>), isEnumerable: true)]
[assembly: {prefix}<IDITest>(typeof(DITest), nameof(DITest), true)]
[assembly: {prefix}<IDITest<string, int>>(typeof(DITest), nameof(DITest), true)]
[assembly: {prefix}<IDITest>(typeof(DITest<string, int>), nameof(DITest), true)]
[assembly: {prefix}<IDITest<string, int>>(typeof(DITest<string, int>), nameof(DITest), true)]";

        private static string GetTwoTypeArgumentsAssemblyAttributes(string prefix)
            => $@"[assembly: {prefix}<IDITest, DITest>]
[assembly: {prefix}<IDITest<string, int>, DITest>]
[assembly: {prefix}<IDITest, DITest<string, int>>]
[assembly: {prefix}<IDITest<string, int>, DITest<string, int>>]
[assembly: {prefix}<IDITest, DITest>(nameof(DITest))]
[assembly: {prefix}<IDITest<string, int>, DITest>(nameof(DITest))]
[assembly: {prefix}<IDITest, DITest<string, int>>(nameof(DITest))]
[assembly: {prefix}<IDITest<string, int>, DITest<string, int>>(nameof(DITest))]
[assembly: {prefix}<IDITest, DITest>(isEnumerable: true)]
[assembly: {prefix}<IDITest<string, int>, DITest>(isEnumerable: true)]
[assembly: {prefix}<IDITest, DITest<string, int>>(isEnumerable: true)]
[assembly: {prefix}<IDITest<string, int>, DITest<string, int>>(isEnumerable: true)]
[assembly: {prefix}<IDITest, DITest>(nameof(DITest), true)]
[assembly: {prefix}<IDITest<string, int>, DITest>(nameof(DITest), true)]
[assembly: {prefix}<IDITest, DITest<string, int>>(nameof(DITest), true)]
[assembly: {prefix}<IDITest<string, int>, DITest<string, int>>(nameof(DITest), true)]";
    }
}
