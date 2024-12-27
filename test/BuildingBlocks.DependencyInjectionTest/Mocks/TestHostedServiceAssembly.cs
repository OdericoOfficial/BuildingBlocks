using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.DependencyInjectionTest.Mocks
{
    internal class TestHostedServiceAssembly : IHostedService
    {
        private readonly ILogger<TestHostedServiceAssembly> _logger;

        public TestHostedServiceAssembly(ILogger<TestHostedServiceAssembly> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("host assembly ok.");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}
