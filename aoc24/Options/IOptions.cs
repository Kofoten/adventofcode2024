namespace aoc24.Options;

public interface IOptions
{
    int Challenge { get; }
    int Part { get; }
    FileInfo InputFile { get; }
}
