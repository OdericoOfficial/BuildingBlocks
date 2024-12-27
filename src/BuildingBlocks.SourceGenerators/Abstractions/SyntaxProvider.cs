using Microsoft.CodeAnalysis;

namespace BuildingBlocks.SourceGenerators.Abstractions
{
    internal abstract class SyntaxProvider<TSyntaxProvider, TSource>
        where TSyntaxProvider : SyntaxProvider<TSyntaxProvider, TSource>, new()
    {
        private static readonly SyntaxProvider<TSyntaxProvider, TSource> _provider = new TSyntaxProvider();
        public static SyntaxProvider<TSyntaxProvider, TSource> Provider
            => _provider;

        public abstract bool Predicate(SyntaxNode node, CancellationToken cancellationToken);

        public abstract TSource Transform(GeneratorSyntaxContext context, CancellationToken cancellationToken);
    }
}
