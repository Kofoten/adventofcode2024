using System.Text;

namespace aoc24.Challenges.Challenge09;

public class Challenge09(IOptions options) : IChallenge
{
    public async Task<string> Part1(IInputReader reader, CancellationToken cancellation)
    {
        var discMap = await reader.ReadToEndAsync(cancellation);
        var disc = new List<int?>();

        var fileId = 0;
        var lastIsFile = false;
        foreach (var b in discMap)
        {
            var value = char.GetNumericValue(b);
            for (int i = 0; i < value; i++)
            {
                if (lastIsFile)
                {
                    disc.Add(null);
                }
                else
                {
                    disc.Add(fileId);
                }
            }

            lastIsFile = !lastIsFile;
            fileId += Convert.ToInt32(lastIsFile);
        }

        if (options.Verbose)
        {
            Print(disc);
        }

        var last = disc.Count - 1;
        for (int i = 0; i < last; i++)
        {
            if (disc[i] is null)
            {
                while (disc[last] is null)
                {
                    last--;
                }

                disc[i] = disc[last];
                disc[last] = null;

                if (options.Verbose)
                {
                    Print(disc);
                }
            }
        }

        var answer = 0L;
        for (int i = 0; disc[i] is not null; i++)
        {
            answer += (long)disc[i]!.Value * i;
        }

        return answer.ToString();
    }

    public async Task<string> Part2(IInputReader reader, CancellationToken cancellation)
    {
        var discMap = await reader.ReadToEndAsync(cancellation);
        var disc = new List<(int, int, long)>();

        var index = 0;
        var fileId = 0L;
        var isFile = true;
        foreach (var b in discMap)
        {
            var value = (int)char.GetNumericValue(b);
            if (isFile)
            {
                disc.Add((index, value, fileId));
                fileId++;
            }

            isFile = !isFile;
            index += value;
        }

        if (options.Verbose)
        {
            Print(disc, index);
        }

        for (int i = disc.Count - 1; i > 0; i--)
        {
            var start = 0;
            var current = disc[i];

            while (start < disc[i].Item1)
            {
                var move = (start, current.Item2, current.Item3);
                var intersecting = disc.FindIndex(x => Intersects(x, move));
                if (intersecting == -1)
                {
                    disc[i] = move;
                    break;
                }

                start = disc[intersecting].Item1 + disc[intersecting].Item2;
            }

            if (options.Verbose)
            {
                Print(disc, index);
            }
        }

        var answer = 0L;
        foreach (var file in disc)
        {
            for (int i = 0; i < file.Item2; i++)
            {
                var position = file.Item1 + i;
                answer += file.Item3 * position;
            }
        }

        return answer.ToString();
    }

    public async Task<string> Part2Retry(IInputReader reader, CancellationToken cancellation)
    {
        var discMap = await reader.ReadToEndAsync(cancellation);
        var disc = new List<int?>();

        var fileId = 0;
        var lastIsFile = false;
        foreach (var b in discMap)
        {
            var value = char.GetNumericValue(b);
            for (int i = 0; i < value; i++)
            {
                if (lastIsFile)
                {
                    disc.Add(null);
                }
                else
                {
                    disc.Add(fileId);
                }
            }

            lastIsFile = !lastIsFile;
            fileId += Convert.ToInt32(lastIsFile);
        }

        if (options.Verbose)
        {
            Print(disc);
        }

        int? fileEnd = null;
        for (int i = disc.Count; i > 0; --i)
        {
            if (disc[i] is null)
            {
                continue;
            }

            if (fileEnd is null)
            {
                fileEnd = i;
            }


            while (disc[last] is null)
            {
                last--;
            }

            disc[i] = disc[last];
            disc[last] = null;

            if (options.Verbose)
            {
                Print(disc);
            }
        }
    }

    var answer = 0L;
        for (int i = 0; disc[i] is not null; i++)
        {
            answer += (long) disc[i]!.Value* i;
}

        return answer.ToString();
    }

    private static bool Intersects((int, int, long) first, (int, int, long) second)
{
    var (firstStart, firstLength, _) = first;
    var (secondStart, secondLength, _) = second;

    if (firstStart == secondStart)
    {
        return true;
    }
    else if (firstStart < secondStart)
    {
        return firstStart + firstLength > secondStart;
    }
    else
    {
        return firstStart < secondStart + secondLength;
    }
}

private static void Print(IList<int?> disc)
{
    var sb = new StringBuilder();

    for (int i = 0; i < disc.Count; i++)
    {
        if (disc[i] is null)
        {
            sb.Append('.');
        }
        else
        {
            sb.Append(disc[i]);
        }
    }

    Console.WriteLine(sb.ToString());
}

private static void Print(IList<(int, int, long)> disc, int length)
{
    var sb = new StringBuilder();

    for (int i = 0; i < length;)
    {
        var found = disc.Where(x => x.Item1 == i);
        if (found.Any())
        {
            var file = found.First();
            sb.Append(Enumerable.Repeat(file.Item3.ToString(), file.Item2).Aggregate(string.Empty, (acc, x) => acc + x));
            i += file.Item2;
        }
        else
        {
            sb.Append('.');
            i++;
        }
    }

    Console.WriteLine(sb.ToString());
}
}
