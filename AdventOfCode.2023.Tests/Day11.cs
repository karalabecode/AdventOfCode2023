namespace AdventOfCode2023.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.IO;

using static AdventOfCode2023.Day11;

[TestClass]
public class Day11
{
    static readonly string TestInput = File.ReadAllText("Inputs/Day11.Example.txt");
    static readonly string RealInput = File.ReadAllText("Inputs/Day11.txt");

    [TestMethod]
    public void SolvePart1_ForWholeTestInput_ReturnsCorrectResult()
    {
        var input = TestInput;

        var result = SolvePart1(input);

        Assert.AreEqual(374, result);
    }

    [TestMethod]
    public void SolvePart1_ForWholeRealInput_ReturnsCorrectResult()
    {
        var input = RealInput;

        var result = SolvePart1(input);

        Assert.AreEqual(9563821, result);
    }

    [TestMethod]
    public void SolvePart2_ForWholeTestInput_ReturnsCorrectResult()
    {
        var input = TestInput;

        var result = SolvePart2(input);

        Assert.AreEqual(82000210, result);
    }

    [TestMethod]
    public void SolvePart2_ForWholeRealInput_ReturnsCorrectResult()
    {
        var input = RealInput;

        var result = SolvePart2(input);

        Assert.AreEqual(827009909817L, result);
    }
}
