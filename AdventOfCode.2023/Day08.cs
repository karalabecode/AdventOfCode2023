namespace AdventOfCode2023;

using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using Big = System.Numerics.BigInteger;

public partial class Day08 : Solution
{
    public static int? SolvePart1(IEnumerable<string> input)
    {
        var steps = input.First();

        var map = input.Skip(2).Select(ParseTransition).ToDictionary(t => t.Item1, t => (t.Item2, t.Item3));

        var s = 0;
        var act = "AAA";

        while (act != "ZZZ")
        {
            for (var i = 0; i < steps.Length && act != "ZZZ"; ++i)
            {
                act = steps[i] == 'L' ? map[act].Item1 : map[act].Item2;
                ++s;
            }
        }

        return s;
    }

    static Big GCF(Big a, Big b)
    {
        while (b != 0)
        {
            var temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    static Big LCM(Big a, Big b)
    {
        return (a / GCF(a, b)) * b;
    }

    public static Big? SolvePart2(IEnumerable<string> input)
    {
        var steps = input.First();

        var map = input.Skip(2).Select(ParseTransition).ToDictionary(t => t.Item1, t => (t.Item2, t.Item3));

        var s = 0;
        var starting = map.Keys.Where(k => k.EndsWith('A')).ToList();
        var current = map.Keys.Where(k => k.EndsWith('A')).ToList();

        var filled = 0;
        var touchMap = new int[starting.Count];

        while (filled != starting.Count)
        {
            for (var i = 0; i < steps.Length; ++i)
            {
                ++s;

                for (var j = 0; j < current.Count; ++j)
                {
                    var act = current[j];

                    act = steps[i] == 'L' ? map[act].Item1 : map[act].Item2;

                    current[j] = act;

                    if (act.EndsWith('Z') && touchMap[j] == 0)
                    {
                        touchMap[j] = s;
                        filled++;
                    }
                }
            }
        }

        return touchMap.Select(i => (Big)i).Aggregate((Big)1, LCM);
    }

    private static (string, string, string) ParseTransition(string line)
    {
        var groups = LineRegex.Match(line).Groups;

        return (groups[1].Value, groups[2].Value, groups[3].Value);
    }

    private static Regex LineRegex = GetLineRegex();

    [GeneratedRegex(@"(.{3}) = \((.{3}), (.{3})\)")]
    private static partial Regex GetLineRegex();
}
