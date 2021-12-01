using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public sealed class Day01 : BaseDay
    {
        private readonly int[] _input;

        public Day01()
        {
            _input = File.ReadAllText(InputFilePath).Split("\n").Select(int.Parse).ToArray();
        }

        public override ValueTask<string> Solve_1() => new(CountDepthIncrements(1).ToString());
        public override ValueTask<string> Solve_2() => new(CountDepthIncrements(3).ToString());

        private int CountDepthIncrements(int slidingWindow)
        {
            var count = 0;

            for (var i = slidingWindow; i < _input.Length; i++)
                if (_input[i] > +_input[i - slidingWindow]) 
                    count++;

            return count;
        }
    }
}