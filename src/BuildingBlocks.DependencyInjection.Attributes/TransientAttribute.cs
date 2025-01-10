using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.DependencyInjection.Attributes
{
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public sealed class TransientAttribute(Type? serviceType = null, Type? implementationType = null,
        string? key = null, bool isEnumerable = false) : ServiceAttribute(ServiceLifetime.Transient, serviceType, implementationType, key, isEnumerable)
    {
    }

    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public sealed class TransientAttribute<TService>(Type? implementationType = null, string? key = null,
        bool isEnumerable = false) : ServiceAttribute(ServiceLifetime.Transient, typeof(TService), implementationType, key, isEnumerable)
    {
    }

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
    public sealed class TransientAttribute<TService, TImplementation>(string? key = null, bool isEnumerable = false) 
        : ServiceAttribute(ServiceLifetime.Transient, typeof(TService), typeof(TImplementation), key, isEnumerable)
        where TImplementation : class, TService
    {
    }
}