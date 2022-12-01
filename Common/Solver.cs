using System.Collections;

namespace AdventOfCode.Common;

public static class Solver
{
    private static readonly List<BaseDay> Days = new();

    static Solver()
    {
        var baseType = typeof(BaseDay);
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => baseType.IsAssignableFrom(p) && !p.IsAbstract);
        foreach (var type in types) Days.Add((BaseDay)Activator.CreateInstance(type)!);
    }

    public static List<BaseDay> GetDays() => Days;

    public static BaseDay GetDay(int day, int year)
    {
        var result = Days.FirstOrDefault(x => x.Year == year && x.Day == day);
        if (result is null)
            throw new Exception($"Couldn't find a solution for {day} of {year}");
        return result;
    }

    public static BaseDay GetMostRecentDay()
    {
        var result = Days
            .OrderByDescending(x => x.Year)
            .ThenByDescending(x => x.Day)
            .FirstOrDefault();
        if (result is null)
            throw new Exception("There are no solutions yet");
        return result;
    }
}