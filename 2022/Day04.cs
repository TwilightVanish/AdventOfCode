using AdventOfCode.Common;
using AdventOfCode.Utility;

namespace AdventOfCode._2022;

public sealed class Day04 : BaseDay
{
    private readonly Assignment[] _pairs;
    
    public Day04() : base(4, 2022)
    {
        _pairs = GetPairs();
    }

    public override void Parse() => GetPairs();
    public override string Part1() => CountFullOverlaps().ToString();
    public override string Part2() => CountOverlaps().ToString();

    private int CountFullOverlaps()
    {
        var counter = 0;
        for (var i = 0; i < _pairs.Length; i++)
        {
            if (IsFullOverlap(_pairs[i].One, _pairs[i].Two)) counter++;
        }

        return counter;
    }
    
    private static bool IsFullOverlap((int Start, int End) one, (int Start, int End) two)
    {
        return one.Start <= two.Start && one.End >= two.End
               || two.Start <= one.Start && two.End >= one.End;
    }

    private int CountOverlaps()
    {
        var counter = 0;
        for (var i = 0; i < _pairs.Length; i++)
        {
            if (IsOverlap(_pairs[i].One, _pairs[i].Two)) counter++;
        }

        return counter;
    }

    private static bool IsOverlap((int Start, int End) one, (int Start, int End) two)
    {
        return one.Start <= two.Start && one.End >= two.Start
               || one.Start <= two.End && one.Start >= two.Start;
    }

    private Assignment[] GetPairs()
    {
        var pairs = new Assignment[Input.Length];
        
        var i = 0;
        var enumerator = RawInput.AsSpan().EnumerateLines();
        while (enumerator.MoveNext())
        {
            var current = enumerator.Current;
            
            var separator = current.IndexOf(',');
            var elfOneSeparator = current.IndexOf('-');
            var elfTwoSeparator = current[separator..].IndexOf('-') + separator;

            pairs[i] = new Assignment((
                CustomParser.ParseInt(current[..elfOneSeparator]),
                CustomParser.ParseInt(current[(elfOneSeparator + 1)..separator])
            ), (
                CustomParser.ParseInt(current[(separator + 1)..elfTwoSeparator]),
                CustomParser.ParseInt(current[(elfTwoSeparator + 1)..])
            ));
            

            i++;
        }
        
        return pairs;
    }

    private record struct Assignment((int Start, int End) One, (int Start, int End) Two);
}