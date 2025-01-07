using System.Collections.Immutable;
using BuildingBlocks.SourceGenerators.Generators;
using Microsoft.CodeAnalysis;

namespace BuildingBlocks.SourceGenerators
{
    public static class IncrementalGenerators
    {
        public static ImmutableArray<IIncrementalGenerator> Generators { get; }
            = [new DependencyInjectionIncrementalGenerator()];

        public static IIncrementalGenerator DependencyInjection
            => Generators[0];
    }
}
