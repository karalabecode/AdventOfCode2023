namespace AdventOfCode2023;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public partial class Solution
{
    public static Regex NumberRegex = GetNumberRegex();

    public static IEnumerable<T> ParseNumbers<T>(string input, Func<string, T> parse) => NumberRegex.Matches(input).Select(m => parse(m.Value)).ToArray();

    [GeneratedRegex(@"(\d+)")]
    public static partial Regex GetNumberRegex();
}
