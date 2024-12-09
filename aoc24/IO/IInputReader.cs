namespace aoc24.IO;

public interface IInputReader : IDisposable
{
    bool CanRead { get; }
    ValueTask<string> ReadLineAsync(CancellationToken cancellation);
    IAsyncEnumerable<string> ReadAllLinesAsync(CancellationToken cancellation);
    ValueTask<string> ReadToEndAsync(CancellationToken cancellation);

    ValueTask<T> ReadLineAsync<T>(Func<string, T> converter, CancellationToken cancellation);
    IAsyncEnumerable<T> ReadAllLinesAsync<T>(Func<string, T> converter, CancellationToken cancellation);
}
