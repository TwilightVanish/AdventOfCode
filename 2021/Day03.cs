using AdventOfCode.Common;

namespace AdventOfCode._2021;

public sealed class Day03 : BaseDay
{
    public Day03() : base(3, 2021) {}

    public override void Parse() {}
    public override string Part1() => GetPowerConsumption().ToString();
    public override string Part2() => GetLifeSupportRating().ToString();
    
    private int GetPowerConsumption()
    {
        var epsilon = "";
        var gamma = "";

        for (var i = 0; i < Input[0].Length; i++)
        {
            var majority = OneCount(Input, i) > 500;
            epsilon += majority ? "0" : "1";
            gamma += majority ? "1" : "0";
        }

        return Convert.ToInt32(epsilon, 2) * Convert.ToInt32(gamma, 2);
    }

    private int GetLifeSupportRating()
    {
        var oxygen = Input;
        var carbon = Input;

        for (var i = 0; i < Input[0].Length; i++)
        {
            if (oxygen.Length == 1 && carbon.Length == 1)
                break;

            if (oxygen.Length != 1)
                oxygen = RemoveDigit(oxygen, i, OneCount(oxygen, i) * 2 >= oxygen.Length ? '1' : '0');

            if (carbon.Length != 1)
                carbon = RemoveDigit(carbon, i, OneCount(carbon, i) * 2 >= carbon.Length ? '0' : '1');
        }

        return Convert.ToInt32(oxygen[0], 2) * Convert.ToInt32(carbon[0], 2);
    }

    private static int OneCount(string[] toCount, int position)
    {
        var count = 0;
        foreach (var t in toCount)
            if (t[position] == '1')
                count++;

        return count;
    }

    private static string[] RemoveDigit(string[] toFilter, int position, char digit)
    {
        var filtered = new List<string>();
        foreach (var t in toFilter)
            if (t[position] == digit)
                filtered.Add(t);

        return filtered.ToArray();
    }
}