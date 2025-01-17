namespace BuildingBlocks.Reflection.Abstractions
{
    public interface IPropertyContext
    {
        TValue GetValue<TTarget, TValue>(ref TTarget target, string name);

        void SetValue<TTarget, TValue>(ref TTarget target, string name, TValue value);
    }
}