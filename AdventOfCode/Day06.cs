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
            var schools = Enumerable.Range(0, 9).ToDictionary(num => num, num => 0L);
            foreach (var f in _input.Split(',').Select(int.Parse))
            {
                schools[f]++;
            }

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
            
            return schools.Sum(x => x.Value);
        }
    }
}