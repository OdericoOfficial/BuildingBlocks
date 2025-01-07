using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;

namespace BuildingBlocks.SourceGeneratorsTest.Helpers.Abstractions
{
    internal abstract class SourceGeneratorVerifyHelper : ISourceGeneratorVerifyHelper
    {
        protected abstract IIncrementalGenerator Generator { get; }

        protected abstract string AssemblyName { get; }

        protected abstract IEnumerable<PortableExecutableReference> References { get; }

        public Task VerifyAsync(IEnumerable<string> sources)
            => Verify(CSharpGeneratorDriver.Create(Generator)
                .RunGenerators(CSharpCompilation.Create(AssemblyName, sources.Select(source => CSharpSyntaxTree.ParseText(source)), References)))
                .UseDirectory("Snapshots");
    }
}
