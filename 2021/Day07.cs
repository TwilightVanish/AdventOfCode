using AdventOfCode.Common;

namespace AdventOfCode._2021;

public sealed class Day07 : BaseDay
{
    private readonly int[] _crabs;
    
    public Day07() : base(7, 2021)
    {
        _crabs = ParseInput();
    }
    
    public override void Parse() => ParseInput();
    public override string Part1() => CheapAlign().ToString();
    public override string Part2() => Align().ToString();
    
    private int CheapAlign()
    {
        Array.Sort(_crabs);
            
        var median = _crabs[_crabs.Length / 2];

        var sum = 0;
        for (var i = 0; i < _crabs.Length; i++)
        {
            sum += Math.Abs(_crabs[i] - median);
        }

        return sum;
    }
        
    private int Align()
    {
        var mean = 0;
        for (var i = 0; i < _crabs.Length; i++)
        {
            mean += _crabs[i];
        }
        mean /= _crabs.Length;

        var floor = 0;
        var ceil = 0;
        for (var i = 0; i < _crabs.Length; i++)
        {
            floor += Math.Abs(_crabs[i] - mean) * (Math.Abs(_crabs[i] - mean) + 1) / 2;
            ceil += Math.Abs(_crabs[i] - mean + 1) * (Math.Abs(_crabs[i] - mean + 1) + 1) / 2;
        }
            
        return Math.Min(floor, ceil);
    }

    private int[] ParseInput()
    {
        var split = RawInput.Split(',');
            
        var crabs = new int[split.Length];
        for (var i = 0; i < split.Length; i++)
        {
            crabs[i] = int.Parse(split[i]);
        }

        return crabs;
    }
}