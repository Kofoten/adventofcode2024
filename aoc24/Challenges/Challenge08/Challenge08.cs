using System.Text;

namespace aoc24.Challenges.Challenge08;

public class Challenge08(IOptions options) : IChallenge
{
    public async Task<string> Part1(IInputReader reader, CancellationToken cancellation)
    {
        (var size, var antennas) = await ReadInput(reader, cancellation);
        var antinodes = new HashSet<(int, int)>();
        foreach (var (type, locations) in antennas)
        {
            for (int i = 0; i < locations.Count; i++)
            {
                for (int j = 1 + i; j < locations.Count; j++)
                {
                    var first = locations[i];
                    var second = locations[j];

                    var diff = (first.Item1 - second.Item1, first.Item2 - second.Item2);

                    var antinode1 = (first.Item1 + diff.Item1, first.Item2 + diff.Item2);
                    var antinode2 = (second.Item1 - diff.Item1, second.Item2 - diff.Item2);

                    antinodes.Add(antinode1);
                    antinodes.Add(antinode2);

                    if (options.Verbose)
                    {
                        Print(type, size, first, second, [antinode1, antinode2]);
                    }
                }
            }
        }

        var answer = 0;
        foreach (var antinode in antinodes)
        {
            if (antinode.Item1 < 0 || antinode.Item2 < 0)
            {
                continue;
            }

            if (antinode.Item1 >= size.Item1 || antinode.Item2 >= size.Item2)
            {
                continue;
            }

            answer++;
        }

        return answer.ToString();
    }

    public async Task<string> Part2(IInputReader reader, CancellationToken cancellation)
    {
        (var size, var antennas) = await ReadInput(reader, cancellation);
        var antinodes = new HashSet<(int, int)>();
        foreach (var (type, locations) in antennas)
        {
            for (int i = 0; i < locations.Count; i++)
            {
                for (int j = 1 + i; j < locations.Count; j++)
                {
                    var first = locations[i];
                    var second = locations[j];

                    var diff = (first.Item1 - second.Item1, first.Item2 - second.Item2);

                    for (int k = 0; ; k++)
                    {
                        var antinode = (first.Item1 + (k * diff.Item1), first.Item2 + (k * diff.Item2));
                        if (IsOutside(antinode, size))
                        {
                            break;
                        }

                        antinodes.Add(antinode);
                    }

                    for (int k = 0; ; k++)
                    {
                        var antinode = (first.Item1 - (k * diff.Item1), first.Item2 - (k * diff.Item2));
                        if (IsOutside(antinode, size))
                        {
                            break;
                        }

                        antinodes.Add(antinode);
                    }
                }
            }
        }

        if (options.Verbose)
        {
            Print('!', size, (-1, -1), (-1, -1), antinodes);
        }

        return antinodes.Count.ToString();
    }

    private static bool IsOutside((int, int) position, (int, int) size)
    {
        if (position.Item1 < 0 || position.Item2 < 0)
        {
            return true;
        }

        return position.Item1 >= size.Item1 || position.Item2 >= size.Item2;
    }

    private static async Task<((int, int), Dictionary<char, List<(int, int)>>)> ReadInput(IInputReader reader, CancellationToken cancellation)
    {
        var size = (0, 0);
        var antennas = new Dictionary<char, List<(int, int)>>();
        await foreach (var line in reader.ReadAllLinesAsync(cancellation))
        {
            for (var i = 0; i < line.Length; i++)
            {
                if (line[i] == '.')
                {
                    continue;
                }

                if (antennas.TryGetValue(line[i], out var locations))
                {
                    locations.Add((size.Item1, i));
                }
                else
                {
                    antennas.Add(line[i], [(size.Item1, i)]);
                }
            }

            size.Item2 = line.Length;
            size.Item1++;
        }

        return (size, antennas);
    }

    private static void Print(char type, (int, int) size, (int, int) first, (int, int) second, IEnumerable<(int, int)> antinodes)
    {
        var sb = new StringBuilder();

        for (int i = 0; i < size.Item1; i++)
        {
            for (int j = 0; j < size.Item2; j++)
            {
                var current = (i, j);
                if (antinodes.Any(a => a == current))
                {
                    sb.Append("#");
                }
                else if (current == first || current == second)
                {
                    sb.Append(type);
                }
                else
                {
                    sb.Append('.');
                }
            }
            sb.AppendLine();
        }

        Console.Clear();
        Console.Write(sb.ToString());
    }
}
