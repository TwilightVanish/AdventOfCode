using AdventOfCode.Common;

namespace AdventOfCode._2025;

public sealed class Day03() : BaseDay(3, 2025)
{
    public override void Parse() {}
    public override string Part1() => GetTotalVoltage(2).ToString();
    public override string Part2() => GetTotalVoltage(12).ToString();

    private long GetTotalVoltage(int amountOfBatteries) => Input.Sum(row => GetMaxRowVoltage(row, amountOfBatteries));
    
    private static long GetMaxRowVoltage(ReadOnlySpan<char> row, int amountOfBatteries)
    {
        long sum = 0;
        var offset = 0;

        for (var b = 0; b < amountOfBatteries; b++)
        {
            var highest = 0;
            var highestIndex = 0;

            var limit = row.Length - (amountOfBatteries - (b + 1));

            for (var i = offset; i < limit; i++)
            {
                if (row[i] <= highest) continue;
                highest = row[i];
                highestIndex = i;
            }

            offset = highestIndex + 1;
            sum *= 10;
            sum += highest - '0';
        }

        return sum;
    }
}