using System.Collections.Immutable;
using BuildingBlocks.SourceGeneratorsTest.Helpers.Abstractions;

namespace BuildingBlocks.SourceGeneratorsTest
{
    public class DependencyInjectionTest
    {
        private static readonly ImmutableArray<string> _lifetime
            = ["Singleton", "Scoped", "Transient", "SingletonAttribute", "ScopedAttribute", "TransientAttribute"];

        private static readonly ImmutableArray<string> _service
            = ["Service", "ServiceAttribute"];

        private static readonly ImmutableArray<string> _hosted
            = ["HostedService", "HostedServiceAttribute"];

        private static readonly string _assemblyAttributeNamespaces = @"using DependencyInjectionTest.Abstractions;
using DependencyInjectionTest;
using Microsoft.Extensions.DependencyInjection;";

        private static readonly string _interfaceName = "ITest";
        private static readonly string _className = "Test";
        private static readonly string _genericInterfaceName = "ITest<T1, T2, T3>";
        private static readonly string _genericClassName = "Test<T1, T2, T3>";
        private static readonly string _hostedClassName = "TestService";
        private static readonly string _key = "\"Test\"";

        private readonly IDependencyInjectionVerifyHelper _helper;

        public DependencyInjectionTest(IDependencyInjectionVerifyHelper helper)
        {
            _helper = helper;
        }

        [Fact]
        public async Task TestServiceAssemblyAsync()
        {
            for (var i = 0; i < 3; i++)
            {
                foreach (var item in _service)
                {
                    #region Normal
                    var attributeSource = $@"{_assemblyAttributeNamespaces}

[assembly: {item}(ServiceLifetime.{_lifetime[i]}, typeof({_className}))]";
                    var classSource = GetClassSource(_className);
                    await _helper.VerifyAsync([attributeSource, classSource]);

                    attributeSource = $@"{_assemblyAttributeNamespaces}

[assembly: {item}(ServiceLifetime.{_lifetime[i]}, typeof({_interfaceName}), typeof({_className}))]";
                    var interfaceSource = GetInterfaceSource(_interfaceName);
                    classSource = GetInheritedClassSource(_interfaceName, _className);
                    await _helper.VerifyAsync([attributeSource, interfaceSource, classSource]);
                    #endregion

                    #region Keyed
                    attributeSource = $@"{_assemblyAttributeNamespaces}

[assembly: {item}(ServiceLifetime.{_lifetime[i]}, typeof({_className}), key: {_key})]";
                    classSource = GetClassSource(_className);
                    await _helper.VerifyAsync([attributeSource, classSource]);

                    attributeSource = $@"{_assemblyAttributeNamespaces}

[assembly: {item}(ServiceLifetime.{_lifetime[i]}, typeof({_interfaceName}), typeof({_className}), {_key})]";
                    interfaceSource = GetInterfaceSource(_interfaceName);
                    classSource = GetInheritedClassSource(_interfaceName, _className);
                    await _helper.VerifyAsync([attributeSource, interfaceSource, classSource]);
                    #endregion

                    #region Enumerable
                    attributeSource = $@"{_assemblyAttributeNamespaces}

[assembly: {item}(ServiceLifetime.{_lifetime[i]}, typeof({_interfaceName}), typeof({_className}), isEnumerable: true)]";
                    interfaceSource = GetInterfaceSource(_interfaceName);
                    classSource = GetInheritedClassSource(_interfaceName, _className);
                    await _helper.VerifyAsync([attributeSource, interfaceSource, classSource]);
                    #endregion

                    #region KeyedEnumerable
                    attributeSource = $@"{_assemblyAttributeNamespaces}

[assembly: {item}(ServiceLifetime.{_lifetime[i]}, typeof({_interfaceName}), typeof({_className}), {_key}, true)]";
                    interfaceSource = GetInterfaceSource(_interfaceName);
                    classSource = GetInheritedClassSource(_interfaceName, _className);
                    await _helper.VerifyAsync([attributeSource, interfaceSource, classSource]);
                    #endregion
                }
            }
        }

        [Fact]
        public void TestLifetimeAssembly()
        {

        }

        [Fact]
        public void TestHostedAssembly()
        {

        }

        [Fact]
        public async Task TestServiceClassAsync()
        {
            for (var i = 0; i < 3; i++)
            {
                foreach (var item in _service)
                {
                    #region Normal
                    var attributeSource = $"[{item}(ServiceLifetime.{_lifetime[i]})]";
                    var classSource = GetClassSource(_className, attributeSource);
                    await _helper.VerifyAsync([classSource]);

                    attributeSource = $"[{item}(ServiceLifetime.{_lifetime[i]}, typeof({_interfaceName}))]";
                    var interfaceSource = GetInterfaceSource(_interfaceName);
                    classSource = GetInheritedClassSource(_interfaceName, _className, attributeSource);
                    await _helper.VerifyAsync([interfaceSource, classSource]);
                    #endregion

                    #region Keyed
                    attributeSource = $"[{item}(ServiceLifetime.{_lifetime[i]}, key: {_key})]";
                    classSource = GetClassSource(_className, attributeSource);
                    await _helper.VerifyAsync([classSource]);

                    attributeSource = $"[{item}(ServiceLifetime.{_lifetime[i]}, typeof({_interfaceName}), key: {_key})]";
                    interfaceSource = GetInterfaceSource(_interfaceName);
                    classSource = GetInheritedClassSource(_interfaceName, _className, attributeSource);
                    await _helper.VerifyAsync([interfaceSource, classSource]);
                    #endregion

                    #region Enumerable
                    attributeSource = $"[{item}(ServiceLifetime.{_lifetime[i]}, typeof({_interfaceName}), isEnumerable: true)]";
                    interfaceSource = GetInterfaceSource(_interfaceName);
                    classSource = GetInheritedClassSource(_interfaceName, _className);
                    await _helper.VerifyAsync([interfaceSource, classSource]);
                    #endregion

                    #region KeyedEnumerable
                    attributeSource = $"[{item}(ServiceLifetime.{_lifetime[i]}, typeof({_interfaceName}), key: {_key}, isEnumerable: true)]";
                    interfaceSource = GetInterfaceSource(_interfaceName);
                    classSource = GetInheritedClassSource(_interfaceName, _className);
                    await _helper.VerifyAsync([attributeSource, interfaceSource, classSource]);
                    #endregion
                }
            }
        }

        [Fact]
        public void TestLifetimeClass()
        {

        }

        [Fact]
        public void TestHostedClass()
        {

        }

        private string GetInterfaceSource(string interfaceName)
            => $@"using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjectionTest.Abstractions
{{
    public interface {interfaceName} {{ }}
}}";

        private string GetInheritedClassSource(string interfaceName, string className, string? attribute = null)
            => $@"using Microsoft.Extensions.DependencyInjection;
using DependencyInjectionTest.Abstractions;

namespace DependencyInjectionTest
{{
    {attribute ?? string.Empty}
    internal class {className} : {interfaceName} {{ }}
}}";

        private string GetClassSource(string className, string? attribute = null)
            => $@"using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjectionTest
{{
    {attribute ?? string.Empty}
    internal class {className} {{ }}
}}";

        private string GetHostedClassSource(string className, string? attribute = null)
            => $@"using DependencyInjectionTest.Abstractions;
using Microsoft.Extensions.Hosting;

namespace DependencyInjectionTest
{{
    {attribute ?? string.Empty}
    internal class {className} : IHostedService
    {{
        public Task StartAsync(CancellationToken cancellationToken)
        {{
            throw new NotImplementedException();
        }}

        public Task StopAsync(CancellationToken cancellationToken)
        {{
            throw new NotImplementedException();
        }}
    }}
}}";
    }
}
