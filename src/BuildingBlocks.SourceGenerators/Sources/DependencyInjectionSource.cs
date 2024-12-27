namespace BuildingBlocks.SourceGenerators.Sources
{
    internal struct DependencyInjectionSource
    {
        internal enum AttributeType
        {
            None = 0b_000000,
            Scoped = 0b_000001,
            Singleton = 0b_000010,
            Transient = 0b_000100,
            HostedService = 0b_001000,
            Enumerable = 0b_010000,
            Keyed = 0b_100000
        }

        public AttributeType Attribute { get; set; } = AttributeType.None;

        public string? ServiceFullName { get; set; }

        public string? ImplementationFullName { get; set; }

        public string? Key { get; set; }

        public DependencyInjectionSource()
        {
        }
    }
}
