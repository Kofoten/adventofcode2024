using System.Text;

namespace aoc24.Challenges.Challenge06;

public class Challenge06 : IChallenge
{
    public async Task<string> Part1(IInputReader reader, CancellationToken cancellation)
    {
        var direction = (-1, 0);
        (var size, var position, var obstacles) = await ReadInput(reader, cancellation);
        var visited = new HashSet<(int, int)> { position };

        while (true)
        {
            var next = MoveForward(position, direction);
            if (IsOutside(next, size))
            {
                break;
            }

            if (obstacles.Contains(next))
            {
                direction = TurnRight(direction);
            }
            else
            {
                visited.Add(next);
                position = next;
            }
        }

        return visited.Count.ToString();
    }

    public async Task<string> Part2(IInputReader reader, CancellationToken cancellation)
    {
        // Answer is close to 1589;
        var direction = (-1, 0);
        (var size, var position, var obstacles) = await ReadInput(reader, cancellation);
        var history = new HashSet<(int, int, int, int)>();
        var path = new HashSet<(int, int)>();

        var answer = 0;
        while (true)
        {
            path.Add(position);
            var next = MoveForward(position, direction);
            if (IsOutside(next, size))
            {
                break;
            }

            if (obstacles.Contains(next))
            {
                direction = TurnRight(direction);
                history.Add((position.Item1, position.Item2, direction.Item1, direction.Item2));
            }
            else
            {
                if (!path.Contains(next))
                {
                    var checkDirection = TurnRight(direction);
                    if (IsLoop(position, checkDirection, size, next, obstacles, history, path))
                    {
                        answer++;
                    }
                }

                position = next;
            }
        }

        return answer.ToString();
    }

    private static bool IsLoop((int, int) position, (int, int) direction, (int, int) size, (int, int) addedObstacle, HashSet<(int, int)> obstacles, HashSet<(int, int, int, int)> history, HashSet<(int, int)> previousPath)
    {
        var visited = new HashSet<(int, int)>();
        var start = (position.Item1, position.Item2, direction.Item1, direction.Item2);
        var innerHistory = new HashSet<(int, int, int, int)>();
        bool? result = null;
        while (result is null)
        {
            visited.Add(position);
            var nextDirection = TurnRight(direction);
            var current = (position.Item1, position.Item2, nextDirection.Item1, nextDirection.Item2);
            if (start == current || history.Contains(current))
            {
                result = true;
                continue;
            }

            if (innerHistory.Contains(current))
            {
                result = false;
                continue;
            }

            var next = MoveForward(position, direction);
            if (IsOutside(next, size))
            {
                result = false;
                continue;
            }

            if (next == addedObstacle || obstacles.Contains(next))
            {
                direction = nextDirection;
                innerHistory.Add(current);
            }
            else
            {
                position = next;
            }
        }

        Print(size, (start.Item1, start.Item2), position, addedObstacle, obstacles, visited, previousPath);
        return result.Value;
    }

    private static (int, int) MoveForward((int, int) position, (int, int) direction) => (position.Item1 + direction.Item1, position.Item2 + direction.Item2);

    private static (int, int) TurnRight((int, int) direction) => direction switch
    {
        (-1, 0) => (0, 1),
        (0, 1) => (1, 0),
        (1, 0) => (0, -1),
        (0, -1) => (-1, 0),
        _ => throw new InvalidOperationException()
    };

    private static bool IsOutside((int, int) position, (int, int) size)
    {
        if (position.Item1 < 0 || position.Item2 < 0)
        {
            return true;
        }

        return position.Item1 >= size.Item1 || position.Item2 >= size.Item2;
    }

    private static async Task<((int, int), (int, int), HashSet<(int, int)>)> ReadInput(IInputReader reader, CancellationToken cancellation)
    {
        var start = (0, 0);
        var obstacles = new HashSet<(int, int)>();
        var linesRead = 0;
        var lineLength = 0;
        await foreach (var line in reader.ReadAllLinesAsync(cancellation))
        {
            for (int i = 0; i < line.Length; i++)
            {
                switch (line[i])
                {
                    case '#':
                        obstacles.Add((linesRead, i));
                        break;
                    case '^':
                        start = (linesRead, i);
                        break;
                    default:
                        break;
                }
            }

            lineLength = line.Length;
            linesRead++;
        }

        return ((linesRead, lineLength), start, obstacles);
    }

    private static void Print((int, int) size, (int, int) start, (int, int) position, (int, int) addedObstacle, HashSet<(int, int)> obstacles, HashSet<(int, int)> visited, HashSet<(int, int)> path)
    {
        var sb = new StringBuilder();

        for (int i = 0; i < size.Item1; i++)
        {
            for (int j = 0; j < size.Item2; j++)
            {
                var current = (i, j);
                if (current == start)
                {
                    sb.Append('S');
                }
                else if (current == position)
                {
                    sb.Append('X');
                }
                else if (current == addedObstacle)
                {
                    sb.Append('O');
                }
                else if (obstacles.Contains(current))
                {
                    sb.Append('#');
                }
                else if (path.Contains(current))
                {
                    sb.Append('¤');
                }
                else if (visited.Contains(current))
                {
                    sb.Append('+');
                }
                else
                {
                    sb.Append(' ');
                }
            }
            sb.AppendLine();
        }

        Console.Clear();
        Console.Write(sb.ToString());
    }
}
