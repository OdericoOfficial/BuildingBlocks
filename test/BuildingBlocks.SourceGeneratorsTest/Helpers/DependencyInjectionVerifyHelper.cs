using BuildingBlocks.SourceGenerators;
using BuildingBlocks.SourceGeneratorsTest.Helpers.Abstractions;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Attributes;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.SourceGeneratorsTest.Helpers
{
    internal class DependencyInjectionVerifyHelper(ILogger<DependencyInjectionVerifyHelper> logger) : SourceGeneratorVerifyHelper(logger), IDependencyInjectionVerifyHelper
    {
        protected override IIncrementalGenerator Generator
            => IncrementalGenerators.DependencyInjection;

        protected override string AssemblyName
            => $"{nameof(IncrementalGenerators.DependencyInjection)}Test";

        protected override IEnumerable<PortableExecutableReference> AdditionalReferences
            => [MetadataReference.CreateFromFile(typeof(IServiceProvider).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(IServiceCollection).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(IHostedService).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(ServiceAttribute).Assembly.Location)];

    }
}