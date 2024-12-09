namespace aoc24.Challenges.Challenge04;

public class Challenge04 : IChallenge
{
    public async Task<string> Part1(IInputReader reader, CancellationToken cancellation)
    {
        var xes = new List<(int line, int index)>();
        var data = new List<string>();
        await foreach (var line in reader.ReadAllLinesAsync(cancellation))
        {
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == 'X')
                {
                    xes.Add((data.Count, i));
                }
            }

            data.Add(line);
        }

        var answer = 0;
        foreach (var x in xes)
        {
            var directions = new List<(int line, int index)>()
            {
                (-1, -1), (-1, 0), (-1, 1),
                (0, -1),           (0, 1),
                (1, -1),  (1, 0),  (1, 1),
            };

            foreach (var direction in directions)
            {
                string word = "X";
                for (int i = 1;  i < 4; i++)
                {
                    var offset = MultiplyPos(direction, i);
                    var pos = AddPos(x, offset);
                    if (pos.line < 0 || pos.line >= data.Count)
                    {
                        break;
                    }

                    var line = data[pos.line];
                    if (pos.index < 0 || pos.index >= line.Length)
                    {
                        break;
                    }

                    word += line[pos.index];
                }

                if (word == "XMAS")
                {
                    answer++;
                }
            }
        }

        return answer.ToString();
    }

    public async Task<string> Part2(IInputReader reader, CancellationToken cancellation)
    {
        var potentialCrossMases = new List<(int line, int index)>();
        var data = new List<string>();
        await foreach (var line in reader.ReadAllLinesAsync(cancellation))
        {
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == 'A')
                {
                    potentialCrossMases.Add((data.Count, i));
                }
            }

            data.Add(line);
        }

        var answer = 0;
        foreach (var potentialCrossMas in potentialCrossMases)
        {
            if (IsOnEdge(potentialCrossMas, data))
            {
                continue;
            }

            var tl = data[potentialCrossMas.line - 1][potentialCrossMas.index - 1];
            var br = data[potentialCrossMas.line + 1][potentialCrossMas.index + 1];

            if (!(tl == 'S' && br == 'M') && !(tl == 'M' && br == 'S'))
            {
                continue;
            }

            var tr = data[potentialCrossMas.line - 1][potentialCrossMas.index + 1];
            var bl = data[potentialCrossMas.line + 1][potentialCrossMas.index - 1];
            
            if (!(tr == 'S' && bl == 'M') && !(tr == 'M' && bl == 'S'))
            {
                continue;
            }

            answer++;
        }

        return answer.ToString();
    }

    private static (int line, int index) MultiplyPos((int line, int index) pos, int multiplier) => (pos.line * multiplier, pos.index * multiplier);
    private static (int line, int index) AddPos((int line, int index) left, (int line, int index) right) => (left.line + right.line, left.index + right.index);
    private static bool IsOnEdge((int line, int index) pos, List<string> data)
    {
        if (pos.line == 0 || pos.line == data.Count - 1)
        {
            return true;
        }

        if (pos.index == 0 || pos.index == data[pos.line].Length - 1)
        {
            return true;
        }

        return false;
    }
}