using System.Numerics;
using AdventOfCode.Common;

namespace AdventOfCode._2025;

public sealed class Day02 : BaseDay
{
    private readonly Range[] _ranges;

    public Day02() : base(2, 2025)
    {
        _ranges = GetRanges();
    }

    public override void Parse() => GetRanges();
    public override string Part1() => SumInvalidIds(GenerateNumbersRepeatedTwice).ToString();
    public override string Part2() => SumInvalidIds(GenerateRepeatingNumbers).ToString();
    
    private long SumInvalidIds(Func<int,List<long>> invalidFunction)
    {
        var max = _ranges.Max(r => r.End).ToString().Length;
        var invalid = invalidFunction(max);

        var sum = 0L;
        
        foreach (var range in _ranges)
        {
            var lowerIndex = invalid.BinarySearch(range.Start);
            if (lowerIndex < 0) lowerIndex = ~lowerIndex;

            var upperIndex = invalid.BinarySearch(range.End);
            if (upperIndex < 0) upperIndex = ~upperIndex;
            else upperIndex += 1;
            
            for (var i = lowerIndex; i < upperIndex; i++)
            {
                sum += invalid[i];
            }
        }

        return sum;
    }
    
    private static List<long> GenerateNumbersRepeatedTwice(int maxDigits)
    {
        var result = new List<long>();

        for (var length = 1; length <= maxDigits / 2; length++)
        {
            var start = (long)Math.Pow(10, length - 1);
            var end = (long)Math.Pow(10, length);
            var multiplier = (long)Math.Pow(10, length);

            for (var pattern = start; pattern < end; pattern++)
            {
                var repeated = pattern * multiplier + pattern;
                if (length * 2 <= maxDigits)
                {
                    result.Add(repeated);
                }
            }
        }

        result.Sort();
        return result;
    }
    
    private static List<long> GenerateRepeatingNumbers(int maxDigits)
    {
        var result = new HashSet<long>();
    
        for (var length = 1; length <= maxDigits / 2; length++)
        {
            var start = (long)Math.Pow(10, length - 1);
            var end = (long)Math.Pow(10, length);
        
            for (var pattern = start; pattern < end; pattern++)
            {
                var temp = pattern;
                var digits = length;
            
                while (digits <= maxDigits)
                {
                    if (digits > length) result.Add(temp);
                    temp = temp * (long)Math.Pow(10, length) + pattern;
                    digits += length;
                }
            }
        }
    
        var sorted = result.ToList();
        sorted.Sort();
        return sorted;
    }
    
    private Range[] GetRanges() => RawInput
        .Split(",")
        .Select(r => r.Split("-"))
        .Select(r => new Range(long.Parse(r[0]), long.Parse(r[1])))
        .ToArray();

    private record struct Range(long Start, long End);
}