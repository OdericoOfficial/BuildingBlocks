using Microsoft.CodeAnalysis;

namespace BuildingBlocks.SourceGenerators
{
    [Generator(LanguageNames.CSharp)]
#pragma warning disable RS1036
    internal class BuildingBlocksIncrementalGenerator : IIncrementalGenerator
#pragma warning restore RS1036
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            foreach (var item in IncrementalGenerators.Generators)
                item.Initialize(context);
        }
    }
}
