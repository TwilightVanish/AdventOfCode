using System;
using System.Linq;
using AdventOfCode.Benchmarks;
using BenchmarkDotNet.Running;

switch (args.Length)
{
    case 0:
        Solver.SolveLast();
        break;
    case 1 when args[0].Contains("all", StringComparison.CurrentCultureIgnoreCase):
        Solver.SolveAll();
        break;
    case 1 when args[0].Contains("bench", StringComparison.CurrentCultureIgnoreCase):
        BenchmarkRunner.Run<BenchmarkLatestDay>();
        break;
    default:
    {
        var indexes = args.Select(arg => uint.TryParse(arg, out var index) ? index : uint.MaxValue);

        Solver.Solve(indexes.Where(i => i < uint.MaxValue));
        break;
    }
}

