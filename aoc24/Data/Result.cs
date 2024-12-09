namespace aoc24.Data;

public class Result(int code, string? answer, string? message, TimeSpan processingTime)
{
    public int Code { get; private init; } = code;
    public string? Answer { get; private init; } = answer;
    public string? Message { get; private init; } = message;
    public TimeSpan ProcessingTime { get; private init; } = processingTime;

    public bool IsSuccess([NotNullWhen(true)] out string? answer, [NotNullWhen(false)] out string? message)
    {
        if (Code == 0 && Answer is not null)
        {
            answer = Answer;
            message = null;
            return true;
        }

        answer = null;
        message = Message ?? "Unknown Error";
        return false;
    }

    public static Result Success(string answer, TimeSpan processingTime)
        => new(0, answer, null, processingTime);

    public static Result Error(int code, string message)
        => Error(code, message, TimeSpan.Zero);

    public static Result Error(int code, string message, TimeSpan processingTime)
        => new(code, null, message, processingTime);
}
