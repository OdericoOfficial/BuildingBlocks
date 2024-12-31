using BuildingBlocks.Aspects.Abstractions;

namespace BuildingBlocks.Aspects.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public sealed class AsyncAspectAttribute<TAsyncAspect> : Attribute
        where TAsyncAspect : class, IAsyncAspect
    {
    }
}