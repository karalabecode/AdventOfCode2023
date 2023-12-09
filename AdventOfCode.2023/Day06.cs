namespace AdventOfCode2023;

using System.Collections.Generic;
using System.Linq;

public partial class Day06 : Solution
{
    public static ulong? SolvePart1(IEnumerable<string> input) => Parse(input).Select(GetSolutionCount).Aggregate((a, b) => a * b);

    public static ulong? SolvePart2(IEnumerable<string> input) => Parse(input.Select(l => l.Replace(" ", ""))).Select(GetSolutionCount).Aggregate((a, b) => a * b);

    public static (ulong Time, ulong Length)[] Parse(IEnumerable<string> input)
    {
        var times = ParseNumbers(input.ElementAt(0), ulong.Parse).ToArray();
        var lengths = ParseNumbers(input.ElementAt(1), ulong.Parse).ToArray();

        return times.Zip(lengths).ToArray();
    }

    public static ulong GetSolutionCount((ulong Time, ulong Length) race)
    {
        var result = (ulong)0;

        for (ulong i =  1; i < race.Time; i++)
        {
            if (race.Length < (race.Time - i) * i) result++;
        }

        return result;
    }
}
