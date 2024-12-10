namespace aoc24.Challenges.Challenge02;

public class Challenge02 : IChallenge
{
    public async Task<string> Part1(IInputReader reader, CancellationToken cancellation)
    {
        var answer = 0;
        await foreach (var item in reader.ReadAllLinesAsync(cancellation))
        {
            var levels = item.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse).ToList();
            if (IsSafe(levels))
            {
                answer++;
            }
        }

        return answer.ToString();
    }

    public async Task<string> Part2(IInputReader reader, CancellationToken cancellation)
    {
        var answer = 0;
        await foreach (var item in reader.ReadAllLinesAsync(cancellation))
        {
            var levels = item.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse).ToList();
            var safe = false;
            for (int i = 0; i < levels.Count; i++)
            {
                var clone = new List<int>(levels);
                clone.RemoveAt(i);
                if (IsSafe(clone))
                {
                    safe = true;
                    break;
                }
            }

            if (safe)
            {
                answer++;
            }
        }

        return answer.ToString();
    }

    private static bool IsSafe(IList<int> levels)
    {
        if (levels.Count < 2)
        {
            return true;
        }

        long decreasing = (levels[1] - levels[0]) & 0x80000000;
        int previous = levels[0];
        for (int i = 1; i < levels.Count; i++)
        {
            var diff = levels[i] - previous;
            var absDiff = Math.Abs(diff);
            if (absDiff < 1 || absDiff > 3)
            {
                return false;
            }

            var currentlyDecreasing = diff & 0x80000000;
            if (decreasing != currentlyDecreasing)
            {
                return false;
            }

            previous = levels[i];
        }

        return true;
    }
}
