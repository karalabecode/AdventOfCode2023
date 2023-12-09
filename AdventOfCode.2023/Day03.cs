namespace AdventOfCode2023;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public readonly record struct Symbol(int Line, int Char, char SymbolChar);
public readonly record struct Number(int Line, int Char, int Length, int Value);

public partial class Day03
{
    private static Regex SymbolRegex = GetSymbolRegex();
    private static Regex NumberRegex = GetNumberRegex();

    public static int? SolvePart1(IEnumerable<string> input) => GetPartNumbers(input).Select(n => n.Value).Sum();

    public static long? SolvePart2(IEnumerable<string> input) => GetGearRatios(input).Sum();

    public static IEnumerable<Number> GetPartNumbers(IEnumerable<string> input)
    {
        var symbolsFromPreviousLine = Enumerable.Empty<Symbol>();
        var numbersFromPreviousLine = Enumerable.Empty<Number>();
        var lineNum = 0;

        foreach (var line in input)
        {
            var symbols = GetSymbols(lineNum, line);
            var numbers = GetNumbers(lineNum++, line).ToList();

            for (var i = 0; i < numbers.Count; ++i)
            {
                var number = numbers[i];

                if (symbolsFromPreviousLine.Any(s => IsAdjacent(s, number))
                    || symbols.Any(s => IsAdjacent(s, number)))
                {
                    yield return number;

                    numbers.RemoveAt(i--);
                }
            }

            var numbersP = numbersFromPreviousLine.ToList();

            foreach (var symbol in symbols)
            {
                for (var i = 0; i < numbersP.Count; ++i)
                {
                    var number = numbersP[i];

                    if (IsAdjacent(symbol, number))
                    {
                        yield return number;

                        numbersP.RemoveAt(i--);
                    }
                }
            }

            symbolsFromPreviousLine = symbols;
            numbersFromPreviousLine = numbers;
        }
    }

    private static bool IsAdjacent(Symbol symbol, Number number)
        => symbol.Char >= number.Char - 1 && symbol.Char <= number.Char + number.Length ||
            symbol.Char == number.Char - 1 || symbol.Char == number.Char + number.Length ||
            number.Char - 1 <= symbol.Char && number.Char + number.Length >= symbol.Char;

    public static IEnumerable<long> GetGearRatios(IEnumerable<string> input)
    {
        var gearSymbolsFromPreviousLine = Enumerable.Empty<Symbol>();
        var numbersFromPreviousLine = Enumerable.Empty<Number>();
        var numbersFromPreviousLineBefore = Enumerable.Empty<Number>();
        var lineNum = 0;

        foreach (var line in input)
        {
            var gearSymbols = GetSymbols(lineNum, line).Where(s => s.SymbolChar == '*');
            var numbers = GetNumbers(lineNum++, line);

            foreach (var symbol in gearSymbolsFromPreviousLine)
            {
                var adj = GetAdjacentNumbers(symbol, numbersFromPreviousLineBefore.Concat(numbersFromPreviousLine).Concat(numbers)).ToArray();

                if (adj.Length == 2) yield return (long)adj[0].Value * adj[1].Value;
            }

            numbersFromPreviousLineBefore = numbersFromPreviousLine;
            numbersFromPreviousLine = numbers;
            gearSymbolsFromPreviousLine = gearSymbols;
        }
    }

    private static IEnumerable<Number> GetAdjacentNumbers(Symbol symbol, IEnumerable<Number> numbers) => numbers.Where(n => IsAdjacent(symbol, n));

    public static IEnumerable<Symbol> GetSymbols(int line,  string input)
        => SymbolRegex.Matches(input).Select(m => new Symbol(line, m.Index, m.Value[0]));

    public static IEnumerable<Number> GetNumbers(int line, string input)
        => NumberRegex.Matches(input).Select(m => new Number(line, m.Index, m.Length, int.Parse(m.Value)));

    [GeneratedRegex(@"([^0-9\.])")]
    private static partial Regex GetSymbolRegex();

    [GeneratedRegex(@"(\d+)")]
    private static partial Regex GetNumberRegex();
}
