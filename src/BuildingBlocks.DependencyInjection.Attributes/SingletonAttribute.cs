namespace Microsoft.Extensions.DependencyInjection.Attributes
{
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class SingletonAttribute(Type? serviceType = null, Type? implementation = null,
        string? key = null, bool isEnumerable = false) : ServiceAttribute(ServiceLifetime.Scoped, serviceType, implementation, key, isEnumerable)
    {
    }

    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class SingletonAttribute<TService>(Type? implementation = null, string? key = null,
        bool isEnumerable = false) : SingletonAttribute(typeof(TService), implementation, key, isEnumerable)
    {
    }

    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class SingletonAttribute<TService, TImplementation>(string? key = null, bool isEnumerable = false)
        : SingletonAttribute<TService>(typeof(TImplementation), key, isEnumerable)
        where TImplementation : class, TService
    {
    }
}