using BuildingBlocks.SourceGenerators;
using BuildingBlocks.SourceGeneratorsTest.Helpers.Abstractions;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BuildingBlocks.SourceGeneratorsTest.Helpers
{
    internal class DependencyInjectionVerifyHelper : SourceGeneratorVerifyHelper, IDependencyInjectionVerifyHelper
    {
        protected override IIncrementalGenerator Generator
            => IncrementalGenerators.DependencyInjection;

        protected override string AssemblyName
            => $"{nameof(IncrementalGenerators.DependencyInjection)}Test";

        protected override IEnumerable<PortableExecutableReference> References
            => [MetadataReference.CreateFromFile(typeof(IServiceProvider).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(IServiceCollection).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(IHostedService).Assembly.Location)];
    }
}
