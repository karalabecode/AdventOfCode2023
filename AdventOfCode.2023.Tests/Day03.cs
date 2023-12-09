namespace AdventOfCode2023.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;
using System.IO;

[TestClass]
public class Day03
{
    static readonly IEnumerable<string> TestInput = File.ReadLines("Inputs/Day03.Example.txt");
    static readonly IEnumerable<string> RealInput = File.ReadLines("Inputs/Day03.txt");

    [TestMethod]
    public void SolvePart1_ForWholeTestInput_ReturnsCorrectResult()
    {
        var input = TestInput;

        var result = AdventOfCode2023.Day03.SolvePart1(input);

        Assert.AreEqual(4361, result);
    }

    [TestMethod]
    public void SolvePart1_ForWholeRealInput_ReturnsCorrectResult()
    {
        var input = RealInput;

        var result = AdventOfCode2023.Day03.SolvePart1(input);

        Assert.AreEqual(531932, result);
    }

    [TestMethod]
    public void SolvePart2_ForWholeTestInput_ReturnsCorrectResult()
    {
        var input = TestInput;

        var result = AdventOfCode2023.Day03.SolvePart2(input);

        Assert.AreEqual(467835, result);
    }

    [TestMethod]
    public void SolvePart2_ForWholeRealInput_ReturnsCorrectResult()
    {
        var input = RealInput;

        var result = AdventOfCode2023.Day03.SolvePart2(input);

        Assert.AreEqual(73646890, result);
    }
}
