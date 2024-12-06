using AdventOfCode.Common;

namespace AdventOfCode._2024;

public sealed class Day06 : BaseDay
{
    private static readonly Direction[] Directions = [new(0, -1), new(1, 0), new(0, 1), new(-1, 0)];
    private readonly Graph _graph;

    public Day06() : base(6, 2024)
    {
        _graph = BuildGraph();
    }

    public override void Parse() => BuildGraph();
    public override string Part1() => CountVisitedPositions().ToString();
    public override string Part2() => CountLoops().ToString();

    private int CountVisitedPositions()
    {
        return _graph.Visited.Length;
    }
    
    private int CountLoops()
    {
        var count = 0;

        Parallel.For(0, _graph.Visited.Length, i =>
        {
            if (CreatesLoop(_graph.Visited[i])) Interlocked.Increment(ref count);
        });
        
        return count;
    }

    private bool CreatesLoop(Point obstruction)
    {
        var width = _graph.Nodes[0].Length;
        var height = _graph.Nodes.Length;

        Span<int> visited = stackalloc int[height * width];
        
        var position = _graph.Start;
        var direction = 0;
        
        while (true)
        {
            if (position.Col >= width || position.Row >= height || position.Col < 0 || position.Row < 0) return false;
            if (visited[position.Row * width + position.Col] == direction + 1) return true;
            
            visited[position.Row * width + position.Col] = direction + 1;

            var node = _graph.Nodes[position.Row][position.Col]!.Value;
            while (!node.Neighbours[direction].HasValue)
            {
                direction = (direction + 1) % 4;
            }
            
            var newPosition = node.Neighbours[direction]!.Value;

            if (CrossesObstruction(position, newPosition, obstruction))
                newPosition = new Point(obstruction.Row + -1 * Directions[direction].DirectionY, obstruction.Col + -1 * Directions[direction].DirectionX);
            
            direction = (direction + 1) % 4;
            position = newPosition;
        }
    }

    private static bool CrossesObstruction(Point start, Point end, Point obstruction)
    {
        if (start.Row == end.Row)
            return obstruction.Row == start.Row && obstruction.Col >= Math.Min(start.Col, end.Col) && obstruction.Col <= Math.Max(start.Col, end.Col);
        
        if (start.Col == end.Col)
            return obstruction.Col == start.Col && obstruction.Row >= Math.Min(start.Row, end.Row) && obstruction.Row <= Math.Max(start.Row, end.Row);
        
        return false;
    }

    private Graph BuildGraph()
    {
        var nodes = new Node?[Input.Length][];
        Point start = default;

        Parallel.For(0, Input.Length, row =>
        {
            nodes[row] = new Node?[Input[0].Length];
            for (var col = 0; col < Input[0].Length; col++)
            {
                if (Input[row][col] == '#') continue;
                if (Input[row][col] == '^') start = new Point(row, col);
                nodes[row][col] = CreateNode(row, col);
            }
        });

        var visited = GetVisitedPositions(nodes, start);
        
        return new Graph(nodes, start, visited);
    }
    
    private Node CreateNode(int row, int col)
    {
        var node = new Node();
        
        for (var index = 0; index < Directions.Length; index++)
        {
            var direction = Directions[index];
            var neighbour = MoveUntilBlocked(row, col, direction);
            
            if (neighbour.row != row || neighbour.col != col)
                node.Neighbours[index] = new Point(neighbour.row, neighbour.col);
        }
        
        return node;
    }

    private (int row, int col) MoveUntilBlocked(int row, int col, Direction direction)
    {
        while (true)
        {
            row += direction.DirectionY;
            col += direction.DirectionX;
            
            if (row < 0 || row >= Input.Length || col < 0 || col >= Input[0].Length)
                return (row, col);
            if (Input[row][col] == '#')
                return (row - direction.DirectionY, col - direction.DirectionX);
        }
    }

    private static Point[] GetVisitedPositions(Node?[][] nodes, Point position)
    {
        var width = nodes[0].Length;
        var height = nodes.Length;

        var visited = new HashSet<Point>();
        var direction = 0;
        
        while (position.Col < width && position.Row < height && position.Col >= 0 && position.Row >= 0)
        {
            var node = nodes[position.Row][position.Col]!.Value;
            while (!node.Neighbours[direction].HasValue)
            {
                direction = (direction + 1) % 4;
            }
            
            var newPosition = node.Neighbours[direction]!.Value;

            while (position.Col != newPosition.Col || position.Row != newPosition.Row)
            {
                visited.Add(position);
                position.Col += Directions[direction].DirectionX;
                position.Row += Directions[direction].DirectionY;
            }
            
            direction = (direction + 1) % 4;
            position = newPosition;
        }
        
        return visited.ToArray();
    }
    
    private record struct Graph(Node?[][] Nodes, Point Start, Point[] Visited);
    
    private struct Node()
    {
        public readonly Point?[] Neighbours = new Point?[4];
    }

    private record struct Point(int Row, int Col);
    
    private record struct Direction(int DirectionX, int DirectionY);
}