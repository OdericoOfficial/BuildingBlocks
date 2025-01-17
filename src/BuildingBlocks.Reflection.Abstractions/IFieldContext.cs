namespace BuildingBlocks.Reflection.Abstractions
{
    public interface IFieldContext
    {
        TValue GetValue<TTarget, TValue>(ref TTarget target, string name);

        void SetValue<TTarget, TValue>(ref TTarget target, string name, TValue value);
    }
}