using Microsoft.Extensions.DependencyInjection;
using Xunit.DependencyInjection.Logging;

namespace BuildingBlocks.DependencyInjectionTest
{
    public class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(builder => builder.AddXunitOutput());
            services.AddRegisteredServices();
        }
    }
}
