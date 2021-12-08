namespace AdventOfCode;

public sealed class Day02 : ExtendedDay
{
    private readonly string[] _input;

    public Day02()
    {
        _input = InputAsArray;
    }

    public override ValueTask<string> Solve_1() => new(FindDestination().ToString());
    public override ValueTask<string> Solve_2() => new(FindDestinationWithAim().ToString());
    

    private int FindDestination()
    {
        var steps = ParseInput();
        var distance = 0;
        var depth = 0;

        foreach (var step in steps)
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
        var steps = ParseInput();
        var distance = 0;
        var depth = 0;
        var aim = 0;

        foreach (var step in steps)
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

    private Instruction[] ParseInput()
    {
        var parsed = new Instruction[_input.Length];
            
        for (var i = 0; i < _input.Length; i++)
        {
            var split = _input[i].Split(' ');
            parsed[i] = new Instruction(split[0], int.Parse(split[1]));
        }

        return parsed;
    }
}

public readonly struct Instruction
{
    public string Direction { get; }
    public int Amount { get; }

    public Instruction(string direction, int amount)
    {
        Direction = direction;
        Amount = amount;
    }
}