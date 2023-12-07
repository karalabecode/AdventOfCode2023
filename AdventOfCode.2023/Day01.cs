namespace AdventOfCode2023;

using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public partial class Day01
{
    private static Regex DigitRegex = MyRegex();

    public static string? Solve(string input)
        => input.Split("\n")
            .Select(line => int.Parse(string.Join("", Filter(DigitRegex.Matches(line).Select(m => m.ToString())))))
            .Sum()
            .ToString();

    public static IEnumerable<string> Filter(IEnumerable<string> source)
    {
        yield return source.First();
        yield return source.Last();
    }

    [GeneratedRegex("[0-9]")]
    private static partial Regex MyRegex();
}
