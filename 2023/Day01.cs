using AdventOfCode.Common;

namespace AdventOfCode._2023;

public sealed class Day01 : BaseDay
{
    public Day01() : base(1, 2023) { }

    public override void Parse() { }
    public override string Part1() => GetSum(RawInput).ToString();
    public override string Part2() => FindActualCalibrationValues().ToString();

    private int FindActualCalibrationValues()
    {
        string[] numbers = {"one", "two", "three", "four", "five", "six", "seven", "eight", "nine"};
        string[] mask = {"o1e","t2o","t3e","f4r","f5e","s6x","s7n","e8t","n9e"};
        
        var masked = RawInput;
        for (var i = 0; i < numbers.Length; i++)
        {
            masked = masked.Replace(numbers[i], mask[i]);
        }

        return GetSum(masked);
    }

    private static int GetSum(string input)
    {
        var sum = 0;
        
        foreach (var line in input.AsSpan().EnumerateLines())
        {
            sum += (line[line.IndexOfAnyInRange('0', '9')] - '0') * 10;
            sum += line[line.LastIndexOfAnyInRange('0', '9')] - '0';
        }

        return sum;
    }
}