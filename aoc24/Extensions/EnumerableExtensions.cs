namespace aoc24.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<T> Range<T>(Func<T> valueFactory, int count)
    {
        for (int i = 0; i < count; i++)
        {
            yield return valueFactory();
        }
    }
}
