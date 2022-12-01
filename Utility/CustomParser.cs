namespace AdventOfCode.Utility;

public static class CustomParser
{
    public static int ParseInt(string input)
    {
        var value = 0;
        for (var i = 0; i < input.Length; i++)
        {
            value = value*10 + (input[i] - '0');
        }
        return value;
    }
}