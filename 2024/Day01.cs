using System.Numerics;
using AdventOfCode.Common;
using AdventOfCode.Utility;

namespace AdventOfCode._2024;

public sealed class Day01 : BaseDay
{
    private readonly LocationData _locationData;
    
    public Day01() : base(1, 2024)
    {
        _locationData = ParseLocationData();
    }

    public override void Parse() => ParseLocationData();
    public override string Part1() => GetDifferenceSum().ToString();
    public override string Part2() => CalculateSimilarityScore().ToString();
    
    private int GetDifferenceSum()
    {
        var one = _locationData.One;
        var two = _locationData.Two;

        var vectorSize = Vector<int>.Count;
        var vectorSum = Vector<int>.Zero;
        int i;

        for (i = 0; i <= one.Length - vectorSize; i += vectorSize)
        {
            var vectorOne = new Vector<int>(one, i);
            var vectorTwo = new Vector<int>(two, i);
            var absDiff = Vector.Abs(vectorOne - vectorTwo);
            vectorSum += absDiff;
        }

        return Vector.Sum(vectorSum);
    }

    private int CalculateSimilarityScore()
    {
        var score = 0;
        var one = _locationData.One;
        var two = _locationData.Two;

        var depth = 0;
        for (var i = 0; i < one.Length; i++)
        {
            var frequency = 0;
            
            while (depth < two.Length && two[depth] < one[i]) depth++;
            while (depth < two.Length && two[depth] == one[i])
            {
                frequency++;
                depth++;
            }
            
            score += one[i] * frequency;
        }
        
        return score;
    }

    private LocationData ParseLocationData()
    {
        var locationData = new LocationData(Input.Length);
        
        var index = 0;
        foreach (var line in RawInput.AsSpan().EnumerateLines())
        {
            var (id1, id2) = ParseLine(line);
            locationData.One[index] = id1;
            locationData.Two[index] = id2;
            index++;
        }
        
        Array.Sort(locationData.One);
        Array.Sort(locationData.Two);

        return locationData;
    }

    private static (int, int) ParseLine(ReadOnlySpan<char> line)
    {
        var id1 = 0;
        var offset = 0;

        while (line[offset] != ' ')
        {
            id1 = id1 * 10 + (line[offset++] - '0');
        }

        var id2 = CustomParser.GreedyParseInt(line[offset..]);
        return (id1, id2);
    }

    private readonly struct LocationData()
    {
        public LocationData(int length) : this()
        {
            One = new int[length];
            Two = new int[length];
        }
        
        public int[] One { get; }
        public int[] Two { get; }
    }
}