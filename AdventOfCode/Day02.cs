using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public sealed class Day02 : BaseDay
    {
        private readonly Instruction[] _input;

        public Day02()
        {
            _input = File.ReadAllText(InputFilePath)
                .Split('\n')
                .Select(x => x.Split(' '))
                .Select(x => new Instruction(Enum.Parse<Direction>(x[0], true), int.Parse(x[1])))
                .ToArray();
        }

        public override ValueTask<string> Solve_1() => new(FindDestination().ToString());
        public override ValueTask<string> Solve_2() => new(FindDestinationWithAim().ToString());
    

        private int FindDestination()
        {
            var distance = 0;
            var depth = 0;

            foreach (var step in _input)
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
            var distance = 0;
            var depth = 0;
            var aim = 0;

            foreach (var step in _input)
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