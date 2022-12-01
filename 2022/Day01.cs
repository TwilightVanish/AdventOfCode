using AdventOfCode.Common;
using AdventOfCode.Utility;

namespace AdventOfCode._2022;

public sealed class Day01 : BaseDay
{
    private List<int> _elves;

    public Day01() : base(1, 2022)
    {
        _elves = ParseElves();
    }

    public override void Parse() => ParseElves();
    public override string Part1() => GetRichElves(1).ToString();
    public override string Part2() => GetRichElves(3).ToString();

    private int GetRichElves(int count)
    {
        var sum = 0;
        for (var i = 0; i < count; i++)
        {
            sum += _elves[i];
        }

        return sum;
    }

    private List<int> ParseElves()
    {
        var elves = new List<int>();
        var buffer = 0;
        
        for (var i = 0; i < Input.Length; i++)
        {
            if (Input[i] == "")
            {
                elves.Add(buffer);
                buffer = 0;
            }

            buffer += CustomParser.ParseInt(Input[i]);
        }
        
        elves.Sort();
        elves.Reverse();

        return elves;
    }
}