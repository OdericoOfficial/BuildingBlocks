using BuildingBlocks.SourceGenerators.Sources;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BuildingBlocks.SourceGenerators.Providers
{
    internal static class ClassTargetInjectProvider
    {
        public static bool Predicate(SyntaxNode node, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return node is AttributeSyntax
                && node.Parent is not null
                && node.Parent.Parent is not null
                && node.Parent.Parent is ClassDeclarationSyntax;
        }

        public static DependencyInjectionSource Transform(GeneratorSyntaxContext context, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

        }
    }
}
