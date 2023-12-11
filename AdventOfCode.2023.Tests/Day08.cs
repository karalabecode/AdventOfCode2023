namespace AdventOfCode2023.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;
using System.IO;

using static AdventOfCode2023.Day08;

[TestClass]
public class Day08
{
    static readonly IEnumerable<string> TestInput1 = File.ReadLines("Inputs/Day08.Example1.txt");
    static readonly IEnumerable<string> TestInput2 = File.ReadLines("Inputs/Day08.Example2.txt");
    static readonly IEnumerable<string> TestInput3 = File.ReadLines("Inputs/Day08.Example3.txt");
    static readonly IEnumerable<string> RealInput = File.ReadLines("Inputs/Day08.txt");

    [TestMethod]
    public void SolvePart1_ForWholeTestInput1_ReturnsCorrectResult()
    {
        var input = TestInput1;

        var result = SolvePart1(input);

        Assert.AreEqual(2, result);
    }

    [TestMethod]
    public void SolvePart1_ForWholeTestInput2_ReturnsCorrectResult()
    {
        var input = TestInput2;

        var result = SolvePart1(input);

        Assert.AreEqual(6, result);
    }

    [TestMethod]
    public void SolvePart1_ForWholeRealInput_ReturnsCorrectResult()
    {
        var input = RealInput;

        var result = SolvePart1(input);

        Assert.AreEqual(14257, result);
    }

    [TestMethod]
    public void SolvePart2_ForWholeTestInput3_ReturnsCorrectResult()
    {
        var input = TestInput3;

        var result = SolvePart2(input);

        Assert.AreEqual(6, result);
    }

    [TestMethod]
    public void SolvePart2_ForWholeRealInput_ReturnsCorrectResult()
    {
        var input = RealInput;

        var result = SolvePart2(input);

        Assert.AreEqual(16187743689077L, result);
    }
}
