namespace aoc24.IO;

public interface IInputReader : IDisposable
{
    bool CanRead { get; }
    ValueTask<string> ReadLineAsync();
    IAsyncEnumerable<string> ReadAllLinesAsync();
}
