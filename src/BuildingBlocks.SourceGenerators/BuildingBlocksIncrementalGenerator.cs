using BuildingBlocks.SourceGenerators.Abstractions;
using BuildingBlocks.SourceGenerators.Sources;
using BuildingBlocks.SourceGenerators.SubIncrementalGenerators;
using BuildingBlocks.SourceGenerators.SyntaxProviders;
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