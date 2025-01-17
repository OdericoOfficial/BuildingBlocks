using System.Collections.Immutable;
using BuildingBlocks.SourceGenerators.Sources;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BuildingBlocks.SourceGenerators.Providers
{
    internal static class ClassTargetInjectProvider
    {
        private static readonly ImmutableArray<string> _names
            = ["HostedService", "HostedServiceAttribute",
                "SingletonClass", "SingletonClassAttribute",
                "ScopedClass", "ScopedClassAttribute",
                "TransientClass", "TransientClassAttribute",];

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
                .Select(item => item.AttributeClass!.Name switch
                {
                    "SingletonClassAttribute" => GetServiceSource(classSymbol, item, 0),
                    "ScopedClassAttribute" => GetServiceSource(classSymbol, item, 1),
                    "TransientClassAttribute" => GetServiceSource(classSymbol, item, 2),
                    "HostedServiceAttribute" => GetHostedSource(classSymbol, item),
                    _ => default
                });
        }

        private static DependencyInjectionSource GetServiceSource(INamedTypeSymbol classSymbol, AttributeData data, int lifetime)
            => new DependencyInjectionSource
            {
                Lifetime = lifetime,
                ServiceName = data.GetServiceName(classSymbol),
                ImplementationName = data.GetImplementationName(classSymbol),
                Key = data.GetKey(),
                IsEnumerable = data.GetIsEnumerableClass(),
                IsHosted = false
            };

        private static DependencyInjectionSource? GetHostedSource(INamedTypeSymbol classSymbol, AttributeData data)
            => data.AttributeClass!.TypeArguments.Length == 0 ? new DependencyInjectionSource
            {
                ServiceName = classSymbol.ToDisplayString(),
                IsHosted = true
            } : default;
    }
}