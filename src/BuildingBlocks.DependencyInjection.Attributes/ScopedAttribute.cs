namespace BuildingBlocks.DependencyInjection.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ScopedAttribute : Attribute
    {
        public string? Key { get; }

        public bool IsEnumerable { get; }

        public ScopedAttribute(string? key = null, bool isEnumerable = false)
        {
            Key = key;
            IsEnumerable = isEnumerable;
        }
    }

    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class ScopedAttribute<TService> : ScopedAttribute
    {
        public ScopedAttribute(string? key = null, bool isEnumerable = false) : base(key, isEnumerable) { }
    }

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
    public class ScopedAttribute<TService, TImplementation> : ScopedAttribute<TService>
        where TImplementation : TService
    {
        public ScopedAttribute(string? key = null, bool isEnumerable = false) : base(key, isEnumerable) { }
    }
}