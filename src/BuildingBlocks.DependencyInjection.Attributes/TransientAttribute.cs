namespace BuildingBlocks.DependencyInjection.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class TransientAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public sealed class TransientAttribute<TService> : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
    public sealed class TransientAttribute<TService, TImplementation> : Attribute
        where TImplementation : class, TService
    {
    }
}