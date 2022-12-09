using AdventOfCode.Common;
using AdventOfCode.Utility;

namespace AdventOfCode._2022;

public sealed class Day09 : BaseDay
{
    private readonly Instruction[] _instructions;
    
    public Day09() : base(9, 2022)
    {
        _instructions = GetInstructions();
    }

    public override void Parse() => GetInstructions();
    public override string Part1() => ShortTail().ToString();
    public override string Part2() => LongTail().ToString();

    private int ShortTail()
    {
        var positions = new HashSet<Point>();
        
        var tail = new Point(0, 0);
        var head = new Point(0, 0);

        for (var i = 0; i < _instructions.Length; i++)
        {
            for (var x = 0; x < _instructions[i].Amount; x++)
            {
                head = MoveHead(head, _instructions[i].Direction);
                tail = MoveTail(head, tail);
                positions.Add(tail);
            }
        }

        return positions.Count;
    }

    private int LongTail()
    {
        var positions = new HashSet<Point>();
        
        var rope = new Point[10];
        for (var i = 0; i < rope.Length; i++)
        {
            rope[i] = new Point(0, 0);
        }

        for (var i = 0; i < _instructions.Length; i++)
        {
            for (var x = 0; x < _instructions[i].Amount; x++)
            {
                rope[0] = MoveHead(rope[0], _instructions[i].Direction);
                for (var k = 1; k < rope.Length; k++)
                {
                    rope[k] = MoveTail(rope[k - 1], rope[k]);
                }

                positions.Add(rope[^1]);
            }
            
        }

        return positions.Count;
    }

    private static Point MoveHead(Point head, char instruction)
    {
        switch (instruction)
        {
            case 'R':
                head.X++;
                break;
            case 'U':
                head.Y++;
                break;
            case 'L':
                head.X--;
                break;
            case 'D':
                head.Y--;
                break;
        }

        return head;
    }

    private static Point MoveTail(Point head, Point tail)
    {
        var distance = (int) Math.Sqrt(Math.Pow(tail.X - head.X, 2) + Math.Pow(tail.Y - head.Y, 2));
        if (distance <= 1) return tail;
        
        var angle = Math.Atan2(tail.X - head.X, tail.Y - head.Y) * (180 / Math.PI);

        if (angle is < 0 and > -180) tail.X++;
        if (angle is > 0 and < 180) tail.X--;
        if (angle is > 90 or < -90) tail.Y++;
        if (angle is < 90 and > -90) tail.Y--;
        
        return tail;
    }

    private Instruction[] GetInstructions()
    {
        var instructions = new Instruction[Input.Length];
        for (var i = 0; i < Input.Length; i++)
        {
            instructions[i] = new Instruction(Input[i][0], CustomParser.ParseInt(Input[i][2..]));
        }

        return instructions;
    }

    private record struct Point(int X, int Y);

    private record struct Instruction(char Direction, int Amount);
}