using AdventOfCode.Common;

namespace AdventOfCode._2024;

public sealed class Day04() : BaseDay(4, 2024)
{
    public override void Parse() {}
    public override string Part1() => CountWords().ToString();
    public override string Part2() => CountCrosses().ToString();

    private int CountWords()
    {
        var count = 0;
        Span<Coordinate> offsets = stackalloc Coordinate[8];
        
        for (var row = 0; row < Input.Length; row++)
        {
            for (var col = 0; col < Input[0].Length; col++)
            {
                if (Input[row][col] == 'X') count += CountWordsFromPosition(new Coordinate(col, row), offsets);
            }
        }
        
        return count;
    }

    private int CountWordsFromPosition(Coordinate start, Span<Coordinate> offsets)
    {
        var count = 0;
        var mCount = FindCharactersAround(start, 'M', offsets);

        for (var i = 0; i < mCount; i++)
        {
            var offset = offsets[i];
            var mLocation = ApplyOffset(start, offset);
            
            var possibleALocation = ApplyOffset(mLocation, offset);
            if (!IsCharacterWithBoundsCheck(possibleALocation.X, possibleALocation.Y, 'A')) continue;
            
            var possibleSLocation = ApplyOffset(possibleALocation, offset);
            if (!IsCharacterWithBoundsCheck(possibleSLocation.X, possibleSLocation.Y, 'S')) continue;
            
            count++;
        }

        return count;
    }

    private int CountCrosses()
    {
        var count = 0;
        Span<Coordinate> mOffsets = stackalloc Coordinate[4];
        Span<Coordinate> sOffsets = stackalloc Coordinate[4];
        
        for (var row = 0; row < Input.Length; row++)
        {
            for (var col = 0; col < Input[0].Length; col++)
            {
                if (Input[row][col] != 'A') continue;
                
                var mCount = FindCharactersDiagonal(col, row, 'M', mOffsets);
                var sCount = FindCharactersDiagonal(col, row, 'S', sOffsets);

                if (mCount == 2 && sCount == 2 && (mOffsets[0].Y == mOffsets[1].Y || mOffsets[0].X == mOffsets[1].X)) count++;
            }
        }
        
        return count;
    }

    private int FindCharactersAround(Coordinate coordinate, char target, Span<Coordinate> offsetBuffer)
    {
        var count = 0;
        
        for (var rowOffset = -1; rowOffset <= 1; rowOffset++)
        {
            for (var colOffset = -1; colOffset <= 1; colOffset++)
            {
                if (colOffset == 0 && rowOffset == 0) continue;

                if (IsCharacterWithBoundsCheck(coordinate.X + colOffset, coordinate.Y + rowOffset, target))
                    offsetBuffer[count++] = new Coordinate(colOffset, rowOffset);
            }
        }
        
        return count;
    }
    
    private int FindCharactersDiagonal(int x, int y, char target, Span<Coordinate> offsetBuffer)
    {
        var count = 0;
        
        for (var rowOffset = -1; rowOffset <= 1; rowOffset += 2)
        {
            for (var colOffset = -1; colOffset <= 1; colOffset += 2)
            {
                if (IsCharacterWithBoundsCheck(x + colOffset, y + rowOffset, target)) 
                    offsetBuffer[count++] = new Coordinate(colOffset, rowOffset);
            }
        }
        
        return count;
    }

    private bool IsCharacterWithBoundsCheck(int x, int y, char target) => y >= 0 && y < Input.Length && x >= 0 && x < Input[y].Length && Input[y][x] == target;

    private static Coordinate ApplyOffset(Coordinate coordinate, Coordinate offset) => new Coordinate(coordinate.X + offset.X, coordinate.Y + offset.Y);
    
    private record struct Coordinate(int X, int Y);
}