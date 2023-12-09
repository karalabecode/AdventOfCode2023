namespace AdventOfCode2023;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public partial class Day04
{
    private static Regex NumberRegex = GetNumberRegex();

    public static int? SolvePart1(IEnumerable<string> input)
        => input.Sum(GetPoints);

    public static int? SolvePart2(IEnumerable<string> input)
        => input.Select((line, index) => (Index: index, Matches: GetMatchCount(line)))
            .Aggregate(new Dictionary<int, int>(), UpdateCardCounts)
            .Sum(p => p.Value);

    private static Dictionary<int, int> UpdateCardCounts(Dictionary<int, int> cardCounts, (int Index, int Matches) card)
    {
        if (!cardCounts.TryGetValue(card.Index, out int value))
        {
            value = 1;
            cardCounts[card.Index] = value;
        }

        var currentInstances = value;

        for (var i = card.Index + 1; i < card.Index + card.Matches + 1; ++i)
        {
            cardCounts[i] = cardCounts.TryGetValue(i, out var r) ? r + currentInstances : 1 + currentInstances;
        }

        return cardCounts;
    }

    private static int GetPoints(string line)
    {
        var matches = GetMatchCount(line);

        return matches > 0 ? (int)Math.Pow(2, matches - 1) : 0;
    }

    private static int GetMatchCount(string line)
    {
        var parts = line.Split(":")[1].Split("|");
        var winning = NumberRegex.Matches(parts[0]).Select(m => m.Value).ToHashSet();
        var mine = NumberRegex.Matches(parts[1]).Select(m => m.Value).ToList();
        return mine.Count(winning.Contains);
    }

    [GeneratedRegex(@"(\d+)")]
    private static partial Regex GetNumberRegex();
}
