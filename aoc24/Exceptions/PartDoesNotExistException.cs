namespace aoc24.Exceptions;

public class PartDoesNotExistException(int part) : Exception($"There exists no part {part}.")
{
}
