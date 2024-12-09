namespace aoc24.Challenges.Challenge01;

public class Challenge01 : IChallenge
{
    public async Task<string> Part1(IInputReader reader, CancellationToken cancellation)
    {
        var left = new List<int>();
        var right = new List<int>();

        await foreach (var item in reader.ReadAllLinesAsync(cancellation))
        {
            var parts = item.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var l = int.Parse(parts[0]);
            var r = int.Parse(parts[1]);

            var li = left.FindIndex(x => x > l);
            if (li < 0)
            {
                left.Add(l);
            }
            else
            {
                left.Insert(li, l);
            }

            var ri = right.FindIndex(x => x > r);
            if (ri < 0)
            {
                right.Add(r);
            }
            else
            {
                right.Insert(ri, r);
            }
        }

        var answer = 0;
        for (int i = 0; i < left.Count; i++)
        {
            answer += Math.Abs(left[i] - right[i]);
        }

        return answer.ToString();
    }

    public async Task<string> Part2(IInputReader reader, CancellationToken cancellation)
    {
        var left = new List<int>();
        var right = new List<int>();

        await foreach (var item in reader.ReadAllLinesAsync(cancellation))
        {
            var parts = item.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            left.Add(int.Parse(parts[0]));
            right.Add(int.Parse(parts[1]));
        }

        var answer = 0;
        for (int i = 0; i < left.Count; i++)
        {
            var countInRight = right.FindAll(x => x == left[i]).Count;
            answer += left[i] * countInRight;
        }
        return answer.ToString();
    }
}
