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
                    ServiceName = (data.ConstructorArguments[0].Value as INamedTypeSymbol)?.ToDisplayString() ??
                        (classSymbol.IsGenericType ? classSymbol.ConstructUnboundGenericType().ToDisplayString() : classSymbol.ToDisplayString()),
                    ImplementationName = data.ConstructorArguments[0].Value as INamedTypeSymbol is not null ? 
                        classSymbol.IsGenericType ? classSymbol.ConstructUnboundGenericType().ToDisplayString() : classSymbol.ToDisplayString() : string.Empty,
                    Key = $"\"{data.ConstructorArguments[2].Value as string ?? string.Empty}\"",
                    IsEnumerable = data.ConstructorArguments[0].Value as INamedTypeSymbol is not null
                        && Convert.ToBoolean(data.ConstructorArguments[3].Value),
                    IsHosted = false
                },
                1 => new DependencyInjectionSource
                {
                    Lifetime = lifetime,
                    ServiceName = data.AttributeClass.TypeArguments[0].ToDisplayString(),
                    ImplementationName = classSymbol.ToDisplayString(),
                    Key = $"\"{data.ConstructorArguments[1].Value as string ?? string.Empty}\"",
                    IsEnumerable = Convert.ToBoolean(data.ConstructorArguments[2].Value),
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
    }
}