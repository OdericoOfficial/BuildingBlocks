using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.SourceGeneratorsTest.Helpers.Abstractions
{
    internal abstract class SourceGeneratorVerifyHelper(ILogger logger) : ISourceGeneratorVerifyHelper
    {
        private static readonly IEnumerable<MetadataReference> _baseReferences
            = [MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(Path.Join(Path.GetDirectoryName(typeof(object).Assembly.Location), @"netstandard.dll")),
                MetadataReference.CreateFromFile(Path.Join(Path.GetDirectoryName(typeof(object).Assembly.Location), @"System.Runtime.dll"))];

        protected abstract IIncrementalGenerator Generator { get; }

        protected abstract string AssemblyName { get; }

        protected abstract IEnumerable<PortableExecutableReference> AdditionalReferences { get; }

        public Task VerifyAsync(IEnumerable<string> sources)
        {
            var compilation = CSharpCompilation.Create(AssemblyName, sources.Select(source => CSharpSyntaxTree.ParseText(source)),
                _baseReferences.Concat(AdditionalReferences), new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, true));
            foreach (var item in compilation.GetDiagnostics())
                logger.LogCritical(item.GetMessage());
            return Verify(CSharpGeneratorDriver.Create(Generator)
                .RunGenerators(compilation))
                .UseDirectory("../../../Snapshots");
        }
    }
}
