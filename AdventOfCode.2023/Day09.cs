namespace AdventOfCode2023;

using System.Collections.Generic;
using System.Linq;

public partial class Day09 : Solution
{
    public static int? SolvePart1(IEnumerable<string> input) => input.Select(l => Predict(Parse(l).ToArray())).Sum();

    public static int? SolvePart2(IEnumerable<string> input) => input.Select(l => Predict(Parse(l).Reverse().ToArray())).Sum();

    private static int Predict(int[] line)
    {
        var iteration = 1;
        var allNull = false;

        while (!allNull)
        {
            allNull = true;

            for (var i = 0; i < line.Length - iteration; i++)
            {
                line[i] = line[i + 1] - line[i];

                allNull &= line[i] == 0;
            }

            iteration++;
        }

        return line.Sum();
    }

    private static IEnumerable<int> Parse(string line) => ParseNumbers(line, int.Parse);
}
