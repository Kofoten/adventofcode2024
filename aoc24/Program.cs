namespace aoc24;

internal class Program
{
    static async Task<int> Main(string[] args)
    {
        if (!BaseOptions.TryParse(args, out var options, out var reason))
        {
            Console.WriteLine($"ERROR: {reason}");
            return 1;
        }

        var runtime = new Runtime(options);

        try
        {
            var result = await runtime.Run();
            if (result.IsSuccess(out var answer, out var error))
            {
                Console.WriteLine(answer);

#if DEBUG
                Console.WriteLine($"Processingtime {result.ProcessingTime}");
#endif

                return 0;
            }

            Console.WriteLine($"FATAL: {error}");
            return result.Code;
        }
        catch (Exception e)
        {
            Console.WriteLine($"CRIT: {e.Message}");
            Console.WriteLine(e.ToString());
            return 42;
        }
    }
}