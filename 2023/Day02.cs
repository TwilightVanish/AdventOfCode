using AdventOfCode.Common;
using AdventOfCode.Utility;

namespace AdventOfCode._2023;

public sealed class Day02 : BaseDay
{
    private readonly Cube[][] _games;
    private const int Red = 12;
    private const int Green = 13;
    private const int Blue = 14;
    
    public Day02() : base(2, 2023)
    {
        _games = ParseGames();
    }

    public override void Parse() => ParseGames();
    public override string Part1() => SumPossibleGames().ToString();
    public override string Part2() => SumCubePower().ToString();

    private int SumPossibleGames()
    {
        var sum = 0;
        for (var game = 0; game < _games.Length; game++)
        {
            if (IsValid(_games[game]))
            {
                sum += game + 1;
            }
        }

        return sum;
    }

    private static bool IsValid(Cube[] cubes)
    {
        for (var i = 0; i < cubes.Length; i++)
        {
            var current = cubes[i];
            switch (current.Color)
            {
                case 'r' when current.Amount > Red:
                    return false;
                case 'g' when current.Amount > Green:
                    return false;
                case 'b' when current.Amount > Blue:
                    return false;
            }
        }

        return true;
    }

    private int SumCubePower()
    {
        var sum = 0;
        for (var game = 0; game < _games.Length; game++)
        {
            var red = 0;
            var green = 0;
            var blue = 0;
            
            for (var cube = 0; cube < _games[game].Length; cube++)
            {
                var current = _games[game][cube];
                switch (current.Color)
                {
                    case 'r' when current.Amount > red:
                        red = current.Amount;
                        break;
                    case 'g' when current.Amount > green:
                        green = current.Amount;
                        break;
                    case 'b' when current.Amount > blue:
                        blue = current.Amount;
                        break;
                }
            }

            sum += red * green * blue;
        }

        return sum;
    }

    private Cube[][] ParseGames()
    {
        var games = new Cube[Input.Length][];
        var gameCounter = 0;
        foreach (var line in RawInput.AsSpan().EnumerateLines())
        {
            games[gameCounter++] = ParseGame(line);
        }

        return games;
    }

    private static Cube[] ParseGame(ReadOnlySpan<char> line)
    {
        var cubeCounter = 0;
        var game = new Cube[(line.Count(" ") - 1) / 2];
        var workingLine = line[line.IndexOf(":")..];
        while (!workingLine.IsEmpty)
        {
            var amount = 0;
            var offset = 2;
            while (workingLine[offset] != ' ')
            {
                amount = amount * 10 + (workingLine[offset] - '0');
                offset++;
            }

            var letter = workingLine[offset + 1];
            offset = letter switch
            {
                'r' => offset + 4,
                'g' => offset + 6,
                'b' => offset + 5
            };

            game[cubeCounter++] = new Cube(letter, amount);
            workingLine = workingLine[offset..];
        }

        return game;
    }

    private record struct Cube(char Color, int Amount);
}