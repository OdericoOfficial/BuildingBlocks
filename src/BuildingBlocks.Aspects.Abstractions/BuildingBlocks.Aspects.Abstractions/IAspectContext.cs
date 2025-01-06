namespace BuildingBlocks.Aspects.Abstractions
{
    public interface IAspectContext
    {
        IServiceProvider ServiceProvider { get; }

        Type InstanceType { get; }

        object Instance { get; }

        T GetParameter<T>(string name);

        void SetParameter<T>(string name);

        T GetReturn<T>();

        void SetReturn<T>();
    }
}
