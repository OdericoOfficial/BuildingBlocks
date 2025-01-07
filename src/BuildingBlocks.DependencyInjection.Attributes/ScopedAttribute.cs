namespace Microsoft.Extensions.DependencyInjection.Attributes
{
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class ScopedAttribute(Type? serviceType = null, Type? implementationType = null,
        string? key = null, bool isEnumerable = false) : ServiceAttribute(ServiceLifetime.Scoped, serviceType, implementationType, key, isEnumerable)
    {
    }

    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class ScopedAttribute<TService>(Type? implementationType = null, string? key = null,
        bool isEnumerable = false) : ScopedAttribute(typeof(TService), implementationType, key, isEnumerable)
    {
    }

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
    public class ScopedAttribute<TService, TImplementation>(string? key = null, bool isEnumerable = false)
        : ScopedAttribute<TService>(typeof(TImplementation), key, isEnumerable)
        where TImplementation : class, TService
    {
    }
}