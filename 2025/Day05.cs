using System.Runtime.CompilerServices;
using AdventOfCode.Common;
using AdventOfCode.Utility;

namespace AdventOfCode._2025;

public sealed class Day05 : BaseDay
{
    private readonly IngredientDatabase _database;

    public Day05() : base(5, 2025)
    {
        _database = ParseDatabase();
    }

    public override void Parse() => ParseDatabase();
    public override string Part1() => CountFreshIngredients().ToString();
    public override string Part2() => SumRanges().ToString();

    private int CountFreshIngredients()
    {
        var count = 0;
        
        foreach (var range in _database.FreshRanges)
        {
            var lowerIndex = _database.Ingredients.BinarySearch(range.Start);
            if (lowerIndex < 0) lowerIndex = ~lowerIndex;

            var upperIndex = _database.Ingredients.BinarySearch(range.End);
            if (upperIndex < 0) upperIndex = ~upperIndex;
            else upperIndex += 1;

            count += upperIndex - lowerIndex;
        }

        return count;
    }

    private long SumRanges() => _database.FreshRanges.Sum(r => r.End - r.Start + 1);

    private IngredientDatabase ParseDatabase()
    {
        var split = Input.IndexOf("");
        var ranges = Input[..split];
        var ingredients = Input[(split + 1)..];
        
        return new IngredientDatabase(ParseRanges(ranges), ParseIngredients(ingredients));
    }
    
    private static long[] ParseIngredients(string[] ingredients)
    {
        var parsedIngredients = new long[ingredients.Length];
        for (var i = 0; i < ingredients.Length; i++)
        {
            parsedIngredients[i] = CustomParser.ParseLong(ingredients[i]);
        }
        Array.Sort(parsedIngredients);

        return parsedIngredients;
    }

    private static List<Range> ParseRanges(string[] ranges)
    {
        var parsedRanges = new Range[ranges.Length];
        for (var i = 0; i < ranges.Length; i++)
        {
            var newRange = ranges[i].AsSpan();
            
            var offset = 0;
            var start = CustomParser.ParseLongWithRefOffset(newRange, ref offset);
            
            offset++;
            var end = CustomParser.ParseLongWithRefOffset(newRange, ref offset);
            
            parsedRanges[i] = new Range(start, end);
        }

        return MergeRanges(parsedRanges);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static List<Range> MergeRanges(Range[] ranges)
    {
        Array.Sort(ranges, (a, b) => a.Start.CompareTo(b.Start));
        var merged = new List<Range>();

        foreach (var r in ranges)
        {
            if (merged.Count == 0 || merged[^1].End < r.Start)
            {
                merged.Add(r);
                continue;
            }

            merged[^1] = new Range(
                merged[^1].Start,
                Math.Max(merged[^1].End, r.End)
            );
        }

        return merged;
    }

    private record struct IngredientDatabase(List<Range> FreshRanges, long[] Ingredients);
    private record struct Range(long Start, long End);
}