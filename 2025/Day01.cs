using AdventOfCode.Common;

namespace AdventOfCode._2025;

public sealed class Day01 : BaseDay
{
    private const int Range = 100;
    private readonly int[] _rotations;
    
    public Day01() : base(1, 2025)
    {
        _rotations = GetRotations();
    }

    public override void Parse() => GetRotations();
    public override string Part1() => CountZeroPositions().ToString();
    public override string Part2() => CountZeroClicks().ToString();

    private int CountZeroPositions()
    {
        var counter = 0;
        var position = 50;
        
        for (var i = 0; i < _rotations.Length; i++)
        {
            position += _rotations[i] % Range;
            if (position < 0) position += Range;
            if (position > Range - 1) position -= Range;
            if (position == 0) counter++;
        }

        return counter;
    }
    
    private int CountZeroClicks()
    {
        var counter = 0;
        var position = 50;
        
        for (var i = 0; i < _rotations.Length; i++)
        {
            var startedOnZero = position == 0;

            position += _rotations[i];
            while (position < 0)
            {
                position += Range;
                if (!startedOnZero) counter++;
                startedOnZero = false;
            }
            
            if (position == 0) counter++;

            while (position > Range - 1)
            {
                position -= Range;
                counter++;
            }
        }

        return counter;
    }

    private int[] GetRotations()
    {
        var rotations = new int[Input.Length];
        var i = 0;
        foreach (var line in Input)
        {
            var direction = line[0] == 'L' ? -1 : 1;
            rotations[i++] = Utility.CustomParser.ParseInt(line.AsSpan()[1..]) * direction;
        }

        return rotations;
    }
}