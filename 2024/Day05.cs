using System.Numerics;
using AdventOfCode.Common;
using AdventOfCode.Utility;

namespace AdventOfCode._2024;

public sealed class Day05 : BaseDay
{
    private readonly Printer _printer;
    
    public Day05() : base(5, 2024)
    {
        _printer = LoadPrinterData();
    }

    public override void Parse() => LoadPrinterData();
    public override string Part1() => CountValidJobs().ToString();
    public override string Part2() => CountFixedJobs().ToString();

    private int CountValidJobs()
    {
        var count = 0;

        Parallel.For(0, _printer.PrintJobs.Length, i =>
        {
            if (IsValidJob(_printer.PrintJobs[i]))
                Interlocked.Add(ref count, _printer.PrintJobs[i][_printer.PrintJobs[i].Length / 2]);
        });
        
        return count;
    }

    private int CountFixedJobs()
    {
        var count = 0;

        Parallel.For(0, _printer.PrintJobs.Length, i =>
        {
            var job = _printer.PrintJobs[i];
            if (IsValidJob(job)) return;

            Interlocked.Add(ref count, FindFixedMiddleValue(job));
        });

        return count;
    }

    private int FindFixedMiddleValue(int[] job)
    {
        var middleIndex = job.Length / 2;
        for (var i = 0; i < job.Length; i++)
        {
            if (job.Length - 1 - CountApplicableRules(job, job[i]) == middleIndex) return job[i];
        }

        return -1;
    }

    private int CountApplicableRules(int[] job, int page)
    {
        var count = 0;
        var rulesForPage = _printer.Rules[page];

        for (var i = 0; i < rulesForPage.Count; i++)
        {
            if (Array.IndexOf(job, rulesForPage[i]) != -1) count++;
        }
        
        return count;
    }
    
    private bool IsValidJob(int[] job)
    {
        Span<int> visitedPages = stackalloc int[job.Length];
        visitedPages[0] = job[0];
        
        for (var i = 1; i < job.Length; i++)
        {
            var currentPage = job[i];
            visitedPages[i] = currentPage;
            
            if (!_printer.Rules.TryGetValue(currentPage, out var requiredRules)) continue;
            
            for (var j = 0; j < requiredRules.Count; j++)
            {
                if (visitedPages.Contains(requiredRules[j])) return false;
            }
        }
        
        return true;
    }

    private Printer LoadPrinterData()
    {
        var ruleLinesCount = Array.IndexOf(Input, "");
        var sectionSplitIndex = RawInput.IndexOf("\r\n\r\n", StringComparison.Ordinal);
        var inputSpan = RawInput.AsSpan();
        
        var rulesSection = inputSpan[..sectionSplitIndex];
        var jobsSection = inputSpan[(sectionSplitIndex + 4)..];
        
        return new Printer(ParseRules(rulesSection), ParseJobs(jobsSection, Input.Length - ruleLinesCount - 1));
    }

    private static Dictionary<int, List<int>> ParseRules(ReadOnlySpan<char> rawRules)
    {
        var rules = new Dictionary<int, List<int>>();
        foreach (var line in rawRules.EnumerateLines())
        {
            var appliesTo = CustomParser.ParseInt(line[..2]);
            var mustBeBefore = CustomParser.ParseInt(line[3..]);
            
            if (rules.TryGetValue(appliesTo, out var appliesToList)) appliesToList.Add(mustBeBefore);
            else rules.Add(appliesTo, [mustBeBefore]);
        }
        
        return rules;
    }

    private static int[][] ParseJobs(ReadOnlySpan<char> rawJobs, int lineCount)
    {
        var jobs = new int[lineCount][];
        var jobIndex = 0;

        foreach (var line in rawJobs.EnumerateLines())
        {
            var pagesInJob = (line.Length + 1) / 3;
            var job = new int[pagesInJob];

            for (var i = 0; i < pagesInJob; i++)
            {
                job[i] = CustomParser.ParseInt(line.Slice(i * 3, 2));
            }
                
            jobs[jobIndex++] = job;
        }
        
        return jobs;
    }

    private record struct Printer(Dictionary<int, List<int>> Rules, int[][] PrintJobs);
}