using System;
using System.Reflection;

namespace aoc24.Challenges.Challenge07;

public class Challenge07 : IChallenge
{
    public async Task<string> Part1(IInputReader reader, CancellationToken cancellation)
    {
        IEnumerable<Func<long, long, long>> operators = [
            (x, y) => x + y,
            (x, y) => x * y,
        ];

        var answer = 0L;
        await foreach (var line in reader.ReadAllLinesAsync(cancellation))
        {
            var colonIndex = line.IndexOf(':');
            var expected = long.Parse(line[..colonIndex]);
            var values = line[(1 + colonIndex)..].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(long.Parse).ToList();
            if (operators.Any(op => Matches(operators, expected, values[0], 1, values)))
            {
                answer += expected;
            }
        }

        return answer.ToString();
    }

    public async Task<string> Part2(IInputReader reader, CancellationToken cancellation)
    {
        IEnumerable<Func<long, long, long>> operators = [
            (x, y) => x + y,
            (x, y) => x * y,
            (x, y) => long.Parse(x.ToString() + y.ToString()),
        ];

        var answer = 0L;
        await foreach (var line in reader.ReadAllLinesAsync(cancellation))
        {
            var colonIndex = line.IndexOf(':');
            var expected = long.Parse(line[..colonIndex]);
            var values = line[(1 + colonIndex)..].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(long.Parse).ToList();
            if (operators.Any(op => Matches(operators, expected, values[0], 1, values)))
            {
                answer += expected;
            }
        }

        return answer.ToString();
    }

    private static bool Matches(IEnumerable<Func<long, long, long>> operators, long expected, long current, int index, IList<long> values)
    {
        if (index == values.Count)
        {
            return current == expected;
        }

        return operators.Any(op => Matches(operators, expected, op.Invoke(current, values[index]), 1 + index, values));
    }
}
