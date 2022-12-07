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
        var neededStorage = _sizes[^1] - 40000000;
        var target = int.MaxValue;
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
            if (Input[i][0] is >= '0' and <= '9')
            {
                currentDirectory!.Size += CustomParser.CheckedParseInt(Input[i]);
                continue;
            }

            if(Input[i][2] != 'c') continue;
            currentDirectory = Input[i] == "$ cd .." ? currentDirectory!.Parent : currentDirectory!.NewDirectory();
        }
        
        var results = new List<int>();
        GetRecursiveSums(root, results);
        
        return results;
    }
    
    private static int GetRecursiveSums(Directory directory, List<int> results)
    {
        var size = directory.Size;
        for (var i = 0; i < directory.Subdirectories.Count; i++)
        {
            size += GetRecursiveSums(directory.Subdirectories[i], results);
        }

        results.Add(size);

        return size;
    }
    
    public class Directory
    {
        public int Size { get; set; }
        public Directory? Parent { get; private init; }
        public List<Directory> Subdirectories { get; } = new();

        public Directory NewDirectory()
        {
            Subdirectories.Add(new Directory{ Parent = this });
            return Subdirectories[^1];
        }
    }
}

