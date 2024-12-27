using System.Diagnostics;
using BuildingBlocks.SourceGenerators.Abstractions;
using BuildingBlocks.SourceGenerators.Sources;
using BuildingBlocks.SourceGenerators.SyntaxProviders;
using Microsoft.CodeAnalysis;

namespace BuildingBlocks.SourceGenerators
{
    [Generator(LanguageNames.CSharp)]
    internal class BuildingBlocksIncrementalGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var provider = context.SyntaxProvider.CreateSyntaxProvider(SyntaxProvider<DependencyInjectionSyntaxProvider, 
                DependencyInjectionSource>.Provider.Predicate, 
                SyntaxProvider<DependencyInjectionSyntaxProvider, 
                DependencyInjectionSource>.Provider.Transform)
                .Collect();

            context.RegisterSourceOutput(provider, (context, sources) =>
            {
            });
        }
    }
}
