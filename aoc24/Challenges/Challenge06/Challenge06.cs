using System.ComponentModel;
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
        var endAtEdge = new HashSet<(int, int, int, int)>();

        var answer = 0;
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
                history.Add((position.Item1, position.Item2, direction.Item1, direction.Item2));
            }
            else
            {
                if (IsLoop(position, TurnRight(direction), size, obstacles, history, endAtEdge))
                {
                    answer++;
                }

                position = next;
            }
        }

        return answer.ToString();
    }
    
    private static bool IsLoop((int, int) position, (int, int) direction, (int, int) size, HashSet<(int, int)> obstacles, HashSet<(int, int, int, int)> history, HashSet<(int, int, int, int)> endAtEdge)
    {
        var start = (position.Item1, position.Item2, direction.Item1, direction.Item2);
        var innerHistory = new HashSet<(int, int, int, int)>();
        while (true)
        {
            var nextDirection = TurnRight(direction);
            var current = (position.Item1, position.Item2, nextDirection.Item1, nextDirection.Item2);
            if (start == current || history.Contains(current))
            {
                return true;
            }
            
            if (innerHistory.Contains(current))
            {
                return false;
            }

            var next = MoveForward(position, direction);
            if (IsOutside(next, size)/* || endAtEdge.Contains(current)*/)
            {
                endAtEdge.UnionWith(innerHistory);
                return false;
            }

            if (obstacles.Contains(next))
            {
                direction = nextDirection;
                innerHistory.Add(current);
            }
            else
            {
                position = next;
            }

        }
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

    private static void Print((int, int) size, (int, int) position, HashSet<(int, int)> obstacles, HashSet<(int, int, int, int)> history, HashSet<(int, int, int, int)> inner)
    {
        var sb = new StringBuilder();
        
        for (int i = 0; i < size.Item1; i++)
        {
            for (int j = 0; j < size.Item2; j++)
            {
                if ((i, j) == position)
                {
                    sb.Append('X');
                }
                else if (obstacles.Contains((i, j)))
                {
                    sb.Append('#');
                }
                else if (history.Any(x => x.Item1 == i && x.Item2 == j))
                {
                    sb.Append('+');
                }
                else if (inner.Any(x => x.Item1 == i && x.Item2 == j))
                {
                    sb.Append('¤');
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
