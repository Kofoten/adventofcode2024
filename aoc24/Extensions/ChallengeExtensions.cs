namespace aoc24.Extensions;

public static class ChallengeExtensions
{
    public static async Task<string> PerformChallenge(this IChallenge challenge, FileInputReader reader, int part) => part switch
    {
        1 => await challenge.Part1(reader),
        2 => await challenge.Part2(reader),
        _ => throw new PartDoesNotExistException(part),
    };
}
