using System.Collections.Immutable;
using BuildingBlocks.SourceGenerators.Abstractions;
using BuildingBlocks.SourceGenerators.Sources;
using Microsoft.CodeAnalysis;

namespace BuildingBlocks.SourceGenerators.SubIncrementalGenerators
{
    internal class AspectSubIncrementalGenerator : SubIncrementalGenerator<AspectSubIncrementalGenerator, AspectSource>
    {
        public override void RegisterOutput(SourceProductionContext context, ImmutableArray<AspectSource> sources)
        {
            throw new NotImplementedException();
        }
    }
}
