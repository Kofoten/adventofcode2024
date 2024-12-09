using System.Text;

namespace aoc24.IO;

public class AocClient(int year)
{
    private readonly int year = year;
    private readonly HttpClient httpClient = new();

    public async Task<Stream> GetInput(int day, string? sessionCookie, CancellationToken cancellation)
    {
        var url = new Uri($"https://adventofcode.com/{year}/day/{day}/input");

        var request = new HttpRequestMessage(HttpMethod.Get, url);
        await Authenticate(request, sessionCookie, cancellation);

        var response = await httpClient.SendAsync(request, cancellation);
        response.EnsureSuccessStatusCode();
        return response.Content.ReadAsStream(cancellation);
    }

    public async Task SendAnswer(int day, int part, string? sessionCookie, CancellationToken cancellation)
    {
        throw new NotImplementedException();
    }

    private async Task Authenticate(HttpRequestMessage request, string? sessionCookie, CancellationToken cancellation)
    {
        var aocPath = Utilities.GetAocCachePath(year);
        var sessionCachePath = Path.Combine(aocPath, "session");

        if (sessionCookie is not null)
        {
            using var stream = new FileStream(sessionCachePath, FileMode.Create, FileAccess.Write, FileShare.Read);
            using var writer = new StreamWriter(stream, Encoding.UTF8);
            await writer.WriteAsync(sessionCookie);
        }
        else if (File.Exists(sessionCachePath))
        {
            using var stream = new FileStream(sessionCachePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var reader = new StreamReader(stream, Encoding.UTF8);
            sessionCookie = await reader.ReadToEndAsync(cancellation);
        }
        else
        {
            throw new InvalidOperationException("No session cookie found. Can not authenticate to AoC.");
        }

        request.Headers.Add("Cookie", $"session={sessionCookie}");
    }
}
