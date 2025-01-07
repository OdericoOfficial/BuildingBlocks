namespace Microsoft.Extensions.DependencyInjection.Attributes
{
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class TransientAttribute(Type? serviceType = null, Type? implementationType = null,
        string? key = null, bool isEnumerable = false) : ServiceAttribute(ServiceLifetime.Scoped, serviceType, implementationType, key, isEnumerable)
    {
    }

    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class TransientAttribute<TService>(Type? implementationType = null, string? key = null,
        bool isEnumerable = false) : TransientAttribute(typeof(TService), implementationType, key, isEnumerable)
    {
    }

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
    public class TransientAttribute<TService, TImplementation>(string? key = null, bool isEnumerable = false) 
        : TransientAttribute<TService>(typeof(TImplementation), key, isEnumerable)
        where TImplementation : class, TService
    {
    }
}