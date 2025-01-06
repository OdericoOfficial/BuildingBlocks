namespace Microsoft.Extensions.DependencyInjection.Attributes
{
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class ServiceAttribute(ServiceLifetime lifetime, Type? serviceType = null, 
        Type? implementation = null, string? key = null, bool isEnumerable = false) : Attribute
    {
        public ServiceLifetime Lifetime { get; } = lifetime;

        public Type? ServiceType { get; } = serviceType;

        public string? Key { get; } = key;

        public Type? implementation { get; } = implementation;

        public bool IsEnumerable { get; } = isEnumerable;
    }
}
