namespace AdventOfCode2023;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Big = System.Numerics.BigInteger;
using Position = (System.Numerics.BigInteger Y, System.Numerics.BigInteger X);

public partial class Day11 : Solution
{
    public static Big? SolvePart1(IEnumerable<string> input)
    {
        var map = Parse(input);

        var positions = GetPositionsFromWideSpace(map);

        return positions.SelectMany((p1, i) => positions.Skip(i + 1).Select(p2 => (Big)(Big.Abs(p1.Y - p2.Y) + Big.Abs(p1.X - p2.X)))).Aggregate(Big.Add);
    }

    public static Big? SolvePart2(IEnumerable<string> input)
    {
        var map = Parse(input);

        var positions = GetPositionsFromWideSpace(map, 999999);

        return positions.SelectMany((p1, i) => positions.Skip(i + 1).Select(p2 => (Big)(Big.Abs(p1.Y - p2.Y) + Big.Abs(p1.X - p2.X)))).Aggregate(Big.Add);
    }

    private static BitMatrix Parse(IEnumerable<string> input)
    {
        var map = new BitMatrix(input.Count(), input.First().Length);
        var c = input.GetEnumerator();

        for (var y = 0; y < map.Height; ++y)
        {
            c.MoveNext();
            for (var x = 0; x < map.Width; ++x)
            {
                if (c.Current[x] == '#') map[y, x] = true;
            }
        }

        return map;
    }

    public static HashSet<Position> GetPositionsFromWideSpace(BitMatrix map, Big? growth = null)
    {
        var rows = new List<Big>();
        var columns = new List<Big>();

        for (var y = 0; y < map.Height; ++y)
        {
            var empty = true;

            for (var x = 0; x < map.Width; ++x)
            {
                if (map[y, x])
                {
                    empty = false;
                    break;
                }
            }

            if (empty) rows.Add(y);
        }

        for (var x = 0; x < map.Width; ++x)
        {
            var empty = true;

            for (var y = 0; y < map.Height; ++y)
            {
                if (map[y, x])
                {
                    empty = false;
                    break;
                }
            }

            if (empty) columns.Add(x);
        }


        var result = new HashSet<Position>();

        for (var y = 0; y < map.Height; ++y)
        {
            for (var x = 0; x < map.Width; ++x)
            {
                if (map[y, x]) result.Add((y + rows.Count(r => r < y) * (growth ?? Big.One), x + columns.Count(c => c < x) * (growth ?? Big.One)));
            }
        }

        return result;
    }

    public static void PrBig(BitMatrix map)
    {
        for (var y = 0; y < map.Height; ++y)
        {
            for (var x = 0; x < map.Width; ++x)
            {
                Debug.Write(map[y, x] ? '#' : '.');
            }

            Debug.WriteLine("");
        }
    }

    public class BitMatrix(int width, int height)
    {
        private readonly BitArray Inner = new(width * height);

#pragma warning disable CS9124 // Parameter is captured Bigo the state of the enclosing type and its value is also used to initialize a field, property, or event.
        public readonly int Width = width;
#pragma warning restore CS9124 // Parameter is captured Bigo the state of the enclosing type and its value is also used to initialize a field, property, or event.
        public readonly int Height = height;

        public bool this[int y, int x]
        {
            get => this.Inner[y * Width + x];
            set => this.Inner[y * Width + x] = value;
        }
    }
}