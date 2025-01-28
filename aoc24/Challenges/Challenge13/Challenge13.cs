using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace aoc24.Challenges.Challenge13;

public partial class Challenge13 : IChallenge
{
    public async Task<string> Part1(IInputReader reader, CancellationToken cancellation)
    {
        var answer = 0;
        await foreach (var machine in ReadInput(reader, cancellation))
        {
            int? cost = null;
            var maxA = machine.Prize.X / machine.ButtonA.X;
            var maxB = machine.Prize.X / machine.ButtonB.X;

            for (int bPresses = maxB; bPresses > -1; bPresses--)
            {
                for (int aPresses = maxA; aPresses > -1; aPresses--)
                {
                    var ax = machine.ButtonA.X * aPresses;
                    var bx = machine.ButtonB.X * bPresses;
                    if (ax + bx != machine.Prize.X)
                    {
                        continue;
                    }

                    var ay = machine.ButtonA.Y * aPresses;
                    var by = machine.ButtonB.Y * bPresses;
                    if (ay + by != machine.Prize.Y)
                    {
                        continue;
                    }

                    var currentCost = aPresses * 3 + bPresses;
                    if (cost is null || currentCost < cost)
                    {
                        cost = currentCost;
                    }
                }
            }

            if (cost.HasValue)
            {
                answer += cost.Value;
            }
        }

        return answer.ToString();
    }

    public async Task<string> Part2(IInputReader reader, CancellationToken cancellation)
    {
        throw new PartNotImplementedException(2);
    }

    private static async IAsyncEnumerable<MachineConfig> ReadInput(IInputReader reader, [EnumeratorCancellation] CancellationToken cancellation)
    {
        var buttonA = Vector2.Zero;
        var buttonB = Vector2.Zero;
        var prize = Vector2.Zero;

        while (reader.CanRead)
        {
            var line = await reader.ReadLineAsync(cancellation);
            if (string.IsNullOrEmpty(line))
            {
                yield return new MachineConfig(buttonA, buttonB, prize);
                continue;
            }

            var settingMatch = MachineSettingRegex().Match(line);
            if (settingMatch.Success)
            {
                var x = int.Parse(settingMatch.Groups[3].Value);
                var y = int.Parse(settingMatch.Groups[5].Value);

                switch (settingMatch.Groups[1].Value)
                {
                    case "Prize":
                        prize = new(x, y);
                        break;
                    case "Button A":
                        buttonA = new(x, y);
                        break;
                    case "Button B":
                        buttonB = new(x, y);
                        break;
                    default:
                        break;
                }
            }
        }

        yield return new MachineConfig(buttonA, buttonB, prize);
    }

    private record MachineConfig(Vector2 ButtonA, Vector2 ButtonB, Vector2 Prize);

    private record Vector2(int X, int Y)
    {
        public static Vector2 Zero => new(0, 0);
    }

    [GeneratedRegex("""^(Prize|Button A|Button B): X(\+|=)(\d+), Y(\+|=)(\d+)$""")]
    private static partial Regex MachineSettingRegex();
}
