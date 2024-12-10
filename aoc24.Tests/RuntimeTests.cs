using aoc24.Options;

namespace aoc24.Tests;

[TestClass]
public class RuntimeTests
{
    [TestMethod]
    [DataRow(1, 1, true, "11", DisplayName = "Day 01 Part 1 Test")]
    [DataRow(1, 1, false, "1590491", DisplayName = "Day 01 Part 1 Actual")]
    [DataRow(1, 2, true, "31", DisplayName = "Day 01 Part 2 Test")]
    [DataRow(1, 2, false, "22588371", DisplayName = "Day 01 Part 2 Actual")]

    [DataRow(2, 1, true, "2", DisplayName = "Day 02 Part 1 Test")]
    [DataRow(2, 1, false, "279", DisplayName = "Day 02 Part 1 Actual")]
    [DataRow(2, 2, true, "4", DisplayName = "Day 02 Part 2 Test")]
    [DataRow(2, 2, false, "343", DisplayName = "Day 02 Part 2 Actual")]

    [DataRow(3, 1, true, "161", DisplayName = "Day 03 Part 1 Test")]
    [DataRow(3, 1, false, "174103751", DisplayName = "Day 03 Part 1 Actual")]
    [DataRow(3, 2, true, "48", DisplayName = "Day 03 Part 2 Test")]
    [DataRow(3, 2, false, "100411201", DisplayName = "Day 03 Part 2 Actual")]

    [DataRow(4, 1, true, "18", DisplayName = "Day 04 Part 1 Test")]
    [DataRow(4, 1, false, "2434", DisplayName = "Day 04 Part 1 Actual")]
    [DataRow(4, 2, true, "9", DisplayName = "Day 04 Part 2 Test")]
    [DataRow(4, 2, false, "1835", DisplayName = "Day 04 Part 2 Actual")]

    [DataRow(5, 1, true, "143", DisplayName = "Day 05 Part 1 Test")]
    [DataRow(5, 1, false, "", DisplayName = "Day 05 Part 1 Actual")]
    [DataRow(5, 2, true, "", DisplayName = "Day 05 Part 2 Test")]
    [DataRow(5, 2, false, "", DisplayName = "Day 05 Part 2 Actual")]

    [DataRow(6, 1, true, "", DisplayName = "Day 06 Part 1 Test")]
    [DataRow(6, 1, false, "", DisplayName = "Day 06 Part 1 Actual")]
    [DataRow(6, 2, true, "", DisplayName = "Day 06 Part 2 Test")]
    [DataRow(6, 2, false, "", DisplayName = "Day 06 Part 2 Actual")]

    [DataRow(7, 1, true, "", DisplayName = "Day 07 Part 1 Test")]
    [DataRow(7, 1, false, "", DisplayName = "Day 07 Part 1 Actual")]
    [DataRow(7, 2, true, "", DisplayName = "Day 07 Part 2 Test")]
    [DataRow(7, 2, false, "", DisplayName = "Day 07 Part 2 Actual")]

    [DataRow(8, 1, true, "", DisplayName = "Day 08 Part 1 Test")]
    [DataRow(8, 1, false, "", DisplayName = "Day 08 Part 1 Actual")]
    [DataRow(8, 2, true, "", DisplayName = "Day 08 Part 2 Test")]
    [DataRow(8, 2, false, "", DisplayName = "Day 08 Part 2 Actual")]

    [DataRow(9, 1, true, "", DisplayName = "Day 09 Part 1 Test")]
    [DataRow(9, 1, false, "", DisplayName = "Day 09 Part 1 Actual")]
    [DataRow(9, 2, true, "", DisplayName = "Day 09 Part 2 Test")]
    [DataRow(9, 2, false, "", DisplayName = "Day 09 Part 2 Actual")]

    [DataRow(10, 1, true, "", DisplayName = "Day 10 Part 1 Test")]
    [DataRow(10, 1, false, "", DisplayName = "Day 10 Part 1 Actual")]
    [DataRow(10, 2, true, "", DisplayName = "Day 10 Part 2 Test")]
    [DataRow(10, 2, false, "", DisplayName = "Day 10 Part 2 Actual")]

    [DataRow(11, 1, true, "", DisplayName = "Day 11 Part 1 Test")]
    [DataRow(11, 1, false, "", DisplayName = "Day 11 Part 1 Actual")]
    [DataRow(11, 2, true, "", DisplayName = "Day 11 Part 2 Test")]
    [DataRow(11, 2, false, "", DisplayName = "Day 11 Part 2 Actual")]

    [DataRow(12, 1, true, "", DisplayName = "Day 12 Part 1 Test")]
    [DataRow(12, 1, false, "", DisplayName = "Day 12 Part 1 Actual")]
    [DataRow(12, 2, true, "", DisplayName = "Day 12 Part 2 Test")]
    [DataRow(12, 2, false, "", DisplayName = "Day 12 Part 2 Actual")]

    [DataRow(13, 1, true, "", DisplayName = "Day 13 Part 1 Test")]
    [DataRow(13, 1, false, "", DisplayName = "Day 13 Part 1 Actual")]
    [DataRow(13, 2, true, "", DisplayName = "Day 13 Part 2 Test")]
    [DataRow(13, 2, false, "", DisplayName = "Day 13 Part 2 Actual")]

    [DataRow(14, 1, true, "", DisplayName = "Day 14 Part 1 Test")]
    [DataRow(14, 1, false, "", DisplayName = "Day 14 Part 1 Actual")]
    [DataRow(14, 2, true, "", DisplayName = "Day 14 Part 2 Test")]
    [DataRow(14, 2, false, "", DisplayName = "Day 14 Part 2 Actual")]

    [DataRow(15, 1, true, "", DisplayName = "Day 15 Part 1 Test")]
    [DataRow(15, 1, false, "", DisplayName = "Day 15 Part 1 Actual")]
    [DataRow(15, 2, true, "", DisplayName = "Day 15 Part 2 Test")]
    [DataRow(15, 2, false, "", DisplayName = "Day 15 Part 2 Actual")]

    [DataRow(16, 1, true, "", DisplayName = "Day 16 Part 1 Test")]
    [DataRow(16, 1, false, "", DisplayName = "Day 16 Part 1 Actual")]
    [DataRow(16, 2, true, "", DisplayName = "Day 16 Part 2 Test")]
    [DataRow(16, 2, false, "", DisplayName = "Day 16 Part 2 Actual")]

    [DataRow(17, 1, true, "", DisplayName = "Day 17 Part 1 Test")]
    [DataRow(17, 1, false, "", DisplayName = "Day 17 Part 1 Actual")]
    [DataRow(17, 2, true, "", DisplayName = "Day 17 Part 2 Test")]
    [DataRow(17, 2, false, "", DisplayName = "Day 17 Part 2 Actual")]

    [DataRow(18, 1, true, "", DisplayName = "Day 18 Part 1 Test")]
    [DataRow(18, 1, false, "", DisplayName = "Day 18 Part 1 Actual")]
    [DataRow(18, 2, true, "", DisplayName = "Day 18 Part 2 Test")]
    [DataRow(18, 2, false, "", DisplayName = "Day 18 Part 2 Actual")]

    [DataRow(19, 1, true, "", DisplayName = "Day 19 Part 1 Test")]
    [DataRow(19, 1, false, "", DisplayName = "Day 19 Part 1 Actual")]
    [DataRow(19, 2, true, "", DisplayName = "Day 19 Part 2 Test")]
    [DataRow(19, 2, false, "", DisplayName = "Day 19 Part 2 Actual")]

    [DataRow(20, 1, true, "", DisplayName = "Day 20 Part 1 Test")]
    [DataRow(20, 1, false, "", DisplayName = "Day 20 Part 1 Actual")]
    [DataRow(20, 2, true, "", DisplayName = "Day 20 Part 2 Test")]
    [DataRow(20, 2, false, "", DisplayName = "Day 20 Part 2 Actual")]

    [DataRow(21, 1, true, "", DisplayName = "Day 21 Part 1 Test")]
    [DataRow(21, 1, false, "", DisplayName = "Day 21 Part 1 Actual")]
    [DataRow(21, 2, true, "", DisplayName = "Day 21 Part 2 Test")]
    [DataRow(21, 2, false, "", DisplayName = "Day 21 Part 2 Actual")]

    [DataRow(22, 1, true, "", DisplayName = "Day 22 Part 1 Test")]
    [DataRow(22, 1, false, "", DisplayName = "Day 22 Part 1 Actual")]
    [DataRow(22, 2, true, "", DisplayName = "Day 22 Part 2 Test")]
    [DataRow(22, 2, false, "", DisplayName = "Day 22 Part 2 Actual")]

    [DataRow(23, 1, true, "", DisplayName = "Day 23 Part 1 Test")]
    [DataRow(23, 1, false, "", DisplayName = "Day 23 Part 1 Actual")]
    [DataRow(23, 2, true, "", DisplayName = "Day 23 Part 2 Test")]
    [DataRow(23, 2, false, "", DisplayName = "Day 23 Part 2 Actual")]

    [DataRow(24, 1, true, "", DisplayName = "Day 24 Part 1 Test")]
    [DataRow(24, 1, false, "", DisplayName = "Day 24 Part 1 Actual")]
    [DataRow(24, 2, true, "", DisplayName = "Day 24 Part 2 Test")]
    [DataRow(24, 2, false, "", DisplayName = "Day 24 Part 2 Actual")]

    [DataRow(25, 1, true, "", DisplayName = "Day 25 Part 1 Test")]
    [DataRow(25, 1, false, "", DisplayName = "Day 25 Part 1 Actual")]
    [DataRow(25, 2, true, "", DisplayName = "Day 25 Part 2 Test")]
    [DataRow(25, 2, false, "", DisplayName = "Day 25 Part 2 Actual")]
    public async Task TestRun(int challange, int part, bool useTestFile, string expected, params string[] challengeSpecificOptions)
    {
        var options = CreateOptions(challange, part, useTestFile, challengeSpecificOptions);
        var runtime = new Runtime(options);

        var result = await runtime.Run(default);

        Assert.IsNotNull(result);
        Assert.IsTrue(result.IsSuccess(out var answer, out var error));
        Assert.IsNotNull(answer);
        Assert.IsNull(error);
        Assert.AreEqual(expected, answer);
    }

    private static IOptions CreateOptions(int challange, int part, bool useTestFile, IList<string> challengeSpecificOptions)
    {
        if (!BaseOptions.TryParseIfSpecific(challange, part, useTestFile, null, challengeSpecificOptions, out var options, out var _))
        {
            throw new InternalTestFailureException($"Could not initialize options to test challenge {challange}");
        }

        return options;
    }
}
