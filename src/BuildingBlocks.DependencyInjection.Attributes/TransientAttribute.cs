namespace BuildingBlocks.DependencyInjection.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class TransientAttribute : Attribute
    {
        public string? Key { get; }

        public bool IsEnumerable { get; }

        public TransientAttribute(string? key = null, bool isEnumerable = false)
        {
            Key = key;
            IsEnumerable = isEnumerable;
        }
    }

    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class TransientAttribute<TService> : TransientAttribute
    {
        public TransientAttribute(string? key = null, bool isEnumerable = false) : base(key, isEnumerable) { }
    }

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
    public class TransientAttribute<TService, TImplementation> : TransientAttribute<TService>
        where TImplementation : TService
    {
        public TransientAttribute(string? key = null, bool isEnumerable = false) : base(key, isEnumerable) { }
    }
}