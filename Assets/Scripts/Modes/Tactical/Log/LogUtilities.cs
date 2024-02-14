using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

internal static class LogUtilities
{
    private static int _rand;

    internal static string GetRandomStringFrom(List<string> messages)
    {
        if (messages.Count == 0) return "";
        _rand = Random.Range(0, messages.Count);
        return messages[_rand];
    }

    internal static string GetRandomStringFrom(params string[] messages)
    {
        _rand = Random.Range(0, messages.Length);
        return messages[_rand];
    }

    internal static string GetGenderString(Unit unit, string female, string male, string mixed)
    {
        if (unit.HasBreasts && unit.HasDick != unit.HasVagina)
            return female;
        else if (!unit.HasBreasts && unit.HasDick != unit.HasVagina) return male;
        return mixed;
    }

    internal static string Capitalize(string str)
    {
        if (str == null) return null;
        return char.ToUpper(str[0]) + str.Substring(1);
    }

    /// <summary>
    ///     Returns given unit's nominative pronoun.<br></br>(e.g. he/she/they)
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string GppHe(Unit unit) => unit.GetPronoun(0);

    /// <summary>
    ///     Returns given unit's nominative pronoun appended with present-tense auxillary.<br></br>(e.g. he is/she is/they are)
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string GppHeIs(Unit unit) => unit.GetPronoun(0) + (unit.GetPronoun(5) == "plural" ? " are" : " is");

    /// <summary>
    ///     Returns given unit's nominative pronoun appended with present-tense auxillary as a contraction.<br></br>(e.g.
    ///     he's/she's/they're)
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string GppHeIsAbbr(Unit unit) => unit.GetPronoun(0) + (unit.GetPronoun(5) == "plural" ? "'re" : "'s");

    /// <summary>
    ///     Returns given unit's nominative pronoun appended with past-tense auxillary.<br></br>(e.g. he was/she was/they were)
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string GppHeWas(Unit unit) => unit.GetPronoun(0) + (unit.GetPronoun(5) == "plural" ? " were" : " was");

    /// <summary>
    ///     Returns given unit's accusative pronoun.<br></br>(e.g. him/her/them)
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string GppHim(Unit unit) => unit.GetPronoun(1);

    /// <summary>
    ///     Returns given unit's pronomial possessive pronoun.<br></br>(e.g. ...<u>their</u> belly...)
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string GppHis(Unit unit) => unit.GetPronoun(2);

    /// <summary>
    ///     Returns given unit's reflexive pronoun.<br></br>(e.g. ...can't help <u>themself</u>...)
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string GppHimself(Unit unit) => unit.GetPronoun(4);

    /// <summary>
    ///     Returns "s" if given unit is referred to with singular grammar.
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string SIfSingular(Unit unit) => unit.GetPronoun(5) == "plural" ? "" : "s";

    /// <summary>
    ///     Returns "es" if given unit is referred to with singular grammar.
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string EsIfSingular(Unit unit) => unit.GetPronoun(5) == "plural" ? "" : "es";

    /// <summary>
    ///     Returns "y" or "ies" based on plurality of given unit.
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string IesIfSingular(Unit unit) => unit.GetPronoun(5) == "plural" ? "y" : "ies";

    /// <summary>
    ///     Returns "has" or "have" based on plurality of given unit.
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string HasHave(Unit unit) => unit.GetPronoun(5) == "plural" ? "have" : "has";

    /// <summary>
    ///     Returns "is" or "are" based on plurality of given unit.
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string IsAre(Unit unit) => unit.GetPronoun(5) == "plural" ? "are" : "is";

    /// <summary>
    ///     Returns "was" or "were" based on plurality of given unit.
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string WasWere(Unit unit) => unit.GetPronoun(5) == "plural" ? "were" : "was";

    internal static string PluralForPart(PreyLocation location)
    {
        switch (location)
        {
            case PreyLocation.Breasts:
            case PreyLocation.Balls:
                return "";
            case PreyLocation.Stomach:
            case PreyLocation.Stomach2:
            case PreyLocation.Womb:
            case PreyLocation.Tail:
            case PreyLocation.Anal:
                return "s";
        }

        return "";
    }

    internal static string BoyGirl(Unit unit)
    {
        if (unit.DefaultBreastSize >= 0 && unit.DickSize < 0) return "girl";
        if (unit.DefaultBreastSize < 0 && unit.DickSize >= 0) return "boy";
        return unit.Race.ToString();
    }

    //internal static string FriendlyWarrior(Unit unit)
    //{
    //    var friendlies = TacticalUtilities.Units.Where(s => s.Unit.Side == unit.Side && s.Unit != unit && s.Visible && s.Targetable && s.Unit.IsDead == false).ToArray();
    //    if (friendlies.Length == 0)
    //        return "NULL";
    //    return friendlies[State.Rand.Next(friendlies.Length)].Unit.Name;  
    //}

    internal static Unit CompetitionWarrior(Unit unit)
    {
        var friendlies = TacticalUtilities.Units.Where(s => Equals(s.Unit.Side, unit.Side) && s.Unit != unit && s.Visible && s.Targetable && s.Unit.IsDead == false && RomanticTarget(unit, s.Unit) == false).ToArray();
        if (friendlies.Length == 0) return null;
        return friendlies[State.Rand.Next(friendlies.Length)].Unit;
    }

    internal static Unit PotentialNextPrey(Unit unit)
    {
        var preyList = TacticalUtilities.Units.Where(s => !Equals(s.Unit.Side, unit.Side) && s.Visible && s.Targetable && !s.Unit.IsDead);
        var preyChanceMap = new Dictionary<ActorUnit, float>();
        foreach (ActorUnit prey in preyList)
        {
            float chance = prey.GetDevourChance(TacticalUtilities.Units.Where(actor => actor.Unit == unit)?.FirstOrDefault(), true);
            preyChanceMap.Add(prey, chance);
        }

        var primePrey = preyChanceMap.OrderBy(x => x.Value).LastOrDefault();
        if (!primePrey.Equals(default(KeyValuePair<ActorUnit, float>)))
        {
            return primePrey.Key.Unit;
        }

        var you = new Unit(Race.Human);
        you.DefaultBreastSize = -1;
        you.DickSize = -1;
        you.Name = "You, the player";
        return you;
    }

    internal static Unit AttractedWarrior(Unit unit)
    {
        if (unit.AttractedTo != null)
        {
            var actor = TacticalUtilities.Units.Where(s => s.Unit == unit.AttractedTo && Equals(s.Unit.Side, unit.Side) && s.Unit != unit && RomanticTarget(unit, s.Unit)).FirstOrDefault(); //If this fails, reassign
            if (actor != null)
            {
                if (actor.Visible && actor.Targetable && actor.Unit.IsDead == false)
                {
                    return actor.Unit;
                }

                return null; //Avoid picking a new target during the same battle
            }
        }

        var friendlies = TacticalUtilities.Units.Where(s => Equals(s.Unit.Side, unit.Side) && s.Unit != unit && s.Visible && s.Targetable && s.Unit.IsDead == false && RomanticTarget(unit, s.Unit)).ToArray();
        if (friendlies.Length == 0) return null;
        return friendlies[State.Rand.Next(friendlies.Length)].Unit;
    }

    internal static bool ActorHumanoid(Unit s)
    {
        return RaceFuncs.IsHumanoid(s.Race);
    }

    internal static bool RomanticTarget(Unit unit, Unit target)
    {
        if (unit.GetGender() == Gender.Hermaphrodite || target.GetGender() == Gender.Hermaphrodite) return true;
        if (unit.GetGender() == Gender.Female)
        {
            switch (Config.FemalesLike)
            {
                case Orientation.Straight:
                    return target.GetGender() == Gender.Male;
                case Orientation.Gay:
                    return target.GetGender() == Gender.Female;
                case Orientation.Bi:
                    return true;
            }
        }

        if (unit.GetGender() == Gender.Male)
        {
            switch (Config.MalesLike)
            {
                case Orientation.Straight:
                    return target.GetGender() == Gender.Female;
                case Orientation.Gay:
                    return target.GetGender() == Gender.Male;
                case Orientation.Bi:
                    return true;
            }
        }

        //Should never make it here
        return false;
    }


    /// <summary>
    ///     Determines whether the string supplied should have either a or an before it and returns the original string with
    ///     the right "thing" in front of it.
    ///     Done this way since otherwise the string might get randomized again and wouldn't match the returned bit.
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    internal static string GetAorAn(string str)
    {
        if (str.StartsWith("a", true, null) || str.StartsWith("e", true, null) || str.StartsWith("i", true, null) || str.StartsWith("o", true, null) || str.StartsWith("u", true, null) || str.StartsWith("y", true, null))
        {
            return "an " + str;
        }

        if (str.StartsWith("b", true, null) || str.StartsWith("c", true, null) || str.StartsWith("d", true, null) || str.StartsWith("f", true, null) || str.StartsWith("g", true, null) || str.StartsWith("h", true, null) ||
            str.StartsWith("j", true, null) || str.StartsWith("k", true, null) || str.StartsWith("l", true, null) || str.StartsWith("m", true, null) || str.StartsWith("n", true, null) || str.StartsWith("p", true, null) ||
            str.StartsWith("q", true, null) || str.StartsWith("r", true, null) || str.StartsWith("s", true, null) || str.StartsWith("t", true, null) || str.StartsWith("v", true, null) || str.StartsWith("w", true, null) || str.StartsWith("x", true, null) || str.StartsWith("z", true, null))
        {
            return "a " + str;
        }

        if (str.StartsWith("'", true, null))
        {
            if (str.StartsWith("'a", true, null) || str.StartsWith("'e", true, null) || str.StartsWith("'i", true, null) || str.StartsWith("'o", true, null) || str.StartsWith("'u", true, null) || str.StartsWith("'y", true, null))
            {
                return "an " + str;
            }

            if (str.StartsWith("'b", true, null) || str.StartsWith("'c", true, null) || str.StartsWith("'d", true, null) || str.StartsWith("'f", true, null) || str.StartsWith("'g", true, null) || str.StartsWith("'h", true, null) ||
                str.StartsWith("'j", true, null) || str.StartsWith("'k", true, null) || str.StartsWith("'l", true, null) || str.StartsWith("'m", true, null) || str.StartsWith("'n", true, null) || str.StartsWith("'p", true, null) ||
                str.StartsWith("'q", true, null) || str.StartsWith("'r", true, null) || str.StartsWith("'s", true, null) || str.StartsWith("'t", true, null) || str.StartsWith("'v", true, null) || str.StartsWith("'w", true, null) || str.StartsWith("'x", true, null) || str.StartsWith("'z", true, null))
            {
                return "a " + str;
            }
        }

        return str;
    }

    internal static string ApostrophizeWithOrWithoutS(string str)
    {
        return str + (str.EndsWith("s") ? "'" : "'s");
    }

    /// <summary>
    ///     <para>
    ///         Gets a descriptive string that fits sentences like "Edmond stuffs Sidney down his maw, enjoying the * morsels
    ///         squirms on her way down."
    ///     </para>
    ///     <para>
    ///         Generally meant for the prey/loser/weaker unit. Has mostly demeaning, belittling, weakness indicating or fear
    ///         portraying terms.
    ///     </para>
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string GetPreyDesc(Unit unit)
    {
        return Races2.GetRace(unit.Race).FlavorText().GetPreyDescription(unit);
    }

    /// <summary>
    ///     <para>
    ///         Gets a descriptive string that fits sentences like "Edmond stuffs Sidney down his maw, the prey filling his *
    ///         body nicely."
    ///     </para>
    ///     <para>
    ///         Generally meant for the predator/winner/stronger unit. Strength describing, contentment/pleasure indicating,
    ///         etc. terms.
    ///     </para>
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string GetPredDesc(Unit unit)
    {
        return Races2.GetRace(unit.Race).FlavorText().GetPredDescription(unit);
    }

    /// <summary>
    ///     <para>
    ///         Gets a descriptive string that fits situations like "Jeanne graps Timothy's head, pushing the *'s face in her
    ///         slit and soon forcing rest of him after it."
    ///     </para>
    ///     <para>
    ///         This is either the species name, a name of the genus the species belongs to or something similar. Can also be
    ///         a synonym of the species name.
    ///     </para>
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string GetRaceDescSingl(Unit unit)
    {
        return Races2.GetRace(unit.Race).FlavorText().GetRaceSingleDescription(unit);
    }

    /// <summary>
    ///     Gets a name that fits the weapon the unit's graphics show it using.
    /// </summary>
    /// <param name="weapon"></param>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string GetWeaponTrueName(Weapon weapon, Unit unit)
    {
        return Races2.GetRace(unit.Race).FlavorText().GetWeaponTrueName(weapon, unit);
    }

    public static bool PreyDead(EventLog s) => s.Prey.IsDead;
    public static bool PreyCumgested(EventLog s) => s.Prey.IsDead && InBalls(s);
    public static bool CanBurp(EventLog s) => Config.BurpFraction > .1f;
    public static bool Farts(EventLog s) => Config.FartOnAbsorb;
    public static bool Scat(EventLog s) => Config.Scat && (s.PreyLocation == PreyLocation.Stomach || s.PreyLocation == PreyLocation.Stomach2);
    public static bool Lewd(EventLog s) => Config.LewdDialog;
    public static bool HardVore(EventLog s) => Config.HardVoreDialog;
    public static bool HardVoreInStomach(EventLog s) => Config.HardVoreDialog && (s.PreyLocation == PreyLocation.Stomach || s.PreyLocation == PreyLocation.Stomach2);
    public static bool InStomach(EventLog s) => s.PreyLocation == PreyLocation.Stomach || s.PreyLocation == PreyLocation.Stomach2;
    public static bool InWomb(EventLog s) => s.PreyLocation == PreyLocation.Womb;
    public static bool InStomachOrWomb(EventLog s) => s.PreyLocation == PreyLocation.Stomach || s.PreyLocation == PreyLocation.Stomach2 || s.PreyLocation == PreyLocation.Womb;
    public static bool InBreasts(EventLog s) => s.PreyLocation == PreyLocation.Breasts || s.PreyLocation == PreyLocation.LeftBreast || s.PreyLocation == PreyLocation.RightBreast;
    public static bool InBalls(EventLog s) => s.PreyLocation == PreyLocation.Balls;
    public static bool FirstTime(EventLog s) => s.Unit.DigestedUnits == 0 && s.Unit.Level < 10 && s.Unit.Type != UnitType.Mercenary && s.Unit.Type != UnitType.SpecialMercenary && State.GameManager.PureTactical == false;
    public static bool FirstTimeAbsorption(EventLog s) => s.Unit.DigestedUnits == 1 && s.Unit.Level < 10 && s.Unit.Type != UnitType.Mercenary && s.Unit.Type != UnitType.SpecialMercenary && State.GameManager.PureTactical == false;
    public static bool TargetFirstTime(EventLog s) => s.Target.DigestedUnits == 0 && s.Target.Level < 10 && s.Target.Type != UnitType.Mercenary && s.Target.Type != UnitType.SpecialMercenary && State.GameManager.PureTactical == false;
    public static bool Friendly(EventLog s) => Equals(s.Unit.Side, s.Target.Side);
    public static bool Endo(EventLog s) => s.Unit.HasTrait(TraitType.Endosoma);
    public static bool HealingEndo(EventLog s) => s.Unit.HasTrait(TraitType.Endosoma) && s.Unit.HasTrait(TraitType.HealingBelly);
    public static bool FriendlyPrey(EventLog s) => Equals(s.Unit.Side, s.Prey.Side);
    public static bool ActorHumanoid(EventLog s) => RaceFuncs.IsHumanoid(s.Unit.Race);
    public static bool HasGreatEscape(EventLog s) => s.Target.HasTrait(TraitType.TheGreatEscape);
    public static bool Cursed(EventLog s) => s.Target.GetStatusEffect(StatusEffectType.WillingPrey) != null;
    public static bool Shrunk(EventLog s) => s.Target.GetStatusEffect(StatusEffectType.Diminished) != null;
    public static bool SizeDiff(EventLog s, float ratio) => State.RaceSettings.GetBodySize(s.Unit.Race) * s.Unit.GetScale(1) >= State.RaceSettings.GetBodySize(s.Target.Race) * s.Target.GetScale(1) * ratio;

    public static bool SizeDiffPrey(EventLog s, float ratio) => State.RaceSettings.GetBodySize(s.Unit.Race) * s.Unit.GetScale(1) >= State.RaceSettings.GetBodySize(s.Prey.Race) * s.Target.GetScale(1) * ratio;

    //bool ReqSSW(EventLog s) => SameSexWarrior(s.Unit) != "NULL";
    public static bool ReqOsw(EventLog s) => AttractedWarrior(s.Unit) != null;
    public static bool ReqOswLewd(EventLog s) => AttractedWarrior(s.Unit) != null && Lewd(s);
    public static bool ReqOswStomach(EventLog s) => AttractedWarrior(s.Unit) != null && InStomach(s);
    public static bool ReqOswBelly(EventLog s) => AttractedWarrior(s.Unit) != null && InStomachOrWomb(s);
    public static bool ReqSswAndOsw(EventLog s) => CompetitionWarrior(s.Unit) != null && AttractedWarrior(s.Unit) != null;
    public static bool ReqTargetCompatible(EventLog s) => RomanticTarget(s.Unit, s.Target);
    public static bool ReqTargetCompatibleLewd(EventLog s) => RomanticTarget(s.Unit, s.Target) && Lewd(s);
    public static bool ReqTargetClothingOn(EventLog s) => s.Target.ClothingType != 0;
    public static bool ReqTargetClothingOff(EventLog s) => s.Target.ClothingType == 0;
    public static bool WeightGain(EventLog s) => Config.WeightGain;
    public static bool BonesDisposal(EventLog s) => Config.Bones && (s.PreyLocation == PreyLocation.Stomach || s.PreyLocation == PreyLocation.Stomach2);
    public static bool TargetBoobs(EventLog s) => s.Target.HasBreasts;
    public static bool ActorBoobs(EventLog s) => s.Unit.HasBreasts;
    public static bool ActorTail(EventLog s) => RaceParameters.GetTraitData(s.Unit).HasTail;
    public static bool TargetLeader(EventLog s) => s.Target.Type == UnitType.Leader;
    public static bool ActorLeader(EventLog s) => s.Unit.Type == UnitType.Leader;
    public static bool TargetHumanoid(EventLog s) => RaceFuncs.IsHumanoid(s.Target.Race);

    public static bool CanAddressPlayer(EventLog s) =>
        Config.FourthWallBreakType == FourthWallBreakType.On ||
        (!TacticalUtilities.IsUnitControlledByPlayer(s.Unit) && Config.FourthWallBreakType == FourthWallBreakType.EnemyOnly) ||
        (TacticalUtilities.IsUnitControlledByPlayer(s.Unit) && Config.FourthWallBreakType == FourthWallBreakType.FriendlyOnly);

    /*
    /// <summary>
    /// <para>Gets a descriptive string that fits sentences like "Edmond stuffs Sidney down his maw, enjoying the * morsels squirms on her way down."</para>
    /// <para>Generally meant for the prey/loser/weaker unit. Has mostly demeaning, belittling, weakness indicating or fear portraying terms.</para>
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string GetPreyDesc(Unit unit)
    {
        return LogRaceData.Get(unit.Race).GetPreyDescription(unit) + "$";
    }

    /// <summary>
    ///<para>Gets a descriptive string that fits sentences like "Edmond stuffs Sidney down his maw, the prey filling his * body nicely."</para>
    ///<para>Generally meant for the predator/winner/stronger unit. Strength describing, contentment/pleasure indicating, etc. terms.</para>
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string GetPredDesc(Unit unit)
    {
        return LogRaceData.Get(unit.Race).GetPredDescription(unit) + "$";
    }

    /// <summary>
    /// <para>Gets a descriptive string that fits situations like "Jeanne graps Timothy's head, pushing the *'s face in her slit and soon forcing rest of him after it."</para>
    /// <para>This is either the species name, a name of the genus the species belongs to or something similar. Can also be a synonym of the species name.</para>
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string GetRaceDescSingl(Unit unit)
    {
        return LogRaceData.Get(unit.Race).GetRaceSingleDescription(unit) + "$";
    }

    /// <summary>
    /// Gets a name that fits the weapon the unit's graphics show it using.
    /// </summary>
    /// <param name="weapon"></param>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string GetWeaponTrueName(Weapon weapon, Unit unit)
    {
        return LogRaceData.Get(unit.Race).GetWeaponTrueName(weapon, unit) + "$";
    }
    */

    /*
    /// <summary>
    /// <para>Gets a descriptive string that fits sentences like "Edmond stuffs Sidney down his maw, enjoying the * morsels squirms on her way down."</para>
    /// <para>Generally meant for the prey/loser/weaker unit. Has mostly demeaning, belittling, weakness indicating or fear portraying terms.</para>
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string GetPreyDesc(Unit unit)
    {
        switch (RaceFuncs.RaceToSwitch(unit.Race))
        {
            case RaceNumbers.Cats:
                return GetRandomStringFrom("whiskered", "hissing", "bristle tailed");
            case RaceNumbers.Dogs:
                return GetRandomStringFrom("yelping", "curly tailed", "whining", "domesticated");
            case RaceNumbers.Youko:
            case RaceNumbers.Foxes:
                return GetRandomStringFrom("fluffy tailed", "squirming", "whimpering");
            case RaceNumbers.Wolves:
                return GetRandomStringFrom("wild", "growling", "wet furred");
            case RaceNumbers.Bunnies:
                return GetRandomStringFrom("long eared", "bushy tailed", "leaf biting");
            case RaceNumbers.Lizards:
                return GetRandomStringFrom("hairless", "cold-blooded", "wiry");
            case RaceNumbers.Slimes:
                return GetRandomStringFrom("amorphous", "sludgy", "juicy");
            case RaceNumbers.Scylla:
                return GetRandomStringFrom("loose limbed", "aquatic", "ten-limbed");
            case RaceNumbers.Harpies:
                return GetRandomStringFrom("feathered", "keening", "grounded");
            case RaceNumbers.Imps:
                return GetRandomStringFrom("infernal", "diminutive", "sized");
            case RaceNumbers.Humans:
                return GetRandomStringFrom("bare skinned", "soft", "shouting");
            case RaceNumbers.Crypters:
                return GetRandomStringFrom("mechanical", "artifical", "whirring");
            case RaceNumbers.Lamia:
                return GetRandomStringFrom("scaly", "noodly", "double-tasty");
            case RaceNumbers.Kangaroos:
                return GetRandomStringFrom("bottom heavy", unit.DefaultBreastSize > 0 ? "pouched" : "pouchless", "long legged");
            case RaceNumbers.Taurus:
                return GetRandomStringFrom("mooing", "bulky", "hooved");
            case RaceNumbers.Crux:
                return GetRandomStringFrom("crazy", "curly eared", "complaining"); // ---------------------------------------------------------
            case RaceNumbers.Succubi:
                return GetRandomStringFrom("devilishly tasty", "beguiling", "batty");
            case RaceNumbers.Tigers:
                return GetRandomStringFrom("striped", "roaring", "mewling");
            case RaceNumbers.Goblins:
                return GetRandomStringFrom("diminutive", "cursing", "short");
            case RaceNumbers.Alligators:
                return GetRandomStringFrom("crocodilian", "lumbering", "swampy");
            case RaceNumbers.Vagrants:
                return GetRandomStringFrom("tentacled", "rubbery", "alien");
            case RaceNumbers.Serpents:
                return GetRandomStringFrom("limbless", "noodly", "slithery");
            case RaceNumbers.Wyvern:
                return GetRandomStringFrom("winged", "horned", "wiry");
            case RaceNumbers.YoungWyvern:
                return GetRandomStringFrom("plumb", "soft scaled", "stretchy");
            case RaceNumbers.Compy:
                return GetRandomStringFrom("tiny", "chirping", "overambitious");
            case RaceNumbers.FeralSharks:
                return GetRandomStringFrom("finned", "torpedo shaped", "chompy");
            case RaceNumbers.FeralWolves:
                return GetRandomStringFrom("shaggy", "gamey", "growling");
            case RaceNumbers.Selicia:
                return GetRandomStringFrom("mighty tasty", "smooth scaled", "huge", "flexible", "formerly mighty", "surprisingly edible");
            case RaceNumbers.EasternDragon:
                return GetRandomStringFrom("tasty noodle", "noodle derg", "spaghetti-like", "easily-slurpable"); ////new, many thanks to Flame_Valxsarion
            case RaceNumbers.Dragon:
                return GetRandomStringFrom("formerly apex predator", "delicious dragon", "ex-predator"); ////new
            case RaceNumbers.FeralLions:
                return GetRandomStringFrom("roaring", "once-vicious", "formerly-fearsome");
            case RaceNumbers.Aabayx:
                return GetRandomStringFrom("strange-headed", "humbled viroid", "awkward-shaped");
            default:
                return "tasty";
        }
    }

    /// <summary>
    ///<para>Gets a descriptive string that fits sentences like "Edmond stuffs Sidney down his maw, the prey filling his * body nicely."</para>
    ///<para>Generally meant for the predator/winner/stronger unit. Strength describing, contentment/pleasure indicating, etc. terms.</para>
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string GetPredDesc(Unit unit)
    {
        switch (RaceFuncs.RaceToSwitch(unit.Race))
        {
            case RaceNumbers.Cats:
                return GetRandomStringFrom("purring", "sharp-toothed", "whiskered");
            case RaceNumbers.Dogs:
                return GetRandomStringFrom("wagging", "panting");
            case RaceNumbers.Youko:
            case RaceNumbers.Foxes:
                return GetRandomStringFrom("cunning", "grinning", "sly");
            case RaceNumbers.Wolves:
                return GetRandomStringFrom("spirited", "panting", "long furred");
            case RaceNumbers.Bunnies:
                return GetRandomStringFrom("sharp eared", "strong footed", "chisel-toothed");
            case RaceNumbers.Lizards:
                return GetRandomStringFrom("thick-scaled", "cold-blooded", "tough");
            case RaceNumbers.Slimes:
                return GetRandomStringFrom("amorphous", "flowing", "hard-cored");
            case RaceNumbers.Scylla:
                return GetRandomStringFrom("tentacled", "aquatic", "ten-limbed");
            case RaceNumbers.Harpies:
                return GetRandomStringFrom("winged", "screeching", "taloned");
            case RaceNumbers.Imps:
                return GetRandomStringFrom("infernal", "deceptive", "devious");
            case RaceNumbers.Humans:
                return GetRandomStringFrom("adaptive", "clever", "resourceful");
            case RaceNumbers.Crypters:
                return GetRandomStringFrom("mechanical", "artifical", "rumbling");
            case RaceNumbers.Lamia:
                return GetRandomStringFrom("scaly", "long bodied", "sizeable");
            case RaceNumbers.Kangaroos:
                return GetRandomStringFrom("thick tailed", unit.DefaultBreastSize > 0 ? "pouched" : "long legged", "black clawed");
            case RaceNumbers.Taurus:
                return GetRandomStringFrom("multi-stomached", "heavy", "strong legged");
            case RaceNumbers.Crux:
                return GetRandomStringFrom("curly eared", "crazed", "eager"); // ---------------------------------------------------------------------------------
            case RaceNumbers.Succubi:
                return GetRandomStringFrom("demonic", "beguiling", "bat-winged");
            case RaceNumbers.Tigers:
                return GetRandomStringFrom("striped", "roaring", "sharp toothed");
            case RaceNumbers.Goblins:
                return GetRandomStringFrom("stronger than looks", "knee kicking", "smart");
            case RaceNumbers.Alligators:
                return GetRandomStringFrom("armoured", "large jawed", "swampy");
            case RaceNumbers.Vagrants:
                return GetRandomStringFrom("alien", "stretchy", "translucent");
            case RaceNumbers.Serpents:
                return GetRandomStringFrom("scaly", "long bodied", "slithering");
            case RaceNumbers.Wyvern:
                return GetRandomStringFrom("mighty", "spined", "great-winged");
            case RaceNumbers.YoungWyvern:
                return GetRandomStringFrom("grinning", "expansive", "rubbery");
            case RaceNumbers.Compy:
                return GetRandomStringFrom("energetic", "tanuki shaming", "ambitious");
            case RaceNumbers.FeralSharks:
                return GetRandomStringFrom("large jawed", "rough scaled", "sharp finned");
            case RaceNumbers.FeralWolves:
                return GetRandomStringFrom("long furred", "spirited", "panting");
            case RaceNumbers.Selicia:
                return GetRandomStringFrom("wide mawed", "smooth scaled", "stretchy", "huge", "impressive", "all-too-eager", "mighty");
            case RaceNumbers.Dragon:
                return GetRandomStringFrom("apex predator", "hungry dragon", "voracious dragon");
            case RaceNumbers.FeralLions:
                return GetRandomStringFrom("indulgent", "greedily snarling", "voracious", "capacious", "insatiable", "dominant", "pleased"); ////new
            default:
                return "strong";
        }
    }

    /// <summary>
    /// <para>Gets a descriptive string that fits situations like "Jeanne graps Timothy's head, pushing the *'s face in her slit and soon forcing rest of him after it."</para>
    /// <para>This is either the species name, a name of the genus the species belongs to or something similar. Can also be a synonym of the species name.</para>
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string GetRaceDescSingl(Unit unit)
    {
        switch (RaceFuncs.RaceToSwitch(unit.Race))
        {
            case RaceNumbers.Cats:
                return GetRandomStringFrom("cat", GetGenderString(unit, "queen", "tom", "cat"), "feline");
            case RaceNumbers.Dogs:
                return GetRandomStringFrom("dog", GetGenderString(unit, "bitch", "dog", "dog"), "canine");
            case RaceNumbers.Youko:
            case RaceNumbers.Foxes:
                return GetRandomStringFrom("fox", GetGenderString(unit, "vixen", "tod", "fox"), "vulpine", "canid");
            case RaceNumbers.Wolves:
                return GetRandomStringFrom("feral", GetGenderString(unit, "wolfess", "wolf", "wolf"), "canine"); ////I changed "wolfen" to "wolfess"
            case RaceNumbers.Bunnies:
                return GetRandomStringFrom("bunny", GetGenderString(unit, "doe", "buck", "lagomorph"), "rabbit");
            case RaceNumbers.Deer:
                return GetRandomStringFrom(GetGenderString(unit, GetRandomStringFrom("doe", "roe"), GetRandomStringFrom("buck", "stag", "hart"), "cervid"), "faun", "deer");
            case RaceNumbers.Lizards:
                return GetRandomStringFrom("lizard", "reptile", "reptilian");
            case RaceNumbers.Slimes:
                return GetRandomStringFrom("slime", "ooze", "jelly");
            case RaceNumbers.Scylla:
                return GetRandomStringFrom("scylla", "octopod", "aquanoid");
            case RaceNumbers.Harpies:
                return GetRandomStringFrom("harpy", "raptor", "harpyia");
            case RaceNumbers.Imps:
                return GetRandomStringFrom("imp", "infernal being", "small demon"); ////added "small demon"
            case RaceNumbers.Humans:
                return GetRandomStringFrom("human", GetGenderString(unit, "woman", "man", "human"), "humanoid");
            case RaceNumbers.Crypters:
                return GetRandomStringFrom("crypter", "machinoid", "synthetic", "robotic", "metallic", "futuristic", "fabricated");////added "synthetic", "robotic", "metallic", "futuristic", "fabricated" thanks to Flame_Valxsarion
            case RaceNumbers.Lamia:
                return GetRandomStringFrom("lamia", "serpent", "half-snake");
            case RaceNumbers.Kangaroos:
                return GetRandomStringFrom("kangaroo", unit.HasBreasts ? "flyer" : "boomer", "'roo", "marsupial");
            case RaceNumbers.Taurus:
                return GetRandomStringFrom("bovine", GetGenderString(unit, "cow", "bull", "taurus"), "taurus");
            case RaceNumbers.Crux:
                return GetRandomStringFrom("crux", "lab-critter", "gene-engineered creature"); // --------------------------------------------------------------------------
            case RaceNumbers.Succubi:
                return GetRandomStringFrom("succubus", "demon", "hellish being");
            case RaceNumbers.Tigers:
                return GetRandomStringFrom("feline", GetGenderString(unit, "tigress", "tiger", "tiger"), "large feline");
            case RaceNumbers.Goblins:
                return GetRandomStringFrom("goblin", "goblinoid", "humanoid");
            case RaceNumbers.Alligators:
                return GetRandomStringFrom("'gator", "alligator", "crocodilian", "reptile");
            case RaceNumbers.Puca:
                return GetRandomStringFrom("puca", "bunny", "lagomorph", "digger");
            case RaceNumbers.Vagrants:
                return GetRandomStringFrom("vagrant", "jellyfish", "medusa");
            case RaceNumbers.Serpents:
                return GetRandomStringFrom("serpent", "snake", "slitherer");
            case RaceNumbers.Wyvern:
                return GetRandomStringFrom("wyvern", "lesser draconic being", "drake");
            case RaceNumbers.YoungWyvern:
                return GetRandomStringFrom("young wyvern", "wyverling", "small wyvern");
            case RaceNumbers.Compy:
                return GetRandomStringFrom("compy", "compsognathus", "dinosaur", "tiny dino");
            case RaceNumbers.FeralSharks:
                return GetRandomStringFrom("skyshark", "shark", "great fish");
            case RaceNumbers.FeralWolves:
                return GetRandomStringFrom("feral", GetGenderString(unit, "wolfess", "wolf", "wolf"), "canine"); ////I changed "wolfen" to "wolfess"
            case RaceNumbers.Cake:
                return GetRandomStringFrom("cake", "baked good", "ghostly confectionary", "delicious dessert");
            case RaceNumbers.Ki:
                return GetRandomStringFrom("small creature", "furry critter");
            case RaceNumbers.Vision:
                return GetRandomStringFrom("alien", "dinosaur");
            case RaceNumbers.Harvesters:
                return GetRandomStringFrom("alien", "harvester");
            case RaceNumbers.Collectors:
                return GetRandomStringFrom("alien", "quadpod");
            case RaceNumbers.Selicia:
                return GetRandomStringFrom("dragon", "salamander dragon", "derg");
            case RaceNumbers.Equines:
                return GetRandomStringFrom("equine", GetGenderString(unit, "mare", "stallion", "horse"), "bronco"); ////new
            case RaceNumbers.Sergal:
                return GetRandomStringFrom("furred", "sergal", "Eltussian"); ////new, many thanks to Flame_Valxsarion
            case RaceNumbers.Dragon:
                return GetRandomStringFrom("dragon", GetGenderString(unit, "dragoness", "drakon", "dragon"), "draconian"); ////new
            case RaceNumbers.EasternDragon:
                return GetRandomStringFrom("oriental dragon", GetGenderString(unit, "eastern dragoness", "eastern dragon", "eastern dragon"), "serpentine dragon");  ////new
            case RaceNumbers.Zera:
                return GetRandomStringFrom("nargacuga", "fluffy wyvern", "big kitty"); ////new, many thanks to Selicia for the last two
            case RaceNumbers.Hippos:
                return GetRandomStringFrom("hippo", "hippopotamus", "pachyderm"); ////new
            case RaceNumbers.Komodos:
                return GetRandomStringFrom("komodo", "komodo dragon", "komodo lizard"); ////new
            case RaceNumbers.Cockatrice:
                return GetRandomStringFrom("cockatrice", GetGenderString(unit, "scary hen", "monster cock", "danger chicken"), "terror chicken"); ////new, blame Flame_Valxsarion for encouraging me. Actually don't, I came up with "monster cock"
            case RaceNumbers.Bees:
                return GetRandomStringFrom("apid", GetGenderString(unit, "worker bee", "drone", "bee"), "bee"); ////new
            case RaceNumbers.Alraune:
                return GetRandomStringFrom("plant", "demi-plant", "flowery being"); ////new
            case RaceNumbers.Bats:
                return GetRandomStringFrom("bat", "chiropter", "demi-bat"); ////new
            case RaceNumbers.Merfolk:
                return GetRandomStringFrom("walking fish", GetGenderString(unit, "mermaid", "merman", "merfolk"), "merfolk"); ////new
            case RaceNumbers.Sharks:
                return GetRandomStringFrom("demi-shark", "shark", "landshark"); ////new
            case RaceNumbers.Gryphons:
                return GetRandomStringFrom("gryphon", "griffin", "griffon"); ////new
            case RaceNumbers.Kobolds:
                return GetRandomStringFrom("kobold", "little lizard", "little reptile"); ////new
            case RaceNumbers.Frogs:
                return GetRandomStringFrom("demi-frog", "amphibian", "frog"); ////new, many thanks to Flame_Valxsarion
            case RaceNumbers.FeralLions:
                return GetRandomStringFrom("feline", GetGenderString(unit, "lioness", "lion", "lion"), "leonine", "kitty");
            case RaceNumbers.Aabayx:
                return GetRandomStringFrom("viroid", "virosapien", "dice-like", "math-obsessed"); ////new, and probably wrong
            default:
                return "creature";
        }
    }

    /// <summary>
    /// Gets a name that fits the weapon the unit's graphics show it using.
    /// </summary>
    /// <param name="weapon"></param>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string GetWeaponTrueName(Weapon weapon, Unit unit)
    {
        if (unit.Race == Race.Vagrants) return "Stinger";
        else if (unit.Race == Race.Wyvern) return "Claws";
        else if (unit.Race == Race.YoungWyvern) return "Beak";
        else if (unit.Race == Race.Serpents) return "Tail Blade";
        else if (unit.Race == Race.FeralSharks) return "Jaws";
        else if (unit.Race == Race.FeralWolves) return "Fangs";
        else if (unit.Race == Race.DarkSwallower) return "Jaws";
        else if (unit.Race == Race.Harvesters) return "Scythes";
        else if (unit.Race == Race.Collectors) return "Maw";
        else if (unit.Race == Race.Ki) return "Jaws";
        else if (unit.Race == Race.Selicia) return "Claws";

        else if (unit.Race == Race.Kangaroos)
        {
            if (weapon.Name == "Mace") return "Club";
            else if (weapon.Name == "Axe") return "Spear";
            else if (weapon.Name == "Simple Bow") return "Boomerang";
            else if (weapon.Name == "Compound Bow") return "Woomera";
            else if (weapon.Name == "Claw") return "Claws";
        }
        else if (unit.Race == Race.Goblins)
        {
            if (weapon.Name == "Mace") return "Cleaver";
            else if (weapon.Name == "Axe") return "Sharpened Cleaver";
            else if (weapon.Name == "Simple Bow") return "Derringer";
            else if (weapon.Name == "Compound Bow") return "Pepperbox Pistol";
            else if (weapon.Name == "Claw") return "Fist";
        }
        else if (unit.Race == Race.Slimes)
        {
            if (weapon.Name == "Mace") return "Bone Blade";
            else if (weapon.Name == "Axe") return "Whip";
            else if (weapon.Name == "Simple Bow") return "Floating Slimey";
            else if (weapon.Name == "Compound Bow") return "Bioelectricity";
            else if (weapon.Name == "Claw") return "Hardened Lump";
        }
        else if (unit.Race == Race.Imps)
        {
            if (weapon.Name == "Mace") return "Morningstar";
            else if (weapon.Name == "Axe") return "Cleaver";
            else if (weapon.Name == "Simple Bow") return "Bow";
            else if (weapon.Name == "Compound Bow") return "Infernal Bow";
            else if (weapon.Name == "Claw") return "Fist";
        }
        else if (unit.Race == Race.Crypters)
        {
            if (weapon.Name == "Mace") return "Sword";
            else if (weapon.Name == "Axe") return "Power Fist";
            else if (weapon.Name == "Simple Bow") return "Crossbow";
            else if (weapon.Name == "Compound Bow") return "Cannon";
            else if (weapon.Name == "Claw") return "Metal Fist";
        }
        else if (unit.Race == Race.Scylla)
        {
            if (weapon.Name == "Mace") return "Knife";
            else if (weapon.Name == "Axe") return "Trident";
            else if (weapon.Name == "Simple Bow") return "Javelin";
            else if (weapon.Name == "Compound Bow") return "Medusa Launcher";
            else if (weapon.Name == "Claw") return "Tentacle";
        }
        else if (unit.Race == Race.Harpies)
        {
            if (weapon.Name == "Mace") return "Bronze Claws";
            else if (weapon.Name == "Axe") return "Steel Claws";
            else if (weapon.Name == "Simple Bow") return "Simple Bow";
            else if (weapon.Name == "Compound Bow") return "Compound Bow";
            else if (weapon.Name == "Claw") return "Talons";
        }
        else if (unit.Race == Race.Taurus)
        {
            if (weapon.Name == "Mace") return "Hammer";
            else if (weapon.Name == "Axe") return "Glaive";
            else if (weapon.Name == "Simple Bow") return "Revolver";
            else if (weapon.Name == "Compound Bow") return "Shotgun";
            else if (weapon.Name == "Claw") return "Fist";
        }
        else if (unit.Race == Race.Crux)
        {
            if (weapon.Name == "Mace" && unit.BasicMeleeWeaponType == 0) return "Bat";
            else if (weapon.Name == "Mace" && unit.BasicMeleeWeaponType == 1) return "Pipe";
            else if (weapon.Name == "Mace" && unit.BasicMeleeWeaponType == 2) return "Dildo";
            else if (weapon.Name == "Axe" && unit.AdvancedMeleeWeaponType == 0) return "Machete";
            else if (weapon.Name == "Axe" && unit.AdvancedMeleeWeaponType == 1) return "Axe";
            else if (weapon.Name == "Simple Bow") return "Handbow";
            else if (weapon.Name == "Compound Bow") return "Compound Bow";
            else if (weapon.Name == "Claw") return "Claws";
        }
        else if (unit.Race == Race.Alligators)
        {
            if (weapon.Name == "Mace") return "Turtle Club";
            else if (weapon.Name == "Axe") return "Flint Spear";
            else if (weapon.Name == "Claw") return "Claws";
        }
        else if (unit.Race == Race.Puca)
        {
            if (weapon.Name == "Mace") return "Shovel";
            else if (weapon.Name == "Axe") return "Shovel";
            else if (weapon.Name == "Simple Bow") return "Slingshot";
            else if (weapon.Name == "Compound Bow") return "Heavy Slingshot";
        }
        else if (unit.Race == Race.Vipers)
        { //V33B ADDITION
            if (weapon.Name == "Mace") return "Arc Blade";
            else if (weapon.Name == "Axe") return "Fusion Blade";
            else if (weapon.Name == "Simple Bow") return "Plasma Pistol";
            else if (weapon.Name == "Compound Bow") return "Plasma Rifle";
        }
        else if (unit.Race == Race.Sergal)
        {
            if (weapon.Name == "Mace") return "Lance";
            else if (weapon.Name == "Axe") return "Twin Glaive";
            else if (weapon.Name == "Simple Bow") return "Speargun";
            else if (weapon.Name == "Compound Bow") return "Prototype Railgun"; ////changed to "prototype railgun", thanks to Flame_Valxsarion
        }
        else if (unit.Race == Race.Bees)
        {
            if (weapon.Name == "Mace") return "Honeycomb Mace";
            else if (weapon.Name == "Axe") return "Quad Punch Claws";
            else if (weapon.Name == "Simple Bow") return "Javelin";
            else if (weapon.Name == "Compound Bow") return "War Javelin";
        }
        else if (unit.Race == Race.Driders)
        {
            if (weapon.Name == "Mace") return "Dagger";
            else if (weapon.Name == "Axe") return "Short Sword";
            else if (weapon.Name == "Simple Bow") return "Pistol Crossbow";
            else if (weapon.Name == "Compound Bow") return "Crossbow";
        }
        else if (unit.Race == Race.Alraune)
        {
            if (weapon.Name == "Mace") return "Vine Whip";
            else if (weapon.Name == "Axe") return "Stem Blade";
            else if (weapon.Name == "Simple Bow") return "Unbloomed Corolla";
            else if (weapon.Name == "Compound Bow") return "Blooming Flower";
        }
        else if (unit.Race == Race.Bats)
        {
            if (weapon.Name == "Mace") return "Push Dagger";
            else if (weapon.Name == "Axe") return "Claw Katar";
            else if (weapon.Name == "Simple Bow") return "Iron Throwing Knife";
            else if (weapon.Name == "Compound Bow") return "Steel Throwing Knife";
        }
        else if (unit.Race == Race.Panthers)
        {
            if (weapon.Name == "Mace") return "Karambit";
            else if (weapon.Name == "Axe") return "Kukri";
            else if (weapon.Name == "Simple Bow") return "Chakram";
            else if (weapon.Name == "Compound Bow") return "Onzil";
        }
        else if (unit.Race == Race.Merfolk)
        {
            if (weapon.Name == "Mace") return "Crude Trident";
            else if (weapon.Name == "Axe") return "Royal Trident";
            else if (weapon.Name == "Simple Bow") return "Scepter";
            else if (weapon.Name == "Compound Bow") return "Orb Staff";
        }
        else if (unit.Race == Race.Ants)
        {
            if (weapon.Name == "Mace") return "Barbed Spear";
            else if (weapon.Name == "Axe") return "Quad Blades";
            else if (weapon.Name == "Simple Bow") return "Simple Bow";
            else if (weapon.Name == "Compound Bow") return "Compound Bow";
        }
        else if (unit.Race == Race.Avians)
        {
            if (weapon.Name == "Mace") return "Knife";
            else if (weapon.Name == "Axe") return "Sword";
            else if (weapon.Name == "Simple Bow") return "Short Bow";
            else if (weapon.Name == "Compound Bow") return "Crossbow";
        }
        else if (unit.Race == Race.Sharks)
        {
            if (weapon.Name == "Mace") return "Sabre";
            else if (weapon.Name == "Axe") return "Cutlass";
            else if (weapon.Name == "Simple Bow") return "Harpoon";
            else if (weapon.Name == "Compound Bow") return "Speargun";
        }
        else if (unit.Race == Race.Frogs)
        {
            if (weapon.Name == "Mace") return "Mace";
            else if (weapon.Name == "Axe") return "Axe";
            else if (weapon.Name == "Simple Bow") return "Slingshot";
            else if (weapon.Name == "Compound Bow") return "Feathered Bow";
            else if (weapon.Name == "Claw") return "Fist";
        }
        else if (unit.Race == Race.Hippos)
        {
            if (weapon.Name == "Mace") return "Tribal Knife";
            else if (weapon.Name == "Axe") return "Axe";
            else if (weapon.Name == "Simple Bow") return "Simple Bow";
            else if (weapon.Name == "Compound Bow") return "Compound Bow";
            else if (weapon.Name == "Claw") return "Fist";
        }
        else if (unit.Race == Race.Kobolds)
        {
            if (weapon.Name == "Mace") return "Pickax";
            else if (weapon.Name == "Axe") return "Pickax";
            else if (weapon.Name == "Simple Bow") return "Dart";
            else if (weapon.Name == "Compound Bow") return "Dart";
            else if (weapon.Name == "Claw") return "Fist";
        }
        else if (unit.Race == Race.Aabayx)
        {
            if (weapon.Name == "Mace") return "Longscalpel";
            else if (weapon.Name == "Axe") return "Personsmasher";
            else if (weapon.Name == "Simple Bow") return "Razorflinger";
            else if (weapon.Name == "Compound Bow") return "Arrowbisector";
            else if (weapon.Name == "Claw") return "Fist";
        }
        else if (weapon.Name == "Claw") return "Claws";
        return weapon.Name;
    }
    */
}