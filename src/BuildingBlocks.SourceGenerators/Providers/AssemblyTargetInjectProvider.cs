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
                && (item.AttributeClass.Name == "SingletonAttribute"
                    || item.AttributeClass.Name == "ScopedAttribute"
                    || item.AttributeClass.Name == "TransientAttribute"
                    || item.AttributeClass.Name == "HostedServiceAttribute"))
                .Select(item => item.AttributeClass!.Name switch
                {
                    "SingletonAttribute" => GetSingletonSource(item),
                    "ScopedAttribute" => GetScopedSource(item),
                    "TransientAttribute" => GetTransientSource(item),
                    "HostedServiceAttribute" => GetHostedSource(item),
                    _ => default
                });
        }

        private static DependencyInjectionSource? GetSingletonSource(AttributeData data)
            => GetServiceSource(data, 0);

        private static DependencyInjectionSource? GetScopedSource(AttributeData data)
            => GetServiceSource(data, 1);

        private static DependencyInjectionSource? GetTransientSource(AttributeData data)
            => GetServiceSource(data, 2);

        private static DependencyInjectionSource? GetServiceSource(AttributeData data, int lifetime)
            => data.AttributeClass!.TypeArguments.Length switch
            {
                0 => new DependencyInjectionSource
                {
                    Lifetime = lifetime,
                    ServiceName = GetServiceName(data),
                    ImplementationName = GetImplementationName(data),
                    Key = GetKey(data),
                    IsEnumerable = GetIsEnumerable(data),
                    IsHosted = false
                },
                1 => new DependencyInjectionSource
                {
                    Lifetime = lifetime,
                    ServiceName = data.AttributeClass.TypeArguments[0].ToDisplayString(),
                    ImplementationName = GetImplementationName(data),
                    Key = GetKey(data),
                    IsEnumerable = GetIsEnumerable(data),
                    IsHosted = false
                },
                2 => new DependencyInjectionSource
                {
                    Lifetime = lifetime,
                    ServiceName = data.AttributeClass.TypeArguments[0].ToDisplayString(),
                    ImplementationName = data.AttributeClass.TypeArguments[1].ToDisplayString(),
                    Key = GetKey(data),
                    IsEnumerable = GetIsEnumerable(data),
                    IsHosted = false
                },
                _ => default
            };

        private static DependencyInjectionSource? GetHostedSource(AttributeData data)
            => data.AttributeClass!.TypeArguments.Length == 1 ? new DependencyInjectionSource
            {
                ServiceName = data.AttributeClass.TypeArguments[0].ToDisplayString(),
                IsHosted = true
            } : default;

        private static string GetServiceName(AttributeData data)
        {
            if (!data.NamedArguments.Any(item => item.Key == "serviceType"))
                return string.Empty;
            return (data.NamedArguments.First(item => item.Key == "serviceType").Value.Value as INamedTypeSymbol)?.ToDisplayString() ?? string.Empty;
        }

        private static string GetImplementationName(AttributeData data)
        {
            if (!data.NamedArguments.Any(item => item.Key == "implementationType"))
                return string.Empty;
            return (data.NamedArguments.First(item => item.Key == "implementationType").Value.Value as INamedTypeSymbol)?.ToDisplayString() ?? string.Empty;
        }

        private static string GetKey(AttributeData data)
        {
            if (!data.NamedArguments.Any(item => item.Key == "key"))
                return string.Empty;
            return (data.NamedArguments.First(item => item.Key == "key").Value.Value as string) ?? string.Empty;
        }

        private static bool GetIsEnumerable(AttributeData data)
            => data.NamedArguments.Any(item => item.Key == "isEnumerable")
                && Convert.ToBoolean(data.NamedArguments.First(item => item.Key == "isEnumerable").Value.Value);
    }
}
