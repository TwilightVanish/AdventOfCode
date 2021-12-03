using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdventOfCode.AOCHelper;

namespace AdventOfCode
{
    public sealed class Day03 : ExtendedDay
    {
        private readonly string[] _input;

        public Day03()
        {
            _input = InputAsArray;
        }

        public override ValueTask<string> Solve_1() => new(GetPowerConsumption().ToString());
        public override ValueTask<string> Solve_2() => new(GetLifeSupportRating().ToString());

        private int GetPowerConsumption()
        {
            var epsilon = "";
            var gamma = "";

            for (var i = 0; i < _input[0].Length; i++)
            {
                var majority = OneCount(_input, i) > 500;
                epsilon += majority ? "0" : "1";
                gamma += majority ? "1" : "0";
            }

            return Convert.ToInt32(epsilon, 2) * Convert.ToInt32(gamma, 2);
        }

        private int GetLifeSupportRating()
        {
            var oxygen = _input;
            var carbon = _input;

            for (var i = 0; i < _input[0].Length; i++)
            {
                if (oxygen.Length == 1 && carbon.Length == 1)
                    break;

                if (oxygen.Length != 1)
                    oxygen = RemoveDigit(oxygen, i, OneCount(oxygen, i) * 2 >= oxygen.Length ? '1' : '0');

                if (carbon.Length != 1)
                    carbon = RemoveDigit(carbon, i, OneCount(carbon, i) * 2 >= carbon.Length ? '0' : '1');
            }

            return Convert.ToInt32(oxygen[0], 2) * Convert.ToInt32(carbon[0], 2);
        }

        private static int OneCount(string[] toCount, int position)
        {
            var count = 0;
            foreach (var t in toCount)
                if (t[position] == '1')
                    count++;

            return count;
        }

        private static string[] RemoveDigit(string[] toFilter, int position, char digit)
        {
            var filtered = new List<string>();
            foreach (var t in toFilter)
                if (t[position] == digit)
                    filtered.Add(t);

            return filtered.ToArray();
        }
    }
}