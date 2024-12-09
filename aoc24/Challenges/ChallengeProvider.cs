namespace aoc24.Challenges;

public static class ChallengeProvider
{
    public static IChallenge GetChallenge(IOptions options) => options switch
    {
        BaseOptions o when o.Challenge == 01 => new Challenge01.Challenge01(),
        BaseOptions o when o.Challenge == 02 => new Challenge02.Challenge02(),
        BaseOptions o when o.Challenge == 03 => new Challenge03.Challenge03(),
        BaseOptions o when o.Challenge == 04 => new Challenge04.Challenge04(),
        BaseOptions o when o.Challenge == 05 => new Challenge05.Challenge05(),
        BaseOptions o when o.Challenge == 06 => new Challenge06.Challenge06(),
        BaseOptions o when o.Challenge == 07 => new Challenge07.Challenge07(),
        BaseOptions o when o.Challenge == 08 => new Challenge08.Challenge08(),
        BaseOptions o when o.Challenge == 09 => new Challenge09.Challenge09(),
        BaseOptions o when o.Challenge == 10 => new Challenge10.Challenge10(),
        BaseOptions o when o.Challenge == 11 => new Challenge11.Challenge11(),
        BaseOptions o when o.Challenge == 12 => new Challenge12.Challenge12(),
        BaseOptions o when o.Challenge == 13 => new Challenge13.Challenge13(),
        BaseOptions o when o.Challenge == 14 => new Challenge14.Challenge14(),
        BaseOptions o when o.Challenge == 15 => new Challenge15.Challenge15(),
        BaseOptions o when o.Challenge == 16 => new Challenge16.Challenge16(),
        BaseOptions o when o.Challenge == 17 => new Challenge17.Challenge17(),
        BaseOptions o when o.Challenge == 18 => new Challenge18.Challenge18(),
        BaseOptions o when o.Challenge == 19 => new Challenge19.Challenge19(),
        BaseOptions o when o.Challenge == 20 => new Challenge20.Challenge20(),
        BaseOptions o when o.Challenge == 21 => new Challenge21.Challenge21(),
        BaseOptions o when o.Challenge == 22 => new Challenge22.Challenge22(),
        BaseOptions o when o.Challenge == 23 => new Challenge23.Challenge23(),
        BaseOptions o when o.Challenge == 24 => new Challenge24.Challenge24(),
        BaseOptions o when o.Challenge == 25 => new Challenge25.Challenge25(),
        _ => throw new ChallengeNotFoundException(options.Challenge),
    };
}
