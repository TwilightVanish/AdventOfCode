using AdventOfCode.Common;

namespace AdventOfCode._2022;

public sealed class Day06 : BaseDay
{
    public Day06() : base(6, 2022) {}

    public override void Parse() {}
    public override string Part1() => FindMarker(4).ToString();
    public override string Part2() => FindMarker(14).ToString();

    private int FindMarker(int length)
    {
        var stream = RawInput.AsSpan();
        for (var i = 0; i < stream.Length - length; i++)
        {
            var isUnique = false;
            for (var j = i; j < i + length; j++)
            {
                isUnique = true;
                for (var k = i; k < j; k++)
                {
                    if (stream[j] != stream[k]) continue;

                    isUnique = false;
                    break;
                }

                if (!isUnique) break;
            }

            if (isUnique) return i + length;
        }

        return -1;
    }
}