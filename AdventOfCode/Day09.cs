using System.Collections.Concurrent;
using System.Drawing;

namespace AdventOfCode;

public sealed class Day09 : ExtendedDay
{
    private readonly string[] _input;

    public Day09()
    {
        _input = InputAsArray;
    }

    public override ValueTask<string> Solve_1() => new(GetRiskLevel().ToString());
    public override ValueTask<string> Solve_2() => new(FindBasins().ToString());

    private int GetRiskLevel()
    {
        var graph = ParseInput();
        var lowPoints = GetLowPoints(graph);

        var riskLevel = 0;
        for (var i = 0; i < lowPoints.Length; i++)
        {
            riskLevel += graph[lowPoints[i].Y][lowPoints[i].X] + 1;
        }
        
        return riskLevel;
    }

    private int FindBasins()
    {
        var graph = ParseInput();
        var lowPoints = GetLowPoints(graph);

        var basins = new ConcurrentBag<int>();
        Parallel.For(0, lowPoints.Length, i =>
        {
            var visited = new HashSet<Point>();
            var queue = new Queue<Point>(new[] {new Point(lowPoints[i].X, lowPoints[i].Y)});

            var pointCount = 0;
            while (queue.TryDequeue(out var point))
            {
                if (!visited.Add(point)) continue;

                if (graph[point.Y][point.X] == 9) continue;
                pointCount++;

                var neighbours = GetValidNeighbours(point);
                for (var n = 0; n < neighbours.Count; n++)
                {
                    queue.Enqueue(neighbours[n]);
                }
            }

            basins.Add(pointCount);
        });

        var results = basins.ToArray();
        Array.Sort(results);
        
        return results[^1] * results[^2] * results[^3];
    }

    private List<Point> GetValidNeighbours(Point point)
    {
        var neighbours = new List<Point>(4);
        
        if (point.X != 0)
        {
            neighbours.Add(new Point(point.X - 1, point.Y));
        }
                        
        if (point.X < _input[0].Length - 1)
        {
            neighbours.Add(new Point(point.X + 1, point.Y));
        }
                        
        if (point.Y != 0)
        {
            neighbours.Add(new Point(point.X, point.Y - 1));
        }
                        
        if (point.Y < _input.Length - 1)
        {
            neighbours.Add(new Point(point.X, point.Y + 1));
        }

        return neighbours;
    }

    private Point[] GetLowPoints(int[][] graph)
    {
        var lowPoints = new ConcurrentBag<Point>();

        Parallel.For(0, graph.Length, line =>
        {
            for (var chr = 0; chr < graph[line].Length; chr++)
            {
                var currentPoint = new Point(chr, line);
                var neighbours = GetValidNeighbours(currentPoint);
                var lowPoint = true;

                for (var n = 0; n < neighbours.Count; n++)
                {
                    if (graph[line][chr] >= graph[neighbours[n].Y][neighbours[n].X])
                    {
                        lowPoint = false;
                    }
                }

                if (lowPoint)
                {
                    lowPoints.Add(currentPoint);
                }
            }
        });

        return lowPoints.ToArray();
    }

    private int[][] ParseInput()
    {
        var parsedGraph = new int[_input.Length][];
        for (var line = 0; line < _input.Length; line++)
        {
            var parsedLine = new int[_input[line].Length];
            for (var chr = 0; chr < _input[line].Length; chr++)
            {
                parsedLine[chr] = _input[line][chr] - '0';
            }

            parsedGraph[line] = parsedLine;
        }

        return parsedGraph;
    }
}