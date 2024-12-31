namespace BuildingBlocks.DependencyInjection.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class EnumerableTransientAttribute<TService> : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
    public sealed class EnumerableTransientAttribute<TService, TImplementation> : Attribute
        where TImplementation : class, TService
    {
    }
}
