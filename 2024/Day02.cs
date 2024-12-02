using System.Numerics;
using AdventOfCode.Common;

namespace AdventOfCode._2024;

public sealed class Day02 : BaseDay
{
    private readonly int[][] _reports;
    
    public Day02() : base(2, 2024)
    {
        _reports = GetReports();
    }

    public override void Parse() => GetReports();
    public override string Part1() => GetSafeReports().ToString();
    public override string Part2() => GetFaultTolerantSafeReports().ToString();
    
    private int GetSafeReports()
    {
        var safeReports = 0;

        for (var reportIndex = 0; reportIndex < _reports.Length; reportIndex++)
        {
            if (IsSafeReport(_reports[reportIndex])) safeReports++;
        }

        return safeReports;
    }

    private int GetFaultTolerantSafeReports()
    {
        var safeReports = 0;

        for (var reportIndex = 0; reportIndex < _reports.Length; reportIndex++)
        {
            if (IsSafeFaultTolerantReport(_reports[reportIndex])) safeReports++;
        }

        return safeReports;
    }
    
    private int[][] GetReports()
    {
        var reports = new int[Input.Length][];
        var reportIndex = 0;
        
        foreach (var line in RawInput.AsSpan().EnumerateLines())
        {
            var spaceCount = line.Count(' ');
            reports[reportIndex] = new int[spaceCount + 1];
            int value = 0, lineIndex = 0;
            
            for (var i = 0; i < line.Length; i++)
            {
                if (line[i] == ' ')
                {
                    reports[reportIndex][lineIndex++] = value;
                    value = 0;
                }
                else
                {
                    value = value * 10 + (line[i] - '0');
                }
            }

            reports[reportIndex++][lineIndex] = value;
        }
        
        return reports;
    }

    private static bool IsSafeFaultTolerantReport(int[] report)
    {
        var faults = 0;
        var increasing = report[0] <= report[1];

        for (var i = 0; i < report.Length - 1; i++)
        {
            if (IsSafeReading(report[i], report[i + 1], increasing)) continue;
            
            if (faults++ > 0) return false;
            
            if (i + 2 >= report.Length || IsSafeReading(report[i], report[i + 2], increasing))
            {
                i++;
                continue;
            }
            
            if (i == 1 && (IsSafeReading(report[i], report[i + 1], !increasing) || IsSafeReading(report[i - 1], report[i + 1], !increasing)))
            {
                increasing = !increasing;
                continue;
            }

            if (i > 0 && !IsSafeReading(report[i - 1], report[i + 1], increasing)) return false;
            
            if (i == 0 && !IsSafeReading(report[i + 1], report[i + 2], increasing)) increasing = !increasing;
        }
        
        return true;
    }
    
    private static bool IsSafeReport(int[] report)
    {
        var increasing = report[0] <= report[1];
        for (var i = 0; i < report.Length - 1; i++)
        {
            if (!IsSafeReading(report[i], report[i + 1], increasing)) return false;
        }

        return true;
    }
    
    private static bool IsSafeReading(int one, int two, bool increasing) => Math.Abs(one - two) <= 3 && (!increasing || one < two) && (increasing || one > two);
}