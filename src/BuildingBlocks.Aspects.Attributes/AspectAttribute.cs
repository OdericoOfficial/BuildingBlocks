using BuildingBlocks.Aspects.Abstractions;

namespace BuildingBlocks.Aspects.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AspectAttribute<TAspect> : Attribute
        where TAspect : IAspect
    {
    }
}
