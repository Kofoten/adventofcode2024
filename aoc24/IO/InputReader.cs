using System.Runtime.CompilerServices;

namespace aoc24.IO;

public class InputReader(Stream stream) : IInputReader
{
    private readonly StreamReader reader = new(stream);

    public bool CanRead => !reader.EndOfStream;

    public async ValueTask<string> ReadLineAsync(CancellationToken cancellation)
    {
        return await reader.ReadLineAsync(cancellation) ?? throw new InvalidDataException("Input is invalid");
    }

    public async IAsyncEnumerable<string> ReadAllLinesAsync([EnumeratorCancellation] CancellationToken cancellation)
    {
        while (!reader.EndOfStream)
        {
            yield return await ReadLineAsync(cancellation);
        }
    }

    public async ValueTask<string> ReadToEndAsync(CancellationToken cancellation)
    {
        return await reader.ReadToEndAsync(cancellation);
    }

    public async ValueTask<T> ReadLineAsync<T>(Func<string, T> converter, CancellationToken cancellation)
    {
        var line = await ReadLineAsync(cancellation);
        return converter.Invoke(line);
    }

    public async IAsyncEnumerable<T> ReadAllLinesAsync<T>(Func<string, T> converter, [EnumeratorCancellation] CancellationToken cancellation)
    {
        while (!reader.EndOfStream)
        {
            yield return await ReadLineAsync(converter, cancellation);
        }
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        reader.Dispose();
    }
}