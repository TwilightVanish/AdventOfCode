using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventOfCode.AOCHelper;

namespace AdventOfCode
{
    public sealed class Day06 : ExtendedDay
    {
        private readonly string _input;

        public Day06()
        {
            _input = InputAsString;
        }

        public override ValueTask<string> Solve_1() => new(SimulatedGrowth(80).ToString());
        public override ValueTask<string> Solve_2() => new(SimulatedGrowth(256).ToString());

        private long SimulatedGrowth(int days)
        {
            Span<long> schools = stackalloc long[9];
            for (var pos = 0; pos < _input.Length; pos += 2)
                schools[_input[pos] - '0']++;

            for (var day = 0; day < days; day++)
            {
                var temp = 0L;
                for (var i = 8; i >= 0; i--)
                {
                    (schools[i], temp) = (temp, schools[i]);
                }
                
                schools[8] = temp;
                schools[6] += temp;
            }

            var sum = 0L;
            for (var s = 0; s <= 8; s++)
            {
                sum += schools[s];
            }

            return sum;
        }
    }
}