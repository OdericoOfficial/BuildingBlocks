using BuildingBlocks.SourceGenerators.Abstractions;
using BuildingBlocks.SourceGenerators.Sources;
using BuildingBlocks.SourceGenerators.SubIncrementalGenerators;
using BuildingBlocks.SourceGenerators.SyntaxProviders;
using Microsoft.CodeAnalysis;

namespace BuildingBlocks.SourceGenerators
{
    [Generator(LanguageNames.CSharp)]
    internal class BuildingBlocksIncrementalGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            context.RegisterSourceOutput(context.SyntaxProvider.CreateSyntaxProvider(
                SyntaxProvider<AspectSyntaxProvider,
                AspectSource>.Provider.Predicate,
                SyntaxProvider<AspectSyntaxProvider,
                AspectSource>.Provider.Transform)
                .Collect(),
                SubIncrementalGenerator<AspectSubIncrementalGenerator,
                AspectSource>.Generator.RegisterOutput);

            context.RegisterSourceOutput(context.SyntaxProvider.CreateSyntaxProvider(
                SyntaxProvider<DependencyInjectionSyntaxProvider,
                DependencyInjectionSource>.Provider.Predicate,
                SyntaxProvider<DependencyInjectionSyntaxProvider,
                DependencyInjectionSource>.Provider.Transform)
                .Collect(), 
                SubIncrementalGenerator<DependencyInjectionSubIncrementalGenerator, 
                DependencyInjectionSource>.Generator.RegisterOutput);
        }
    }
}