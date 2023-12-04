using System.Numerics;
using AdventOfCode.Common;
using AdventOfCode.Utility;

namespace AdventOfCode._2023;

public sealed class Day04 : BaseDay
{
    private readonly Scratcher[] _scratchers;

    public Day04() : base(4, 2023)
    {
        _scratchers = ParseScratchers();
    }

    public override void Parse() => ParseScratchers();
    public override string Part1() => GetScratcherPoints().ToString();
    public override string Part2() => GetScratcherCount().ToString();

    private int GetScratcherPoints()
    {
        var points = 0;

        for (var i = 0; i < _scratchers.Length; i++)
        {
            var scratcher = _scratchers[i];
            var currentPoints = 0;
            
            for (var j = 0; j < scratcher.ScratchedNumbers.Length; j++)
            {
                if (!scratcher.WinningNumbers.Contains(scratcher.ScratchedNumbers[j])) continue;
                if (currentPoints == 0) currentPoints = 1;
                else currentPoints *= 2;
            }

            points += currentPoints;
        }


        return points;
    }

    private int GetScratcherCount()
    {
        var scratcherCount = 0;

        for (var i = 0; i < _scratchers.Length; i++)
        {
            var scratcher = _scratchers[i];
            var currentWins = 0;

            for (var j = 0; j < scratcher.ScratchedNumbers.Length; j++)
            {
                if (!scratcher.WinningNumbers.Contains(scratcher.ScratchedNumbers[j])) continue;
                currentWins++;
                _scratchers[i + currentWins].Count += scratcher.Count;
            }

            scratcherCount += scratcher.Count;
        }

        return scratcherCount;
    }

    private Scratcher[] ParseScratchers()
    {
        var scratchers = new Scratcher[Input.Length];

        var index = 0;
        foreach (var line in RawInput.AsSpan().EnumerateLines())
        {
            var scratched = new int[10];
            var winning = new int[25];

            ParseWithOffset(scratched, line, 10);
            ParseWithOffset(winning, line, 42);

            scratchers[index] = new Scratcher(scratched, winning);
            index++;
        }

        return scratchers;
    }

    private static void ParseWithOffset(int[] targetArray, ReadOnlySpan<char> line, int startIndex)
    {
        for (var i = 0; i < targetArray.Length; i++)
        {
            targetArray[i] = CustomParser.GreedyParseInt(line.Slice(startIndex + 3 * i, 2));
        }
    }

    private record struct Scratcher(int[] ScratchedNumbers, int[] WinningNumbers, int Count = 1);
}