using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using AdventOfCode.AOCHelper;

namespace AdventOfCode
{
    public sealed class Day05 : ExtendedDay
    {
        private readonly string[] _input;

        public Day05()
        {
            _input = InputAsArray;
        }
        
        public override ValueTask<string> Solve_1() => new(FindOverlaps(false).ToString());
        public override ValueTask<string> Solve_2() => new(FindOverlaps(true).ToString());
        
        private int FindOverlaps(bool diagonals)
        {
            var lines = GetLines();
            var points = new Dictionary<Point, int>();

            for (var i = 0; i < lines.Length; i++)
            {
                var currentPoints = GetPoints(lines[i], diagonals);
                for (var p = 0; p < currentPoints.Length; p++)
                    if (points.ContainsKey(currentPoints[p]))
                        points[currentPoints[p]]++;
                    else
                        points.Add(currentPoints[p], 1);
            }

            var count = 0;
            foreach (var value in points.Values)
            {
                if (value > 1) count++;
            }

            return count;
        }

        private static Point[] GetPoints(Line line, bool diagonals)
        {
            if (!diagonals && !(line.Start.X == line.End.X || line.Start.Y == line.End.Y))
                return Array.Empty<Point>();

            var x0 = line.Start.X;
            var x1 = line.End.X;
            var y0 = line.Start.Y;
            var y1 = line.End.Y;

            var points = x0 != x1 ? new Point[Math.Abs(x0 - x1) + 1] : new Point[Math.Abs(y0 - y1) + 1];

            var steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
            if (steep)
            {
                (x0, y0) = (y0, x0);
                (x1, y1) = (y1, x1);
            }

            if (x0 > x1)
            {
                (x0, x1) = (x1, x0);
                (y0, y1) = (y1, y0);
            }

            var xDelta = x1 - x0;
            var yDelta = Math.Abs(y1 - y0);
            var error = xDelta / 2;
            var yStep = y0 < y1 ? 1 : -1;
            var y = y0;
            for (var x = x0; x <= x1; x++)
            {
                points[x - x0] = new Point(steep ? y : x, steep ? x : y);
                error -= yDelta;
                if (error >= 0) continue;
                y += yStep;
                error += xDelta;
            }

            return points;
        }

        private Line[] GetLines()
        {
            var lines = new Line[_input.Length];
            for (var i = 0; i < _input.Length; i++)
            {
                var split = _input[i].Split(" -> ");
                var start = split[0].Split(',');
                var end = split[1].Split(',');
                var startPoint = new Point(int.Parse(start[0]), int.Parse(start[1]));
                var endPoint = new Point(int.Parse(end[0]), int.Parse(end[1]));

                lines[i] = new Line(startPoint, endPoint);
            }

            return lines;
        }
    }

    public readonly struct Line
    {
        public Point Start { get; }
        public Point End { get; }

        public Line(Point start, Point end)
        {
            Start = start;
            End = end;
        }
    }
}