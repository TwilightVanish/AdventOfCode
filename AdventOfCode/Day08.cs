namespace AdventOfCode;

public sealed class Day08 : ExtendedDay
{
	private readonly string[] _input;

	public Day08()
	{
		_input = InputAsArray;
	}

	public override ValueTask<string> Solve_1() => new(UniqueCount().ToString());
	public override ValueTask<string> Solve_2() => new(FindOutput().ToString());

	private int UniqueCount()
	{
		var total = 0;
		for (var i = 0; i < _input.Length; i++)
		{
			var split = _input[i].Split(" | ")[1].Split(' ');
			for (var h = 0; h < split.Length; h++)
			{
				if (split[h].Length is 2 or 3 or 4 or 7)
				{
					total++;
				}
			}
		}

		return total;
	}

	private int FindOutput()
	{
		//Refactor SoonTM
		return 0;
	}
}