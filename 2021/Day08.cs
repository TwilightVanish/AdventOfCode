using AdventOfCode.Common;

namespace AdventOfCode._2021;

public sealed class Day08 : BaseDay
{
    public Day08() : base(8, 2021) {}

    public override void Parse() {}
    public override string Part1() => UniqueCount().ToString();
    public override string Part2() => FindOutput().ToString();
    
    private int UniqueCount()
    {
        var total = 0;
        for (var i = 0; i < Input.Length; i++)
        {
            var split = Input[i].Split(" | ")[1].Split(' ');
            for (var h = 0; h < split.Length; h++)
            {
                if (split[h].Length is 2 or 3 or 4 or 7) total++;
            }
        }

        return total;
    }

    private int FindOutput()
    {
        const string stdPattern = "abcefg cf acdeg acdfg bdcf abdfg abdefg acf abcdefg abcdfg";
        var stdPatternSplit = stdPattern.Split(' ');
		
        var parsed = new string[Input.Length][];
        for (var i = 0; i < Input.Length; i++)
        {
            parsed[i] = Input[i].Split("|");
        }
		
        var dict = new Dictionary<string, int>();
        for (var i = 0; i < stdPatternSplit.Length; i++)
        {
            dict[FindPattern(stdPatternSplit[i], CountCharacters(stdPattern))] = i;
        }

        var total = 0;
        for (var i = 0; i < parsed.Length; i++)
        {
            var results = parsed[i][1].Trim().Split(" ");
            var count = CountCharacters(parsed[i][0]);
            var resolved = new int[results.Length];

            for (var s = 0; s < results.Length; s++)
            {
                var pattern = FindPattern(results[s], count);
                resolved[s] = dict[pattern];
            }

            total += int.Parse(string.Join("", resolved));
        }

        return total;
    }

    private Dictionary<char, int> CountCharacters(string line)
    {
        var counts = new Dictionary<char, int>();
        line = line.Replace(" ", "");
        for (var c = 0; c < line.Length; c++)
        {
            if (!counts.ContainsKey(line[c]))
                counts.Add(line[c], 0);

            counts[line[c]]++;
        }

        return counts;
    }

    private string FindPattern(string segments, Dictionary<char, int> count)
    {
        var pattern = new int[segments.Length];
        for (var c = 0; c < segments.Length; c++)
        {
            pattern[c] = count[segments[c]];
        }
        Array.Sort(pattern);

        return string.Join("", pattern);
    }
}