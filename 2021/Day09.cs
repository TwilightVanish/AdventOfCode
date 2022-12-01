using System.Collections.Concurrent;
using System.Drawing;
using AdventOfCode.Common;

namespace AdventOfCode._2021;

public sealed class Day09 : BaseDay
{
    private readonly int[][] graph;
    
    public Day09() : base(9, 2021)
    {
        graph = ParseInput();
    }
    
    public override void Parse() => ParseInput();
    public override string Part1() => GetRiskLevel().ToString();
    public override string Part2() => FindBasins().ToString();
    
    private int GetRiskLevel()
    {
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
                        
        if (point.X < Input[0].Length - 1)
        {
            neighbours.Add(new Point(point.X + 1, point.Y));
        }
                        
        if (point.Y != 0)
        {
            neighbours.Add(new Point(point.X, point.Y - 1));
        }
                        
        if (point.Y < Input.Length - 1)
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
        var parsedGraph = new int[Input.Length][];
        for (var line = 0; line < Input.Length; line++)
        {
            var parsedLine = new int[Input[line].Length];
            for (var chr = 0; chr < Input[line].Length; chr++)
            {
                parsedLine[chr] = Input[line][chr] - '0';
            }

            parsedGraph[line] = parsedLine;
        }

        return parsedGraph;
    }
}