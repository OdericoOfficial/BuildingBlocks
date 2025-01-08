
namespace BuildingBlocks.SourceGenerators.Sources
{
    internal class DependencyInjectionSource
    {
        public int Lifetime { get; set; } = 3;
        
        public string ServiceName { get; set; } = string.Empty;
        
        public string ImplementationName { get; set; } = string.Empty;

        public string Key { get; set; } = string.Empty;

        public bool IsEnumerable { get; set; }
        
        public bool IsHosted { get; set; }
    }
}
