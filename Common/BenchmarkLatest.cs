using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Common;

[MemoryDiagnoser]
public class BenchmarkLatest
{
    private readonly BaseDay _day;

    public BenchmarkLatest()
    {
        _day = Solver.GetMostRecentDay();
    }

    [Benchmark]
    public string Part1()
    {
        return _day.Part1();
    }

    [Benchmark]
    public string Part2()
    {
        return _day.Part2();
    }

    [Benchmark]
    public void Parse()
    {
        _day.Parse();
    }
}