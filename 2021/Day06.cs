using AdventOfCode.Common;

namespace AdventOfCode._2021;

public sealed class Day06 : BaseDay
{
    public Day06() : base(6, 2021) {}

    public override void Parse() {}
    public override string Part1() => SimulatedGrowth(80).ToString();
    public override string Part2() => SimulatedGrowth(256).ToString();
    
    private long SimulatedGrowth(int days)
    {
        Span<long> schools = stackalloc long[9];
        for (var pos = 0; pos < RawInput.Length; pos += 2)
            schools[RawInput[pos] - '0']++;

        for (var day = 0; day < days; day++)
        {
            var temp = 0L;
            for (var i = 8; i >= 0; i--)
            {
                (schools[i], temp) = (temp, schools[i]);
            }
                
            schools[8] = temp;
            schools[6] += temp;
        }

        var sum = 0L;
        for (var s = 0; s <= 8; s++)
        {
            sum += schools[s];
        }

        return sum;
    }
}