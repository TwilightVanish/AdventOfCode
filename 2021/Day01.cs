using AdventOfCode.Common;

namespace AdventOfCode._2021;

public sealed class Day01 : BaseDay
{
    private readonly int[] _depths;
    
    public Day01() : base(1, 2021)
    {
        _depths = ParseDepths();
    }
    
    public override void Parse() => ParseDepths();
    public override string Part1() => CountDepthIncrements(1).ToString();
    public override string Part2() => CountDepthIncrements(3).ToString();
    
    private int CountDepthIncrements(int slidingWindow)
    {
        var count = 0;

        for (var i = slidingWindow; i < _depths.Length; i++)
            if (_depths[i] > _depths[i - slidingWindow]) 
                count++;

        return count;
    }

    private int[] ParseDepths()
    {
        var parsed = new int[Input.Length];
        for (var i = 0; i < Input.Length; i++)
        {
            parsed[i] = int.Parse(Input[i]);
        }

        return parsed;
    }
}