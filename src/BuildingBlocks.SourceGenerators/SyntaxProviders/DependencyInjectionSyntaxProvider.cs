using System.Diagnostics;
using System.Text.RegularExpressions;
using BuildingBlocks.DependencyInjection.Attributes;
using BuildingBlocks.SourceGenerators.Abstractions;
using BuildingBlocks.SourceGenerators.Sources;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BuildingBlocks.SourceGenerators.SyntaxProviders
{
    internal class DependencyInjectionSyntaxProvider : SyntaxProvider<DependencyInjectionSyntaxProvider, DependencyInjectionSource>
    {
        public override bool Predicate(SyntaxNode node, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (node is AttributeSyntax attribute)
            {
                var name = attribute.Name.ToString();
                return name.StartsWith("Scoped")
                    || name.StartsWith("Singleton")
                    || name.StartsWith("Transient")
                    || name.StartsWith("HostedService");
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
                    return AnalysisClassDeclarationSyntax(classDeclaration, context, cancellationToken);
                else if (context.Node.Parent.Parent is CompilationUnitSyntax)
                    return AnalysisCompilationUnitSyntax((AttributeSyntax)context.Node, context, cancellationToken);
            }
            
            return default;
        }

        private DependencyInjectionSource AnalysisCompilationUnitSyntax(AttributeSyntax attribute,
            GeneratorSyntaxContext context, CancellationToken cancellationToken)
        {
            var source = new DependencyInjectionSource();

            if (attribute.ArgumentList is not null)
            {
                foreach (var item in attribute.ArgumentList.Arguments)
                {
                    var expression = item.ToString()
                        .Trim();

                    if (expression == "true"
                        || expression == "isEnumerable: true")
                        source.Attribute |= DependencyInjectionSource.AttributeType.Enumerable;
                    else if (expression != "false"
                        && expression != "isEnumerable: false")
                    {
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
                        source.Attribute |= DependencyInjectionSource.AttributeType.Keyed;
                    }
                }
            }

            var symbol = ((IMethodSymbol)(context.SemanticModel.GetSymbolInfo(attribute).Symbol!)).ContainingType;

            source.ServiceFullName = symbol.TypeArguments[0].ToDisplayString();
            source.ImplementationFullName = symbol.TypeArguments.Length == 2 ? symbol.TypeArguments[1].ToDisplayString() : null;
            source.Attribute |= symbol.Name switch
            {
                nameof(SingletonAttribute) => DependencyInjectionSource.AttributeType.Singleton,
                nameof(ScopedAttribute) => DependencyInjectionSource.AttributeType.Scoped,
                nameof(TransientAttribute) => DependencyInjectionSource.AttributeType.Transient,
                _ => DependencyInjectionSource.AttributeType.HostedService
            };

            return source;
        }

        private DependencyInjectionSource AnalysisClassDeclarationSyntax(ClassDeclarationSyntax classDeclaration, 
            GeneratorSyntaxContext context, CancellationToken cancellationToken)
        {
            var symbol = (INamedTypeSymbol)context.SemanticModel.GetDeclaredSymbol(classDeclaration, cancellationToken)!;
            var attributeData = symbol.GetAttributes()
                .First(item =>
                    item.AttributeClass!.Name == nameof(SingletonAttribute)
                        || item.AttributeClass.Name == nameof(ScopedAttribute)
                        || item.AttributeClass.Name == nameof(TransientAttribute)
                        || item.AttributeClass.Name == nameof(HostedServiceAttribute));

            var source = new DependencyInjectionSource
            {
                ServiceFullName = attributeData.AttributeClass!.TypeArguments.Length switch
                {
                    1 => attributeData.AttributeClass.TypeArguments[0].ToDisplayString(),
                    _ => symbol.ToDisplayString()
                },
                ImplementationFullName = attributeData.AttributeClass.TypeArguments.Length switch
                {
                    1 => symbol.ToDisplayString(),
                    _ => null
                },
                Attribute = attributeData.AttributeClass.Name switch
                {
                    nameof(SingletonAttribute) => DependencyInjectionSource.AttributeType.Singleton,
                    nameof(ScopedAttribute) => DependencyInjectionSource.AttributeType.Scoped,
                    nameof(TransientAttribute) => DependencyInjectionSource.AttributeType.Transient,
                    _ => DependencyInjectionSource.AttributeType.HostedService
                }
            };

            if (attributeData.ConstructorArguments.Length > 0)
            {
                source.Key = attributeData.ConstructorArguments[0].Value as string ?? null;
                if (source.Key is not null)
                {
                    source.Key = $"\"{source.Key}\"";
                    source.Attribute |= DependencyInjectionSource.AttributeType.Keyed;
                }

                if (attributeData.ConstructorArguments.Length > 1
                    && Convert.ToBoolean(attributeData.ConstructorArguments[1].Value))
                    source.Attribute |= DependencyInjectionSource.AttributeType.Enumerable;
            }

            return source;
        }
    }
}
