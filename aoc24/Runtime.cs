using System.Diagnostics;
namespace aoc24;

public class Runtime(IOptions options)
{
    public readonly IOptions options = options;

    public async Task<Result> Run(CancellationToken cancellation)
    {
        using var inputStream = await InputProvider.GetInput(options.Challenge, options.UseExampleInput, options.SessionCookie, cancellation);
        using var inputReader = new InputReader(inputStream);

        IChallenge challenge;
        try
        {
            challenge = ChallengeProvider.GetChallenge(options);
        }
        catch (ChallengeNotFoundException e)
        {
            return Result.Error(3, e.Message);
        }

        var stopwatch = new Stopwatch();
        string answer;
        try
        {
            stopwatch.Start();
            answer = await challenge.PerformChallenge(inputReader, options.Part);
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
