using System.Reflection.Metadata.Ecma335;

namespace aoc24.Challenges.Challenge05;

public class Challenge05 : IChallenge
{
    public async Task<string> Part1(IInputReader reader, CancellationToken cancellation)
    {
        (var printRules, var printOrders) = await ReadInput(reader, cancellation);

        var answer = 0;
        foreach (var printOrder in printOrders)
        {
            var invalid = false; 
            for (int i = 0; i < printOrder.Count && !invalid; i++)
            {
                var page = printOrder[i];
                if (!printRules.TryGetValue(page, out var mustAppearAfter))
                {
                    continue;
                }

                var previous = printOrder.Take(i);
                invalid = previous.Any(mustAppearAfter.Contains);
            }

            if (invalid)
            {
                continue;
            }

            var middleIndex = printOrder.Count / 2;
            answer += printOrder[middleIndex];
        }

        return answer.ToString();
    }

    public async Task<string> Part2(IInputReader reader, CancellationToken cancellation)
    {
        (var printRules, var printOrders) = await ReadInput(reader, cancellation);

        var answer = 0;
        foreach (var printOrder in printOrders)
        {
            var invalid = false;
            for (int i = 0; i < printOrder.Count && !invalid; i++)
            {
                var page = printOrder[i];
                if (!printRules.TryGetValue(page, out var mustAppearAfter))
                {
                    continue;
                }

                var previous = printOrder.Take(i);
                invalid = previous.Any(mustAppearAfter.Contains);
            }

            if (!invalid)
            {
                continue;
            }

            var corrected = new List<int>();
            for (int i = 0; i < printOrder.Count; i++)
            {
                var page = printOrder[i];
                if (!printRules.TryGetValue(page, out var mustAppearAfter))
                {
                    corrected.Add(page);
                    continue;
                }

                var insertAt = corrected.FindIndex(mustAppearAfter.Contains);
                if (insertAt == -1)
                {
                    corrected.Add(page);
                }
                else
                {
                    corrected.Insert(insertAt, page);
                }
            }
            
            var middleIndex = corrected.Count / 2;
            answer += corrected[middleIndex];
        }

        return answer.ToString();
    }

    private static async Task<(Dictionary<int, HashSet<int>> printRules, List<IList<int>> printOrders)> ReadInput(IInputReader reader, CancellationToken cancellation)
    {
        var printRules = new Dictionary<int, HashSet<int>>();
        string line = await reader.ReadLineAsync(cancellation);
        do
        {
            var parts = line.Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var key = int.Parse(parts[0]);
            if (printRules.TryGetValue(key, out var set))
            {
                set.Add(int.Parse(parts[1]));
            }
            else
            {
                set = [int.Parse(parts[1])];
                printRules.Add(key, set);
            }

            line = await reader.ReadLineAsync(cancellation);
        }
        while (!string.IsNullOrEmpty(line));

        var printOrders = new List<IList<int>>();
        while (reader.CanRead)
        {
            line = await reader.ReadLineAsync(cancellation);
            var printOrder = line.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse).ToList();
            printOrders.Add(printOrder);
        }

        return (printRules, printOrders);
    }
}
