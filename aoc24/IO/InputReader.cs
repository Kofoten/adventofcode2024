namespace aoc24.IO;

public class InputReader(Stream stream) : IInputReader
{
    private readonly StreamReader reader = new(stream);

    public bool CanRead => !reader.EndOfStream;

    public async ValueTask<string> ReadLineAsync()
    {
        return await reader.ReadLineAsync() ?? throw new InvalidDataException("Input is invalid");
    }

    public async IAsyncEnumerable<string> ReadAllLinesAsync()
    {
        while (!reader.EndOfStream)
        {
            yield return await ReadLineAsync();
        }
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        reader.Dispose();
    }

    public async ValueTask<T> ReadLineAsync<T>(Func<string, T> converter)
    {
        var line = await ReadLineAsync();
        return converter.Invoke(line);
    }

    public async IAsyncEnumerable<T> ReadAllLinesAsync<T>(Func<string, T> converter)
    {
        while (!reader.EndOfStream)
        {
            yield return await ReadLineAsync(converter);
        }
    }
}