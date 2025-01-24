using System.Collections.Concurrent;

namespace aoc24.Challenges.Challenge11;

public class Challenge11 : IChallenge
{
    public async Task<string> Part1(IInputReader reader, CancellationToken cancellation)
    {
        var line = await reader.ReadLineAsync(cancellation);
        var stones = line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();

        var answer = 0L;
        for (int i = 0; i < stones.Count; i++)
        {
            answer += ComputeStoneCount(stones[i], 0, 25);
        }

        return answer.ToString();
    }

    public async Task<string> Part2(IInputReader reader, CancellationToken cancellation)
    {
        var line = await reader.ReadLineAsync(cancellation);
        var stones = line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();

        var results = new ConcurrentBag<long>();
        Parallel.ForEach(stones, stone => results.Add(ComputeStoneCount(stone, 0, 75)));

        return results.Sum().ToString();
    }

    private static long ComputeStoneCount(long stone, int depth, int maxDepth)
    {
        if (depth == maxDepth)
        {
            return 1L;
        }

        if (stone == 0L)
        {
            return ComputeStoneCount(1L, depth + 1, maxDepth);
        }

        var digits = 1L + (long)Math.Log10(stone);
        if (digits % 2 == 0)
        {
            var separator = (long)Math.Pow(10L, digits / 2);
            var leftStone = stone / separator;
            var rightStone = stone % separator;
            var left = ComputeStoneCount(leftStone, depth + 1, maxDepth);
            var right = ComputeStoneCount(rightStone, depth + 1, maxDepth);
            return left + right;
        }

        return ComputeStoneCount(stone * 2024, depth + 1, maxDepth);
    }
}
