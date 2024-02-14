using System.Collections.Generic;

public static class PreyLocStrings
{
    private static readonly List<string> WombSyn = new List<string>() { "womb", "lower belly", "pussy", "slit", "muff", "cunt" };
    private static readonly List<string> BreastSyn = new List<string>() { "breasts", "bosom", "bust", "mammaries", "boobs", "cleavage", "tits" };
    private static readonly List<string> BreastSynPlural = new List<string>() { "breasts", "mammaries", "boobs", "tits" };
    private static readonly List<string> BallsSyn = new List<string>() { "balls", "scrotum", "testicles", "nuts", "orbs", "nutsack" };
    private static readonly List<string> BallsSynSing = new List<string>() { "scrotum", "nutsack", "sack", "ballsack" };
    private static readonly List<string> BallsSynPlural = new List<string>() { "balls", "testicles", "nuts", "orbs", "testis" };
    private static readonly List<string> StomachSyn = new List<string>() { "gut", "stomach", "belly", "tummy", "middle" };
    private static readonly List<string> AnalSyn = new List<string>() { "butt", "ass", "bottom", "backside", "bum", "rear", "rump", "booty", "tush" };
    private static readonly List<string> CockSyn = new List<string>() { "wang", "dick", "cock", "phallus", "member", "shaft", "pecker", "schlong" };

    private static readonly List<string> WombFluid = new List<string>() { "cum", "ejaculate", "honey", "fem-fluids", "pussy juice" };
    private static readonly List<string> BreastFluid = new List<string>() { "milk", "delicious milk", "leaking milk", "lactation", "nourishing fluid" };
    private static readonly List<string> BallsFluid = new List<string>() { "cum", "sperm", "semen", "jizz", "spunk", "seed" };
    private static readonly List<string> StomachFluid = new List<string>() { "nutritious soup", "mush", "nutritious mush", "digestive juices", "chyme", "bubbling mush", "hot slurry", "meaty chunks", "stew", "melting flesh and bones" };


    private static readonly List<string> WombVerb = new List<string>() { "release", "birth", "ejaculate" };
    private static readonly List<string> BreastVerb = new List<string>() { "release", "disgorge", "milk out" };
    private static readonly List<string> BallsVerb = new List<string>() { "cum", "release", "ejaculate" };
    private static readonly List<string> StomachVerb = new List<string>() { "puke", "spit", "spew", "disgorge" };

    private static readonly List<string> WombVerbPastTense = new List<string>() { "released", "gave birth", "ejaculated" };
    private static readonly List<string> BreastVerbPastTense = new List<string>() { "released", "disgorged", "milked out" };
    private static readonly List<string> BallsVerbPastTense = new List<string>() { "cummed", "released", "ejaculated" };
    private static readonly List<string> StomachVerbPastTense = new List<string>() { "puked", "spat", "spewed", "disgorged" };

    private static readonly List<string> OralVoreVerbPresentTense = new List<string>() { "eats", "devours", "swallows", "gulps", "wolfs", "horks" };
    private static readonly List<string> OralVoreVerbPresentContinuousTense = new List<string>() { "eating", "devouring", "swallowing", "gobbling", "gulping", "wolfing", "horking", "downing" };
    private static readonly List<string> OralVoreVerbPastTense = new List<string>() { "ate", "devoured", "swallowed", "gobbled", "gulped", "downed" };

    private static string GenRandom(List<string> options)
    {
        int index = State.Rand.Next() % options.Count;
        return options[index];
    }

    /// <summary>
    ///     Gets a random synonym for the body part(s) associatied with the provided <c>PreyLocation</c>.
    ///     <br></br>
    ///     NOTICE: Using this function for balls or breasts may return a singular or plural noun!
    ///     <br></br>
    ///     If specifically needing a singluar or plural noun, use <c>ToBreastSynPlural()</c>, <c>ToBallSynPlural()</c>, or
    ///     <c>ToBallSynSing()</c> instead.
    /// </summary>
    public static string ToSyn(this PreyLocation preyLocation)
    {
        switch (preyLocation)
        {
            case PreyLocation.Breasts:
                return GenRandom(BreastSyn);
            case PreyLocation.LeftBreast:
                return GenRandom(BreastSyn);
            case PreyLocation.RightBreast:
                return GenRandom(BreastSyn);
            case PreyLocation.Balls:
                return GenRandom(BallsSyn);
            case PreyLocation.Stomach:
                return GenRandom(StomachSyn);
            case PreyLocation.Stomach2:
                return GenRandom(StomachSyn);
            case PreyLocation.Womb:
                return GenRandom(WombSyn);
            case PreyLocation.Anal:
                return GenRandom(AnalSyn);
            case PreyLocation.Tail:
                return "tail";
            default:
                return "";
        }
    }

    /// <summary>
    ///     Gets a random synonym for penis.
    /// </summary>
    public static string ToCockSyn()
    {
        return GenRandom(CockSyn);
    }

    /// <summary>
    ///     Gets a random plural synonym for breasts.
    /// </summary>
    public static string ToBreastSynPlural()
    {
        return GenRandom(BreastSynPlural);
    }

    /// <summary>
    ///     Gets a random plural synonym for scrotum.
    /// </summary>
    public static string ToBallSynPlural()
    {
        return GenRandom(BallsSynPlural);
    }

    /// <summary>
    ///     Gets a random singular synonym for scrotum.
    /// </summary>
    public static string ToBallSynSing()
    {
        return GenRandom(BallsSynSing);
    }

    public static string ToFluid(this PreyLocation preyLocation)
    {
        switch (preyLocation)
        {
            case PreyLocation.Breasts:
                return GenRandom(BreastFluid);
            case PreyLocation.LeftBreast:
                return GenRandom(BreastFluid);
            case PreyLocation.RightBreast:
                return GenRandom(BreastFluid);
            case PreyLocation.Balls:
                return GenRandom(BallsFluid);
            case PreyLocation.Stomach:
                return GenRandom(StomachFluid);
            case PreyLocation.Stomach2:
                return GenRandom(StomachFluid);
            case PreyLocation.Womb:
                return GenRandom(WombFluid);
            default:
                return "";
        }
    }

    public static string ToVerb(this PreyLocation preyLocation)
    {
        switch (preyLocation)
        {
            case PreyLocation.Breasts:
                return GenRandom(BreastVerb);
            case PreyLocation.LeftBreast:
                return GenRandom(BreastVerb);
            case PreyLocation.RightBreast:
                return GenRandom(BreastVerb);
            case PreyLocation.Balls:
                return GenRandom(BallsVerb);
            case PreyLocation.Stomach:
                return GenRandom(StomachVerb);
            case PreyLocation.Stomach2:
                return GenRandom(StomachVerb);
            case PreyLocation.Womb:
                return GenRandom(WombVerb);
            default:
                return "";
        }
    }

    public static string ToVerbPastTense(this PreyLocation preyLocation)
    {
        switch (preyLocation)
        {
            case PreyLocation.Breasts:
                return GenRandom(BreastVerbPastTense);
            case PreyLocation.LeftBreast:
                return GenRandom(BreastVerbPastTense);
            case PreyLocation.RightBreast:
                return GenRandom(BreastVerbPastTense);
            case PreyLocation.Balls:
                return GenRandom(BallsVerbPastTense);
            case PreyLocation.Stomach:
                return GenRandom(StomachVerbPastTense);
            case PreyLocation.Stomach2:
                return GenRandom(StomachVerbPastTense);
            case PreyLocation.Womb:
                return GenRandom(WombVerbPastTense);
            default:
                return "";
        }
    }

    /// <summary>
    ///     Gets a random Oral Vore Verb in Present Continuous Tense, such as "swallowing" or "eating".
    /// </summary>
    /// <returns></returns>
    public static string GetOralVoreVpct()
    {
        return GenRandom(OralVoreVerbPresentContinuousTense);
    }

    /// <summary>
    ///     Gets a random Oral Vore Verb in Past Tense, such as "swallowed" or "ate".
    /// </summary>
    /// <returns></returns>
    public static string GetOralVoreVPastT()
    {
        return GenRandom(OralVoreVerbPastTense);
    }

    /// <summary>
    ///     Gets a random Oral Vore Verb in Present Tense, such as "eats" or "devours".
    /// </summary>
    /// <returns></returns>
    public static string GetOralVoreVpt()
    {
        return GenRandom(OralVoreVerbPresentTense);
    }
}