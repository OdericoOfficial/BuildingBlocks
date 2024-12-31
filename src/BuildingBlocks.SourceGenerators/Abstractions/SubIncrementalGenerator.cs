using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace BuildingBlocks.SourceGenerators.Abstractions
{
    internal abstract class SubIncrementalGenerator<TSubIncrementalGenerator, TSource>
        where TSubIncrementalGenerator : SubIncrementalGenerator<TSubIncrementalGenerator, TSource>, new()
    {
        private static readonly SubIncrementalGenerator<TSubIncrementalGenerator, TSource> _generator = new TSubIncrementalGenerator();
        public static SubIncrementalGenerator<TSubIncrementalGenerator, TSource> Generator
            => _generator;

        public abstract void RegisterOutput(SourceProductionContext context, ImmutableArray<TSource> sources);
    }
}
