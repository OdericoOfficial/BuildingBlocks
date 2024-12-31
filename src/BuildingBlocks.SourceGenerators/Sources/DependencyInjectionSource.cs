namespace BuildingBlocks.SourceGenerators.Sources
{
    internal struct DependencyInjectionSource
    {
        public InjectType InjectType { get; set; } = InjectType.None;

        public string ServiceName { get; set; } = string.Empty;

        public string ServiceNamespace { get; set; } = string.Empty;

        public string ImplementationNamespace { get; set; } = string.Empty;

        public string ImplementationName { get; set; } = string.Empty;

        public string Key { get; set; } = string.Empty;

        public DependencyInjectionSource()
        {
        }
    }
}
