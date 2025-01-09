using BuildingBlocks.SourceGeneratorsTest.Helpers;
using BuildingBlocks.SourceGeneratorsTest.Helpers.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Xunit.DependencyInjection.Logging;

namespace BuildingBlocks.SourceGeneratorsTest
{
    public static class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            VerifyDiffPlex.Initialize();
            VerifySourceGenerators.Initialize();
            services.AddLogging(builder => builder.AddXunitOutput());
            services.TryAddSingleton<IDependencyInjectionVerifyHelper, DependencyInjectionVerifyHelper>();
        }
    }
}
