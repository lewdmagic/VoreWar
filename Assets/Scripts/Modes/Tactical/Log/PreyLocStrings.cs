﻿using System.Collections.Generic;

public static class PreyLocStrings
{
    private static readonly List<string> wombSyn = new List<string>() { "womb", "lower belly", "pussy", "slit", "muff", "cunt" };
    private static readonly List<string> breastSyn = new List<string>() { "breasts", "bosom", "bust", "mammaries", "boobs", "cleavage", "tits" };
    private static readonly List<string> breastSynPlural = new List<string>() { "breasts", "mammaries", "boobs", "tits" };
    private static readonly List<string> ballsSyn = new List<string>() { "balls", "scrotum", "testicles", "nuts", "orbs", "nutsack" };
    private static readonly List<string> ballsSynSing = new List<string>() { "scrotum", "nutsack", "sack", "ballsack" };
    private static readonly List<string> ballsSynPlural = new List<string>() { "balls", "testicles", "nuts", "orbs", "testis" };
    private static readonly List<string> stomachSyn = new List<string>() { "gut", "stomach", "belly", "tummy", "middle" };
    private static readonly List<string> analSyn = new List<string>() { "butt", "ass", "bottom", "backside", "bum", "rear", "rump", "booty", "tush" };
    private static readonly List<string> cockSyn = new List<string>() { "wang", "dick", "cock", "phallus", "member", "shaft", "pecker", "schlong" };

    private static readonly List<string> wombFluid = new List<string>() { "cum", "ejaculate", "honey", "fem-fluids", "pussy juice" };
    private static readonly List<string> breastFluid = new List<string>() { "milk", "delicious milk", "leaking milk", "lactation", "nourishing fluid" };
    private static readonly List<string> ballsFluid = new List<string>() { "cum", "sperm", "semen", "jizz", "spunk", "seed" };
    private static readonly List<string> stomachFluid = new List<string>() { "nutritious soup", "mush", "nutritious mush", "digestive juices", "chyme", "bubbling mush", "hot slurry", "meaty chunks", "stew", "melting flesh and bones" };


    private static readonly List<string> wombVerb = new List<string>() { "release", "birth", "ejaculate" };
    private static readonly List<string> breastVerb = new List<string>() { "release", "disgorge", "milk out" };
    private static readonly List<string> ballsVerb = new List<string>() { "cum", "release", "ejaculate" };
    private static readonly List<string> stomachVerb = new List<string>() { "puke", "spit", "spew", "disgorge" };

    private static readonly List<string> wombVerbPastTense = new List<string>() { "released", "gave birth", "ejaculated" };
    private static readonly List<string> breastVerbPastTense = new List<string>() { "released", "disgorged", "milked out" };
    private static readonly List<string> ballsVerbPastTense = new List<string>() { "cummed", "released", "ejaculated" };
    private static readonly List<string> stomachVerbPastTense = new List<string>() { "puked", "spat", "spewed", "disgorged" };

    private static readonly List<string> oralVoreVerbPresentTense = new List<string>() { "eats", "devours", "swallows", "gulps", "wolfs", "horks" };
    private static readonly List<string> oralVoreVerbPresentContinuousTense = new List<string>() { "eating", "devouring", "swallowing", "gobbling", "gulping", "wolfing", "horking", "downing" };
    private static readonly List<string> oralVoreVerbPastTense = new List<string>() { "ate", "devoured", "swallowed", "gobbled", "gulped", "downed" };

    private static string genRandom(List<string> options)
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
            case PreyLocation.breasts:
                return genRandom(breastSyn);
            case PreyLocation.leftBreast:
                return genRandom(breastSyn);
            case PreyLocation.rightBreast:
                return genRandom(breastSyn);
            case PreyLocation.balls:
                return genRandom(ballsSyn);
            case PreyLocation.stomach:
                return genRandom(stomachSyn);
            case PreyLocation.stomach2:
                return genRandom(stomachSyn);
            case PreyLocation.womb:
                return genRandom(wombSyn);
            case PreyLocation.anal:
                return genRandom(analSyn);
            case PreyLocation.tail:
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
        return genRandom(cockSyn);
    }

    /// <summary>
    ///     Gets a random plural synonym for breasts.
    /// </summary>
    public static string ToBreastSynPlural()
    {
        return genRandom(breastSynPlural);
    }

    /// <summary>
    ///     Gets a random plural synonym for scrotum.
    /// </summary>
    public static string ToBallSynPlural()
    {
        return genRandom(ballsSynPlural);
    }

    /// <summary>
    ///     Gets a random singular synonym for scrotum.
    /// </summary>
    public static string ToBallSynSing()
    {
        return genRandom(ballsSynSing);
    }

    public static string ToFluid(this PreyLocation preyLocation)
    {
        switch (preyLocation)
        {
            case PreyLocation.breasts:
                return genRandom(breastFluid);
            case PreyLocation.leftBreast:
                return genRandom(breastFluid);
            case PreyLocation.rightBreast:
                return genRandom(breastFluid);
            case PreyLocation.balls:
                return genRandom(ballsFluid);
            case PreyLocation.stomach:
                return genRandom(stomachFluid);
            case PreyLocation.stomach2:
                return genRandom(stomachFluid);
            case PreyLocation.womb:
                return genRandom(wombFluid);
            default:
                return "";
        }
    }

    public static string ToVerb(this PreyLocation preyLocation)
    {
        switch (preyLocation)
        {
            case PreyLocation.breasts:
                return genRandom(breastVerb);
            case PreyLocation.leftBreast:
                return genRandom(breastVerb);
            case PreyLocation.rightBreast:
                return genRandom(breastVerb);
            case PreyLocation.balls:
                return genRandom(ballsVerb);
            case PreyLocation.stomach:
                return genRandom(stomachVerb);
            case PreyLocation.stomach2:
                return genRandom(stomachVerb);
            case PreyLocation.womb:
                return genRandom(wombVerb);
            default:
                return "";
        }
    }

    public static string ToVerbPastTense(this PreyLocation preyLocation)
    {
        switch (preyLocation)
        {
            case PreyLocation.breasts:
                return genRandom(breastVerbPastTense);
            case PreyLocation.leftBreast:
                return genRandom(breastVerbPastTense);
            case PreyLocation.rightBreast:
                return genRandom(breastVerbPastTense);
            case PreyLocation.balls:
                return genRandom(ballsVerbPastTense);
            case PreyLocation.stomach:
                return genRandom(stomachVerbPastTense);
            case PreyLocation.stomach2:
                return genRandom(stomachVerbPastTense);
            case PreyLocation.womb:
                return genRandom(wombVerbPastTense);
            default:
                return "";
        }
    }

    /// <summary>
    ///     Gets a random Oral Vore Verb in Present Continuous Tense, such as "swallowing" or "eating".
    /// </summary>
    /// <returns></returns>
    public static string GetOralVoreVPCT()
    {
        return genRandom(oralVoreVerbPresentContinuousTense);
    }

    /// <summary>
    ///     Gets a random Oral Vore Verb in Past Tense, such as "swallowed" or "ate".
    /// </summary>
    /// <returns></returns>
    public static string GetOralVoreVPastT()
    {
        return genRandom(oralVoreVerbPastTense);
    }

    /// <summary>
    ///     Gets a random Oral Vore Verb in Present Tense, such as "eats" or "devours".
    /// </summary>
    /// <returns></returns>
    public static string GetOralVoreVPT()
    {
        return genRandom(oralVoreVerbPresentTense);
    }
}