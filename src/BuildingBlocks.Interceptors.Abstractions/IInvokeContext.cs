namespace BuildingBlocks.Interceptors.Abstractions
{
    public interface IInvokeContext
    {
        IServiceProvider ServiceProvider { get; }

        object Implementation { get; }

        TValue GetParameter<TValue>(string parameterName);

        void SetParameter<TValue>(string parameterName, TValue value);

        TValue GetReturn<TValue>();

        void SetReturn<TValue>(TValue value);
    }
}
