namespace AdventOfCode2023;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Number = long;

public partial class Day07 : Solution
{
    public static int? SolvePart1(IEnumerable<string> input) => input.Select(Parse).Order(new HandComparer()).Select((h, i) => (i + 1) * h.Bid).Sum();

    public static Number? SolvePart2(IEnumerable<string> input) => input.Select(Parse2).Order(new HandComparer2()).Select((h, i) => ((Number)i + 1) * h.Bid).Sum();

    public static Hand Parse(string line)
    {
        var parts = line.Split(' ');

        var cards = parts[0].Select(c => c switch
        {
            '2' => Card.N2,
            '3' => Card.N3,
            '4' => Card.N4,
            '5' => Card.N5,
            '6' => Card.N6,
            '7' => Card.N7,
            '8' => Card.N8,
            '9' => Card.N9,
            'T' => Card.N10,
            'J' => Card.J,
            'Q' => Card.Q,
            'K' => Card.K,
            'A' => Card.A,
            _ => throw new NotSupportedException()
        }).ToArray();
        var groups = parts[0].GroupBy(c => c).GroupBy(g => g.Count()).ToDictionary(g => g.Key, g => g.Count());
        var handType = groups.ContainsKey(5) ? HandType.FiveOfAKind
            : groups.ContainsKey(4) ? HandType.FourOfAKind
            : groups.ContainsKey(3) && groups.ContainsKey(2) ? HandType.FullHouse
            : groups.ContainsKey(3) ? HandType.ThreeOfAKind
            : groups.TryGetValue(2, out var v) && v == 2 ? HandType.TwoPairs
            : groups.ContainsKey(2) ? HandType.OnePair
            : HandType.HighCard;

        var bid = int.Parse(parts[1]);

        return new(cards, handType, bid);
    }

    public static Hand Parse2(string line)
    {
        var parts = line.Split(' ');

        var cards = parts[0].Select(c => c switch
        {
            '2' => Card.N2,
            '3' => Card.N3,
            '4' => Card.N4,
            '5' => Card.N5,
            '6' => Card.N6,
            '7' => Card.N7,
            '8' => Card.N8,
            '9' => Card.N9,
            'T' => Card.N10,
            'J' => Card.J,
            'Q' => Card.Q,
            'K' => Card.K,
            'A' => Card.A,
            _ => throw new NotSupportedException()
        }).ToArray();
        var groups = parts[0].Where(c => c != 'J').GroupBy(c => c).GroupBy(g => g.Count()).ToDictionary(g => g.Key, g => g.Count());
        var handType = groups.ContainsKey(5) ? HandType.FiveOfAKind
            : groups.ContainsKey(4) ? HandType.FourOfAKind
            : groups.ContainsKey(3) && groups.ContainsKey(2) ? HandType.FullHouse
            : groups.ContainsKey(3) ? HandType.ThreeOfAKind
            : groups.TryGetValue(2, out var v) && v == 2 ? HandType.TwoPairs
            : groups.ContainsKey(2) ? HandType.OnePair
            : HandType.HighCard;

        var jokerCount = parts[0].Count(c => c == 'J');

        if (jokerCount > 0)
        {
            var p = handType;
            handType = jokerCount >= 4 ? HandType.FiveOfAKind
                : jokerCount == 3 ? (handType == HandType.OnePair ? HandType.FiveOfAKind : HandType.FourOfAKind)
                : handType == HandType.FourOfAKind ? HandType.FiveOfAKind
                : handType == HandType.ThreeOfAKind && jokerCount == 2 ? HandType.FiveOfAKind
                : handType == HandType.ThreeOfAKind && jokerCount == 1 ? HandType.FourOfAKind
                : handType == HandType.TwoPairs ? HandType.FullHouse
                : handType == HandType.OnePair && jokerCount == 2 ? HandType.FourOfAKind
                : handType == HandType.OnePair && jokerCount == 1 ? HandType.ThreeOfAKind
                : handType == HandType.HighCard && jokerCount == 2 ? HandType.ThreeOfAKind
                : handType == HandType.HighCard && jokerCount == 1 ? HandType.OnePair
                : handType;

            Debug.WriteLine($"{line} is {handType} (was {p})");
        }


        var bid = int.Parse(parts[1]);

        return new(cards, handType, bid);
    }

    public readonly record struct Hand(Card[] Cards, HandType HandType, int Bid);

    public class HandComparer : IComparer<Hand>
    {
        public int Compare(Hand x, Hand y)
        {
            var byHandType = x.HandType.CompareTo(y.HandType);

            return byHandType != 0 ? byHandType : x.Cards.Zip(y.Cards).Select(p => p.First.CompareTo(p.Second)).SkipWhile(c => c == 0).FirstOrDefault();
        }
    }

    public class HandComparer2 : IComparer<Hand>
    {
        public int Compare(Hand x, Hand y)
        {
            var byHandType = x.HandType.CompareTo(y.HandType);

            static byte ToNum(Card c) => c == Card.J ? (byte)0 : (byte)c;

            return byHandType != 0 ? byHandType : x.Cards.Zip(y.Cards).Select(p => ToNum(p.First).CompareTo(ToNum(p.Second))).SkipWhile(c => c == 0).FirstOrDefault();
        }
    }

    public enum HandType : byte
    {
        HighCard = 0,
        OnePair = 1,
        TwoPairs = 2,
        ThreeOfAKind = 3,
        FullHouse = 4,
        FourOfAKind = 5,
        FiveOfAKind = 6
    }

    public enum Card : byte
    {
        A = 14,
        K = 13,
        Q = 12,
        J = 11,
        N10 = 10,
        N9 = 9,
        N8 = 8,
        N7 = 7,
        N6 = 6,
        N5 = 5,
        N4 = 4,
        N3 = 3,
        N2 = 2
    }
}
