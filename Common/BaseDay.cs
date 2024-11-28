namespace AdventOfCode.Common;

public abstract class BaseDay
{
    public int Day { get; }
    public int Year { get; }
    protected string[] Input { get; }
    protected string RawInput { get; }
    
    protected BaseDay(int day, int year)
    {
        Day = day;
        Year = year;
        
        var directory = new DirectoryInfo("../../../../").Name;
        var isBenchmark = directory.StartsWith("net");
        if (isBenchmark) Console.SetOut(TextWriter.Null);
        var relative = isBenchmark ? "../../../../../../../" : "../../../";
        try
        {
            Input = File.ReadLines($"{relative}{Year}/Inputs/{Day:D2}.txt").ToArray();
            RawInput = File.ReadAllText($"{relative}{Year}/Inputs/{Day:D2}.txt");
        }
        catch (FileNotFoundException)
        {
            throw new Exception($"There was no input found for {Day} of {Year}");
        }
    }

    public abstract string Part1();
    public abstract string Part2();
    public abstract void Parse();
}