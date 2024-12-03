using AdventOfCode.Common;
using AdventOfCode.Utility;

namespace AdventOfCode._2024;

public sealed class Day03 : BaseDay
{
    private readonly List<MultiplyOperation> _multiplyOperations;
    
    public Day03() : base(3, 2024)
    {
        _multiplyOperations = ParseMultiplyOperations();
    }

    public override void Parse() => ParseMultiplyOperations();
    public override string Part1() => SumMultiplications().ToString();
    public override string Part2() => SumEnabledMultiplications().ToString();

    private int SumMultiplications()
    {
        var sum = 0;
        
        for (var index = 0; index < _multiplyOperations.Count; index++)
        {
            sum += _multiplyOperations[index].Result;
        }

        return sum;
    }
    
    private int SumEnabledMultiplications()
    {
        var sum = 0;
        
        for (var index = 0; index < _multiplyOperations.Count; index++)
        {
            if (_multiplyOperations[index].Enabled) sum += _multiplyOperations[index].Result;
        }

        return sum;
    }

    private List<MultiplyOperation> ParseMultiplyOperations()
    {
        var multiplyOperations = new List<MultiplyOperation>();
        var enabled = true;
        var inSpan = RawInput.AsSpan();

        for (var i = 0; i <= inSpan.Length - 8; i++)
        {
            if (inSpan[i] == 'd')
            {
                if (inSpan.Slice(i, 4).SequenceEqual("do()"))
                {
                    enabled = true;
                    i += 3;
                    continue;
                }
                if (inSpan.Slice(i, 7).SequenceEqual("don't()"))
                {
                    enabled = false;
                    i += 6;
                }
                continue;
            }

            if (inSpan[i] != 'm' || !inSpan.Slice(i, 4).SequenceEqual("mul(")) continue;
            
            var offset = i + 4;
            if (!CustomParser.TryParseIntWithRefOffset(inSpan, ref offset, out var amount1) || 
                offset >= inSpan.Length || inSpan[offset++] != ',' ||
                !CustomParser.TryParseIntWithRefOffset(inSpan, ref offset, out var amount2) || 
                offset >= inSpan.Length || inSpan[offset] != ')') continue;

            multiplyOperations.Add(new MultiplyOperation(amount1 * amount2, enabled));
            i = offset;
        }

        return multiplyOperations;
    }
    
    private record struct MultiplyOperation(int Result, bool Enabled);
}