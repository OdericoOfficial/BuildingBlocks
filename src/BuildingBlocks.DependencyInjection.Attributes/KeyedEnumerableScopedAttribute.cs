﻿namespace BuildingBlocks.DependencyInjection.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class KeyedEnumerableScopedAttribute<TService>(string key) : Attribute
    {
        public string Key { get; } = key;
    }

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
    public sealed class KeyedEnumerableScopedAttribute<TService, TImplementation>(string key) : Attribute
        where TImplementation : class, TService
    {
        public string Key { get; } = key;
    }
}
