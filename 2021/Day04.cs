using AdventOfCode.Common;

namespace AdventOfCode._2021;

public sealed class Day04 : BaseDay
{
    private readonly BingoBoard[] _boards;
    
    public Day04() : base(4, 2021)
    {
        _boards = ParseBoards();
    }

    public override void Parse() => ParseBoards();
    public override string Part1() => FindBoard(false).ToString();
    public override string Part2() => FindBoard(true).ToString();
   
    private int FindBoard(bool worst)
    {
        var draws = Input[0].Split(',');
        var wins = 0;

        for (var nIndex = 0; nIndex < draws.Length; nIndex++)
        {
            var currentNum = int.Parse(draws[nIndex]);
            for (var bIndex = 0; bIndex < _boards.Length; bIndex++)
            {
                var currentBoard = _boards[bIndex];
                if (currentBoard.Won) continue;
                for (var rIndex = 0; rIndex < currentBoard.Rows.Length; rIndex++)
                {
                    var currentRow = currentBoard.Rows[rIndex];
                    for (var sIndex = 0; sIndex < currentRow.Length; sIndex++)
                    {
                        if (currentRow[sIndex].Number != currentNum) continue;
                        currentRow[sIndex].Called = true;

                        var horizontal = true;
                        for (var hIndex = 0; hIndex < currentRow.Length; hIndex++)
                        {
                            if (currentRow[hIndex].Called) continue;
                            horizontal = false;
                            break;
                        }

                        var vertical = true;
                        for (var vIndex = 0; vIndex < currentBoard.Rows.Length; vIndex++)
                        {
                            if (currentBoard.Rows[vIndex][sIndex].Called) continue;
                            vertical = false;
                            break;
                        }

                        if (!horizontal && !vertical) continue;
                        if (worst && wins != _boards.Length - 1)
                        {
                            _boards[bIndex].Won = true;
                            wins++;
                            continue;
                        }

                        return GetSum(currentBoard.Rows) * currentNum;
                    }
                }
            }
        }

        return 0;
    }

    private static int GetSum(BingoSquare[][] board)
    {
        var sum = 0;

        for (var rIndex = 0; rIndex < board.Length; rIndex++)
        for (var sIndex = 0; sIndex < board[rIndex].Length; sIndex++)
            if (board[rIndex][sIndex].Called == false)
                sum += board[rIndex][sIndex].Number;

        return sum;
    }

    private BingoBoard[] ParseBoards()
    {
        var currentBoard = new List<BingoSquare[]>();
        var boards = new List<BingoBoard>();
        for (var i = 2; i < Input.Length; i++)
            if (string.IsNullOrEmpty(Input[i]))
            {
                boards.Add(new BingoBoard(currentBoard.ToArray()));
                currentBoard = new List<BingoSquare[]>();
            }
            else
            {
                var split = Input[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var nums = new BingoSquare[split.Length];
                for (var b = 0; b < split.Length; b++) nums[b] = new BingoSquare(int.Parse(split[b]));
                currentBoard.Add(nums);
            }

        return boards.ToArray();
    }

    private struct BingoBoard
    {
        public BingoSquare[][] Rows { get; }
        public bool Won { get; set; }

        public BingoBoard(BingoSquare[][] rows)
        {
            Rows = rows;
            Won = false;
        }
    }

    private struct BingoSquare
    {
        public int Number { get; }
        public bool Called { get; set; }

        public BingoSquare(int num)
        {
            Number = num;
            Called = false;
        }
    }
}