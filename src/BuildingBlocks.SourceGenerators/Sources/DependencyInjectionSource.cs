namespace BuildingBlocks.SourceGenerators.Sources
{
    internal struct DependencyInjectionSource
    {
        public InjectType InjectType { get; set; }

        public string ServiceName { get; set; }

        public string ImplementationName { get; set; }

        public string Key { get; set; }
    }
}
