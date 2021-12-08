namespace AdventOfCode;

public sealed class Day04 : ExtendedDay
{
    private readonly string[] _input;

    public Day04()
    {
        _input = InputAsArray;
    }

    public override ValueTask<string> Solve_1() => new(FindBoard(false).ToString());
    public override ValueTask<string> Solve_2() => new(FindBoard(true).ToString());

    private int FindBoard(bool worst)
    {
        var boards = ParseBoards();
        var draws = _input[0].Split(',');
        var wins = 0;

        for (var nIndex = 0; nIndex < draws.Length; nIndex++)
        {
            var currentNum = int.Parse(draws[nIndex]);
            for (var bIndex = 0; bIndex < boards.Length; bIndex++)
            {
                var currentBoard = boards[bIndex];
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
                        if (worst && wins != boards.Length - 1)
                        {
                            boards[bIndex].Won = true;
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
        for (var i = 2; i < _input.Length; i++)
            if (string.IsNullOrEmpty(_input[i]))
            {
                boards.Add(new BingoBoard(currentBoard.ToArray()));
                currentBoard = new List<BingoSquare[]>();
            }
            else
            {
                var split = _input[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var nums = new BingoSquare[split.Length];
                for (var b = 0; b < split.Length; b++) nums[b] = new BingoSquare(int.Parse(split[b]));
                currentBoard.Add(nums);
            }

        return boards.ToArray();
    }
}

public struct BingoBoard
{
    public BingoSquare[][] Rows { get; }
    public bool Won { get; set; }

    public BingoBoard(BingoSquare[][] rows)
    {
        Rows = rows;
        Won = false;
    }
}

public struct BingoSquare
{
    public int Number { get; }
    public bool Called { get; set; }

    public BingoSquare(int num)
    {
        Number = num;
        Called = false;
    }
}