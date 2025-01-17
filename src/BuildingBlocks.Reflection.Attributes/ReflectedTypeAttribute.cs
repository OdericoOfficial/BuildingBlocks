namespace BuildingBlocks.Reflection.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
#pragma warning disable CS9113
    public sealed class ReflectedTypeAttribute(Type targetType) : Attribute
#pragma warning restore CS9113
    {
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public sealed class ReflectedTypeAttribute<TTarget> : Attribute
    {
    }
}
