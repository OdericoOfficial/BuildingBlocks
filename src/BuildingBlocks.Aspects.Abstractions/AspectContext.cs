namespace BuildingBlocks.Aspects.Abstractions
{
    public readonly ref struct AspectContext
    {
        public IServiceProvider ServiceProvider { get; }

        public Type InstanceType { get; }

        public object Instance { get; }

        public AspectContext(IServiceProvider serviceProvider, Type instanceType, object instance)
        {
            ServiceProvider = serviceProvider;
            InstanceType = instanceType;
            Instance = instance;
        }
    }
}