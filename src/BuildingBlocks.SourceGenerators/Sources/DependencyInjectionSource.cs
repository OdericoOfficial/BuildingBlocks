using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.SourceGenerators.Sources
{
    internal struct DependencyInjectionSource(ServiceLifetime lifetime, string serviceName, 
        string implementationName, string key, bool isEnumerable, bool isHosted)
    {
        public ServiceLifetime Lifetime { get; } = lifetime;
        
        public string ServiceName { get; } = serviceName;
        
        public string ImplementationName { get; } = implementationName;
        
        public string Key { get; } = key;
        
        public bool IsEnumerable { get; } = isEnumerable;
        
        public bool IsHosted { get; } = isHosted;
    }
}
