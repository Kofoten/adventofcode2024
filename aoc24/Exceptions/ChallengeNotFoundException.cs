namespace aoc24.Exceptions;

public class ChallengeNotFoundException(int id) : Exception($"A challenge for id {id} could not be found.")
{
}
