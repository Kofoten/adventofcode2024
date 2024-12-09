namespace aoc24.IO;

public static class Utilities
{
    public static string GetAocCachePath()
    {
        var homeDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        var aocCachePath = Path.Combine(homeDir, ".aoc");
        if (!Directory.Exists(aocCachePath))
        {
            Directory.CreateDirectory(aocCachePath);
        }

        return aocCachePath;
    }

    public static string GetAocChallengeCachePath(int year)
    {
        var aocChallengeCachePath = Path.Combine(GetAocCachePath(), "cache");
        if (!Directory.Exists(aocChallengeCachePath))
        {
            Directory.CreateDirectory(aocChallengeCachePath);
        }

        var aocYearChallengeCachePath = Path.Combine(aocChallengeCachePath, year.ToString());
        if (!Directory.Exists(aocYearChallengeCachePath))
        {
            Directory.CreateDirectory(aocYearChallengeCachePath);
        }

        return aocYearChallengeCachePath;
    }
}
