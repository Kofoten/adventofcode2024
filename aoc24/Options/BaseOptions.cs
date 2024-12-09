namespace aoc24.Options;

public record BaseOptions(int Challenge, int Part, bool UseExampleInput, string? SessionCookie) : IOptions
{
    public static bool TryParse(string[] args, [NotNullWhen(true)] out IOptions? options, [NotNullWhen(false)] out string? reason)
    {
        if (args.Length < 2)
        {
            options = null;
            reason = "Invalid number of arguments, allowed arguments are: challenge (positional: index 0, integer), part (positional: index 1, integer), inputFile (optional, positional: index 2, filename), challengeSpecificOptions (required if any, named, any[]).";
            return false;
        }

        if (!int.TryParse(args[0], out var challenge) || challenge < 1 || challenge > 25)
        {
            options = null;
            reason = "Challenge (positional argument 0) must be a valid integer with a minimum value of 1 and a maximum value of 25.";
            return false;
        }

        if (!int.TryParse(args[1], out var part) || part < 1 || part > 2)
        {
            options = null;
            reason = "Part (positional argument 1) must be a valid integer with a minimum value of 1 and a maximum value of 2.";
            return false;
        }

        bool useExampleInput = false;
        string? sessionCookie = null;

        var remainingArgs = new List<string>();
        string? previous = null;
        for (int i = 2; i < args.Length; i++)
        {
            if (args[i].StartsWith('-'))
            {
                switch (args[i])
                {
                    case "--use-example":
                        useExampleInput = true;
                        break;
                    case "--session-cookie":
                        previous = args[i];
                        break;
                    default:
                        remainingArgs.Add(args[i]);
                        break;
                }
            }
            else
            {
                switch (previous)
                {
                    case "--session-cookie":
                        sessionCookie = args[i];
                        break;
                    default:
                        remainingArgs.Add(args[i]);
                        break;
                }
            }
        }

        return TryParseIfSpecific(challenge, part, useExampleInput, sessionCookie, remainingArgs, out options, out reason);
    }

    public static bool TryParseIfSpecific(int challenge, int part, bool useExampleInput, string? sessionCookie, IList<string> challengeSpecificOptions, [NotNullWhen(true)] out IOptions? options, [NotNullWhen(false)] out string? reason) => challenge switch
    {
        _ => CreateBaseOptions(challenge, part, useExampleInput, sessionCookie, out options, out reason),
    };

    private static bool CreateBaseOptions(int challenge, int part, bool useExampleInput, string? sessionCookie, [NotNullWhen(true)] out IOptions? options, [NotNullWhen(false)] out string? reason)
    {
        options = new BaseOptions(challenge, part, useExampleInput, sessionCookie);
        reason = null;
        return true;
    }
}