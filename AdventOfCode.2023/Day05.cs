namespace AdventOfCode2023;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using static System.Linq.Expressions.Expression;
using System.Text.RegularExpressions;

using Big = System.Numerics.BigInteger;

public partial class Day05
{
    public static Regex NumberRegex = GetNumberRegex();

    public readonly record struct Range(Big Start, Big End)
    {
        public Big Length => this.End - this.Start + 1;

        public static Range FromStartLength(Big start, Big length) => new(start, start + length - 1);

        public Range GetLeft(Big leftLength) => FromStartLength(this.Start, leftLength);
        public Range GetRight(Big rightLength) => FromStartLength(this.End - rightLength + 1, rightLength);
        public Range GetInner(Big leftLength, Big rightLength) => new(this.Start + leftLength, this.End - rightLength);
    }

    public readonly record struct Mapping(Range Source, Range Destination)
    {
        public Mapping(Big destinationStart, Big sourceStart, Big length) : this(Range.FromStartLength(sourceStart, length), Range.FromStartLength(destinationStart, length)) { }

        public Big Length => this.Source.Length;

        public static implicit operator Mapping(Range range) => new(range, range);

        public Mapping GetLeft(Big leftLength)
            => new(this.Source.GetLeft(leftLength), this.Destination.GetLeft(leftLength));

        public Mapping GetRight(Big rightLength)
            => new(this.Source.GetRight(rightLength), this.Destination.GetRight(rightLength));
    }

    public static Big? SolvePart1(IEnumerable<string> input)
    {
        var enumerator = input.GetEnumerator();
        enumerator.MoveNext();

        var seeds = NumberRegex.Matches(enumerator.Current).Select(m => Big.Parse(m.Value)).ToArray();
        var getLocation = GetLocationMethod(enumerator);

        return seeds.Min(getLocation);
    }

    public static Big? SolvePart2(IEnumerable<string> input)
    {
        var enumerator = input.GetEnumerator();
        enumerator.MoveNext();

        var seedRanges = NumberRegex.Matches(enumerator.Current).Select(m => Big.Parse(m.Value)).ToArray();
        var result = GetInitialMappings(seedRanges).ToList();

        SkipUntilNext(enumerator);

        var seedToSoilMap = GetMap(enumerator);
        result = Apply(result, seedToSoilMap);

        SkipUntilNext(enumerator);

        var soilToFertilizerMap = GetMap(enumerator);
        result = Apply(result, soilToFertilizerMap);

        SkipUntilNext(enumerator);

        var fertilizerToWaterMap = GetMap(enumerator);
        result = Apply(result, fertilizerToWaterMap);

        SkipUntilNext(enumerator);

        var waterToLightMap = GetMap(enumerator);
        result = Apply(result, waterToLightMap);

        SkipUntilNext(enumerator);

        var lightToTemperatureMap = GetMap(enumerator);
        result = Apply(result, lightToTemperatureMap);

        SkipUntilNext(enumerator);

        var temperatureToHumidityMap = GetMap(enumerator);
        result = Apply(result, temperatureToHumidityMap);

        SkipUntilNext(enumerator);

        var humidityToLocationMap = GetMap(enumerator);
        result = Apply(result, humidityToLocationMap);

        return result.Min(m => m.Destination.Start);
    }

    public static Range? GetIntersection(Range range1, Range range2)
        => range1.Start <= range2.Start && range1.End >= range2.Start && range1.End <= range2.End
        ? new(range2.Start, range1.End)
        : range1.Start >= range2.Start && range1.Start <= range2.End && range1.End > range2.End
        ? new(range1.Start, range2.End)
        : range1.Start <= range2.Start && range1.End >= range2.End
        ? new(range2.Start, range2.End)
        : range1.Start >= range2.Start && range1.End <= range2.End
        ? new(range1.Start, range1.End)
        : null;

    public static List<Mapping> Apply(List<Mapping> source, List<Mapping> mapping)
    {
        var mappingOrdered = mapping.OrderBy(m => m.Source.Start).ToArray();
        var sourceOrdered = source.OrderBy(m => m.Destination.Start).ToArray();

        var result = new List<Mapping>();

        for (var i = 0; i < sourceOrdered.Length; ++i)
        {
            var currentSource = (Mapping?)sourceOrdered[i];

            for (var j = 0; j < mappingOrdered.Length && currentSource is Mapping cs; ++j)
            {
                var cm = mappingOrdered[j];
                
                if (GetIntersection(cs.Destination, cm.Source) is Range r)
                {
                    var leftLength = r.Start > cs.Destination.Start ? r.Start - cs.Destination.Start : 0;

                    if (leftLength > 0)
                    {
                        result.Add(cs.GetLeft(leftLength));
                    }

                    var rightLength = cs.Destination.End > r.End ? cs.Destination.End - r.End : 0;

                    currentSource = rightLength > 0 ? cs.GetRight(rightLength) : null;

                    var mLeftLength = r.Start > cm.Source.Start ? r.Start - cm.Source.Start : 0;
                    var mRightLength = cm.Source.End > r.End ? cm.Source.End - r.End : 0;

                    result.Add(new Mapping(
                        Source: cs.Source.GetInner(leftLength, rightLength),
                        Destination: cm.Destination.GetInner(mLeftLength, mRightLength)
                    ));
                }
            }

            if (currentSource is Mapping csm)
            {
                result.Add(csm);
            }
        }

        return result;
    }

    public static IEnumerable<Mapping> GetInitialMappings(Big[] seedRanges)
        => Enumerable.Range(0, seedRanges.Length / 2)
            .Select(i => (Mapping)Range.FromStartLength(seedRanges[i * 2], seedRanges[i * 2 + 1]));

    public static Func<Big, Big> GetLocationMethod(IEnumerator<string> enumerator)
    {
        var arg = Parameter(typeof(Big), "i");

        SkipUntilNext(enumerator);

        var seedToSoilMap = GetMap(enumerator);
        var seedToSoilExpr = CreateExpression(arg, seedToSoilMap);
        var seedToSoil = seedToSoilExpr.Compile();

        SkipUntilNext(enumerator);

        var soilToFertilizerMap = GetMap(enumerator);
        var soilToFertilizerExpr = CreateExpression(arg, soilToFertilizerMap);
        var soilToFertilizer = soilToFertilizerExpr.Compile();

        SkipUntilNext(enumerator);

        var fertilizerToWaterMap = GetMap(enumerator);
        var fertilizerToWaterExpr = CreateExpression(arg, fertilizerToWaterMap);
        var fertilizerToWater = fertilizerToWaterExpr.Compile();

        SkipUntilNext(enumerator);

        var waterToLightMap = GetMap(enumerator);
        var waterToLightExpr = CreateExpression(arg, waterToLightMap);
        var waterToLight = waterToLightExpr.Compile();

        SkipUntilNext(enumerator);

        var lightToTemperatureMap = GetMap(enumerator);
        var lightToTemperatureExpr = CreateExpression(arg, lightToTemperatureMap);
        var lightToTemperature = lightToTemperatureExpr.Compile();

        SkipUntilNext(enumerator);

        var temperatureToHumidityMap = GetMap(enumerator);
        var temperatureToHumidityExpr = CreateExpression(arg, temperatureToHumidityMap);
        var temperatureToHumidity = temperatureToHumidityExpr.Compile();

        SkipUntilNext(enumerator);

        var humidityToLocationMap = GetMap(enumerator);
        var humidityToLocationExpr = CreateExpression(arg, humidityToLocationMap);
        var humidityToLocation = humidityToLocationExpr.Compile();

        return i => humidityToLocation(
            temperatureToHumidity(
                lightToTemperature(
                    waterToLight(
                        fertilizerToWater(
                            soilToFertilizer(
                                seedToSoil(i)))))));
    }

    public static Expression<Func<Big, Big>> CreateExpression(ParameterExpression arg, List<Mapping> map)
        => (Expression<Func<Big, Big>>)Lambda(map.Aggregate((Expression)arg, (prev, mapping) =>
            Condition
            (
                test: And
                (
                    GreaterThanOrEqual(arg, Constant(mapping.Source.Start)),
                    LessThan(arg, Constant(mapping.Source.Start + mapping.Length))
                ),
                ifTrue: Add(arg, Constant(mapping.Destination.Start - mapping.Source.Start)),
                ifFalse: prev
            )
        ), arg);

    public static void SkipUntilNext(IEnumerator<string> enumerator)
    {
        enumerator.MoveNext();
        while (string.IsNullOrWhiteSpace(enumerator.Current)) enumerator.MoveNext();
    }

    public static List<Mapping> GetMap(IEnumerator<string> enumerator)
    {
        var result = new List<Mapping>();

        while (enumerator.MoveNext() && !string.IsNullOrWhiteSpace(enumerator.Current))
        {
            var numbers = NumberRegex.Matches(enumerator.Current).Select(m => Big.Parse(m.Value)).ToArray();

            result.Add(new Mapping(Range.FromStartLength(numbers[1], numbers[2]), Range.FromStartLength(numbers[0], numbers[2])));
        }

        return result;
    }

    public static Func<Big, Big> GetFunc(List<Mapping> map)
        => i => map.Select(m => (Mapping?)m)
            .FirstOrDefault(mapping => i >= mapping!.Value.Source.Start && i < mapping.Value.Source.Start + mapping.Value.Length) is Mapping m
            ? m.Destination.Start + i - m.Source.Start
            : i;

    [GeneratedRegex(@"(\d+)")]
    public static partial Regex GetNumberRegex();
}
