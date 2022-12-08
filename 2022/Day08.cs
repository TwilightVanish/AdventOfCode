using AdventOfCode.Common;

namespace AdventOfCode._2022;

public sealed class Day08 : BaseDay
{
    private readonly Tree[] _trees;

    public Day08() : base(8, 2022)
    {
        _trees = ParseTrees();
    }

    public override void Parse() => ParseTrees();
    public override string Part1() => CountVisible().ToString();
    public override string Part2() => GetScores().ToString();

    private int CountVisible()
    {
        var count = Input.Length * 2 + Input[0].Length * 2 - 4;
        for (var i = 0; i < _trees.Length; i++)
        {
            if (_trees[i].Visible) count++;
        }
        
        return count;
    }

    private int GetScores()
    {
        var bestScore = 0;
        for (var i = 0; i < _trees.Length; i++)
        {
            if (_trees[i].Score > bestScore)
                bestScore = _trees[i].Score;
        }

        return bestScore;
    }

    private Tree[] ParseTrees()
    {
        var height = Input.Length;
        var width = Input[0].Length;

        var trees = new Tree[(height - 1) * (width - 1)];
        
        for (var row = 1; row < height - 1; row++)
        {
            for (var column = 1; column < width - 1; column++)
            {
                var currentTree = Input[row][column];

                var visible = false;
                int left = 0, right = 0, up = 0, down = 0;
        
                for (var i = column - 1; i >= 0; i--)
                {
                    left++;
                    if (Input[row][i] >= currentTree) break;
                    if (i == 0) visible = true;
                }
        
                for (var i = column + 1; i < width; i++)
                {
                    right++;
                    if (Input[row][i] >= currentTree) break;
                    if (i == width - 1) visible = true;
                }
        
                for (var i = row - 1; i >= 0; i--)
                {
                    up++;
                    if (Input[i][column] >= currentTree) break;
                    if (i == 0) visible = true;
                }
        
                for (var i = row + 1; i < height; i++)
                {
                    down++;
                    if (Input[i][column] >= currentTree) break;
                    if (i == height - 1) visible = true;
                }

                trees[(row - 1) * width + column - 1] = new Tree(visible, left * right * up * down);
            }
        }

        return trees;
    }

    private record struct Tree(bool Visible, int Score);
}