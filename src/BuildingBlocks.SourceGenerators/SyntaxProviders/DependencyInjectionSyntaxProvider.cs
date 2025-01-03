using System.Collections.Immutable;
using System.Text.RegularExpressions;
using BuildingBlocks.SourceGenerators.Abstractions;
using BuildingBlocks.SourceGenerators.Sources;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BuildingBlocks.SourceGenerators.SyntaxProviders
{
    internal class DependencyInjectionSyntaxProvider : SyntaxProvider<DependencyInjectionSyntaxProvider, DependencyInjectionSource>
    {
        private static readonly ImmutableArray<string> _attributeNames
            = ["HostedService", "HostedServiceAttribute",
                "Scoped", "Singleton", "Transient", 
                "ScopedAttribute", "SingletonAttribute", "TransientAttribute",
                "KeyedScoped", "KeyedSingleton", "KeyedTransient",
                "KeyedScopedAttribute", "KeyedSingletonAttribute", "KeyedTransientAttribute",
                "EnumerableScoped", "EnumerableSingleton", "EnumerableTransient",
                "EnumerableScopedAttribute", "EnumerableSingletonAttribute", "EnumerableTransientAttribute",
                "KeyedEnumerableScoped", "KeyedEnumerableSingleton", "KeyedEnumerableTransient",
                "KeyedEnumerableScopedAttribute", "KeyedEnumerableSingletonAttribute", "KeyedEnumerableTransientAttribute"];

        public override bool Predicate(SyntaxNode node, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (node is AttributeSyntax attribute)
            {
                var name = attribute.Name.ToString();
                return attribute.Parent is not null
                    && attribute.Parent.Parent is not null
                    && _attributeNames.Any(name.StartsWith);
            }
            return false;
        }

        public override DependencyInjectionSource Transform(GeneratorSyntaxContext context, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            if (context.Node.Parent is not null
                && context.Node.Parent.Parent is not null)
            {
                if (context.Node.Parent.Parent is ClassDeclarationSyntax classDeclaration)
                    return AnalysisClassDeclarationSyntax((AttributeSyntax)context.Node, classDeclaration, 
                        context, cancellationToken);
                else if (context.Node.Parent.Parent is CompilationUnitSyntax)
                    return AnalysisCompilationUnitSyntax((AttributeSyntax)context.Node, context, cancellationToken);
            }
            
            return default;
        }

        private DependencyInjectionSource AnalysisCompilationUnitSyntax(AttributeSyntax attribute,
            GeneratorSyntaxContext context, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var symbol = ((IMethodSymbol)(context.SemanticModel.GetSymbolInfo(attribute, cancellationToken).Symbol!)).ContainingType;
            
            var source = new DependencyInjectionSource
            {
                ServiceName = symbol.TypeArguments[0].ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
                ImplementationName = symbol.TypeArguments.Length == 2 ?
                    symbol.TypeArguments[1].ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat) : string.Empty,
                InjectType = symbol.Name switch
                {
                    "HostedServiceAttribute" => InjectType.HostedService,
                    "ScopedAttribute" => InjectType.Scoped,
                    "SingletonAttribute" => InjectType.Singleton,
                    "TransientAttribute" => InjectType.Transient,
                    "KeyedScopedAttribute" => InjectType.KeyedScoped,
                    "KeyedSingletonAttribute" => InjectType.KeyedSingleton,
                    "KeyedTransientAttribute" => InjectType.KeyedTransient,
                    "EnumerableScopedAttribute" => InjectType.EnumerableScoped,
                    "EnumerableSingletonAttribute" => InjectType.EnumerableSingleton,
                    "EnumerableTransientAttribute" => InjectType.EnumerableTransient,
                    "KeyedEnumerableScopedAttribute" => InjectType.KeyedEnumerableScoped,
                    "KeyedEnumerableSingletonAttribute" => InjectType.KeyedEnumerableSingleton,
                    "KeyedEnumerableTransientAttribute" => InjectType.KeyedEnumerableTransient,
                    _ => InjectType.None
                }
            };

            if ((source.InjectType == InjectType.KeyedScoped
                || source.InjectType == InjectType.KeyedSingleton
                || source.InjectType == InjectType.KeyedTransient
                || source.InjectType == InjectType.KeyedEnumerableScoped
                || source.InjectType == InjectType.KeyedEnumerableSingleton
                || source.InjectType == InjectType.KeyedEnumerableTransient)
                && attribute.ArgumentList is not null
                && attribute.ArgumentList.Arguments.Count == 1)
            {
                var expression = attribute.ArgumentList.Arguments[0].ToString()
                    .Trim();

                if (expression.StartsWith("key:"))
                    expression = expression.Substring(4)
                        .Trim();

                if (Regex.IsMatch(expression, @"^nameof\(\w+\)$"))
                {
                    var start = expression.IndexOf('(');
                    var end = expression.IndexOf(')');
                    expression = $"\"{expression.Substring(start + 1, end - start - 1)}\"";
                }

                source.Key = expression;
            }

            return source;
        }

        private DependencyInjectionSource AnalysisClassDeclarationSyntax(AttributeSyntax attribute, ClassDeclarationSyntax classDeclaration, 
            GeneratorSyntaxContext context, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var classSymbol = (INamedTypeSymbol)context.SemanticModel.GetDeclaredSymbol(classDeclaration, cancellationToken)!;
            var attributeSymbol = ((IMethodSymbol)(context.SemanticModel.GetSymbolInfo(attribute, cancellationToken).Symbol!)).ContainingType;

            var source = new DependencyInjectionSource
            {
                ServiceName = attributeSymbol.TypeArguments.Length == 1 ?
                    attributeSymbol.TypeArguments[0].ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat) : classSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
                ImplementationName = attributeSymbol.TypeArguments.Length == 1 ?
                    classSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat) : string.Empty,
                InjectType = attributeSymbol.Name switch
                {
                    "HostedServiceAttribute" => InjectType.HostedService,
                    "ScopedAttribute" => InjectType.Scoped,
                    "SingletonAttribute" => InjectType.Singleton,
                    "TransientAttribute" => InjectType.Transient,
                    "KeyedScopedAttribute" => InjectType.KeyedScoped,
                    "KeyedSingletonAttribute" => InjectType.KeyedSingleton,
                    "KeyedTransientAttribute" => InjectType.KeyedTransient,
                    "EnumerableScopedAttribute" => InjectType.EnumerableScoped,
                    "EnumerableSingletonAttribute" => InjectType.EnumerableSingleton,
                    "EnumerableTransientAttribute" => InjectType.EnumerableTransient,
                    "KeyedEnumerableScopedAttribute" => InjectType.KeyedEnumerableScoped,
                    "KeyedEnumerableSingletonAttribute" => InjectType.KeyedEnumerableSingleton,
                    "KeyedEnumerableTransientAttribute" => InjectType.KeyedEnumerableTransient,
                    _ => InjectType.None
                }
            };

            if ((source.InjectType == InjectType.KeyedScoped
                || source.InjectType == InjectType.KeyedSingleton
                || source.InjectType == InjectType.KeyedTransient
                || source.InjectType == InjectType.KeyedEnumerableScoped
                || source.InjectType == InjectType.KeyedEnumerableSingleton
                || source.InjectType == InjectType.KeyedEnumerableTransient)
                && attribute.ArgumentList is not null
                && attribute.ArgumentList.Arguments.Count == 1)
            {
                var expression = attribute.ArgumentList.Arguments[0].ToString()
                    .Trim();

                if (expression.StartsWith("key:"))
                    expression = expression.Substring(4)
                        .Trim();

                if (Regex.IsMatch(expression, @"^nameof\(\w+\)$"))
                {
                    var start = expression.IndexOf('(');
                    var end = expression.IndexOf(')');
                    expression = $"\"{expression.Substring(start + 1, end - start - 1)}\"";
                }

                source.Key = expression;
            }

            return source;
        } 
    }
}