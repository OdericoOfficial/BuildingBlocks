﻿namespace BuildingBlocks.DependencyInjection.Attributes
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
#pragma warning disable CS9113
    public sealed class SingletonAssemblyAttribute(Type serviceType) : Attribute
#pragma warning restore CS9113
    {
        public Type ImplementationType { get; set; } = typeof(object);

        public string Key { get; set; } = string.Empty;

        public bool IsEnumerable { get; set; }
    }

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
    public sealed class SingletonAssemblyAttribute<TService> : Attribute
        where TService : class
    {
        public Type ImplementationType { get; set; } = typeof(object);

        public string Key { get; set; } = string.Empty;

        public bool IsEnumerable { get; set; }
    }

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
    public sealed class SingletonAssemblyAttribute<TService, TImplementation> : Attribute
        where TService : class
        where TImplementation : class, TService
    {
        public string Key { get; set; } = string.Empty;

        public bool IsEnumerable { get; set; }
    }
}
