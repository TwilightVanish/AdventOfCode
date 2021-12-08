namespace AdventOfCode;

public sealed class Day07 : ExtendedDay
{
    private readonly string _input;

    public Day07()
    {
        _input = InputAsString;
    }

    public override ValueTask<string> Solve_1() => new(CheapAlign().ToString());
    public override ValueTask<string> Solve_2() => new(Align().ToString());

    private int CheapAlign()
    {
        var crabs = ParseInput();
        Array.Sort(crabs);
            
        var median = crabs[crabs.Length / 2];

        var sum = 0;
        for (var i = 0; i < crabs.Length; i++)
        {
            sum += Math.Abs(crabs[i] - median);
        }

        return sum;
    }
        
    private int Align()
    {
        var crabs = ParseInput();

        var mean = 0;
        for (var i = 0; i < crabs.Length; i++)
        {
            mean += crabs[i];
        }
        mean /= crabs.Length;

        var floor = 0;
        var ceil = 0;
        for (var i = 0; i < crabs.Length; i++)
        {
            floor += Math.Abs(crabs[i] - mean) * (Math.Abs(crabs[i] - mean) + 1) / 2;
            ceil += Math.Abs(crabs[i] - mean + 1) * (Math.Abs(crabs[i] - mean + 1) + 1) / 2;
        }
            
        return Math.Min(floor, ceil);
    }

    private int[] ParseInput()
    {
        var split = _input.Split(',');
            
        var crabs = new int[split.Length];
        for (var i = 0; i < split.Length; i++)
        {
            crabs[i] = int.Parse(split[i]);
        }

        return crabs;
    }
}