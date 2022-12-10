using AdventOfCode.Common;
using AdventOfCode.Utility;

namespace AdventOfCode._2022;

public sealed class Day10 : BaseDay
{
    private readonly int[] _states;
    
    public Day10() : base(10, 2022)
    {
        _states = GetStates();
    }

    public override void Parse() => GetStates();
    public override string Part1() => GetSignalStrength().ToString();
    public override string Part2() => GetScreen();

    private int GetSignalStrength()
    {
        var signalStrength = 0;

        for (var i = 1; i < _states.Length; i++)
        {
            if ((i - 20) % 40 == 0)
            {
                signalStrength += _states[i - 1] * i;
            }
        }

        return signalStrength;
    }

    private string GetScreen()
    {
        Span<char> screen = stackalloc char[6 * 41];

        for (var row = 0; row < _states.Length / 40; row++)
        {
            for (var col = 0; col < 40; col++)
            {
                var cycle = row * 40 + col;
                var offset = cycle % 40 - _states[cycle];
                screen[cycle + row + 1] = offset > -2 && offset < 2 ? '▓' : '░';
                
            }
            screen[row * 40 + row] = '\n';
        }
        
        return new string(screen);
    }

    private int[] GetStates()
    {
        var register = 1;
        var instruction = 0;
        var states = new int[240];

        for (var i = 0; i < 240; i++)
        {
            states[i] = register;
            
            if (Input[instruction].Length > 4)
            {
                i++;
                states[i] = register;
                register += CustomParser.SignedParseInt(Input[instruction].AsSpan(), 5);
            }
            
            instruction++;
        }

        return states;
    }
}