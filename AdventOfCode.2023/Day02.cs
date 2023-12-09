namespace AdventOfCode2023;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using Round = (int Red, int Green, int Blue);
using Game = (int Id, System.Collections.Generic.IEnumerable<(int Red, int Green, int Blue)> Rounds);

public partial class Day02
{
    private static Regex LineRegex = GetLineRegex();
    
    private static Regex RoundRegex = GetRoundRegex();

    public static int? SolvePart1(string input)
        => Parse(input)
            .Where(g => g.Rounds.All(IsPossible))
            .Select(g => g.Id)
            .Sum();

    public static int? SolvePart2(string input)
        => Parse(input)
            .Select(g => PowerOf(GetMinimumAmounts(g.Rounds)))
            .Sum();

    public static IEnumerable<Game> Parse(string input)
        => input.Split("\n").Select(ParseLine);

    public static Game ParseLine(string input)
    {
        var match = LineRegex.Match(input);
        var id = int.Parse(match.Groups[1].Value);
        var rounds = match.Groups[2].Captures.Select(c => ParseRound(c.Value)).ToList();

        return (id, rounds);
    }

    public static bool IsPossible(Round round) => round.Red <= 12 && round.Green <= 13 && round.Blue <= 14;

    public static Round GetMinimumAmounts(IEnumerable<Round> rounds)
        => (
            Red: rounds.Select(r => r.Red).Max(),
            Green: rounds.Select(r => r.Green).Max(),
            Blue: rounds.Select(r => r.Blue).Max()
        );

    public static int PowerOf(Round round) => round.Red * round.Green * round.Blue;

    public static Round ParseRound(string input)
    {
        var match = RoundRegex.Match(input);
        var groups = match.Groups[1].Captures
            .Select(c => c.Value.Split(" "))
            .ToDictionary(a => a[1], a => int.Parse(a[0]));
        return (
            Red: groups.TryGetValue("red", out var r) ? r : 0,
            Green: groups.TryGetValue("green", out var g) ? g : 0,
            Blue: groups.TryGetValue("blue", out var b) ? b : 0
        );
    }

    [GeneratedRegex(@"Game (\d+): (?:([^;]+)(?:; )?)+")]
    private static partial Regex GetLineRegex();

    [GeneratedRegex(@"(?:(\d+ (?:red|green|blue))(?:, )?)+")]
    private static partial Regex GetRoundRegex();
}
