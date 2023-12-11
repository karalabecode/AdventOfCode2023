using static AdventOfCode2023.Day10;

namespace AdventOfCode2023;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using static Day10.Tile;

using Position = (int Y, int X);
using Map = Tile[,];

public partial class Day10 : Solution
{
    public static int? SolvePart1(IEnumerable<string> input)
    {
        var (start, map) = Parse(input.ToArray());

        return GetMaxDistance(start, map);
    }

    public static int? SolvePart2(IEnumerable<string> input)
    {
        var (start, map) = Parse(input.ToArray());
        map = SimplifyMap(start, map);

        return CountInnerPositions(map);
    }

    private static int? CountInnerPositions(Map map)
    {
        var wide = WidenMap(map);
        var wideSpaceCount = CountFreeSpaces(wide);

        var innerPositions = new HashSet<Position>();
        var outerPositions = new HashSet<Position>();

        while (innerPositions.Count + outerPositions.Count != wideSpaceCount)
        {
            var start = GetNextFreeSpaceWihout(wide, innerPositions.Union(outerPositions).ToHashSet());

            var nextSet = WidenSpace(wide, start!.Value);

            if (nextSet.Any(p => p.Y == 0 || p.X == 0 || p.Y == wide.GetLength(0) - 1 || p.X == wide.GetLength(1) - 1))
            {
                foreach (var p in nextSet) outerPositions.Add(p);
            }
            else
            {
                foreach (var p in nextSet) innerPositions.Add(p);
            }
        }

        return innerPositions.Count(p => p.Y % 2 == 0 && p.X % 2 == 0);
    }

    private static Position? GetNextFreeSpaceWihout(Map map, HashSet<Position> alreadyDone)
    {
        for (var y = 0; y < map.GetLength(0); ++y)
        {
            for (var x = 0; x < map.GetLength(1); ++x)
            {
                if (map[y, x] == None && !alreadyDone.Contains((y, x))) return (y, x);
            }
        }

        return null;
    }

    private static HashSet<Position> WidenSpace(Map map, Position start)
    {
        var nextSet = new HashSet<Position>() { start };
        var q = new Queue<Position>();
        q.Enqueue(start);

        while (q.TryDequeue(out var current))
        {
            var top = (Y: current.Y - 1, current.X);
            var bottom = (Y: current.Y + 1, current.X);
            var left = (current.Y, X: current.X - 1);
            var right = (current.Y, X: current.X + 1);

            if (top.Y >= 0 && map[top.Y, top.X] == None && !nextSet.Contains(top))
            {
                nextSet.Add(top);
                q.Enqueue(top);
            }
            if (bottom.Y < map.GetLength(0) && map[bottom.Y, bottom.X] == None && !nextSet.Contains(bottom))
            {
                nextSet.Add(bottom);
                q.Enqueue(bottom);
            }
            if (left.X >= 0 && map[left.Y, left.X] == None && !nextSet.Contains(left))
            {
                nextSet.Add(left);
                q.Enqueue(left);
            }
            if (right.X < map.GetLength(1) && map[right.Y, right.X] == None && !nextSet.Contains(right))
            {
                nextSet.Add(right);
                q.Enqueue(right);
            }
        }

        return nextSet;
    }

    private static int CountFreeSpaces(Tile[,] wide)
    {
        int result = 0;

        for (var y = 0; y < wide.GetLength(0); ++y)
        {
            for (var x = 0; x < wide.GetLength(1); ++x)
            {
                if (wide[y, x] == None) ++result;
            }
        }

        return result;
    }

    static Tile Next(Tile from, Tile to) => to & (~(from switch { Top => Bottom, Bottom => Top, Right => Left, _ => Right, }));

    static Position MoveTowards(Position from, Tile to) => to switch
    {
        Top => (from.Y - 1, from.X),
        Bottom => (from.Y + 1, from.X),
        Right => (from.Y, from.X + 1),
        _ => (from.Y, from.X - 1),
    };

    static (Tile, Position) MoveFrom(Map map, Tile from, Position p)
    {
        var position = MoveTowards(p, from);
        var orientation = Next(from, map[position.Y, position.X]);

        return (orientation, position);
    }

    public static Map SimplifyMap(Position start, Map map)
    {
        var result = new Tile[map.GetLength(0), map.GetLength(1)];
        result[start.Y, start.X] = map[start.Y, start.X];

        var startTile = map[start.Y, start.X];

        var (o1, o2)
            = startTile == H ? (Left, Right)
            : startTile == V ? (Top, Bottom)
            : startTile == BR ? (Bottom, Right)
            : startTile == BL ? (Bottom, Left)
            : startTile == TR ? (Top, Right)
            : (Top, Left);
        var (p1, p2) = (start, start);

        do
        {
            (o1, p1) = MoveFrom(map, o1, p1);
            result[p1.Y, p1.X] = map[p1.Y, p1.X];

            if (p1 != p2) (o2, p2) = MoveFrom(map, o2, p2);
            result[p2.Y, p2.X] = map[p2.Y, p2.X];
        }
        while (p1 != p2);

        return result;
    }

    public static Map WidenMap(Map map)
    {
        var result = new Tile[map.GetLength(0) * 2, map.GetLength(1) * 2];

        for (var y = 0; y < map.GetLength(0); y++)
        {
            for (var x = 0; x < map.GetLength(1); x++)
            {
                result[2 * y, 2 * x] = map[y, x];

                if (map[y, x].HasFlag(Right)) result[2 * y, 2 * x + 1] = H;
                if (map[y, x].HasFlag(Bottom)) result[2 * y + 1, 2 * x] = V;
            }
        }

        return result;
    }



    private static int? GetMaxDistance(Position start, Map map)
    {
        var startTile = map[start.Y, start.X];

        var (o1, o2) = startTile == H ? (Left, Right)
            : startTile == V ? (Top, Bottom)
            : startTile == BR ? (Bottom, Right)
            : startTile == BL ? (Bottom, Left)
            : startTile == TR ? (Top, Right)
            : (Top, Left);
        var (p1, p2) = (start, start);

        var result = 0;

        do
        {
            ++result;
            (o1, p1) = MoveFrom(map, o1, p1);

            if (p1 != p2) (o2, p2) = MoveFrom(map, o2, p2);
        }
        while (p1 != p2);

        return result;
    }

    private static ((int, int), Map) Parse(string[] input)
    {
        var map = new Tile[input.Length, input.First().Length];
        var start = (Y: 0, X: 0);

        for (var i = 0; i < input.Length; ++i)
        {
            for (var j = 0; j < input[i].Length; ++j)
            {
                map[i, j] = input[i][j] switch
                {
                    '-' => H,
                    '|' => V,

                    'F' => BR,
                    '7' => BL,
                    'L' => TR,
                    'J' => TL,

                    'S' => Start,

                    _ => None,
                };

                if (map[i, j] == Start) start = (i, j);
            }
        }

        var (h, w) = (map.GetLength(0) - 1, map.GetLength(1) - 1);
        map[start.Y, start.X]
            = (start.Y < h && map[start.Y + 1, start.X].HasFlag(Top) ? Bottom : 0)
            | (start.Y > 0 && map[start.Y - 1, start.X].HasFlag(Bottom) ? Top : 0)
            | (start.X < w && map[start.Y, start.X + 1].HasFlag(Left) ? Right : 0)
            | (start.X > 0 && map[start.Y, start.X - 1].HasFlag(Right) ? Left : 0);

        return (start, map);
    }

    [Flags]
    public enum Tile : byte
    {
        None = 0,

        Left = 1,
        Top = 2,
        Right = 4,
        Bottom = 8,

        H = Left | Right,
        V = Top | Bottom,

        BR = Bottom | Right,
        BL = Bottom | Left,
        TR = Top | Right,
        TL = Top | Left,

        Start = 128,
    }
}