namespace AdventOfCode2023.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;
using System.IO;

using static AdventOfCode2023.Day10;

[TestClass]
public class Day10
{
    static readonly IEnumerable<string> TestInput1 = File.ReadLines("Inputs/Day10.Example1.txt");
    static readonly IEnumerable<string> TestInput2 = File.ReadLines("Inputs/Day10.Example2.txt");
    static readonly IEnumerable<string> RealInput = File.ReadLines("Inputs/Day10.txt");

    [TestMethod]
    public void SolvePart1_ForWholeTestInput1_ReturnsCorrectResult()
    {
        var input = TestInput1;

        var result = SolvePart1(input);

        Assert.AreEqual(4, result);
    }

    [TestMethod]
    public void SolvePart1_ForWholeTestInput2_ReturnsCorrectResult()
    {
        var input = TestInput2;

        var result = SolvePart1(input);

        Assert.AreEqual(8, result);
    }

    [TestMethod]
    public void SolvePart1_ForWholeRealInput_ReturnsCorrectResult()
    {
        var input = RealInput;

        var result = SolvePart1(input);

        Assert.AreEqual(6690, result);
    }

    [TestMethod]
    public void SolvePart2_ForWholeTestInput1_ReturnsCorrectResult()
    {
        var input = TestInput1;

        var result = SolvePart2(input);

        Assert.AreEqual(1, result);
    }

    [TestMethod]
    public void SolvePart2_ForWholeTestInput2_ReturnsCorrectResult()
    {
        var input = TestInput2;

        var result = SolvePart2(input);

        Assert.AreEqual(1, result);
    }

    [TestMethod]
    public void SolvePart2_ForWholeRealInput_ReturnsCorrectResult()
    {
        var input = RealInput;

        var result = SolvePart2(input);

        Assert.AreEqual(525, result);
    }
}
