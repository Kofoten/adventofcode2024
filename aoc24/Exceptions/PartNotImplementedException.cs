namespace aoc24.Exceptions;

public class PartNotImplementedException(int part) : Exception($"The part {part} is not implemented yet.")
{
}
