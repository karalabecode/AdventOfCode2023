namespace AdventOfCode2023.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;
using System.IO;

using static AdventOfCode2023.Day05;

[TestClass]
public class Day05
{
    static readonly IEnumerable<string> TestInput = File.ReadLines("Inputs/Day05.Example.txt");
    static readonly IEnumerable<string> RealInput = File.ReadLines("Inputs/Day05.txt");

    [TestMethod]
    public void SolvePart1_ForWholeTestInput_ReturnsCorrectResult()
    {
        var input = TestInput;

        var result = SolvePart1(input);

        Assert.AreEqual("35", result);
    }

    [TestMethod]
    public void SolvePart1_ForWholeRealInput_ReturnsCorrectResult()
    {
        var input = RealInput;

        var result = SolvePart1(input);

        Assert.AreEqual("227653707", result);
    }

    [TestMethod]
    public void Apply_ForSame_MapsInput()
    {
        var source = new List<Mapping>() { new(2, 1, 3) };
        var map = new List<Mapping>() { new(5, 2, 3) };

        var applied = Apply(source, map);

        CollectionAssert.AreEquivalent(new List<Mapping>() { new(5, 1, 3) }, applied);
    }

    [TestMethod]
    public void GetIntersection_ForNone_ReturnsNone()
    {
        var result = GetIntersection(new(0, 1), new(4, 5));

        Assert.IsNull(result);
    }

    [TestMethod]
    public void GetIntersection_ForSame_ReturnsSame()
    {
        var result = GetIntersection(new(4, 5), new(4, 5));

        Assert.AreEqual(new(4, 5), result);
    }

    [TestMethod]
    public void GetIntersection_ForLeftSideEqual_CorrectResult()
    {
        var result = GetIntersection(new(4, 5), new(4, 6));

        Assert.AreEqual(new(4, 5), result);
    }

    [TestMethod]
    public void GetIntersection_ForLeftSideEqualButWider_CorrectResult()
    {
        var result = GetIntersection(new(4, 6), new(4, 5));

        Assert.AreEqual(new(4, 5), result);
    }

    [TestMethod]
    public void GetIntersection_ForRightSideEqual_CorrectResult()
    {
        var result = GetIntersection(new(4, 5), new(3, 5));

        Assert.AreEqual(new(4, 5), result);
    }

    [TestMethod]
    public void GetIntersection_ForRightSideEqualButWider_CorrectResult()
    {
        var result = GetIntersection(new(3, 5), new(4, 5));

        Assert.AreEqual(new(4, 5), result);
    }

    [TestMethod]
    public void GetIntersection_ForInner_ReturnsCorrectResult()
    {
        var result = GetIntersection(new(4, 5), new(3, 6));

        Assert.AreEqual(new(4, 5), result);
    }

    [TestMethod]
    public void GetIntersection_ForOuter_ReturnsCorrectResult()
    {
        var result = GetIntersection(new(3, 6), new(4, 5));

        Assert.AreEqual(new(4, 5), result);
    }

    [TestMethod]
    public void GetIntersection_ForLeftNonEqual_ReturnsCorrectResult()
    {
        var result = GetIntersection(new(3, 5), new(4, 6));

        Assert.AreEqual(new(4, 5), result);
    }

    [TestMethod]
    public void GetIntersection_ForRightNonEqual_ReturnsCorrectResult()
    {
        var result = GetIntersection(new(4, 6), new(3, 5));

        Assert.AreEqual(new(4, 5), result);
    }

    [TestMethod]
    public void Apply_ForCurrentInsideMapping_MapsInput()
    {
        var source = new List<Mapping>() { new(7, 2, 2) };
        var map = new List<Mapping>() { new(5, 6, 4) };

        var applied = Apply(source, map);

        CollectionAssert.AreEquivalent(new List<Mapping>() { new(6, 2, 2) }, applied);
    }

    [TestMethod]
    public void Apply_ForSameStartButLessOnSource_MapsInput()
    {
        var source = new List<Mapping>() { new(2, 1, 2) };
        var map = new List<Mapping>() { new(5, 2, 3) };

        var applied = Apply(source, map);

        CollectionAssert.AreEquivalent(new List<Mapping>() { new(5, 1, 2) }, applied);
    }

    [TestMethod]
    public void Apply_ForSameStartButMoreOnSource_MapsInput()
    {
        var source = new List<Mapping>() { new(2, 1, 4) };
        var map = new List<Mapping>() { new(7, 2, 3) };

        var applied = Apply(source, map);

        CollectionAssert.AreEquivalent(new List<Mapping>() { new(7, 1, 3), new(5, 4, 1) }, applied);
    }

    [TestMethod]
    public void Apply_ForSameEndButLessOnSource_MapsInput()
    {
        var source = new List<Mapping>() { new(3, 1, 2) };
        var map = new List<Mapping>() { new(5, 2, 3) };

        var applied = Apply(source, map);

        CollectionAssert.AreEquivalent(new List<Mapping>() { new(6, 1, 2) }, applied);
    }

    [TestMethod]
    public void Apply_ForSameEndButMoreOnSource_MapsInput()
    {
        var source = new List<Mapping>() { new(3, 1, 4) };
        var map = new List<Mapping>() { new(7, 4, 3) };

        var applied = Apply(source, map);

        CollectionAssert.AreEquivalent(new List<Mapping>() { new(3, 1, 1), new(7, 2, 3) }, applied);
    }

    [TestMethod]
    public void SolvePart2_ForWholeTestInput_ReturnsCorrectResult()
    {
        var input = TestInput;

        var result = SolvePart2(input);

        Assert.AreEqual("46", result);
    }

    [TestMethod]
    public void SolvePart2_ForWholeRealInput_ReturnsCorrectResult()
    {
        var input = RealInput;

        var result = SolvePart2(input);

        Assert.AreEqual("78775051", result);
    }
}
