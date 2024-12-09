namespace aoc24.IO;

public static class Utilities
{
    public static string GetAocCachePath(int year)
    {
        var homeDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        var aocCachePath = Path.Combine(homeDir, ".aoc");
        if (!Directory.Exists(aocCachePath))
        {
            Directory.CreateDirectory(aocCachePath);
        }

        var aocYearCachePath = Path.Combine(aocCachePath, year.ToString());
        if (!Directory.Exists(aocYearCachePath))
        {
            Directory.CreateDirectory(aocYearCachePath);
        }

        return aocYearCachePath;
    }
}
