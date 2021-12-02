using System;
using System.Linq;
using System.Threading.Tasks;
using AdventOfCode.AOCHelper;

namespace AdventOfCode
{
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
                    case Direction.Forward:
                        distance += step.Amount;
                        break;
                    case Direction.Up:
                        depth -= step.Amount;
                        break;
                    case Direction.Down:
                        depth += step.Amount;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
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
                    case Direction.Forward:
                        distance += step.Amount;
                        depth += step.Amount * aim;
                        break;
                    case Direction.Up:
                        aim -= step.Amount;
                        break;
                    case Direction.Down:
                        aim += step.Amount;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

            return distance * depth;
        }

        private Instruction[] ParseInput()
        {
            return _input
                .Select(x => x.Split(' '))
                .Select(x => new Instruction(Enum.Parse<Direction>(x[0], true), int.Parse(x[1])))
                .ToArray();
        }
    }

    public readonly struct Instruction
    {
        public Direction Direction { get; }
        public int Amount { get; }

        public Instruction(Direction direction, int amount)
        {
            Direction = direction;
            Amount = amount;
        }
    }

    public enum Direction
    {
        Forward,
        Up,
        Down
    }
}