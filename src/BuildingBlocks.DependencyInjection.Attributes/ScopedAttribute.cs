namespace Microsoft.Extensions.DependencyInjection.Attributes
{
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class ScopedAttribute(Type? serviceType = null, Type? implementation = null,
        string? key = null, bool isEnumerable = false) : ServiceAttribute(ServiceLifetime.Scoped, serviceType, implementation, key, isEnumerable)
    {
    }

    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class ScopedAttribute<TService>(Type? implementation = null, string? key = null,
        bool isEnumerable = false) : ScopedAttribute(typeof(TService), implementation, key, isEnumerable)
    {
    }

    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class ScopedAttribute<TService, TImplementation>(string? key = null, bool isEnumerable = false)
        : ScopedAttribute<TService>(typeof(TImplementation), key, isEnumerable)
        where TImplementation : class, TService
    {
    }
}