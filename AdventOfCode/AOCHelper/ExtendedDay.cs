using System.IO;

namespace AdventOfCode.AOCHelper
{
    public abstract class ExtendedDay : BaseDay
    {
        protected string InputAsString => File.ReadAllText(InputFilePath);
        protected string[] InputAsArray => File.ReadAllLines(InputFilePath);
    }
}