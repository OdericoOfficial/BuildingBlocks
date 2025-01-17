using BuildingBlocks.SourceGenerators.Sources;
using Microsoft.CodeAnalysis;

namespace BuildingBlocks.SourceGenerators.Providers
{
    internal static class AssemblyTargetInjectProvider
    {
        public static IEnumerable<DependencyInjectionSource?> Transform(Compilation compilation, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return compilation.Assembly.GetAttributes()
                .Where(item => item.AttributeClass is not null
                && item.AttributeClass.TypeKind != TypeKind.Error
                && (item.AttributeClass.Name == "SingletonAssemblyAttribute"
                    || item.AttributeClass.Name == "ScopedAssemblyAttribute"
                    || item.AttributeClass.Name == "TransientAssemblyAttribute"
                    || item.AttributeClass.Name == "HostedServiceAttribute"))
                .Select(item => item.AttributeClass!.Name switch
                {
                    "SingletonAssemblyAttribute" => GetServiceSource(item, 0),
                    "ScopedAssemblyAttribute" => GetServiceSource(item, 1),
                    "TransientAssemblyAttribute" => GetServiceSource(item, 2),
                    "HostedServiceAttribute" => GetHostedSource(item),
                    _ => default
                });
        }

        private static DependencyInjectionSource GetServiceSource(AttributeData data, int lifetime)
            => new DependencyInjectionSource
            {
                Lifetime = lifetime,
                ServiceName = data.GetServiceName(),
                ImplementationName = data.GetImplementationName(),
                Key = data.GetKey(),
                IsEnumerable = data.GetIsEnumerableAssembly(),
                IsHosted = false
            };

        private static DependencyInjectionSource? GetHostedSource(AttributeData data)
            => data.AttributeClass!.TypeArguments.Length == 1 ? new DependencyInjectionSource
            {
                ServiceName = data.AttributeClass.TypeArguments[0].ToDisplayString(),
                IsHosted = true
            } : default;
    }
}