namespace aoc24.Options;

public interface IChallengeSpecificOptions : IOptions
{
    static abstract bool TryParse(int challenge, int part, FileInfo fileInfo, string[] specificArgs, [NotNullWhen(true)] out IOptions? options, [NotNullWhen(false)] out string? reason);
}