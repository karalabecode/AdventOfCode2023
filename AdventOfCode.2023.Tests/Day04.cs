namespace AdventOfCode2023.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;
using System.IO;

[TestClass]
public class Day04
{
    static readonly IEnumerable<string> TestInput = File.ReadLines("Inputs/Day04.Example.txt");
    static readonly IEnumerable<string> RealInput = File.ReadLines("Inputs/Day04.txt");

    [TestMethod]
    public void SolvePart1_ForWholeTestInput_ReturnsCorrectResult()
    {
        var input = TestInput;

        var result = AdventOfCode2023.Day04.SolvePart1(input);

        Assert.AreEqual(13, result);
    }

    [TestMethod]
    public void SolvePart1_ForWholeRealInput_ReturnsCorrectResult()
    {
        var input = RealInput;

        var result = AdventOfCode2023.Day04.SolvePart1(input);

        Assert.AreEqual(27059, result);
    }

    [TestMethod]
    public void SolvePart2_ForWholeTestInput_ReturnsCorrectResult()
    {
        var input = TestInput;

        var result = AdventOfCode2023.Day04.SolvePart2(input);

        Assert.AreEqual(30, result);
    }

    [TestMethod]
    public void SolvePart2_ForWholeRealInput_ReturnsCorrectResult()
    {
        var input = RealInput;

        var result = AdventOfCode2023.Day04.SolvePart2(input);

        Assert.AreEqual(5744979, result);
    }
}
