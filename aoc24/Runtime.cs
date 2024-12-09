using System.Diagnostics;
namespace aoc24;

public class Runtime(IOptions options)
{
    public readonly IOptions options = options;

    public async Task<Result> Run()
    {
        if (!options.InputFile.Exists)
        {
            return Result.Error(2, $"Could not find input file: {options.InputFile.FullName}");
        }

        IChallenge challenge;
        try
        {
            challenge = ChallengeProvider.GetChallenge(options);
        }
        catch (ChallengeNotFoundException e)
        {
            return Result.Error(3, e.Message);
        }


        using var reader = FileInputReader.Create(options.InputFile);
        var stopwatch = new Stopwatch();

        string answer;
        try
        {
            stopwatch.Start();
            answer = await challenge.PerformChallenge(reader, options.Part);
            stopwatch.Stop();
        }
        catch (PartDoesNotExistException e)
        {
            stopwatch.Stop();
            return Result.Error(4, e.Message, stopwatch.Elapsed);
        }
        catch (PartNotImplementedException e)
        {
            stopwatch.Stop();
            return Result.Error(5, e.Message, stopwatch.Elapsed);
        }

        return Result.Success(answer, stopwatch.Elapsed);
    }
}
