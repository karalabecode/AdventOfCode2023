namespace AdventOfCode2023.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.IO;

[TestClass]
public class Day01
{
    static readonly string TestInput = File.ReadAllText("Inputs/Day01.Example.txt");
    static readonly string RealInput = File.ReadAllText("Inputs/Day01.txt");

    [TestMethod]
    public void TestInputLine1()
    {
        var input = TestInput.Split("\n")[0];

        var result = AdventOfCode2023.Day01.Solve(input);

        Assert.AreEqual(12, result);
    }

    [TestMethod]
    public void TestInputLine2()
    {
        var input = TestInput.Split("\n")[1];

        var result = AdventOfCode2023.Day01.Solve(input);

        Assert.AreEqual(38, result);
    }

    [TestMethod]
    public void TestInputLine3()
    {
        var input = TestInput.Split("\n")[2];

        var result = AdventOfCode2023.Day01.Solve(input);

        Assert.AreEqual(15, result);
    }

    [TestMethod]
    public void TestInputLine4()
    {
        var input = TestInput.Split("\n")[3];

        var result = AdventOfCode2023.Day01.Solve(input);

        Assert.AreEqual(77, result);
    }

    [TestMethod]
    public void WholeTestInput()
    {
        var input = TestInput;

        var result = AdventOfCode2023.Day01.Solve(input);

        Assert.AreEqual(142, result);
    }

    [TestMethod]
    public void WholeRealInput()
    {
        var input = RealInput;

        var result = AdventOfCode2023.Day01.Solve(input);

        Assert.AreEqual(56506, result);
    }
}
