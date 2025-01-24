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
        var map = await reader.ReadAllLinesAsync(cancellation).ToListAsync(cancellation);
        var regions = GetRegions(map);

        var answer = 0;
        foreach (var region in regions)
        {
            var northFacingSides = region.Where(n => !region.Contains((n.l - 1, n.c))).GroupBy(n => n.l).SelectMany(g => SplitFakeSides(g, n => n.c)).Count();
            var southFacingSides = region.Where(n => !region.Contains((n.l + 1, n.c))).GroupBy(n => n.l).SelectMany(g => SplitFakeSides(g, n => n.c)).Count();
            var westFacingSides = region.Where(n => !region.Contains((n.l, n.c - 1))).GroupBy(n => n.c).SelectMany(g => SplitFakeSides(g, n => n.l)).Count();
            var eastFacingSides = region.Where(n => !region.Contains((n.l, n.c + 1))).GroupBy(n => n.c).SelectMany(g => SplitFakeSides(g, n => n.l)).Count();

            var totalSides = northFacingSides + southFacingSides + westFacingSides + eastFacingSides;
            answer += region.Count * totalSides;
        }

        return answer.ToString();
    }

    private static IEnumerable<IEnumerable<(int l, int c)>> SplitFakeSides(IGrouping<int, (int l, int c)> fakeSide, Func<(int l, int c), int> sortKey)
    {
        var orderedNodes = fakeSide.OrderBy(sortKey);
        List<(int l, int c)> side = [orderedNodes.First()];
        int prevKey = sortKey.Invoke(orderedNodes.First());
        foreach (var node in orderedNodes.Skip(1))
        {
            var key = sortKey.Invoke(node);
            if (key - 1 > prevKey)
            {
                yield return side;
                side = [];
            }

            prevKey = key;
            side.Add(node);
        }

        yield return side;
    }

    private static IEnumerable<HashSet<(int l, int c)>> GetRegions(List<string> map)
    {
        var visited = new HashSet<(int l, int c)>();
        for (int i = 0; i < map.Count; i++)
        {
            for (int j = 0; j < map[i].Length; j++)
            {
                if (visited.Contains((i, j)))
                {
                    continue;
                }

                var (region, _) = ExploreRegion(map, map[i][j], i, j);
                visited.UnionWith(region);
                yield return region;
            }
        }
    }

    private static (HashSet<(int l, int c)> region, int perimiter) ExploreRegion(List<string> map, char key, int l, int c)
    {
        var region = new HashSet<(int l, int c)>();
        var perimiter = 0;

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
                        perimiter++;
                    }
                }
            }
        }

        return (region, perimiter);
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
