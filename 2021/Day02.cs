using AdventOfCode.Common;

namespace AdventOfCode._2021;

public sealed class Day02 : BaseDay
{
    private readonly Instruction[] _steps;
    
    public Day02() : base(2, 2021)
    {
        _steps = ParseSteps();
    }
    
    public override void Parse() => ParseSteps();
    public override string Part1() => FindDestination().ToString();
    public override string Part2() => FindDestinationWithAim().ToString();
    
    private int FindDestination()
    {
        var distance = 0;
        var depth = 0;

        foreach (var step in _steps)
            switch (step.Direction)
            {
                case "forward":
                    distance += step.Amount;
                    break;
                case "up":
                    depth -= step.Amount;
                    break;
                default:
                    depth += step.Amount;
                    break;
            }

        return distance * depth;
    }

    private int FindDestinationWithAim()
    {
        var distance = 0;
        var depth = 0;
        var aim = 0;

        foreach (var step in _steps)
            switch (step.Direction)
            {
                case "forward":
                    distance += step.Amount;
                    depth += step.Amount * aim;
                    break;
                case "up":
                    aim -= step.Amount;
                    break;
                default:
                    aim += step.Amount;
                    break;
            }

        return distance * depth;
    }
    
    private Instruction[] ParseSteps()
    {
        var parsed = new Instruction[Input.Length];
            
        for (var i = 0; i < Input.Length; i++)
        {
            var split = Input[i].Split(' ');
            parsed[i] = new Instruction(split[0], int.Parse(split[1]));
        }

        return parsed;
    }

    private readonly struct Instruction
    {
        public string Direction { get; }
        public int Amount { get; }

        public Instruction(string direction, int amount)
        {
            Direction = direction;
            Amount = amount;
        }
    }
}