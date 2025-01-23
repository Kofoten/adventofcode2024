namespace aoc24.Challenges.Challenge12;

public class Challenge12 : IChallenge
{
    public async Task<string> Part1(IInputReader reader, CancellationToken cancellation)
    {
        var visited = new HashSet<(int l, int c)>();
        var map = await reader.ReadAllLinesAsync(cancellation).ToListAsync(cancellation);

        var answer = 0;
        for (int i = 0; i < map.Count; i++)
        {
            for (int j = 0; j < map[i].Length; j++)
            {
                if (visited.Contains((i, j)))
                {
                    continue;
                }

                var (region, perimiter) = ExploreRegion(map, map[i][j], i, j);
                answer += region.Count * perimiter;
                visited.UnionWith(region);
            }
        }

        return answer.ToString();
    }

    public async Task<string> Part2(IInputReader reader, CancellationToken cancellation)
    {
        throw new PartNotImplementedException(2);
    }

    private static (HashSet<(int l, int c)> region, int perimiter) ExploreRegion(List<string> map, char key, int l, int c)
    {
        var region = new HashSet<(int l, int c)>();
        var perimiter = new List<(int l, int c)>();

        var toCheck = new Queue<(int l, int c)>([(l, c)]);

        while (toCheck.TryDequeue(out var pos))
        {
            if (region.Add(pos))
            {
                var neigbours = GetNeighbours(map, key, pos.l, pos.c);
                foreach (var neigbour in neigbours)
                {
                    if (neigbour.isSame)
                    {
                        toCheck.Enqueue(neigbour.pos);
                    }
                    else
                    {
                        perimiter.Add(neigbour.pos);
                    }
                }
            }
        }

        return (region, perimiter.Count);
    }

    private static IEnumerable<((int l, int c) pos, bool isSame)> GetNeighbours(List<string> map, char key, int l, int c)
    {
        if (l > 0)
        {
            yield return ((l - 1, c), map[l - 1][c] == key);
        }
        else
        {
            yield return ((l - 1, c), false);
        }

        if (l < map.Count - 1)
        {
            yield return ((l + 1, c), map[l + 1][c] == key);
        }
        else
        {
            yield return ((l + 1, c), false);
        }

        if (c > 0)
        {
            yield return ((l, c - 1), map[l][c - 1] == key);
        }
        else
        {
            yield return ((l, c - 1), false);
        }

        if (c < map[l].Length - 1)
        {
            yield return ((l, c + 1), map[l][c + 1] == key);
        }
        else
        {
            yield return ((l, c + 1), false);
        }
    }
}
