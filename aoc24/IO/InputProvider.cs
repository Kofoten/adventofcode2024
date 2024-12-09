using System.Reflection;

namespace aoc24.IO;

public class InputProvider()
{
    public const int Year = 2024;

    public static async Task<Stream> GetInput(int day, bool useExampleInput, string? sessionCookie, CancellationToken cancellation)
    {
        var filePath = GetInputFilePath(day, useExampleInput);
        if (useExampleInput || File.Exists(filePath))
        {
            return new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        }

        var client = new AocClient(Year);

        var inputStream = await client.GetInput(day, sessionCookie, cancellation);
        using (var cacheStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Read))
        {
            inputStream.CopyTo(cacheStream);
        }

        inputStream.Seek(0, SeekOrigin.Begin);
        return inputStream;
    }

    private static string GetInputFilePath(int day, bool useExampleInput)
    {
        string fileName = $"day{day:D2}.txt";
        if (useExampleInput)
        {
            var assembly = Assembly.GetAssembly(typeof(InputProvider)) ?? throw new InvalidOperationException($"Could not find assembly");
            var location = Path.GetDirectoryName(assembly.Location) ?? throw new NullReferenceException("Could not get assembly directory");
            return Path.Combine(location, "example-input", fileName);
        }
        else
        {
            var cacheDirectory = Utilities.GetAocCachePath(Year);
            return Path.Combine(cacheDirectory, "input", fileName);
        }
    }
}
