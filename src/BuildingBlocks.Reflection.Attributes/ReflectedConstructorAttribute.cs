namespace BuildingBlocks.Reflection.Attributes
{
    [AttributeUsage(AttributeTargets.Constructor, AllowMultiple = false, Inherited = false)]
#pragma warning disable CS9113
    public class ReflectedConstructorAttribute(string perference = ".ctor") : Attribute
#pragma warning restore CS9113
    {
    }
}
