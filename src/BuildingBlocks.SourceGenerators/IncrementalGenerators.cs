using BuildingBlocks.SourceGenerators.Generators;
using Microsoft.CodeAnalysis;

namespace BuildingBlocks.SourceGenerators
{
    public static class IncrementalGenerators
    {
        public static IEnumerable<IIncrementalGenerator> Generators { get; }
            = [DependencyInjection];

        public static IIncrementalGenerator DependencyInjection
            => new DependencyInjectionIncrementalGenerator();
    }
}
