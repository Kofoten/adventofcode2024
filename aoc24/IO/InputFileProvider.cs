using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace aoc24.IO;

public class InputFileProvider(DirectoryInfo inputDirectory)
{
    private readonly DirectoryInfo inputDirectory = inputDirectory;

    public FileInfo GetInputFile(int challegeNumber, bool useTestData)
    {
        if (TryGetInputFile(challegeNumber, useTestData, out var file))
        {
            return file;
        }

        throw new IOException($"Could not find any file for challenge {challegeNumber}");
    }

    public bool TryGetInputFile(int challegeNumber, bool useTestData, [NotNullWhen(true)] out FileInfo? file)
    {
        var fileName = useTestData ? "test.txt" : "input.txt";
        var folder = $"challenge{challegeNumber:D2}";
        var path = Path.Combine(inputDirectory.FullName, folder, fileName);

        if (Path.Exists(path))
        {
            file = new FileInfo(path);
            return true;
        }

        file = null;
        return false;
    }

    public static InputFileProvider Create()
    {
        var assembly = Assembly.GetAssembly(typeof(InputFileProvider)) ?? throw new InvalidOperationException($"Could not find assembly");
        var location = Path.GetDirectoryName(assembly.Location) ?? throw new NullReferenceException("Could not get assembly directory");
        var directory = new DirectoryInfo(Path.Combine(location, "input"));
        if (directory.Exists)
        {
            return new InputFileProvider(directory);
        }

        throw new IOException($"Input directory not found");
    }
}
