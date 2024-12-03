using System.Runtime.CompilerServices;

namespace AdventOfCode.Utility;

public static class CustomParser
{
    public static int ParseInt(string input)
    {
        var value = 0;
        for (var i = 0; i < input.Length; i++) value = value * 10 + (input[i] - '0');
        return value;
    }

    public static int ParseInt(ReadOnlySpan<char> input)
    {
        var value = 0;
        for (var i = 0; i < input.Length; i++) value = value * 10 + (input[i] - '0');
        return value;
    }

    public static int CheckedParseInt(string input)
    {
        var value = 0;
        for (var i = 0; i < input.Length; i++)
        {
            if (input[i] is < '0' or > '9') break;
            value = value * 10 + (input[i] - '0');
        }

        return value;
    }
    
    public static int GreedyParseInt(ReadOnlySpan<char> input)
    {
        var value = 0;
        for (var i = 0; i < input.Length; i++)
        {
            if (input[i] is < '0' or > '9') continue;
            value = value * 10 + (input[i] - '0');
        }

        return value;
    }
    
    public static int ParseIntWithRefOffset(ReadOnlySpan<char> span, ref int offset)
    {
        var result = 0;
        var start = offset;
        while (offset < span.Length && span[offset] >= '0' && span[offset] <= '9')
        {
            result = result * 10 + (span[offset++] - '0');
        }
        return result;
    }
    
    public static bool TryParseIntWithRefOffset(ReadOnlySpan<char> span, ref int offset, out int result)
    {
        result = 0;
        var start = offset;
        while (offset < span.Length && span[offset] >= '0' && span[offset] <= '9')
        {
            result = result * 10 + (span[offset++] - '0');
        }
        return offset > start;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public static int SignedParseInt(ReadOnlySpan<char> input, int offset)
    {
        var negative = false;
        var start = offset;

        if (input[offset] == '-')
        {
            negative = true;
            start++;
        }

        var value = 0;
        for (var i = start; i < input.Length; i++)
        {
            value = value * 10 + (input[i] - '0');
        }

        return negative ? -value : value;
    }
}