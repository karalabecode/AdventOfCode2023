namespace AdventOfCode2023.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;
using System.IO;

using static AdventOfCode2023.Day06;

[TestClass]
public class Day06
{
    static readonly IEnumerable<string> TestInput = File.ReadLines("Inputs/Day06.Example.txt");
    static readonly IEnumerable<string> RealInput = File.ReadLines("Inputs/Day06.txt");

    [TestMethod]
    public void SolvePart1_ForWholeTestInput_ReturnsCorrectResult()
    {
        var input = TestInput;

        var result = SolvePart1(input);

        Assert.AreEqual(288ul, result);
    }

    [TestMethod]
    public void SolvePart1_ForWholeRealInput_ReturnsCorrectResult()
    {
        var input = RealInput;

        var result = SolvePart1(input);

        Assert.AreEqual(316800ul, result);
    }

    [TestMethod]
    public void SolvePart2_ForWholeTestInput_ReturnsCorrectResult()
    {
        var input = TestInput;

        var result = SolvePart2(input);

        Assert.AreEqual(71503ul, result);
    }

    [TestMethod]
    public void SolvePart2_ForWholeRealInput_ReturnsCorrectResult()
    {
        var input = RealInput;

        var result = SolvePart2(input);

        Assert.AreEqual(45647654ul, result);
    }
}
