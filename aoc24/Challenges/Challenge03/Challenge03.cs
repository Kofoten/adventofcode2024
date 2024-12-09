using System.Text.RegularExpressions;

namespace aoc24.Challenges.Challenge03;

public partial class Challenge03 : IChallenge
{
    public async Task<string> Part1(IInputReader reader, CancellationToken cancellation)
    {
        var answer = 0;
        var corruptedMemory = await reader.ReadToEndAsync(cancellation);
        var instructions = InstructionRegex().Matches(corruptedMemory);
        foreach (var instruction in instructions.OfType<Match>())
        {
            if (!instruction.Value.StartsWith("mul"))
            {
                continue;
            }

            var left = int.Parse(instruction.Groups[1].Value);
            var right = int.Parse(instruction.Groups[2].Value);
            answer += left * right;
        }

        return answer.ToString();
    }

    public async Task<string> Part2(IInputReader reader, CancellationToken cancellation)
    {
        var answer = 0;
        var corruptedMemory = await reader.ReadToEndAsync(cancellation);
        var instructions = InstructionRegex().Matches(corruptedMemory);
        var mulEnabled = true;
        foreach (var instruction in instructions.OfType<Match>())
        {
            switch (instruction.Value)
            {
                case "do()":
                    mulEnabled = true;
                    break;
                case "don't()":
                    mulEnabled = false;
                    break;
                default:
                    if (mulEnabled)
                    {
                        var left = int.Parse(instruction.Groups[1].Value);
                        var right = int.Parse(instruction.Groups[2].Value);
                        answer += left * right;
                    }
                    break;
            }

        }

        return answer.ToString();
    }

    [GeneratedRegex("mul\\((\\d{1,3}),(\\d{1,3})\\)|don't\\(\\)|do\\(\\)")]
    private static partial Regex InstructionRegex();
}
