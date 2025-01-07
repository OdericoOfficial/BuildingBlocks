namespace BuildingBlocks.SourceGeneratorsTest.Helpers.Abstractions
{
    public interface ISourceGeneratorVerifyHelper
    {
        Task VerifyAsync(IEnumerable<string> sources);
    }
}
