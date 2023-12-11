namespace AdventOfCode2023;

using System;
using System.Collections.Generic;
using System.Linq;

using Number = long;
using Position = (long Y, long X);

public partial class Day11 : Solution
{
    public static Number? SolvePart1(string input) => Solve(input, 1);

    public static Number? SolvePart2(string input) => Solve(input, 999999);

    public static Number Solve(string map, Number growth)
    {
        var rows = new List<Number>();
        var columns = new List<Number>();

        var height = map.Trim().Count(c => c == '\n') + 1;
        var width = map.IndexOf('\n');

        for (var y = 0; y < height; ++y)
        {
            var empty = true;

            for (var x = 0; x < width; ++x)
            {
                if (map[y * (width + 1) + x] == '#')
                {
                    empty = false;
                    break;
                }
            }

            if (empty) rows.Add(y);
        }

        for (var x = 0; x < width; ++x)
        {
            var empty = true;

            for (var y = 0; y < height; ++y)
            {
                if (map[y * (width + 1) + x] == '#')
                {
                    empty = false;
                    break;
                }
            }

            if (empty) columns.Add(x);
        }

        var positions = new HashSet<Position>();

        for (var y = 0; y < height; ++y)
        {
            for (var x = 0; x < width; ++x)
            {
                if (map[y * (width + 1) + x] == '#') positions.Add((y + rows.Count(r => r < y) * growth, x + columns.Count(c => c < x) * growth));
            }
        }

        return positions.SelectMany((p1, i) => positions.Skip(i + 1).Select(p2 => (Number)(Number.Abs(p1.Y - p2.Y) + Number.Abs(p1.X - p2.X)))).Sum();//.Aggregate(Number.Add);
    }
}