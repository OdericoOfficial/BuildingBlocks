namespace BuildingBlocks.SourceGenerators.Sources
{
    public enum InjectType
    {
        None,
        Scoped,
        Singleton,
        Transient,
        KeyedScoped,
        KeyedSingleton,
        KeyedTransient,
        EnumerableScoped,
        EnumerableSingleton,
        EnumerableTransient,
        KeyedEnumerableScoped,
        KeyedEnumerableSingleton,
        KeyedEnumerableTransient,
        HostedService
    }
}
