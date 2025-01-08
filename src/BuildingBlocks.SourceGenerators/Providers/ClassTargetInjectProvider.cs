using System.Collections.Immutable;
using BuildingBlocks.SourceGenerators.Sources;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BuildingBlocks.SourceGenerators.Providers
{
    internal static class ClassTargetInjectProvider
    {
        private static readonly ImmutableArray<string> _names 
            = ["Service", "ServiceAttribute",
            "HostedService", "HostedServiceAttribute",
            "Singleton", "SingletonAttribute",
            "Scoped", "ScopedAttribute",
            "Transient", "TransientAttribute",];

        public static bool Predicate(SyntaxNode node, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return node is ClassDeclarationSyntax classDeclaration
                && classDeclaration.AttributeLists.SelectMany(attributeList => attributeList.Attributes)
                    .Any(attribute =>
                    {
                        var name = attribute.Name.ToString();
                        return _names.Any(item => name.StartsWith(item));
                    });
        }

        public static IEnumerable<DependencyInjectionSource?> Transform(GeneratorSyntaxContext context, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var classSymbol = (INamedTypeSymbol)context.SemanticModel.GetDeclaredSymbol(context.Node, cancellationToken)!;
            return classSymbol.GetAttributes()
                .Where(item => item.AttributeClass is not null
                    && item.AttributeClass.TypeKind != TypeKind.Error)
                .Select(item => GetSource(classSymbol, item));
        }

        private static DependencyInjectionSource? GetSource(INamedTypeSymbol classSymbol, AttributeData data)
            => data.AttributeClass!.Name switch
            {
                "SingletonAttribute" => GetSingletonSource(classSymbol, data),
                "ScopedAttribute" => GetScopedSource(classSymbol, data),
                "TransientAttribute" => GetTransientSource(classSymbol, data),
                "HostedServiceAttribute" => GetHostedSource(classSymbol, data),
                _ => default
            };

        private static DependencyInjectionSource? GetSingletonSource(INamedTypeSymbol classSymbol, AttributeData data)
            => GetServiceSource(classSymbol, data, 0);

        private static DependencyInjectionSource? GetScopedSource(INamedTypeSymbol classSymbol, AttributeData data)
            => GetServiceSource(classSymbol, data, 1);

        private static DependencyInjectionSource? GetTransientSource(INamedTypeSymbol classSymbol, AttributeData data)
            => GetServiceSource(classSymbol, data, 2);

        private static DependencyInjectionSource? GetServiceSource(INamedTypeSymbol classSymbol, AttributeData data, int lifetime)
            => data.AttributeClass!.TypeArguments.Length switch
            {
                0 => new DependencyInjectionSource
                {
                    Lifetime = lifetime,
                    ServiceName = GetServiceName(classSymbol, data),
                    ImplementationName = GetImplementationName(classSymbol, data),
                    Key = GetKey(data),
                    IsEnumerable = GetIsEnumerableZeroTypeArgument(data),
                    IsHosted = false
                },
                1 => new DependencyInjectionSource
                {
                    Lifetime = lifetime,
                    ServiceName = data.AttributeClass.TypeArguments[0].ToDisplayString(),
                    ImplementationName = classSymbol.ToDisplayString(),
                    Key = GetKey(data),
                    IsEnumerable = GetIsEnumerableOneTypeArgument(data),
                    IsHosted = false
                },
                _ => default
            };

        private static DependencyInjectionSource? GetHostedSource(INamedTypeSymbol classSymbol, AttributeData data)
            => data.AttributeClass!.TypeArguments.Length == 0 ? new DependencyInjectionSource
            {
                ServiceName = classSymbol.ToDisplayString(),
                IsHosted = true
            } : default;

        private static string GetServiceName(INamedTypeSymbol classSymbol, AttributeData data)
        {
            if (!data.NamedArguments.Any(item => item.Key == "serviceType"))
                return (classSymbol.IsGenericType ? classSymbol.ConstructUnboundGenericType().ToDisplayString() : classSymbol.ToDisplayString());
            return (data.NamedArguments.First(item => item.Key == "serviceType").Value.Value as INamedTypeSymbol)?.ToDisplayString() ??
                (classSymbol.IsGenericType ? classSymbol.ConstructUnboundGenericType().ToDisplayString() : classSymbol.ToDisplayString());
        }

        private static string GetImplementationName(INamedTypeSymbol classSymbol, AttributeData data)
        {
            if (!data.NamedArguments.Any(item => item.Key == "serviceType"))
                return string.Empty;
            return classSymbol.IsGenericType ? classSymbol.ConstructUnboundGenericType().ToDisplayString() : classSymbol.ToDisplayString();
        }

        private static string GetKey(AttributeData data)
        {
            if (!data.NamedArguments.Any(item => item.Key == "key"))
                return string.Empty;
            return data.NamedArguments.First(item => item.Key == "key").Value.Value as string ?? string.Empty;
        }

        private static bool GetIsEnumerableZeroTypeArgument(AttributeData data)
            => data.NamedArguments.Any(item => item.Key == "isEnumerable")
                && Convert.ToBoolean(data.NamedArguments.First(item => item.Key == "isEnumerable").Value.Value)
                && data.NamedArguments.Any(item => item.Key == "serviceType")
                && data.NamedArguments.First(item => item.Key == "serviceType").Value.Value is INamedTypeSymbol;

        private static bool GetIsEnumerableOneTypeArgument(AttributeData data)
            => data.NamedArguments.Any(item => item.Key == "isEnumerable")
                && Convert.ToBoolean(data.NamedArguments.First(item => item.Key == "isEnumerable").Value.Value);
    }
}
