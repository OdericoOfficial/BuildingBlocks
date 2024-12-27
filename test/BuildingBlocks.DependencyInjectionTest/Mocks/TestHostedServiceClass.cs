using BuildingBlocks.DependencyInjection.Attributes;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.DependencyInjectionTest.Mocks
{
    [HostedService]
    internal class TestHostedServiceClass : IHostedService
    {
        private readonly ILogger<TestHostedServiceClass> _logger;

        public TestHostedServiceClass(ILogger<TestHostedServiceClass> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("host class ok.");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}
