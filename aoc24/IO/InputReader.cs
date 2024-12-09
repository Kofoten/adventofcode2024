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
}