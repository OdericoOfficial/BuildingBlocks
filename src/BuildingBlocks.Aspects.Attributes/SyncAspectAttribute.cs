using BuildingBlocks.Aspects.Abstractions;

namespace BuildingBlocks.Aspects.Attributes
{
    [AttributeUsage(AttributeTargets.Class |AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class SyncAspectAttribute<TSyncAspect> : Attribute
        where TSyncAspect : class, ISyncAspect
    {
    }
}