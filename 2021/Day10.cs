using AdventOfCode.Common;

namespace AdventOfCode._2021;

public sealed class Day10 : BaseDay
{
    public Day10() : base(10, 2021) {}

    public override void Parse() {}
    public override string Part1() => FindSyntaxErrors().ToString();
    public override string Part2() => RepairSyntaxErrors().ToString();
    
    private int FindSyntaxErrors()
    {
        var points = 0;
        foreach (var line in Input)
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
        foreach (var line in Input)
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