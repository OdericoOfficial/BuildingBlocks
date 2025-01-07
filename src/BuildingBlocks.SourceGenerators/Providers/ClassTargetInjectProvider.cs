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
            return node is AttributeSyntax attribute
                && node.Parent is not null
                && node.Parent.Parent is not null
                && node.Parent.Parent is ClassDeclarationSyntax
                && _names.Any(item => attribute.Name.ToString().StartsWith(item));
        }

        public static DependencyInjectionSource Transform(GeneratorSyntaxContext context, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var classSymbol = (INamedTypeSymbol)context.SemanticModel.GetDeclaredSymbol(context.Node.Parent!.Parent!, cancellationToken)!;
            var attributeSymbol = (INamedTypeSymbol)context.SemanticModel.GetTypeInfo(context.Node, cancellationToken).Type!;
            var data = classSymbol.GetAttributes()
                .FirstOrDefault(item => item.AttributeClass is not null
                    && item.AttributeClass.Name == attributeSymbol.Name);
            return data is not null ? GetSource(classSymbol, data) : default;
        }

        private static DependencyInjectionSource GetSource(INamedTypeSymbol classSymbol, AttributeData data)
            => data.AttributeClass!.Name switch
            {
                "SingletonAttribute" => GetSingletonSource(classSymbol, data),
                "ScopedAttribute" => GetScopedSource(classSymbol, data),
                "TransientAttribute" => GetTransientSource(classSymbol, data),
                "ServiceAttribute" => GetServiceSource(classSymbol, data),
                "HostedServiceAttribute" => GetHostedSource(classSymbol, data),
                _ => default
            };

        private static DependencyInjectionSource GetSingletonSource(INamedTypeSymbol classSymbol, AttributeData data)
            => GetServiceSource(classSymbol, data, 0);

        private static DependencyInjectionSource GetScopedSource(INamedTypeSymbol classSymbol, AttributeData data)
            => GetServiceSource(classSymbol, data, 1);

        private static DependencyInjectionSource GetTransientSource(INamedTypeSymbol classSymbol, AttributeData data)
            => GetServiceSource(classSymbol, data, 2);

        private static DependencyInjectionSource GetServiceSource(INamedTypeSymbol classSymbol, AttributeData data, int lifetime)
            => data.AttributeClass!.TypeArguments.Length switch
            {
                0 => new DependencyInjectionSource
                {
                    Lifetime = lifetime,
                    ServiceName = (data.ConstructorArguments[0].Value as INamedTypeSymbol)?.ToDisplayString() ??
                        (classSymbol.IsGenericType ? classSymbol.ConstructUnboundGenericType().ToDisplayString() : classSymbol.ToDisplayString()),
                    ImplementationName = data.ConstructorArguments[0].Value is null ? string.Empty :
                        classSymbol.IsGenericType ? classSymbol.ConstructUnboundGenericType().ToDisplayString() : classSymbol.ToDisplayString(),
                    Key = data.ConstructorArguments[2].Value as string ?? string.Empty,
                    IsEnumerable = Convert.ToBoolean(data.ConstructorArguments[3].Value ?? false) 
                        && data.ConstructorArguments[0].Value is not null,
                    IsHosted = false
                },
                1 => new DependencyInjectionSource
                {
                    Lifetime = lifetime,
                    ServiceName = data.AttributeClass.TypeArguments[0].ToDisplayString(),
                    ImplementationName = classSymbol.ToDisplayString(),
                    Key = data.ConstructorArguments[1].Value as string ?? string.Empty,
                    IsEnumerable = Convert.ToBoolean(data.ConstructorArguments[2].Value ?? false),
                    IsHosted = false
                },
                _ => default
            };

        private static DependencyInjectionSource GetServiceSource(INamedTypeSymbol classSymbol, AttributeData data)
            => new DependencyInjectionSource
            {
                Lifetime = Convert.ToInt32(data.ConstructorArguments[0].Value ?? 3),
                ServiceName = (data.ConstructorArguments[1].Value as INamedTypeSymbol)?.ToDisplayString() ?? 
                    (classSymbol.IsGenericType ? classSymbol.ConstructUnboundGenericType().ToDisplayString() : classSymbol.ToDisplayString()),
                ImplementationName = data.ConstructorArguments[1].Value is null ? string.Empty :
                    classSymbol.IsGenericType ? classSymbol.ConstructUnboundGenericType().ToDisplayString() : classSymbol.ToDisplayString(),
                Key = data.ConstructorArguments[3].Value as string ?? string.Empty,
                IsEnumerable = Convert.ToBoolean(data.ConstructorArguments[4].Value ?? false)
                    && data.ConstructorArguments[1].Value is not null,
                IsHosted = false
            };

        private static DependencyInjectionSource GetHostedSource(INamedTypeSymbol classSymbol, AttributeData data)
            => data.AttributeClass!.TypeArguments.Length == 0 ? new DependencyInjectionSource
            {
                ServiceName = classSymbol.ToDisplayString(),
                IsHosted = true
            } : default;
    }
}
