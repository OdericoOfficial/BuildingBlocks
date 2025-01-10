using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;

namespace BuildingBlocks.ReflectionBenchmark
{
    internal class Program
    {
        internal class MinimalBenchmarkConfig : ManualConfig
        {
            public static MinimalBenchmarkConfig Instance { get; } = new MinimalBenchmarkConfig();

            private MinimalBenchmarkConfig()
            {
                AddLogger(ConsoleLogger.Unicode);
                AddColumnProvider(DefaultColumnProviders.Instance);
                AddExporter(DefaultExporters.Csv);
            }
        }
        static void Main(string[] args)
            => BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, MinimalBenchmarkConfig.Instance);
    }
}
