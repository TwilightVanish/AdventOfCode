namespace AdventOfCode;

public sealed class Day01 : ExtendedDay
{
    private readonly string[] _input;

    public Day01()
    {
        _input = InputAsArray;
    }

    public override ValueTask<string> Solve_1() => new(CountDepthIncrements(1).ToString());
    public override ValueTask<string> Solve_2() => new(CountDepthIncrements(3).ToString());

    private int CountDepthIncrements(int slidingWindow)
    {
        var depths = ParseInput();
        var count = 0;

        for (var i = slidingWindow; i < depths.Length; i++)
            if (depths[i] > depths[i - slidingWindow]) 
                count++;

        return count;
    }

    private int[] ParseInput()
    {
        var parsed = new int[_input.Length];
        for (var i = 0; i < _input.Length; i++)
        {
            parsed[i] = int.Parse(_input[i]);
        }

        return parsed;
    }
}