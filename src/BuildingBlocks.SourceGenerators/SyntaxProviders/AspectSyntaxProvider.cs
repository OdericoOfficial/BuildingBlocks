using BuildingBlocks.SourceGenerators.Abstractions;
using BuildingBlocks.SourceGenerators.Sources;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BuildingBlocks.SourceGenerators.SyntaxProviders
{
    internal class AspectSyntaxProvider : SyntaxProvider<AspectSyntaxProvider, AspectSource>
    {
        public override bool Predicate(SyntaxNode node, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return node is ClassDeclarationSyntax classDeclaration
                && classDeclaration.AttributeLists.SelectMany(item => item.Attributes)
                    .Any(item => item.Name.ToString().StartsWith("GeneratedProxy"))
                && (classDeclaration.AttributeLists.SelectMany(item => item.Attributes)
                    .Any(item =>
                    {
                        var name = item.Name.ToString();
                        return name.StartsWith("SyncAspect")
                            || name.StartsWith("AsyncAspect");
                    }));
        }

        public override AspectSource Transform(GeneratorSyntaxContext context, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var classSymbol = (INamedTypeSymbol)context.SemanticModel.GetDeclaredSymbol(context.Node, cancellationToken)!;
            var classAspects = AnalysisClassAspects(classSymbol, cancellationToken);

            throw new NotImplementedException();
        }

        private List<AspectSource> AnalysisClassAspects(INamedTypeSymbol symbol, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private ProxyMethodSource AnalysisMethodAspects(IMethodSymbol symbol, List<AspectSource> classAspect, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private AspectSource AnalysisAspectAttribute(INamedTypeSymbol attributeSymbol)
        {
            throw new NotImplementedException();
        }
    }
}
