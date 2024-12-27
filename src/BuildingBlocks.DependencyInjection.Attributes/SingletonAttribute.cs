namespace BuildingBlocks.DependencyInjection.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class SingletonAttribute(string? key = null) : Attribute
    {
        public string? Key { get; } = key;
    }

    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class SingletonAttribute<TService>(string? key = null, bool isEnumerable = false) : SingletonAttribute(key)
    {
        public bool IsEnumerable { get; } = isEnumerable;
    }

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
    public class SingletonAttribute<TService, TImplementation>(string? key = null, bool isEnumerable = false) : SingletonAttribute<TService>(key, isEnumerable)
        where TImplementation : TService
    {
    }
}