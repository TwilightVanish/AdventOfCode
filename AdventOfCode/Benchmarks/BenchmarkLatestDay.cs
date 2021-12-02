using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Benchmarks
{
    [MemoryDiagnoser]
    public class BenchmarkLatestDay
    {
        private readonly BaseProblem _day;

        public BenchmarkLatestDay()
        {
            var solver = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Last(type => typeof(BaseProblem).IsAssignableFrom(type) && !type.IsAbstract && !type.IsInterface);

            _day = Activator.CreateInstance(solver) as BaseProblem;
        }

        [Benchmark]
        public ValueTask<string> Part1()
        {
            return _day.Solve_1();
        }
        
        [Benchmark]
        public ValueTask<string> Part2()
        {
            return _day.Solve_2();
        }
    }
}