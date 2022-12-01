using AdventOfCode.Common;
using BenchmarkDotNet.Running;

switch (args.Length)
{
    case 0:
        PrintDay(Solver.GetMostRecentDay());
        break;
    case 1 when args[0].Contains("all", StringComparison.CurrentCultureIgnoreCase):
        Solver.GetDays().ForEach(PrintDay);
        break;
    case 1 when args[0].Contains("bench", StringComparison.CurrentCultureIgnoreCase):
        BenchmarkRunner.Run<BenchmarkLatest>();
        break;
    case 2:
        PrintDay(Solver.GetDay(int.Parse(args[0]), int.Parse(args[1])));
        break;
}

void PrintDay(BaseDay day)
{
    Console.WriteLine("┌────────────────┐");
    Console.WriteLine($"│ Day {day.Day.ToString("D2")} of {day.Year} │");
    Console.WriteLine("└────────────────┘");
    try
    {
        Console.WriteLine($"- Part 1: {day.Part1()}");
        Console.WriteLine($"- Part 2: {day.Part2()}");
        Console.WriteLine();
    }
    catch (NotImplementedException) {}
}