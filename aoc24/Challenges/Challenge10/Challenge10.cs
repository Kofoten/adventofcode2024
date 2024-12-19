namespace aoc24.Challenges.Challenge10;

public class Challenge10 : IChallenge
{
    public async Task<string> Part1(IInputReader reader, CancellationToken cancellation)
    {
        var map = new List<IList<int>>();
        var trailheads = new List<(int l, int c)>();

        for (int i = 0; reader.CanRead; i++)
        {
            var row = new List<int>();
            map.Add(row);
            var line = await reader.ReadLineAsync(cancellation);
            for (int j = 0; j < line.Length; j++)
            {
                var value = (int)char.GetNumericValue(line[j]);
                row.Add(value);
                if (value == 0)
                {
                    trailheads.Add((i, j));
                }
            }
        }

        var size = (map.Count, map[0].Count);

        var answer = 0;
        foreach (var trailhead in trailheads)
        {
            var explored = new HashSet<(int l, int c)>([trailhead]);
            var toExplore = new Queue<(int l, int c)>([trailhead]);

            while (toExplore.Count > 0)
            {
                var current = toExplore.Dequeue();
                var height = map[current.l][current.c];

                var neighbours = GenerateNeighbours(current);
                foreach (var neighbour in neighbours)
                {
                    if (IsOutside(neighbour, size))
                    {
                        continue;
                    }

                    if (explored.Contains(neighbour))
                    {
                        continue;
                    }

                    var neighbourHeight = map[neighbour.l][neighbour.c];
                    if (neighbourHeight != height + 1)
                    {
                        continue;
                    }

                    explored.Add(neighbour);
                    if (neighbourHeight == 9)
                    {
                        answer++;
                    }
                    else
                    {
                        toExplore.Enqueue(neighbour);
                    }
                }
            }
        }

        return answer.ToString();
    }

    public async Task<string> Part2(IInputReader reader, CancellationToken cancellation)
    {
        var map = new List<IList<int>>();
        var trailheads = new List<(int l, int c)>();

        for (int i = 0; reader.CanRead; i++)
        {
            var row = new List<int>();
            map.Add(row);
            var line = await reader.ReadLineAsync(cancellation);
            for (int j = 0; j < line.Length; j++)
            {
                var value = (int)char.GetNumericValue(line[j]);
                row.Add(value);
                if (value == 0)
                {
                    trailheads.Add((i, j));
                }
            }
        }

        var size = (map.Count, map[0].Count);

        var answer = 0;
        foreach (var trailhead in trailheads)
        {
            var toExplore = new Queue<(int l, int c)>([trailhead]);

            while (toExplore.Count > 0)
            {
                var current = toExplore.Dequeue();
                var height = map[current.l][current.c];

                var neighbours = GenerateNeighbours(current);
                foreach (var neighbour in neighbours)
                {
                    if (IsOutside(neighbour, size))
                    {
                        continue;
                    }

                    var neighbourHeight = map[neighbour.l][neighbour.c];
                    if (neighbourHeight != height + 1)
                    {
                        continue;
                    }

                    if (neighbourHeight == 9)
                    {
                        answer++;
                    }
                    else
                    {
                        toExplore.Enqueue(neighbour);
                    }
                }
            }
        }

        return answer.ToString();
    }

    private static bool IsOutside((int l, int c) position, (int l, int c) size)
    {
        if (position.l < 0 || position.c < 0)
        {
            return true;
        }

        return position.l >= size.l || position.c >= size.c;
    }

    private static IEnumerable<(int l, int c)> GenerateNeighbours((int l, int c) position)
    {
        yield return (l: position.l - 1, position.c);
        yield return (l: position.l + 1, position.c);
        yield return (position.l, c: position.c - 1);
        yield return (position.l, c: position.c + 1);
    }
}
