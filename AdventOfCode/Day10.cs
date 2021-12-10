namespace AdventOfCode;

public sealed class Day10 : ExtendedDay
{
    private readonly string[] _input;

    public Day10()
    {
        _input = InputAsArray;
    }

    public override ValueTask<string> Solve_1() => new(FindSyntaxErrors().ToString());
    public override ValueTask<string> Solve_2() => new(RepairSyntaxErrors().ToString());

    private int FindSyntaxErrors()
    {
        var points = 0;
        foreach (var line in _input)
        {
            var openOrder = new Stack<char>();

            foreach (var character in line)
            {
                if (character is '(' or '[' or '{' or '<')
                {
                    openOrder.Push(character);
                    continue;
                }

                openOrder.TryPop(out var last);
                points += character switch
                {
                    ')' when last != '(' => 3,
                    ']' when last != '[' => 57,
                    '}' when last != '{' => 1197,
                    '>' when last != '<' => 25137,
                    _ => 0
                };
            }
        }

        return points;
    }
    
    private long RepairSyntaxErrors()
    {
        var points = new List<long>();
        foreach (var line in _input)
        {
            var openOrder = new Stack<char>();
            var invalid = false;

            foreach (var character in line)
            {
                if (character is '(' or '[' or '{' or '<')
                {
                    openOrder.Push(character);
                    continue;
                }

                openOrder.TryPop(out var last);
                switch (character)
                {
                    case ')' when last != '(':
                    case ']' when last != '[':
                    case '}' when last != '{':
                    case '>' when last != '<':
                        invalid = true;
                        break;
                }

                if (invalid) break;
            }
            if (invalid) continue;

            var score = 0L;
            foreach (var toClose in openOrder)
            {
                score *= 5;
                score += toClose switch
                {
                    '(' => 1,
                    '[' => 2,
                    '{' => 3,
                    '<' => 4,
                    _ => 0
                };
            }
            
            points.Add(score);
        }

        var result = points.ToArray();
        Array.Sort(result);
        
        return result[result.Length / 2];
    }
}