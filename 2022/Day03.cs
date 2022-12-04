using AdventOfCode.Common;

namespace AdventOfCode._2022;

public sealed class Day03 : BaseDay
{
    public Day03() : base(3, 2022) {}

    public override void Parse() {}
    public override string Part1() => FindStrays().ToString();
    public override string Part2() => FindBadges().ToString();

    private int FindStrays()
    {
        var sum = 0;
        for (var bag = 0; bag < Input.Length; bag++)
        {
            var pocket2 = Input[bag][(Input[bag].Length / 2)..];

            for (var item = 0; item < pocket2.Length; item++)
            {
                if (pocket2.Contains(Input[bag][item]))
                {
                    sum += GetItemPriority(Input[bag][item]);
                    break;
                }
            }
        }
        
        return sum;
    }

    private int FindBadges()
    {
        var sum = 0;
        for (var bag = 0; bag < Input.Length / 3; bag++)
        {
            for (var item = 0; item < Input[bag * 3].Length; item++)
            {
                if (Input[bag * 3 + 1].Contains(Input[bag * 3][item]) &&
                    Input[bag * 3 + 2].Contains(Input[bag * 3][item]))
                {
                    sum += GetItemPriority(Input[bag * 3][item]);
                    break;
                }
            }
        }

        return sum;
    }

    private static int GetItemPriority(char c) => char.IsLower(c) ? c - 96 : c - 38;
}