namespace aoc24.Challenges.Challenge05;

public class Challenge05 : IChallenge
{
    public async Task<string> Part1(IInputReader reader, CancellationToken cancellation)
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

        var printOrders = new List<IEnumerable<int>>();
        while (reader.CanRead)
        {
            line = await reader.ReadLineAsync(cancellation);
            var printOrder = line.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse);
            printOrders.Add(printOrder);
        }

        return "z";
    }

    public async Task<string> Part2(IInputReader reader, CancellationToken cancellation)
    {
        throw new PartNotImplementedException(2);
    }
}
