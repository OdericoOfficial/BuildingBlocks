using System.Reflection;

namespace BuildingBlocks.Aspects.Abstractions
{
    public sealed class AspectContext
    {
        public object Implementation
            => throw new NotImplementedException();

        public Type ImplementationType
            => throw new NotImplementedException();

        public Type ReturnType
            => throw new NotImplementedException();

        public MethodBase? ImplementationMethod
            => throw new NotImplementedException();

        public bool IsAsyncMethod
            => throw new NotImplementedException();

        public IServiceProvider ServiceProvider
            => throw new NotImplementedException();

        private AspectContext() { }
        
        public ref T Parameter<T>(int index)
            => throw new NotImplementedException();

        public ref T Return<T>()
            => throw new NotImplementedException();

        public ref T Field<T>(string name)
            => throw new NotImplementedException();

        public ref T Property<T>(string name)
            => throw new NotImplementedException();
    }
}