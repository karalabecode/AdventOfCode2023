namespace AdventOfCode2023.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;
using System.IO;

using static AdventOfCode2023.Day09;

[TestClass]
public class Day09
{
    static readonly IEnumerable<string> TestInput = File.ReadLines("Inputs/Day09.Example.txt");
    static readonly IEnumerable<string> RealInput = File.ReadLines("Inputs/Day09.txt");

    [TestMethod]
    public void SolvePart1_ForWholeTestInput_ReturnsCorrectResult()
    {
        var input = TestInput;

        var result = SolvePart1(input);

        Assert.AreEqual(114, result);
    }

    [TestMethod]
    public void SolvePart1_ForWholeRealInput_ReturnsCorrectResult()
    {
        var input = RealInput;

        var result = SolvePart1(input);

        Assert.AreEqual(1898776583, result);
    }

    [TestMethod]
    public void SolvePart2_ForWholeTestInput_ReturnsCorrectResult()
    {
        var input = TestInput;

        var result = SolvePart2(input);

        Assert.AreEqual(2, result);
    }

    [TestMethod]
    public void SolvePart2_ForWholeRealInput_ReturnsCorrectResult()
    {
        var input = RealInput;

        var result = SolvePart2(input);

        Assert.AreEqual(1100, result);
    }
}
