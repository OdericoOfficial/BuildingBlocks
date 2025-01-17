namespace BuildingBlocks.DependencyInjection.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class ScopedClassAttribute : Attribute
    {
        public Type ServiceType { get; set; } = typeof(object);

        public string Key { get; set; } = string.Empty;

        public bool IsEnumerable { get; set; }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class ScopedClassAttribute<TService> : Attribute
        where TService : class
    {
        public string Key { get; set; } = string.Empty;

        public bool IsEnumerable { get; set; }
    }
}
