using BenchmarkDotNet.Running;

namespace BuildingBlocks.ReflectionBenchmark
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<FieldGetValue>();
        }
    }
}
