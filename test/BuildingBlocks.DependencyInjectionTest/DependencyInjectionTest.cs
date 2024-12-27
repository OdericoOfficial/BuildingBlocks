namespace BuildingBlocks.DependencyInjectionTest
{
    public class DependencyInjectionTest
    {
        private readonly IServiceProvider _serviceProvider;

        public DependencyInjectionTest(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [Fact]
        public void Test()
        {

        }
    }
}
