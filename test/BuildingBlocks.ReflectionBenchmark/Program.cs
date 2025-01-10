using BenchmarkDotNet.Running;

namespace BuildingBlocks.ReflectionBenchmark
{
    internal class Program
    {
        static void Main(string[] args)
            => BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
    }
}
