namespace AdventOfCode2023.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.IO;

[TestClass]
public class Day02
{
    static readonly string TestInput = File.ReadAllText("Inputs/Day02.Example.txt");
    static readonly string RealInput = File.ReadAllText("Inputs/Day02.txt");

    [TestMethod]
    public void SolvePart1_ForFirstLine_ReturnsCorrectResult()
    {
        var input = TestInput.Split("\n")[0];

        var result = AdventOfCode2023.Day02.SolvePart1(input);

        Assert.AreEqual(1, result);
    }

    [TestMethod]
    public void SolvePart1_ForWholeTestInput_ReturnsCorrectResult()
    {
        var input = TestInput;

        var result = AdventOfCode2023.Day02.SolvePart1(input);

        Assert.AreEqual(8, result);
    }

    [TestMethod]
    public void SolvePart1_ForWholeRealInput_ReturnsCorrectResult()
    {
        var input = RealInput;

        var result = AdventOfCode2023.Day02.SolvePart1(input);

        Assert.AreEqual(1853, result);
    }

    [TestMethod]
    public void SolvePart2_ForWholeTestInput_ReturnsCorrectResult()
    {
        var input = TestInput;

        var result = AdventOfCode2023.Day02.SolvePart2(input);

        Assert.AreEqual(2286, result);
    }

    [TestMethod]
    public void SolvePart2_ForWholeRealInput_ReturnsCorrectResult()
    {
        var input = RealInput;

        var result = AdventOfCode2023.Day02.SolvePart2(input);

        Assert.AreEqual(72706, result);
    }
}
