using AdventOfCode.Common;
using AdventOfCode.Utility;

namespace AdventOfCode._2022;

public sealed class Day05 : BaseDay
{
    private CargoBay _cargoBay;
    
    public Day05() : base(5, 2022)
    {
        _cargoBay = ParseCargoBay();
    }

    public override void Parse() => ParseCargoBay();
    public override string Part1() => CrateMover9000();
    public override string Part2() => CrateMover9001();

    private string CrateMover9000()
    {
        var stacks = GetStacks();
        var instructions = _cargoBay.Instructions;
        
        for (var i = 0; i < instructions.Length; i++)
        {
            for (var crate = 0; crate < instructions[i].Amount; crate++)
            {
                stacks[instructions[i].To].Push(stacks[instructions[i].From].Pop());
            }
        }

        return GetMessage(stacks);
    }
    
    private string CrateMover9001()
    {
        var stacks = GetStacks();
        var instructions = _cargoBay.Instructions;
        
        for (var i = 0; i < instructions.Length; i++)
        {
            var toMove = new char[instructions[i].Amount];
            for (var crate = 0; crate < instructions[i].Amount; crate++)
            {
                toMove[crate] = stacks[instructions[i].From].Pop();
            }
            
            for (var crate = 1; crate <= instructions[i].Amount; crate++)
            {
                stacks[instructions[i].To].Push(toMove[^crate]);
            }
        }

        return GetMessage(stacks);
    }

    private string GetMessage(Stack<char>[] stacks)
    {
        var message = "";
        for (var i = 0; i < _cargoBay.Stacks.Length; i++)
        {
            message += stacks[i].Pop();
        }

        return message;
    }

    private Stack<char>[] GetStacks()
    {
        var stacks = new Stack<char>[_cargoBay.Stacks.Length];
        
        for (var i = 0; i < _cargoBay.Stacks.Length; i++)
        {
            stacks[i] = new Stack<char>(_cargoBay.Stacks[i]);
        }

        return stacks;
    }

    private CargoBay ParseCargoBay()
    {
        int empty;
        for (empty = 0; empty < Input.Length; empty++)
        {
            if (Input[empty] == "") break;
        }
        
        var rawCrates = Input[..empty];
        var columns = rawCrates[^1];
        var columnCount = columns.Length / 4 + 1;
        var parsedCrates = new IEnumerable<char>[columnCount];
        
        for (var column = 0; column < columnCount; column++)
        {
            var crate = "";
            for (var row = rawCrates.Length - 2; row >= 0; row--)
            {
                if(rawCrates[row][column * 4 + 1] == ' ') continue;
                crate += rawCrates[row][column * 4 + 1];
            }

            parsedCrates[column] = crate;
        }
        
        
        var rawInstructions = Input[(empty + 1)..];
        var parsedInstructions = new Instruction[rawInstructions.Length];

        for (var row = 0; row < rawInstructions.Length; row++)
        {
            var splitLine = rawInstructions[row].Split(' ');
            parsedInstructions[row] = new Instruction(
                CustomParser.ParseInt(splitLine[1]),
                CustomParser.ParseInt(splitLine[3]) - 1,
                CustomParser.ParseInt(splitLine[5]) - 1
            );
        }

        return new CargoBay(parsedCrates, parsedInstructions);
    }
    
    private record struct Instruction(int Amount, int From, int To);
    
    private record struct CargoBay(IEnumerable<char>[] Stacks, Instruction[] Instructions);
}