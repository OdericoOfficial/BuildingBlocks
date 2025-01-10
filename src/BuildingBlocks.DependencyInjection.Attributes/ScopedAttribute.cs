using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.DependencyInjection.Attributes
{
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public sealed class ScopedAttribute(Type? serviceType = null, Type? implementationType = null,
        string? key = null, bool isEnumerable = false) : ServiceAttribute(ServiceLifetime.Scoped, serviceType, implementationType, key, isEnumerable)
    {
    }

    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public sealed class ScopedAttribute<TService>(Type? implementationType = null, string? key = null,
        bool isEnumerable = false) : ServiceAttribute(ServiceLifetime.Scoped, typeof(TService), implementationType, key, isEnumerable)
    {
    }

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
    public sealed class ScopedAttribute<TService, TImplementation>(string? key = null, bool isEnumerable = false)
        : ServiceAttribute(ServiceLifetime.Scoped, typeof(TService), typeof(TImplementation), key, isEnumerable)
        where TImplementation : class, TService
    {
    }
}