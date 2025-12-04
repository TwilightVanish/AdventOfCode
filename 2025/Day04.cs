using AdventOfCode.Common;

namespace AdventOfCode._2025;

public sealed class Day04() : BaseDay(4, 2025)
{
    private static readonly (int dx, int dy)[] Offsets =
    {
        (-1, -1), (0, -1), (1, -1),
        (-1,  0),          (1,  0),
        (-1,  1), (0,  1), (1,  1)
    };

    public override void Parse() {}
    public override string Part1() => CountAccessibleRolls().ToString();
    public override string Part2() => ClearRollsUntilStuck().ToString();
    
    private int ClearRollsUntilStuck()
    {
        var count = 0;
        var grid = ParseGrid();
        
        while (true)
        {
            var accessible = FindAccessibleRolls(grid);
            if (accessible.Count == 0) return count;
            count += accessible.Count;

            foreach (var a in accessible)
            {
                grid[a.y][a.x] = false;
            }
        }
    }
    
    private int CountAccessibleRolls()
    {
        var count = 0;
        var grid = ParseGrid();
        
        Parallel.For(0, grid.Length, () => 0, (y, _, local) =>
            {
                for (var x = 0; x < grid[y].Length; x++)
                    if (grid[y][x] && CountNeighbors(grid, x, y) < 4)
                        local++;
                return local;
            },
            local => Interlocked.Add(ref count, local));
        return count;
    }
    
    private static List<(int x, int y)> FindAccessibleRolls(bool[][] grid) =>
        Enumerable.Range(0, grid.Length)
            .AsParallel()
            .SelectMany(y =>
            {
                var result = new List<(int x, int y)>();
                for (var x = 0; x < grid[y].Length; x++)
                {
                    if (!grid[y][x]) continue;
                    if (CountNeighbors(grid, x, y) < 4)
                        result.Add((x, y));
                }
                return result;
            })
            .ToList();
    
    private static int CountNeighbors(bool[][] grid, int x, int y)
    {
        var count = 0;

        foreach (var (dx, dy) in Offsets)
        {
            var nx = x + dx;
            var ny = y + dy;

            if ((uint)nx >= (uint)grid[0].Length) continue;
            if ((uint)ny >= (uint)grid.Length) continue;

            if (grid[ny][nx]) count++;
            if (count == 4) return count;
        }

        return count;
    }

    private bool[][] ParseGrid()
    {
        var parsed = new bool[Input.Length][];

        for (var y = 0; y < Input.Length; y++)
        {
            var lineArray = new bool[Input[y].Length];
            for (var x = 0; x < Input[y].Length; x++)
            {
                lineArray[x] = Input[y][x] == '@';
            }

            parsed[y] = lineArray;
        }

        return parsed;
    }
}