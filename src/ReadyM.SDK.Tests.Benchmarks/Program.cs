using BenchmarkDotNet.Running;

namespace ReadyM.SDK.Tests.Benchmarks;

public static class Program
{
    public static void Main(string[] args) => BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
}
