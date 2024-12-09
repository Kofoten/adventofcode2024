namespace aoc24.Challenges;

public interface IChallenge
{
    Task<string> Part1(IInputReader reader, CancellationToken cancellation);
    Task<string> Part2(IInputReader reader, CancellationToken cancellation);
}
