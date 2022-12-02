using AdventOfCode.Common;

namespace AdventOfCode._2022;

public sealed class Day02 : BaseDay
{
    public Day02() : base(2, 2022)
    {
    }

    public override void Parse() => Part1();
    public override string Part1() => GetWrongPoints().ToString();
    public override string Part2() => GetPoints().ToString();

    private int GetWrongPoints()
    {
        int[][] scores =
        {
            new[] { 4, 8, 3 },
            new[] { 1, 5, 9 },
            new[] { 7, 2, 6 }
        };
        
        var points = 0;
        for (var i = 0; i < Input.Length; i++)
        {
            points += scores[Input[i][0] - 'A'][Input[i][2] - 'X'];
        }

        return points;
    }

    private int GetPoints()
    {
        int[][] scores =
        {
            new[] { 3, 4, 8 },
            new[] { 1, 5, 9 },
            new[] { 2, 6, 7 }
        };
        
        var points = 0;
        for (var i = 0; i < Input.Length; i++)
        {
            points += scores[Input[i][0] - 'A'][Input[i][2] - 'X'];
        }

        return points;
    }
}