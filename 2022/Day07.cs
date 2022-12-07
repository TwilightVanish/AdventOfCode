using AdventOfCode.Common;
using AdventOfCode.Utility;

namespace AdventOfCode._2022;

public sealed class Day07 : BaseDay
{
    private readonly List<int> _sizes;

    public Day07() : base(7, 2022)
    {
        _sizes = GetFolderSizes();
    }

    public override void Parse() => GetFolderSizes();
    public override string Part1() => GetSmallFolders().ToString();
    public override string Part2() => GetTargetFolder().ToString();

    private int GetSmallFolders()
    {
        var sum = 0;
        for (var i = 0; i < _sizes.Count; i++)
        {
            if(_sizes[i] > 100000) continue;
            sum += _sizes[i];
        }

        return sum;
    }

    private int GetTargetFolder()
    {
        var neededStorage = -40000000 + _sizes[^1];
        var target = 70000000;
        for (var i = 0; i < _sizes.Count; i++)
        {
            if (_sizes[i] >= neededStorage && _sizes[i] < target) 
                target = _sizes[i];
        }

        return target;
    }



    private List<int> GetFolderSizes()
    {
        var root = new Directory();
        var currentDirectory = root;
        
        for (var i = 1; i < Input.Length; i++)
        {
            switch (Input[i][..4])
            {
                case "$ cd":
                    var path = Input[i][5..];
                    currentDirectory = path == ".." ? currentDirectory!.Parent : currentDirectory!.Subdirectories[path];
                    break;
                case "dir ":
                    currentDirectory!.AddDirectory(Input[i][4..]);
                    break;
                case "$ ls":
                    break;
                default:
                    currentDirectory!.Size += CustomParser.ParseInt(Input[i].Split(' ')[0]);
                    break;
            }
        }
        
        var results = new List<int>();
        GetRecursiveSums(root, results);
        
        return results;
    }
    
    private static int GetRecursiveSums(Directory directory, List<int> results)
    {
        var size = directory.Size;
        foreach (var dir in directory.Subdirectories)
        {
            size += GetRecursiveSums(dir.Value, results);
        }
        results.Add(size);

        return size;
    }
    
    public class Directory
    {
        public int Size { get; set; }
        public Directory? Parent { get; private init; }
        public Dictionary<string, Directory> Subdirectories { get; } = new();

        public void AddDirectory(string name)
        {
            Subdirectories.Add(name, new Directory{ Parent = this });
        }
    }
}

