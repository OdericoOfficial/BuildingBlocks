using BuildingBlocks.SourceGenerators.Generators;
using BuildingBlocks.SourceGenerators.Providers;
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
            RegisterDependenceInjection(context);
        }

        private void RegisterDependenceInjection(IncrementalGeneratorInitializationContext context)
        {
            var classTargets = context.SyntaxProvider.CreateSyntaxProvider(ClassTargetInjectProvider.Predicate, ClassTargetInjectProvider.Transform).Collect();
            var assmblyTargets = context.CompilationProvider.SelectMany(AssemblyTargetInjectProvider.Transform).Collect();
            context.RegisterSourceOutput(classTargets.Combine(assmblyTargets), DependencyInjectionGenerator.RegisterServices);
        }
    }
}