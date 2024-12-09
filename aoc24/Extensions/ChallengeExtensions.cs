namespace aoc24.Extensions;

public static class ChallengeExtensions
{
    public static async Task<string> PerformChallenge(this IChallenge challenge, InputReader reader, int part, CancellationToken cancellation) => part switch
    {
        1 => await challenge.Part1(reader, cancellation),
        2 => await challenge.Part2(reader, cancellation),
        _ => throw new PartDoesNotExistException(part),
    };
}
