namespace aoc24.Options;

public interface IOptions
{
    int Challenge { get; }
    int Part { get; }
    bool UseExampleInput { get; }
    string? SessionCookie { get; }
}
