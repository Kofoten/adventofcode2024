namespace aoc24.Challenges.Challenge02;

public class Challenge02 : IChallenge
{
    public async Task<string> Part1(IInputReader reader, CancellationToken cancellation)
    {
        var answer = 0;
        await foreach (var item in reader.ReadAllLinesAsync(cancellation))
        {
            var parts = item.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            bool safe = true;
            long? decreasing = null;
            int previous = int.Parse(parts[0]);
            for (int i = 1; i < parts.Length; i++)
            {
                var current = int.Parse(parts[i]);
                var diff = current - previous;

                var absDiff = Math.Abs(diff);
                if (absDiff < 1 || absDiff > 3)
                {
                    safe = false;
                    break;
                }

                var currentlyDecreasing = diff & 0x80000000;
                if (decreasing is not null && decreasing != currentlyDecreasing)
                {
                    safe = false;
                    break;
                }

                decreasing = currentlyDecreasing;
                previous = current;
            }

            if (safe)
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
            //if (IsReportSafe(item))
            //{
            //    answer++;
            //}
        }

        return answer.ToString();
    }

    //private static bool IsReportSafe(string report)
    //{
    //    var parts = report.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse).ToList();

    //    var ignored = new HashSet<int>();
        
    //    for (int i = 1; i < parts.Count; i++)
    //    {
    //        var current = int.Parse(parts[i]);
    //        var diff = current - previous;

    //        var absDiff = Math.Abs(diff);
    //        if (absDiff < 1 || absDiff > 3)
    //        {
    //            violations.Add(i);
    //        }

    //        if ((diff & 0x80000000) == 0)
    //        {
    //            increases.Add(i);
    //        }
    //        else
    //        {
    //            decreases.Add(i);
    //        }

    //        previous = current;
    //    }
    //}
}
