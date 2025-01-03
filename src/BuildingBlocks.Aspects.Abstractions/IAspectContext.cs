namespace BuildingBlocks.Aspects.Abstractions
{
    public interface IAspectContext
    {
        IServiceProvider ServiceProvider { get; }

        Type ImplementationType { get; }

        object Implementation { get; }

        T GetParameter<T>(string name);

        T SetParameter<T>(string name, T value);

        T GetReturn<T>();

        T SetReturn<T>(T value);
    }
}