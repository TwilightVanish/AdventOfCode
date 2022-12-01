using AdventOfCode.Common;
using AdventOfCode.Utility;

namespace AdventOfCode._2022;

public sealed class Day01 : BaseDay
{
  
    public Day01() : base(1, 2022) {}
    
    public override void Parse() {}
    public override string Part1() => GetRichElves(1).ToString();
    public override string Part2() => GetRichElves(3).ToString();

    private int GetRichElves(int count)
    {
        var elves = new List<int>();
        var buffer = 0;

        // Create elves
        for (var i = 0; i < Input.Length; i++)
        {
            if (Input[i] == "")
            {
                elves.Add(buffer);
                buffer = 0;
            }

            buffer += CustomParser.ParseInt(Input[i]);
        }
        
        // Find richest
        elves.Sort();
        elves.Reverse();

        var sum = 0;
        for (var i = 0; i < count; i++)
        {
            sum += elves[i];
        }

        return sum;
    }
}