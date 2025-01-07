using BuildingBlocks.SourceGenerators.Sources;
using Microsoft.CodeAnalysis;

namespace BuildingBlocks.SourceGenerators.Providers
{
    internal static class AssemblyTargetInjectProvider
    {
        public static IEnumerable<DependencyInjectionSource> Transform(Compilation compilation, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return compilation.Assembly.GetAttributes()
                .Where(item => item.AttributeClass is not null
                && (item.AttributeClass.Name == "SingletonAttribute"
                    || item.AttributeClass.Name == "ScopedAttribute"
                    || item.AttributeClass.Name == "TransientAttribute"
                    || item.AttributeClass.Name == "ServiceAttribute"
                    || item.AttributeClass.Name == "HostedServiceAttribute"))
                .Select(item => item.AttributeClass!.Name switch
                {
                    "SingletonAttribute" => GetSingletonSource(item),
                    "ScopedAttribute" => GetScopedSource(item),
                    "TransientAttribute" => GetTransientSource(item),
                    "ServiceAttribute" => GetServiceSource(item),
                    "HostedServiceAttribute" => GetHostedSource(item),
                    _ => default
                });
        }

        private static DependencyInjectionSource GetSingletonSource(AttributeData data)
            => GetServiceSource(data, 0);

        private static DependencyInjectionSource GetScopedSource(AttributeData data)
            => GetServiceSource(data, 1);

        private static DependencyInjectionSource GetTransientSource(AttributeData data)
            => GetServiceSource(data, 2);

        private static DependencyInjectionSource GetServiceSource(AttributeData data, int lifetime)
            => data.AttributeClass!.TypeArguments.Length switch
            {
                0 => new DependencyInjectionSource
                {
                    Lifetime = lifetime,
                    ServiceName = (data.ConstructorArguments[0].Value as INamedTypeSymbol)?.ToDisplayString() ?? string.Empty,
                    ImplementationName = (data.ConstructorArguments[1].Value as INamedTypeSymbol)?.ToDisplayString() ?? string.Empty,
                    Key = data.ConstructorArguments[2].Value as string ?? string.Empty,
                    IsEnumerable = Convert.ToBoolean(data.ConstructorArguments[3].Value ?? false),
                    IsHosted = false
                },
                1 => new DependencyInjectionSource
                {
                    Lifetime = lifetime,
                    ServiceName = data.AttributeClass.TypeArguments[0].ToDisplayString(),
                    ImplementationName = (data.ConstructorArguments[0].Value as INamedTypeSymbol)?.ToDisplayString() ?? string.Empty,
                    Key = data.ConstructorArguments[1].Value as string ?? string.Empty,
                    IsEnumerable = Convert.ToBoolean(data.ConstructorArguments[2].Value ?? false),
                    IsHosted = false
                },
                2 => new DependencyInjectionSource
                {
                    Lifetime = lifetime,
                    ServiceName = data.AttributeClass.TypeArguments[0].ToDisplayString(),
                    ImplementationName = data.AttributeClass.TypeArguments[1].ToDisplayString(),
                    Key = data.ConstructorArguments[0].Value as string ?? string.Empty,
                    IsEnumerable = Convert.ToBoolean(data.ConstructorArguments[1].Value ?? false),
                    IsHosted = false
                },
                _ => default
            };

        private static DependencyInjectionSource GetServiceSource(AttributeData data)
            => new DependencyInjectionSource
            {
                Lifetime = Convert.ToInt32(data.ConstructorArguments[0].Value ?? 3),
                ServiceName = (data.ConstructorArguments[1].Value as INamedTypeSymbol)?.ToDisplayString() ?? string.Empty,
                ImplementationName = (data.ConstructorArguments[2].Value as INamedTypeSymbol)?.ToDisplayString() ?? string.Empty,
                Key = data.ConstructorArguments[3].Value as string ?? string.Empty,
                IsEnumerable = Convert.ToBoolean(data.ConstructorArguments[4].Value ?? false),
                IsHosted = false
            };

        private static DependencyInjectionSource GetHostedSource(AttributeData data)
            => data.AttributeClass!.TypeArguments.Length == 1 ? new DependencyInjectionSource
            {
                ServiceName = data.AttributeClass.TypeArguments[0].ToDisplayString(),
                IsHosted = true
            } : default;
    }
}
