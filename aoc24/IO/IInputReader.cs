namespace aoc24.IO;

public interface IInputReader : IDisposable
{
    bool CanRead { get; }
    ValueTask<string> ReadLineAsync();
    IAsyncEnumerable<string> ReadAllLinesAsync();

    ValueTask<T> ReadLineAsync<T>(Func<string, T> converter);
    IAsyncEnumerable<T> ReadAllLinesAsync<T>(Func<string, T> converter);
}
