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
        for (int i = disc.Count - 1; i > 0; i--)
        {
            if (disc[i] is null)
            {
                continue;
            }

            fileEnd ??= i;
            var current = disc[i]!.Value;
            if (disc[i - 1] != current)
            {
                var fileSize = 1 + fileEnd - i;
                int? emptyBlockStart = null;
                for (int j = 0; j < i; j++)
                {
                    if (emptyBlockStart is null)
                    {
                        if (disc[j] is null)
                        {
                            emptyBlockStart = j;
                        }
                    }
                    else if (j - emptyBlockStart == fileSize)
                    {
                        for (int k = 0; k < fileSize; k++)
                        {
                            disc[k + emptyBlockStart.Value] = current;
                            disc[k + i] = null;
                        }
                        break;
                    }
                    else if (disc[j] is not null)
                    {
                        emptyBlockStart = null;
                    }
                }

                fileEnd = null;
            }

            if (options.Verbose)
            {
                Print(disc);
            }
        }
       
        var answer = 0L;
        for (int i = 0; i < disc.Count; i++)
        {
            if (disc[i].HasValue)
            {
                answer += (long)disc[i]!.Value * i;
            }
        }

        return answer.ToString();
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
}
