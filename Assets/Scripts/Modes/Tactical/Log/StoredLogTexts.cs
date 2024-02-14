using System;
using System.Collections.Generic;
using System.Linq;
using static LogUtilities;
using static TacticalMessageLog;

internal static class StoredLogTexts
{
    static StoredLogTexts()
    {
        InitializeLists();
    }

    internal enum MessageTypes
    {
        SwallowMessages,
        RandomDigestionMessages,
        BellyRubMessages,
        CockVoreMessages,
        AnalVoreMessages,
        UnbirthMessages,
        TailVoreMessages,
        DigestionDeathMessages,
        BreastVoreMessages,
        AbsorptionMessages,
        BreastRubMessages,
        TailRubMessages,
        BallMassageMessages,
        TransferMessages,
        VoreStealMessages,
        BreastFeedMessages,
        CumFeedMessages,
        GreatEscapeKeep,
        GreatEscapeFlee,
    }

    internal static List<EventString> Redirect(MessageTypes type)
    {
        switch (type)
        {
            case MessageTypes.SwallowMessages:
                return SwallowMessages;
            case MessageTypes.RandomDigestionMessages:
                return RandomDigestionMessages;
            case MessageTypes.BellyRubMessages:
                return BellyRubMessages;
            case MessageTypes.CockVoreMessages:
                return CockVoreMessages;
            case MessageTypes.AnalVoreMessages:
                return AnalVoreMessages;
            case MessageTypes.UnbirthMessages:
                return UnbirthMessages;
            case MessageTypes.TailVoreMessages:
                return TailVoreMessages;
            case MessageTypes.DigestionDeathMessages:
                return DigestionDeathMessages;
            case MessageTypes.BreastVoreMessages:
                return BreastVoreMessages;
            case MessageTypes.AbsorptionMessages:
                return AbsorptionMessages;
            case MessageTypes.BreastRubMessages:
                return BreastRubMessages;
            case MessageTypes.TailRubMessages:
                return TailRubMessages;
            case MessageTypes.BallMassageMessages:
                return BallMassageMessages;
            case MessageTypes.TransferMessages:
                return TransferMessages;
            case MessageTypes.VoreStealMessages:
                return VoreStealMessages;
            case MessageTypes.BreastFeedMessages:
                return BreastFeedMessages;
            case MessageTypes.CumFeedMessages:
                return CumFeedMessages;
            case MessageTypes.GreatEscapeKeep:
                return GreatEscapeKeepMessages;
            case MessageTypes.GreatEscapeFlee:
                return GreatEscapeFleeMessages;
            default:
                return SwallowMessages;
        }
    }

    internal static List<EventString> SwallowMessages;
    internal static List<EventString> RandomDigestionMessages;
    internal static List<EventString> BellyRubMessages;
    internal static List<EventString> CockVoreMessages;
    internal static List<EventString> AnalVoreMessages;
    internal static List<EventString> UnbirthMessages;
    internal static List<EventString> TailVoreMessages;
    internal static List<EventString> DigestionDeathMessages;
    internal static List<EventString> BreastVoreMessages;
    internal static List<EventString> AbsorptionMessages;
    internal static List<EventString> BreastRubMessages;
    internal static List<EventString> TailRubMessages;
    internal static List<EventString> BallMassageMessages;
    internal static List<EventString> TransferMessages;
    internal static List<EventString> VoreStealMessages;
    internal static List<EventString> BreastFeedMessages;
    internal static List<EventString> CumFeedMessages;
    internal static List<EventString> GreatEscapeKeepMessages;
    internal static List<EventString> GreatEscapeFleeMessages;


    internal static void InitializeLists()
    {
        if (SwallowMessages != null) return;

        SwallowMessages = global::SwallowMessages.SwallowMessagesList;
        RandomDigestionMessages = global::RandomDigestionMessages.RandomDigestionMessagesList;
        BellyRubMessages = global::BellyRubMessages.BellyRubMessagesList;

        BreastRubMessages = new List<EventString>()
        {
            new EventString((i) => $"<b>{i.Unit.Name}</b> massages {(i.Unit == i.Target ? GPPHis(i.Target) : "<b>" + i.Target.Name + "</b>'s")} full breasts.", priority: 8),
            new EventString((i) => $"<b>{i.Unit.Name}</b> massages {(i.Unit == i.Target ? GPPHis(i.Target) : "<b>" + i.Target.Name + "</b>'s")} full breasts, milk leaking out of {(i.Unit == i.Target ? GPPHis(i.Target) : "<b>" + i.Target.Name + "</b>'s")} engorged nipples.", priority: 8),
            new EventString((i) => $"<b>{i.Unit.Name}</b> grabs {GPPHis(i.Unit)} {i.preyLocation.ToSyn()}, lifting them up and then letting them bounce against {GPPHis(i.Unit)} chest, sloshing their contents about!", priority: 8, conditional: s => s.Target == s.Unit),
            new EventString((i) => $"With a happy sigh, <b>{i.Unit.Name}</b> places {GPPHis(i.Unit)} arms behind {GPPHis(i.Unit)} back and shakes {GPPHis(i.Unit)} torso vigorusly, giving {GPPHis(i.Unit)} {i.preyLocation.ToSyn()} a good stirring. ", priority: 8, conditional: s => s.Target == s.Unit),

            //Kangaroos (pouch vore) belly rubs

            // Live Prey

            // Other Ver. A
            new EventString((i) => $"<b>{i.Unit.Name}</b> approaches <b>{i.Target.Name}</b> and rubs {GPPHis(i.Target)} still squirming {GetRandomStringFrom("lower torso", "pouch", "marsupium")}, feeling <b>{i.Prey.Name}</b> within, and tiring <b>{i.Prey.Name}</b> out a little bit more.",
                actorRace: Race.Kangaroo, priority: 11, conditional: s => s.Target != s.Unit && !PreyDead(s)),
            // Other Ver. B
            new EventString((i) => $"<b>{i.Target.Name}</b> beckons <b>{i.Unit.Name}</b> over and asks {GPPHim(i.Unit)} to rub {GPPHis(i.Target)} still moving {GetRandomStringFrom("lower torso", "pouch", "marsupium")}. As <b>{i.Unit.Name}</b> rubs, they can feel the whole of <b>{i.Prey.Name}</b>'s body. <b>{i.Prey.Name}</b>, for {GPPHis(i.Prey)} part, breathes a little harder at <b>{i.Unit.Name}</b>'s touch, inadvertently wasting some of {GPPHis(i.Prey)} precious {GetRandomStringFrom("air", "oxygen", "O2")}.",
                actorRace: Race.Kangaroo, priority: 11, conditional: s => s.Target != s.Unit && !PreyDead(s)),
            // Other Ver. C: What do I even do about spines. Write a lookup for if races are invertebrate?
            //new EventString((i) => $"As <b>{i.Unit.Name}</b> walks past <b>{i.Target.Name}</b>, {GPPHim(i.Unit)} brush{EsIfSingular(i.Unit)} up against {GPPHis(i.Target)} full {GetRandomStringFrom("lower torso", "pouch", "marsupium")}, causing a shiver to go down the {spines/spine analogs<used when <b>{i.Prey.Name}</b> or <b>{i.Unit.Name}</b>(or I guess <b>{i.Target.Name}</b>) belong to races that lack spines>} of all three; <b>{i.Target.Name}</b>, <b>{i.Unit.Name}</b>, and <b>{i.Prey.Name}</b>.",
            //actorRace: Race.Kangaroos, priority: 11, conditional: s => s.Target != s.Unit && !PreyDead(s)),
            // Other Ver. D
            new EventString((i) => $"<b>{i.Target.Name}</b> has <b>{i.Unit.Name}</b> rub {GPPHis(i.Target)} still moving {GetRandomStringFrom("lower torso", "pouch", "marsupium")}, robbing <b>{i.Prey.Name}</b> of just a little more air.",
                actorRace: Race.Kangaroo, priority: 11, conditional: s => s.Target != s.Unit && !PreyDead(s)),
            // Other Ver. E
            new EventString((i) => $"As <b>{i.Unit.Name}</b> walks past <b>{i.Target.Name}</b>, {GPPHe(i.Unit)} brush{EsIfSingular(i.Unit)} up against {GPPHis(i.Target)} full {GetRandomStringFrom("lower torso", "pouch", "marsupium")}, pushing a little more air from <b>{i.Target.Name}</b>'s pouch.",
                actorRace: Race.Kangaroo, priority: 11, conditional: s => s.Target != s.Unit && !PreyDead(s)),

            // Self Ver. A
            new EventString((i) => $"As <b>{i.Prey.Name}</b> squirms within <b>{i.Unit.Name}</b>'s pouch, <b>{i.Unit.Name}</b> pushes down on {GPPHis(i.Unit)} pouch, forcing a little bit of <b>{i.Prey.Name}</b>'s limited air supply out with a barely perceptible -hwuh- sound.",
                actorRace: Race.Kangaroo, priority: 11, conditional: s => s.Target == s.Unit && !PreyDead(s)),
            // Self Ver. B & C: This one's future-proofed, huh? Also, mixed the two.
            new EventString((i) => $"<b>{i.Unit.Name}</b> decides to reach down and knead {GPPHis(i.Unit)} pouch, feeling <b>{i.Prey.Name}</b>'s form trapped inside{(State.Rand.Next(2) == 0 ? "" : " " + GPPHis(i.Unit) + " own")}. As {GPPHe(i.Unit)} do{EsIfSingular(i.Unit)} this, {GPPHe(i.Unit)} lean{SIfSingular(i.Unit)} in and whispers, \"I'd tell you your fate, but frankly, even us {i.Unit.Race} don't fully \'get\' how it works.{(State.Rand.Next(2) == 0 ? "" : " All I know is soon, you\'ll shrink down to nothing, except maybe a little fat.\"")}",
                actorRace: Race.Kangaroo, priority: 11, conditional: s => s.Target == s.Unit && !PreyDead(s)),
            // Self Ver. D
            new EventString((i) => $"<b>{i.Unit.Name}</b> decides to reach down and punch {GPPHis(i.Unit)} own pouch, knocking quite a bit of {GetRandomStringFrom("wind", "air")} out of <b>{i.Prey.Name}</b>.",
                actorRace: Race.Kangaroo, priority: 11, conditional: s => s.Target == s.Unit && !PreyDead(s)),
            // Self Ver. E
            new EventString((i) => $"<b>{i.Unit.Name}</b> kneads {GPPHis(i.Unit)} own pouch, forcing out a little more of <b>{i.Prey.Name}</b>'s{GetRandomStringFrom("", " limited")} air{GetRandomStringFrom("", " supply")}.",
                actorRace: Race.Kangaroo, priority: 11, conditional: s => s.Target == s.Unit && !PreyDead(s)),

            // Dead Prey

            // Other Ver. A
            new EventString((i) => $"<b>{i.Unit.Name}</b> approaches <b>{i.Target.Name}</b> and rubs {GPPHis(i.Target)} {GetRandomStringFrom("lower torso", "pouch", "marsupium")}. Curiously, the bulge <b>{i.Prey.Name}</b> makes in <b>{i.Target.Name}</b>'s figure seems slightly smaller than before.",
                actorRace: Race.Kangaroo, priority: 11, conditional: s => s.Target != s.Unit && PreyDead(s)),
            // Other Ver. B
            new EventString((i) => $"<b>{i.Target.Name}</b> beckons <b>{i.Unit.Name}</b> over and asks {GPPHim(i.Unit)} to rub {GPPHis(i.Target)} still moving {GetRandomStringFrom("lower torso", "pouch", "marsupium")}. Once <b>{i.Unit.Name}</b> is done and walks away, the {GetRandomStringFrom("bump", "lump")} in <b>{i.Target.Name}</b>'s pouch seems slightly... smaller?",
                actorRace: Race.Kangaroo, priority: 11, conditional: s => s.Target != s.Unit && PreyDead(s)),
            // Other Ver. C: Same thing as the other one
            //new EventString((i) => $"As <b>{i.Unit.Name}</b> walks past <b>{i.Target.Name}</b>, {GPPHe(i.Unit)} brush{EsIfSingular(i.Unit)} up against {GPPHis(i.Target)} full {GetRandomStringFrom("lower torso", "pouch", "marsupium")}, causing a shiver to go down <b>{i.Target.Name}</b>'s [spine/spine analog]. Oddly, once <b>{i.Target.Name}</b> is done shaking, the lump <b>{i.Prey.Name}</b> makes in <b>{i.Target.Name}</b>'s figure seems somewhat reduced.",
            //actorRace: Race.Kangaroos, priority: 11, conditional: s => s.Target != s.Unit && PreyDead(s)),
            // Other Ver. D
            new EventString((i) => $"As <b>{i.Unit.Name}</b> rubs <b>{i.Target.Name}</b>'s pouch, the bulge <b>{i.Prey.Name}</b> makes in <b>{i.Target.Name}</b>'s figure curiously seems to grow slightly smaller than it was.",
                actorRace: Race.Kangaroo, priority: 11, conditional: s => s.Target != s.Unit && PreyDead(s)),
            // Other Ver. E
            new EventString((i) => $"<b>{i.Target.Name}</b> has <b>{i.Unit.Name}</b> rub {GPPHis(i.Target)} {GetRandomStringFrom("lower torso", "pouch", "marsupium")}, causing the bulge made by <b>{i.Prey.Name}</b> in <b>{i.Target.Name}</b>'s figure to shrink slightly.",
                actorRace: Race.Kangaroo, priority: 11, conditional: s => s.Target != s.Unit && PreyDead(s)),
            // Other Ver. F
            new EventString((i) => $"As <b>{i.Unit.Name}</b> walks past <b>{i.Target.Name}</b>, {GPPHe(i.Unit)} brush{EsIfSingular(i.Unit)} up against {GPPHis(i.Target)} {GetRandomStringFrom("lower torso", "pouch", "marsupium")}, causing the bulging pouch to go a little bit down.",
                actorRace: Race.Kangaroo, priority: 11, conditional: s => s.Target != s.Unit && PreyDead(s)),

            // Self Ver. A, B, & D: Combined. (D was also identical to A for some reason?)
            new EventString((i) => $"<b>{i.Unit.Name}</b> decides to reach down and knead {GPPHis(i.Unit)} pouch. {GetRandomStringFrom("Curiously", "Strangely", "Oddly")}, with each rub, the bulge of <b>{i.Prey.Name}</b> seems a {GetRandomStringFrom("touch", "bit")} {GetRandomStringFrom("smaller", "shrunken")}.",
                actorRace: Race.Kangaroo, priority: 11, conditional: s => s.Target == s.Unit && PreyDead(s)),
            // Self Ver. C
            new EventString((i) => $"<b>{i.Unit.Name}</b> decides to reach down and punch {GPPHis(i.Unit)} own pouch. For a moment, a dent is left in {GetRandomStringFrom("the shape", "what's left")} of <b>{i.Prey.Name}</b> in <b>{i.Unit.Name}</b>'s pouch, as though <b>{i.Prey.Name}</b> was made of putty.{(State.Rand.Next(2) == 0 ? "" : " Then the dent fills, though if one were observent, they would notice that <b>" + i.Unit.Name + "</b>'s pouch was smaller than before it was punched.")}",
                actorRace: Race.Kangaroo, priority: 11, conditional: s => s.Target == s.Unit && PreyDead(s)),
            // Self Ver. E
            new EventString((i) => $"<b>{i.Unit.Name}</b> decides to reach down and knead {GPPHis(i.Unit)} pouch, which oddly causes the bulge in said pouch to shrink.",
                actorRace: Race.Kangaroo, priority: 11, conditional: s => s.Target == s.Unit && PreyDead(s)),
            // Self Ver. F & H: Combined.
            new EventString((i) => $"<b>{i.Unit.Name}</b> decides to reach down and punch {GPPHis(i.Unit)} own pouch, forcing to get just a bit smaller.{(i.Prey.HealthPct > -0.75f ? "" : " Just a little bit more to go.")}",
                actorRace: Race.Kangaroo, priority: 11, conditional: s => s.Target == s.Unit && PreyDead(s)),
            // Self Ver. G
            new EventString((i) => $"<b>{i.Unit.Name}</b> decides to reach down and knead {GPPHis(i.Unit)} pouch.",
                actorRace: Race.Kangaroo, priority: 11, conditional: s => s.Target == s.Unit && PreyDead(s)),

            //succs
            new EventString((i) => $"The mere touch of <b>{i.Unit.Name}</b> is enough to make <b>{i.Target.Name}</b> gasp and quiver in pleasure.", actorRace: Race.Succubus, priority: 9, conditional: s => Equals(s.Target.Side, s.Unit.Side)),
            new EventString((i) => $"<b>{i.Unit.Name}</b> moans and hugs <b>{i.Target.Name}</b> tightly and grinds their breasts against one another, sending tingling bliss down both their spines.", actorRace: Race.Succubus, priority: 9, conditional: s => Equals(s.Target.Side, s.Unit.Side) && ReqTargetCompatibleLewd(s) && ActorBoobs(s)),
        };

        TailRubMessages = new List<EventString>()
        {
            new EventString((i) => $"<b>{i.Unit.Name}</b> strokes {(i.Unit == i.Target ? GPPHis(i.Target) : "<b>" + i.Target.Name + "</b>'s")} tail, feeling it clench and squeeze its delicious contents.", priority: 8),
            new EventString((i) => $"<b>{i.Unit.Name}</b> eagerly strokes {(i.Unit == i.Target ? GPPHis(i.Target) : "<b>" + i.Target.Name + "</b>'s")} tail, feeling the tasty burden getting lighter with each stroke.", priority: 8),
            new EventString((i) => $"<b>{i.Unit.Name}</b> soothes the sloshing in {(i.Unit == i.Target ? GPPHis(i.Target) : "<b>" + i.Target.Name + "</b>'s")} tail with a quick rub.", priority: 8),
            new EventString((i) => $"<b>{i.Unit.Name}</b> squeezes {GPPHis(i.Unit)} tail hard and sighs contently, feels the large mass finally slither into {GPPHis(i.Unit)} {PreyLocStrings.ToSyn(PreyLocation.stomach)}.", priority: 8, conditional: s => s.Target == s.Unit),
            new EventString((i) => $"With a grunt, <b>{i.Unit.Name}</b> pats {GPPHis(i.Unit)} tail to guide the digesting food on its way to {GPPHis(i.Unit)} {PreyLocStrings.ToSyn(PreyLocation.stomach)}.", priority: 8, conditional: s => s.Target == s.Unit),
            new EventString((i) => $"<b>{i.Unit.Name}</b> shifts {GPPHimself(i.Unit)} sideways so that {GPPHe(i.Unit)} can press down hard on {GPPHis(i.Unit)} tail, shifting it deeper into {GPPHis(i.Unit)} body for digestion.", priority: 8, conditional: s => s.Target == s.Unit),
            new EventString((i) => $"<b>{i.Unit.Name}</b> curls back to stroke the bulge on {GPPHis(i.Unit)} tail, slowly guiding the lump deeper inside {GPPHim(i.Unit)}.", priority: 8, conditional: s => s.Target == s.Unit),
            new EventString((i) => $"<b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> tail exhales a puff of acrid gas from the steady massages <b>{i.Unit.Name}</b> is giving it. <b>{i.Target.Name}</b> soon finds {GPPHimself(i.Unit)} dunked into strong stomach acids.", priority: 8, conditional: s => HardVore(s) && CanBurp(s)),

            // Can we do an OR for actorrace? 
            new EventString((i) => $"<b>{i.Unit.Name}</b> massages each lumpy bump on {(i.Unit == i.Target ? GPPHis(i.Target) : "<b>" + i.Target.Name + "</b>'s")} tail, enjoying each bulge slowly soften and flatten.", actorRace: Race.Viper, priority: 9),
            new EventString((i) => $"<b>{i.Unit.Name}</b> massages each lumpy bump on {(i.Unit == i.Target ? GPPHis(i.Target) : "<b>" + i.Target.Name + "</b>'s")} tail, enjoying each bulge slowly soften and flatten.", actorRace: Race.Lamia, priority: 9),
            new EventString((i) => $"<b>{i.Unit.Name}</b> swishes {GPPHis(i.Unit)} filled tail against the ground, eager for it to squeeze its prey into {GPPHis(i.Unit)} stomach for digestion.", priority: 9, actorRace: Race.Lamia, conditional: s => s.Target == s.Unit),


            //succs
            new EventString((i) => $"The mere touch of <b>{i.Unit.Name}</b> is enough to make <b>{i.Target.Name}</b> gasp and quiver in pleasure.", actorRace: Race.Succubus, priority: 9),
            new EventString((i) => $"<b>{i.Unit.Name}</b> passionately caresses the bulging length of {GPPHis(i.Unit)} tail, sending a burst of sensual sensations rippling down {GPPHis(i.Unit)} spine. \"Enjoy the ride my little snack.\" {GPPHe(i.Unit)} coo{SIfSingular(i.Unit)} softly.", actorRace: Race.Succubus, priority: 9, conditional: s => s.Target == s.Unit),
            new EventString((i) => $"<b>{i.Unit.Name}</b> stretches {GPPHis(i.Unit)} wings backwards, rubbing the wing span against {GPPHis(i.Unit)} stuffed tail to guide the contents into {GPPHis(i.Unit)} belly.", actorRace: Race.Succubus, priority: 9, conditional: s => s.Target == s.Unit),
            new EventString((i) => $"<b>{i.Unit.Name}</b> tucks {GPPHis(i.Unit)} bloated tail between {GPPHis(i.Unit)} legs then grinds {GPPHis(i.Unit)} {(i.Unit.HasDick ? "dick" : "pussy")} depravedly against it for <b>{AttractedWarrior(i.Unit)}</b> to see. \"Come join me my dear!\"", actorRace: Race.Succubus, priority: 9, conditional: s => s.Target == s.Unit && ReqOSWLewd(s)),
        };

        BallMassageMessages = new List<EventString>()
        {
            new EventString((i) => $"<b>{i.Unit.Name}</b> nuzzles each of {(i.Unit == i.Target ? GPPHis(i.Target) : "<b>" + i.Target.Name + "</b>'s")} balls and gives them a round of stimulating licks, making them twitch up and down as they work on <b>{i.Prey.Name}</b>.",
                priority: 16, targetRace: Race.FeralLion, actorRace: Race.FeralLion),
            new EventString((i) => $"<b>{i.Target.Name}</b> is about to rub {GPPHis(i.Target)} own balls, when <b>{i.Unit.Name}</b> quickly claims the spot and starts grinding against {GPPHis(i.Target)} crotch. The two lions make out, and this evolves into mutual dry humping, threatening to liquefy any genital captives as the sex juices start flowing.",
                priority: 16, conditional: s => s.Target != s.Unit, targetRace: Race.FeralLion, actorRace: Race.FeralLion),
            new EventString((i) => $"<b>{i.Unit.Name}</b> nonchalantly sits down on {GPPHis(i.Target)} balls, compressing <b>{i.Prey.Name}</b> together with all the spunk.",
                priority: 16, conditional: s => s.Target == s.Unit, targetRace: Race.FeralLion, actorRace: Race.FeralLion),
            new EventString((i) => $"<b>{i.Unit.Name}</b> showers <b>{i.Target.Name}</b> with playful affection and wears {GPPHis(i.Target)} balls like spectacles. The attention makes <b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.balls)} churn around <b>{i.Prey.Name}</b>.",
                priority: 16, conditional: s => s.Target != s.Unit, targetRace: Race.FeralLion, actorRace: Race.FeralLion),
            new EventString((i) => $"<b>{i.Unit.Name}</b> notices that <b>{i.Target.Name}</b> seems distressed with {GPPHis(i.Target)} swollen {PreyLocStrings.ToSyn(PreyLocation.balls)} and carefully investigates. <b>{i.Unit.Name}</b> has no idea whether it's the fullness or any prey thrashing inside, but surely a big smooch on the sheath will calm the {PreyLocStrings.ToSyn(PreyLocation.balls)}. Astonishingly, it does provide relief, because the arousal is turning <b>{ApostrophizeWithOrWithoutS(i.Prey.Name)}</b> lifeforce into cum.",
                priority: 16, conditional: s => s.Target != s.Unit, targetRace: Race.FeralLion, actorRace: Race.FeralLion),
            new EventString((i) => $"<b>{i.Target.Name}</b> startles as <b>{i.Unit.Name}</b> playfully paws at {GPPHis(i.Target)} testicles, but it doesn't hurt. {Capitalize(GPPHe(i.Target))} eventually relaxes as <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> kneading and kisses help turn <b>{ApostrophizeWithOrWithoutS(i.Prey.Name)}</b> into {GPPHis(i.Target)} next sticky load.",
                priority: 16, conditional: s => s.Target != s.Unit, targetRace: Race.FeralLion, actorRace: Race.FeralLion),
            new EventString((i) => $"<b>{i.Unit.Name}</b> gets <b>{i.Target.Name}</b> onto {GPPHis(i.Target)} back, kneads {GPPHis(i.Target)} {PreyLocStrings.ToSyn(PreyLocation.balls)} and suckles {GPPHis(i.Target)} erect rod, but carefully has {GPPHim(i.Target)} remain off the edge. {Capitalize(GPPHe(i.Unit))} even gives the {PreyLocStrings.ToSyn(PreyLocation.balls)} some gentle nibbles – all to turn <b>{i.Prey.Name}</b> into lion cummies.",
                priority: 16, conditional: s => s.Target != s.Unit, targetRace: Race.FeralLion, actorRace: Race.FeralLion),

            // Merged original 1.5 line(s) into one entry that now handles self vs. ally rubbing
            new EventString((i) => $"<b>{i.Unit.Name}</b> massages {(i.Unit == i.Target ? $"{GPPHis(i.Target)} own" : $"<b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b>")} full scrotum{GetRandomStringFrom(".", $", a bit of pre leaking out of {(i.Unit == i.Target ? GPPHis(i.Target) : "<b>" + i.Target.Name + "</b>'s")} erect {PreyLocStrings.ToCockSyn()}.")}.", priority: 8),
            // New lines (Thanks Cartography! :thumbsup:)
            new EventString((i) => $"<b>{i.Unit.Name}</b> fondles {(i.Unit == i.Target ? $"{GPPHis(i.Target)} own" : $"<b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b>")} squirming {PreyLocStrings.ToSyn(PreyLocation.balls)}, feeling <b>{i.Prey.Name}</b> within.", priority: 8),
            new EventString((i) => $"<b>{i.Target.Name}</b> {(i.Unit == i.Target ? $"feels {GPPHis(i.Target)} own" : $"has <b>{i.Unit.Name}</b> feel {GPPHis(i.Target)}")} {PreyLocStrings.ToSyn(PreyLocation.balls)}, soaking poor <b>{i.Prey.Name}</b> within further.", priority: 8),
            new EventString((i) => $"<b>{i.Unit.Name}</b> rubs over <b>{i.Target.Name}</b>'s wiggling {GetRandomStringFrom($"{PreyLocStrings.ToBallSynSing()}, amazed that it", $"{PreyLocStrings.ToBallSynPlural()}, amazed that they")} can contain a whole {GetRaceDescSingl(i.Prey)}.", priority: 8, conditional: s => !SizeDiffPrey(s, 0.75f) && s.Unit != s.Target),
            new EventString((i) => $"As <b>{i.Target.Name}</b> walks, {GPPHis(i.Target)} wriggling {GetRandomStringFrom($"{PreyLocStrings.ToBallSynSing()} slides", $"{PreyLocStrings.ToBallSynPlural()} slide")} along the ground, hastening <b>{i.Prey.Name}</b>'s digestion within.", priority: 8, conditional: s => s.Unit == s.Target),
            new EventString((i) => $"<b>{i.Target.Name}</b> rests {GPPHis(i.Target)} {PreyLocStrings.ToSyn(PreyLocation.balls)} on the ground and shifts {GPPHis(i.Target)} hips around, jostling <b>{i.Prey.Name}</b> around inside.", priority: 8, conditional: s => s.Unit == s.Target),

            // PreyCumgested() is now finally used! :partying_face:
            new EventString((i) => $"<b>{i.Unit.Name}</b> fondles {(i.Unit == i.Target ? $"{GPPHis(i.Target)} own" : $"<b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b>")} bulging {GetRandomStringFrom($"{PreyLocStrings.ToBallSynSing()}, sloshing around its", $"{PreyLocStrings.ToBallSynPlural()}, sloshing around their")} contents.", priority: 12, conditional: s => PreyCumgested(s)),
            new EventString((i) => $"<b>{i.Target.Name}</b> {(i.Unit == i.Target ? $"feels {GPPHis(i.Target)} own" : $"has <b>{i.Unit.Name}</b> feel {GPPHis(i.Target)}")} {PreyLocStrings.ToSyn(PreyLocation.balls)}. {(i.Unit == i.Target ? $"As {GPPHe(i.Target)} touch{EsIfSingular(i.Target)} {GPPHimself(i.Target)}" : $"At {GPPHis(i.Unit)} touch")}, <b>{i.Target.Name}</b>'s {GetRandomStringFrom($"{PreyLocStrings.ToBallSynSing()} shrinks", $"{PreyLocStrings.ToBallSynPlural()} shrink")} down a little.", priority: 12, conditional: s => PreyCumgested(s)),
            new EventString((i) => $"<b>{i.Unit.Name}</b> rubs over <b>{i.Target.Name}</b>'s stuffed {GetRandomStringFrom($"{PreyLocStrings.ToBallSynSing()}, feeling the warmth it gives", $"{PreyLocStrings.ToBallSynPlural()}, feeling the warmth they give")} off as <b>{i.Prey.Name}</b> is absorbed within.", priority: 12, conditional: s => PreyCumgested(s)),
            new EventString((i) => $"As <b>{i.Target.Name}</b> walks, {GPPHis(i.Target)} full {GetRandomStringFrom($"{PreyLocStrings.ToBallSynSing()} slides", $"{PreyLocStrings.ToBallSynPlural()} slide")} along the ground, speeding up <b>{ApostrophizeWithOrWithoutS(i.Prey.Name)}</b> conversion to liquid within.", priority: 12, conditional: s => PreyCumgested(s)),

            new EventString((i) => $"<b>{i.Unit.Name}</b> forcefully strokes <b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> erect cock, either waiting for the prey inside {GPPHis(i.Target)} balls to melt down faster, or for {GPPHim(i.Target)} to spurt it out for <b>{i.Unit.Name}</b> to snatch it.", priority: 9, conditional: s => s.Target != s.Unit && ReqTargetCompatibleLewd(s)),
            new EventString((i) => $"<b>{i.Unit.Name}</b> puts <b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> throbbing member between {GPPHis(i.Unit)} breasts, stroking and squishing it, waiting for release.", priority: 9, conditional: s => ReqTargetCompatibleLewd(s) && ActorBoobs(s)),
            new EventString((i) => $"<b>{i.Unit.Name}</b> quickly strokes {(i.Unit == i.Target ? GPPHis(i.Target) : $"<b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b>")} cock, trying to relieve {GPPHis(i.Target)} engorged balls of their contents.", priority: 9, conditional: s => ReqTargetCompatibleLewd(s) && ActorBoobs(s)),
            //succs
            new EventString((i) => $"The mere touch of <b>{i.Unit.Name}</b> is enough to make <b>{i.Target.Name}</b> gasp and quiver in pleasure.", actorRace: Race.Succubus, priority: 9),
            new EventString((i) => $"\"My, my, what a beautiful ensemble we have here~\" coos <b>{i.Unit.Name}</b> straddling <b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> sloshing balls and stroking {GPPHis(i.Unit)} erection - \"Now, would you let me take care of it?\"", actorRace: Race.Succubus, priority: 9, conditional: ReqTargetCompatibleLewd),
            new EventString((i) => $"<b>{i.Unit.Name}</b> gives a gentle kiss to <b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> cockhead, giggling at {GPPHis(i.Unit)} muffled gasp of pleasure.", actorRace: Race.Succubus, priority: 9),

            new EventString((i) => $"<b>{i.Unit.Name}</b> begins massaging <b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> full {PreyLocStrings.ToSyn(PreyLocation.balls)}, causing the dragon to groan in pleasure.",
                actorRace: Race.Kobold, priority: 10, conditional: s => (Lewd(s) && Equals(s.Target.Race, Race.Dragon)) || Equals(s.Target.Race, Race.EasternDragon)),
            new EventString((i) => $"<b>{i.Unit.Name}</b> prods <b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> full {PreyLocStrings.ToSyn(PreyLocation.stomach)}, {GPPHis(i.Unit)} hands sinking into the organ as he rubs it. The increasing drops of precum falling on {GPPHis(i.Unit)} back and the pleasured groans of the dragon signal to {GPPHim(i.Unit)} that {GPPHe(i.Unit)}'s doing a good job.",
                actorRace: Race.Kobold, priority: 10, conditional: s => (Lewd(s) && Equals(s.Target.Race, Race.Dragon)) || Equals(s.Target.Race, Race.EasternDragon)),
            new EventString((i) => $"<b>{i.Unit.Name}</b> begins massaging <b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> full {PreyLocStrings.ToSyn(PreyLocation.balls)}, kneading deep and trying to massage every inch {GPPHe(i.Unit)} can. The deep, pleasured rumbling the dragon is making is a sign that the kobold is doing a fantastic job.",
                actorRace: Race.Kobold, priority: 10, conditional: s => (Lewd(s) && Equals(s.Target.Race, Race.Dragon)) || Equals(s.Target.Race, Race.EasternDragon)),
            new EventString((i) => $"<b>{i.Target.Name}</b> beckons <b>{i.Unit.Name}</b> over, the kobold excitedly complies. <b>{i.Unit.Name}</b> digs {GPPHimself(i.Unit)} onto the outside of the dragon's {PreyLocStrings.ToSyn(PreyLocation.balls)}, rubbing {GPPHis(i.Unit)} head, hands, and entire body on as much of the sack as possible; <b>{i.Target.Name}</b> rumbling in great pleasure the whole time. <b>{i.Unit.Name}</b> might not be a dragon, but after {GPPHe(i.Unit)}'s done, {GPPHe(i.Unit)} sure smells like one.",
                actorRace: Race.Kobold, priority: 10, conditional: s => (Lewd(s) && Equals(s.Target.Race, Race.Dragon)) || Equals(s.Target.Race, Race.EasternDragon)),

            new EventString((i) => $"<b>{i.Unit.Name}</b> begins massaging <b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> full balls, causing the dragon to groan in pleasure.",
                actorRace: Race.Kobold, priority: 10, conditional: s => Equals(s.Target.Race, Race.Dragon) || Equals(s.Target.Race, Race.EasternDragon)),
            new EventString((i) => $"<b>{i.Unit.Name}</b> prods <b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> full scrotum, {GPPHis(i.Unit)} hands sinking into the organ as {GPPHe(i.Unit)} rub{SIfSingular(i.Unit)} it. The increasing drops of precum falling on {GPPHis(i.Unit)} back signal to {GPPHim(i.Unit)} that {GPPHeIsAbbr(i.Unit)} doing a good job.",
                actorRace: Race.Kobold, priority: 10, conditional: s => Equals(s.Target.Race, Race.Dragon) || Equals(s.Target.Race, Race.EasternDragon)),

            new EventString((i) => $"<b>{i.Unit.Name}</b> massages {(i.Unit == i.Target ? GPPHis(i.Target) : "<b>" + i.Target.Name + "</b>'s")} full scrotum, a bit of pre leaking out of {GPPHis(i.Target)} erect {PreyLocStrings.ToCockSyn()}.", priority: 8),
            new EventString((i) => $"<b>{i.Unit.Name}</b> settles down and kneads {GPPHis(i.Unit)} {PreyLocStrings.ToSyn(PreyLocation.balls)}, feeling them slosh with increasing amounts of {PreyLocStrings.ToFluid(PreyLocation.balls)}.", priority: 11, conditional: s => s.Target == s.Unit),
            new EventString((i) => $"Dropping to the ground, <b>{i.Unit.Name}</b> spreads {GPPHis(i.Unit)} legs and rubs {GPPHis(i.Unit)} thighs against {GPPHis(i.Unit)} {PreyLocStrings.ToCockSyn()}, enjoying the deep burbling within.", priority: 11, conditional: s => s.Target == s.Unit),
            new EventString((i) => $"As <b>{i.Unit.Name}</b> rubs {GPPHis(i.Unit)} {PreyLocStrings.ToSyn(PreyLocation.balls)}, {GPPHe(i.Unit)} begin{SIfSingular(i.Unit)} to ponder how big of a puddle {GPPHis(i.Unit)} prey will make.",
                priority: 11, conditional: s => s.Target == s.Unit),

            new EventString((i) => $"<b>{i.Unit.Name}</b> can't believe {GPPHe(i.Unit)} got the honour to rub the balls of the famous {(TargetHumanoid(i) ? "warrior" : "beast")}, <b>{i.Target.Name}</b>.",
                priority: 15, conditional: s => s.Unit.Level + 9 < s.Target.Level),
            new EventString((i) => $"\"Oh gods this is actually happening!\" <b>{i.Unit.Name}</b> thinks to {GPPHimself(i.Unit)} as {GPPHe(i.Unit)} knead{SIfSingular(i.Unit)} the prey-filled {PreyLocStrings.ToSyn(PreyLocation.balls)} of thier idol, <b>{i.Target.Name}</b>.",
                priority: 15, conditional: s => s.Unit.Level + 9 < s.Target.Level),
            new EventString((i) => $"<b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> eyes light up as the great {(TargetHumanoid(i) ? "warrior" : "beast")}, <b>{i.Target.Name}</b>, lets {GPPHim(i.Unit)} rub {GPPHis(i.Target)} prey-filled {PreyLocStrings.ToSyn(PreyLocation.balls)}.",
                priority: 15, conditional: s => s.Unit.Level + 9 < s.Target.Level),
            new EventString((i) => $"<b>{i.Unit.Name}</b> excitedly rubs <b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> belly. The famous, battle-hardened {(TargetHumanoid(i) ? "warrior" : "beast")} closing {GPPHis(i.Target)} eyes and groans in pleasure as <b>{i.Target.Name}</b> is clearly doing a good job.",
                priority: 15, conditional: s => s.Unit.Level + 9 < s.Target.Level),
            new EventString((i) => $"<b>{i.Unit.Name}</b> kneads and prods the {PreyLocStrings.ToSyn(PreyLocation.balls)} of <b>{i.Target.Name}</b>, hoping that the famous {(TargetHumanoid(i) ? "warrior" : "beast")} will be impressed with {GPPHis(i.Unit)} work.",
                priority: 15, conditional: s => s.Unit.Level + 9 < s.Target.Level),

            new EventString((i) => $"<b>{i.Unit.Name}</b> excitedly rubs the {PreyLocStrings.ToSyn(PreyLocation.balls)} of <b>{i.Target.Name}</b>. The famous {(TargetHumanoid(i) ? "warrior" : "beast")} groans lustfully and <b>{i.Unit.Name}</b> hopes it's enough to possibly get a 'special reward' later tonight.",
                priority: 15, conditional: s => Lewd(s) && s.Unit.Level + 9 < s.Target.Level),
            new EventString((i) => $"<b>{i.Unit.Name}</b> seductively rubs the {PreyLocStrings.ToSyn(PreyLocation.balls)} of their idol, <b>{i.Target.Name}</b>. <b>{i.Unit.Name}</b> makes sure that {GPPHis(i.Unit)} assets draw the strong {(TargetHumanoid(i) ? "warrior" : "beast")}'s attention and hopes that <b>{i.Target.Name}</b> will think of taking {GPPHim(i.Unit)} tonight.",
                priority: 15, conditional: s => Lewd(s) && s.Unit.Level + 9 < s.Target.Level),
            new EventString((i) => $"As <b>{i.Unit.Name}</b> eagerly rubs the {PreyLocStrings.ToSyn(PreyLocation.balls)} of <b>{i.Target.Name}</b>, the renowned {(TargetHumanoid(i) ? "warrior" : "beast")} whispers to {GPPHim(i.Unit)}, \"If you keep that up, I might test out my new potency on you tonight~.\" <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> eyes go wide and {GPPHe(i.Unit)} go{EsIfSingular(i.Unit)} back to rubbing with thrice the vigor!",
                priority: 15, conditional: s => Lewd(s) && s.Unit.Level + 9 < s.Target.Level),
            new EventString((i) => $"As <b>{i.Unit.Name}</b> rubs the {PreyLocStrings.ToSyn(PreyLocation.balls)} of <b>{i.Target.Name}</b>, the strong {(TargetHumanoid(i) ? "warrior" : "beast")} whispers something into {GPPHis(i.Unit)} ear. <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> eyes light up as {GPPHe(i.Unit)} begin{SIfSingular(i.Unit)} to rub ever harder, hoping to make their idol's prey churn even faster so {GPPHe(i.Unit)} can spend more time with <b>{i.Unit.Name}</b> alone.",
                priority: 15, conditional: s => Lewd(s) && s.Unit.Level + 9 < s.Target.Level),
            new EventString((i) => $"\"Keep that up, and I'll give you a mating session you will never forget.\" <b>{i.Target.Name}</b> tells <b>{i.Unit.Name}</b> who immediately rubs harder and faster, clearly unable to hide their excitement after hearing that <b>{i.Target.Name}</b> wants to have sex with {GPPHim(i.Unit)}.",
                priority: 15, conditional: s => Lewd(s) && s.Unit.Level + 9 < s.Target.Level),
            new EventString((i) => $"<b>{i.Unit.Name}</b> excitedly rubs the {PreyLocStrings.ToSyn(PreyLocation.balls)} of the battle-hardened {(TargetHumanoid(i) ? "warrior" : "beast")}, <b>{i.Target.Name}</b>. The smell of such a powerful {(TargetHumanoid(i) ? "warrior" : "beast")}'s nether regions is too arousing for <b>{i.Unit.Name}</b> and {GPPHis(i.Unit)} other hand drifts down to {GPPHis(i.Unit)} crotch.",
                priority: 15, conditional: s => Lewd(s) && s.Unit.Level + 9 < s.Target.Level),
        };

        BreastVoreMessages = new List<EventString>()
        {
            new EventString((i) => $"<b>{i.Target.Name}</b> lets out a distressed yelp as {GPPHeIs(i.Target)} shoved headfirst into <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> nipple!", priority: 8),
            new EventString((i) => $"<b>{i.Unit.Name}</b> casts aside {GPPHis(i.Unit)} weapon and grabs onto <b>{i.Target.Name}</b>, stuffing {GPPHim(i.Target)} into {GPPHis(i.Unit)} cleavage!", priority: 8),
            new EventString((i) => $"<b>{i.Unit.Name}</b> swiftly jumps on <b>{i.Target.Name}</b>, {GPPHis(i.Target)} form engulfed by <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> breasts!", priority: 8),
            new EventString((i) => $"<b>{i.Unit.Name}</b> shoves <b>{i.Target.Name}</b> onto the ground and shoves {GPPHim(i.Target)} legfirst into {GPPHis(i.Unit)} nipple!", priority: 8),
            new EventString((i) => $"<b>{i.Unit.Name}</b> overpowers <b>{i.Target.Name}</b> and swiftly shoves {GPPHim(i.Target)} into {GPPHis(i.Unit)} bust!", priority: 8),
            new EventString((i) => $"<b>{i.Unit.Name}</b> shoves <b>{i.Target.Name}</b> into {GPPHis(i.Unit)} supple, hungry bosom.", priority: 8),
            new EventString((i) => $"<b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> nipples ache after stretching far enough to consume {GPPHis(i.Unit)} rival warrior.", priority: 8),
            new EventString((i) => $"<b>{i.Unit.Name}</b> holds <b>{i.Target.Name}</b> tightly, preparing to swallow them, but just as {GPPHe(i.Unit)} go{EsIfSingular(i.Unit)} to swallow them, {GPPHe(i.Unit)} see{SIfSingular(i.Unit)} {GPPHis(i.Unit)} prey disappearing into {GPPHis(i.Unit)} engorged cleavage.", priority: 8),
            //Slime pred
            new EventString((i) => $"<b>{i.Unit.Name}</b> launches {GPPHimself(i.Unit)} at <b>{i.Target.Name}</b>, engulfing them in {GPPHis(i.Unit)} bosom!", actorRace: Race.Slime, priority: 10),
            new EventString((i) => $"<b>{i.Unit.Name}</b> seems to hug <b>{i.Target.Name}</b>, engulfing them in {GPPHis(i.Unit)} chest!", actorRace: Race.Slime, priority: 10),

            //Slime prey
            new EventString((i) => $"<b>{i.Unit.Name}</b> reaches for <b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> core, shoving it into {GPPHis(i.Unit)} cleavage!", targetRace: Race.Slime, priority: 10),
            new EventString((i) => $"<b>{i.Unit.Name}</b> grabs <b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> viscous form and slurps {GPPHim(i.Target)} into {GPPHis(i.Unit)} nipples!", targetRace: Race.Slime, priority: 10),

            //Kangaroo pred (pouch vore)
            new EventString((i) => $"<b>{i.Unit.Name}</b> grabs <b>{i.Target.Name}</b> and shoves {GPPHim(i.Target)} into {GPPHis(i.Unit)} pouch. Once <b>{i.Target.Name}</b> is fully inside, the pouch entrance seals tight.", actorRace: Race.Kangaroo, priority: 10),
            // Todo: Write a method for checking the unit's feet when I'm not so tired
            //new EventString((i) => $"<b>{i.Unit.Name}</b> grabs <b>{i.Target.Name}</b> with both arms and lifts {GPPHim(i.Target)} up. At first, <b>{i.Target.Name}</b> is worried, but as <b>{i.Unit.Name}</b> starts to lower {GPPHim(i.Target)}, <b>{i.Target.Name}</b> feels relieved. That is, until <b>{i.Target.Name}</b> feels {GPPHis(i.Target)} {(i.Target.Race == Race.Lamia ? "tail" : i.Target.Race == Race.Slimes ? "feet")} enter a warm chamber. Before <b>{i.Target.Name}</b> knows it, {GPPHe(i.Target)} find{SIfSingular(i.Target)} the opening of <b>{i.Unit.Name}</b>'s pouch sealing tight above {GPPHim(i.Target)}!", actorRace: Race.Kangaroos, priority: 10),
            new EventString((i) => $"As <b>{i.Unit.Name}</b> tries to tackle <b>{i.Target.Name}</b>, {GPPHe(i.Unit)} fumble{SIfSingular(i.Unit)}, flying through the air at an odd angle. When <b>{i.Unit.Name}</b> gets back up, {GPPHe(i.Unit)} find{SIfSingular(i.Unit)} {GPPHis(i.Unit)} pouch entrance sealed, and <b>{i.Target.Name}</b> struggling vigorously within it!", actorRace: Race.Kangaroo, priority: 10),
            new EventString((i) => $"As <b>{i.Unit.Name}</b> tries to tackle <b>{i.Target.Name}</b>, {GPPHe(i.Unit)} fumble{SIfSingular(i.Unit)}, and somehow end{SIfSingular(i.Unit)} up with {GetRandomStringFrom("<b>" + i.Target.Name + "</b>", "the " + GetRaceDescSingl(i.Target), "the " + GetPreyDesc(i.Target) + " " + GetRaceDescSingl(i.Target))} in {GPPHis(i.Unit)} pouch!", actorRace: Race.Kangaroo, priority: 10),
            new EventString((i) => $"<b>{i.Unit.Name}</b> approaches <b>{i.Target.Name}</b> and taps on {GPPHis(i.Target)} shoulder. <b>{i.Unit.Name}</b> then gestures to {GPPHis(i.Unit)} pouch, which {GPPHeIs(i.Unit)} holding open with {GPPHis(i.Unit)} fingers. After <b>{i.Target.Name}</b> reluctantly gets in, <b>{i.Unit.Name}</b>'s pouch's entrance closes somewhat above <b>{i.Target.Name}</b>, not enough to block oxygen from flowing in, but enough to stop <b>{i.Target.Name}</b> from just falling out.", actorRace: Race.Kangaroo, priority: 10, conditional: s => Equals(s.Target.Side, s.Unit.Side)),

            // Endo pouch vore
            new EventString((i) => $"<b>{i.Unit.Name}</b> approaches <b>{i.Target.Name}</b> and asks {GPPHim(i.Target)} to get in {GPPHis(i.Unit)} pouch, which <b>{i.Target.Name}</b> quickly does.", actorRace: Race.Kangaroo, priority: 10, conditional: s => Equals(s.Target.Side, s.Unit.Side)),
            new EventString((i) => $"As <b>{i.Unit.Name}</b> walks by <b>{i.Target.Name}</b>, <b>{i.Target.Name}</b> asks if {GPPHe(i.Target)} could hide in <b>{i.Unit.Name}</b>'s pouch for a bit, to which <b>{i.Unit.Name}</b> shrugs and says \"sure.\"", actorRace: Race.Kangaroo, priority: 10, conditional: s => Equals(s.Target.Side, s.Unit.Side)),
            new EventString((i) => $"<b>{i.Unit.Name}</b> approaches <b>{i.Target.Name}</b> and tells {GPPHim(i.Target)} to get in {GPPHis(i.Unit)} pouch. Not one to question orders, <b>{i.Target.Name}</b> gets in <b>{i.Unit.Name}</b>'s pouch.", actorRace: Race.Kangaroo, priority: 10, conditional: s => Equals(s.Target.Side, s.Unit.Side)),
            new EventString((i) => $"As <b>{i.Unit.Name}</b> approaches <b>{i.Target.Name}</b>, {GPPHe(i.Target)} surprises <b>{i.Unit.Name}</b> by jumping in {GPPHis(i.Unit)} pouch!", actorRace: Race.Kangaroo, priority: 10, conditional: s => Equals(s.Target.Side, s.Unit.Side)),
            new EventString((i) => $"As <b>{i.Unit.Name}</b> approaches <b>{i.Target.Name}</b>, {GPPHe(i.Target)} surprises <b>{i.Unit.Name}</b> by jumping in {GPPHis(i.Unit)} pouch! \"Not really the time for roleplay right now,\" <b>{i.Unit.Name}</b> whispers to the giggling {InfoPanel.RaceSingular(i.Target)}.", actorRace: Race.Kangaroo, priority: 10, conditional: s => Equals(s.Target.Side, s.Unit.Side)),
            new EventString((i) => $"As <b>{i.Unit.Name}</b> approaches <b>{i.Target.Name}</b>, {GPPHe(i.Target)} surprises <b>{i.Unit.Name}</b> by jumping in {GPPHis(i.Unit)} pouch! <b>{i.Unit.Name}</b> snarls slightly, and mutters \"coward\" under {GPPHis(i.Unit)} breath at the shaking {InfoPanel.RaceSingular(i.Target)}.", actorRace: Race.Kangaroo, priority: 10, conditional: s => Equals(s.Target.Side, s.Unit.Side)),
            new EventString((i) => $"As <b>{i.Unit.Name}</b> approaches <b>{i.Target.Name}</b>, {GPPHe(i.Target)} surprises <b>{i.Unit.Name}</b> by jumping in {GPPHis(i.Unit)} pouch! \"Ooh, kinky!\" <b>{i.Unit.Name}</b> says to the squirming {InfoPanel.RaceSingular(i.Target)}.", actorRace: Race.Kangaroo, priority: 10, conditional: s => Equals(s.Target.Side, s.Unit.Side)),
            new EventString((i) => $"As <b>{i.Unit.Name}</b> walks by <b>{i.Target.Name}</b>, <b>{i.Target.Name}</b> asks if {GPPHe(i.Target)} could hide in <b>{i.Unit.Name}</b>'s pouch for a bit. <b>{i.Unit.Name}</b>, who had secretly been hoping for this, grabs <b>{i.Target.Name}</b> and shoves {GPPHim(i.Target)} into {GPPHis(i.Unit)} pouch before <b>{i.Target.Name}</b> can even think of changing {GPPHis(i.Target)} mind.", actorRace: Race.Kangaroo, priority: 10, conditional: s => Equals(s.Target.Side, s.Unit.Side)),

            // Additional pouch vore
            // Todo: Find some way to check if PreyLocation is already occupied when voring
            //new EventString((i) => $"", actorRace: Race.Kangaroos, priority: 10, conditional: s => s.Unit.),

            new EventString((i) => $"<b>{i.Unit.Name}</b> and <b>{i.Target.Name}</b> grapple each other tightly, grinding their breasts hard against one another until <b>{i.Target.Name}</b> sinks into <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> bosom!", priority: 8, conditional: s => ActorBoobs(s) && TargetBoobs(s)),
            new EventString((i) => $"<b>{i.Unit.Name}</b> presses {GPPHis(i.Unit)} breasts over <b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> head, muffling the voice of the leader of the {GetPreyDesc(i.Target)} {GetRaceDescSingl(i.Target)} and absorbing {GPPHim(i.Unit)} into the hungry breast flesh.", priority: 8, conditional: s => Lewd(s) && TargetLeader(s)),

            new EventString((i) => $"<b>{i.Unit.Name}</b> stuffs <b>{i.Target.Name}</b> into {GPPHis(i.Unit)} boobs.", priority: 25, conditional: HasGreatEscape),
            new EventString((i) => $"<b>{i.Unit.Name}</b> sucks <b>{i.Target.Name}</b> up {GPPHis(i.Unit)} breasts, the engorged bosom jiggling as prey struggles inside.", priority: 25, conditional: HasGreatEscape),
            new EventString((i) => $"Not a moment after being sucked in, <b>{i.Target.Name}</b> is already plotting {GPPHis(i.Target)} escape from <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> bosom.", priority: 25, conditional: HasGreatEscape),
        };

        CockVoreMessages = new List<EventString>()
        {
            //Generic messages
            new EventString((i) => $"<b>{i.Target.Name}</b> lets out a distressed yelp as {GPPHeIs(i.Target)} shoved headfirst into <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> penis!",
                priority: 8),
            new EventString((i) => $"<b>{i.Unit.Name}</b> casts aside {GPPHis(i.Unit)} weapon and grabs onto <b>{i.Target.Name}</b>, stuffing {GPPHim(i.Target)} into {GPPHis(i.Unit)} testicles!",
                priority: 8, conditional: ActorHumanoid),
            new EventString((i) => $"<b>{i.Unit.Name}</b> swiftly jumps on <b>{i.Target.Name}</b>, {GPPHis(i.Target)} form engulfed by <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> cock!",
                priority: 8),
            new EventString((i) => $"<b>{i.Unit.Name}</b> shoves <b>{i.Target.Name}</b> onto the ground and shoves {GPPHim(i.Target)} into {GPPHis(i.Unit)} dick!",
                priority: 8),
            new EventString((i) => $"<b>{i.Unit.Name}</b> overpowers <b>{i.Target.Name}</b> and swiftly shoves {GPPHim(i.Target)} into {GPPHis(i.Unit)} balls!",
                priority: 8),
            new EventString((i) => $"Grunting with each throb, <b>{i.Unit.Name}</b> sees <b>{i.Target.Name}</b> disappear further and further into {GPPHis(i.Unit)} cock.",
                priority: 8),
            new EventString((i) => $"<b>{i.Unit.Name}</b> lewdly sways {GPPHis(i.Unit)} hips, giving friend and foe alike the sight of <b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> body disappearing into into {GPPHis(i.Unit)} cock.",
                priority: 8),
            new EventString((i) => $"Tired of <b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> struggling, <b>{i.Unit.Name}</b> grabs {GPPHim(i.Target)} by {GPPHis(i.Target)} shoulders and swiftly humps <b>{i.Target.Name}</b> into {GPPHis(i.Unit)} cock.",
                priority: 8),
            new EventString((i) => $"Just bringing <b>{i.Target.Name}</b> close enough to glans causes <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> cock to swell and instantly swallow {GPPHis(i.Unit)} prey.",
                priority: 8),

            //Slime pred
            new EventString((i) => $"<b>{i.Unit.Name}</b> launches {GPPHimself(i.Unit)} at <b>{i.Target.Name}</b>, engulfing {GPPHim(i.Target)} in {GPPHis(i.Unit)} testicles!",
                actorRace: Race.Slime, priority: 10),
            new EventString((i) => $"<b>{i.Unit.Name}</b> cascades over <b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> body, sucking {GPPHim(i.Target)} into {GPPHis(i.Unit)} dick!",
                actorRace: Race.Slime, priority: 10),
            new EventString((i) => $" Everyone on the battlefield gets a good view of <b>{i.Target.Name}</b> sliding through translucent member of <b>{i.Unit.Name}</b> into {GPPHis(i.Unit)} balls.",
                actorRace: Race.Slime, priority: 10),
            new EventString((i) => $"With a naughty grin, <b>{i.Unit.Name}</b> swells {GPPHis(i.Unit)} dick to grotesque proportions and slams it down on <b>{i.Target.Name}</b>, stunning and absorbing them through it.",
                actorRace: Race.Slime, priority: 10),

            //Slime prey
            new EventString((i) => $"<b>{i.Unit.Name}</b> reaches for <b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> core, shoving it into {GPPHis(i.Unit)} penis!",
                targetRace: Race.Slime, priority: 9),
            new EventString((i) => $"<b>{i.Unit.Name}</b> grabs <b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> viscous form and slurps {GPPHim(i.Target)} into {GPPHis(i.Unit)} dick!",
                targetRace: Race.Slime, priority: 9),
            new EventString((i) => $"<b>{i.Unit.Name}</b> shoves {GPPHis(i.Unit)} dick into <b>{i.Target.Name}</b>. Before <b>{i.Target.Name}</b> could react, <b>{i.Unit.Name}</b> slurps {GPPHim(i.Target)} up {GPPHis(i.Unit)} throbbing member.",
                targetRace: Race.Slime, priority: 9, conditional: Lewd),

            new EventString((i) => $"<b>{i.Unit.Name}</b> smirks at <b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> fearful face before {GPPHe(i.Unit)} cram{SIfSingular(i.Unit)} the so-called leader down {GPPHis(i.Unit)} mighty cock.",
                priority: 8, conditional: s => TargetLeader(s) && ActorHumanoid(s)),

            new EventString((i) => $"<b>{i.Unit.Name}</b> grabs the shrunken form of <b>{i.Target.Name}</b> and holds {GPPHim(i.Target)} tight against {GPPHis(i.Unit)} crotch. <b>{i.Unit.Name}</b> makes sure that the struggling {GetRaceDescSingl(i.Target)} gets good whiff before bringing {GPPHim(i.Target)} up to {GPPHis(i.Unit)} shaft and sticking {GPPHim(i.Target)} inside.",
                priority: 11, conditional: s => Shrunk(s) && Lewd(s)),
            new EventString((i) => $"<b>{i.Unit.Name}</b> grabs the shrunken form of <b>{i.Target.Name}</b>, getting all sorts of dirty thoughts of what to do with {GPPHim(i.Target)}. <b>{i.Unit.Name}</b> gets an idea and holds the wiggling {GetRaceDescSingl(i.Target)} against {GPPHis(i.Unit)} length, stroking up and down for a bit before shoving <b>{i.Target.Name}</b> into {GPPHis(i.Unit)} dick.",
                priority: 11, conditional: s => Shrunk(s) && Lewd(s)),
            new EventString((i) => $"A shrunken <b>{i.Target.Name}</b> screams in horror as the massive hand of <b>{i.Unit.Name}</b> envelops {GPPHis(i.Target)} entire body. <b>{i.Unit.Name}</b> smirks and gives the scared {GetRaceDescSingl(i.Target)} a good look at {GPPHis(i.Unit)} member before stuffing {GPPHim(i.Target)} inside.",
                priority: 11, conditional: Shrunk),

            new EventString((i) => $"<b>{i.Unit.Name}</b> stuffs <b>{i.Target.Name}</b> into {GPPHis(i.Unit)} cock.", priority: 25, conditional: HasGreatEscape),
            new EventString((i) => $"In a humping frenzy accompanying <b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> descent into <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> gurgling balls', <b>{i.Unit.Name}</b> doesn't notice <b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> smug expression. \"Enjoy it while it lasts\" - the prey says disappearing inside.", priority: 25, conditional: HasGreatEscape),
            new EventString((i) => $"<b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> shaft greedily consumes <b>{i.Target.Name}</b>, {GPPHis(i.Target)} form engorging the pred's balls. <b>{i.Target.Name}</b> settles within and starts probing the walls of {GPPHis(i.Target)} fleshy prison, looking for a way out.", priority: 25, conditional: HasGreatEscape),

            //Endo
            new EventString((i) => $"<b>{i.Target.Name}</b> blushes when <b>{i.Unit.Name}</b> embraces {GPPHim(i.Target)} and sends {GPPHim(i.Target)} sliding down {GPPHis(i.Unit)} shaft.",
                priority: 12, conditional: s => Friendly(s) && Endo(s)),
            new EventString((i) => $"<b>{i.Unit.Name}</b> gently engulfs {GPPHis(i.Unit)} ally. For <b>{i.Target.Name}</b>, the noise of the battlefield is replaced with gently sloshing and throbbing, as well as an all-encompassing heartbeat.",
                priority: 12, conditional: s => Friendly(s) && Endo(s)),
            new EventString((i) => $"After a moment of nervous hesitation, <b>{i.Target.Name}</b> comfortably slips into <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> {i.preyLocation.ToSyn()}.",
                priority: 12, conditional: s => Friendly(s) && Endo(s)),
            new EventString((i) => $"<b>{i.Target.Name}</b> finds {GPPHimself(i.Target)} thoroughly played with by <b>{i.Unit.Name}</b>, {GPPHis(i.Target)} ally, before being packed away into the safety of {GPPHis(i.Unit)} {i.preyLocation.ToSyn()}.",
                priority: 12, conditional: s => Friendly(s) && Endo(s)),
            new EventString((i) => $"<b>{i.Target.Name}</b> squirms and thrashes about, trying to free {GPPHimself(i.Target)} from the grasp of {GPPHis(i.Target)} vicious predator... and then {(Lewd(i) ? "moans in delight" : "giggles")} as the last of {GPPHim(i.Target)} slide{SIfSingular(i.Target)} into <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> {i.preyLocation.ToSyn()}.\n<b>{i.Unit.Name}</b> appreciates the act and caresses {GPPHis(i.Unit)} {i.preyLocation.ToSyn()}.",
                priority: 12, conditional: s => Friendly(s) && Endo(s)),
            new EventString((i) => $"<b>{i.Unit.Name}</b> has to reassure <b>{i.Target.Name}</b> that it's safe, before the latter finally dives head first down the length of {GPPHis(i.Unit)} cock.",
                priority: 12, conditional: s => Friendly(s) && Endo(s)),
        };

        AnalVoreMessages = new List<EventString>()
        {
            new EventString((i) => $"<b>{i.Unit.Name}</b> grab{SIfSingular(i.Unit)} <b>{i.Target.Name}</b> and stick{SIfSingular(i.Unit)} {GPPHis(i.Target)} {(TargetHumanoid(i) ? "legs" : "hind legs")} into his anus, leaving {GPPHim(i.Target)} to dangle against the {GetRaceDescSingl(i.Unit)}'s genitals, while the quicksand trap that is {GPPHis(i.Unit)} asshole slowly pulls {GPPHim(i.Target)} into {GPPHis(i.Target)} smelly doom.",
                conditional: s => SizeDiff(s, 3), actorRace: Race.FeralLion, priority: 9),

            new EventString((i) => $"<b>{i.Unit.Name}</b> leaps high into the air before coming down onto <b>{i.Target.Name}</b> ass-first, slurping the terrified prey up {GPPHis(i.Unit)} tailhole.", priority: 8),
            new EventString((i) => $"<b>{i.Target.Name}</b> leaves skidmarks on the ground as {GPPHe(i.Target)} is being pulled up <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> booty.", priority: 8),
            new EventString((i) => $"<b>{i.Unit.Name}</b> clenches <b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> head between {GPPHis(i.Unit)} asscheeks and pulls {GPPHim(i.Target)} in.", priority: 8),
            new EventString((i) => $"<b>{i.Unit.Name}</b> sits on <b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> face, letting the lucky prey feel {GPPHis(i.Unit)} plump booty before quickly pulling them inside.", priority: 8),
            new EventString((i) => $"<b>{i.Unit.Name}</b> smooshes <b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> face under {GPPHis(i.Unit)} rump, grinding indulgently and working {GPPHim(i.Target)} into {GPPHis(i.Unit)} anus among muffled protests, before getting up and stretching {GPPHis(i.Unit)} butt in the air, letting gravity do the rest.", priority: 8),
            new EventString((i) => $"<b>{i.Unit.Name}</b> baits a sneak attack from <b>{i.Target.Name}</b>, only to let {GPPHim(i.Target)} collide with {GPPHis(i.Unit)} gaping tailhole, which consumes {GPPHim(i.Target)} in a series of powerful anal clenches.", priority: 8, conditional: s => ActorTail(s)),
            new EventString((i) => $"<b>{i.Target.Name}</b> is pulled screaming and thrashing into <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> behind!", priority: 8),
            new EventString((i) => $"<b>{i.Unit.Name}</b> lies on the ground watching <b>{i.Target.Name}</b> disappear up {GPPHis(i.Unit)} ass.", priority: 8),
            new EventString((i) => $"<b>{i.Unit.Name}</b> knocks <b>{i.Target.Name}</b> onto the ground and swiftly shoves the terrified prey’s lower body up {GPPHis(i.Unit)} ass. After a few contractions, {i.Unit.Name} leans over, caressing their now enormous belly.", priority: 8),
            new EventString((i) => $"<b>{i.Unit.Name}</b> slides <b>{i.Target.Name}</b> into {GPPHis(i.Unit)} {PreyLocStrings.ToSyn(PreyLocation.anal)}!", priority: 8),
            new EventString((i) => $"<b>{i.Unit.Name}</b> backs up into <b>{i.Target.Name}</b> until {GPPHe(i.Target)} trip{SIfSingular(i.Target)}, dooming {GPPHim(i.Target)} to getting face-first engulfed in ass.", priority: 8),
            new EventString((i) => $"Once <b>{i.Target.Name}</b> finds {GPPHimself(i.Target)} immobilized, {GPPHe(i.Target)} can only watch in fear as <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> sphincter slurps up {GPPHis(i.Target)} body like a noodle", priority: 8),
            new EventString((i) => $"<b>{i.Target.Name}</b> looks up at <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> pucker in awe, realizing {GPPHe(i.Target)} want{SIfSingular(i.Target)} to slip up that hole so bad – which makes {GPPHim(i.Target)} rather cooperative when the butt thwumps down.", priority: 10, conditional: s => Cursed(s) && Shrunk(s)),
            new EventString((i) => $"<b>{i.Unit.Name}</b> sits down on the shrunken <b>{i.Target.Name}</b> and grinds {GPPHis(i.Unit)} butt on the ground. After {GPPHe(i.Unit)} stand{SIfSingular(i.Unit)} up, <b>{i.Target.Name}</b> is nowhere to be seen.",
                priority: 10, conditional: Shrunk),
            new EventString((i) => $"<b>{i.Unit.Name}</b> sits down on the shrunken <b>{i.Target.Name}</b> and clenches {GPPHis(i.Unit)} ass hard. After {GPPHe(i.Unit)} stand{SIfSingular(i.Unit)} up, <b>{i.Target.Name}</b> is nowhere to be seen.",
                priority: 10, conditional: Shrunk),
            new EventString((i) => $"<b>{i.Unit.Name}</b> unceremoniously turns around and thwumps down on the shrunken <b>{i.Target.Name}</b>, making {GPPHim(i.Target)} slip up into {GPPHis(i.Unit)} anal depths.",
                priority: 10, conditional: Shrunk),
            new EventString((i) => $"<b>{i.Unit.Name}</b> grabs the shrunken <b>{i.Target.Name}</b> and brings {GPPHim(i.Target)} to {GPPHis(i.Unit)} ass. Using {(ActorHumanoid(i) ? "a finger" : GPPHis(i.Unit) + " tongue")}, {GPPHe(i.Unit)} shove{SIfSingular(i.Unit)} the struggling {GetRaceDescSingl(i.Target)} inside.",
                priority: 10, conditional: s => Shrunk(s)),
            new EventString((i) => $"<b>{i.Unit.Name}</b> grabs the shrunken <b>{i.Target.Name}</b> and brings {GPPHim(i.Target)} to {GPPHis(i.Unit)} ass, shoving {GPPHim(i.Target)} halfway inside {GPPHis(i.Unit)} hole. Using {(ActorHumanoid(i) ? "a finger" : GPPHis(i.Unit) + " tongue")}, {GPPHe(i.Unit)} shove{SIfSingular(i.Unit)} the struggling {GetRaceDescSingl(i.Target)} inside completely.",
                priority: 10, conditional: s => Shrunk(s)),
            new EventString((i) => $"<b>{i.Unit.Name}</b> {(ActorHumanoid(i) ? "reaches" : "leans")} down and grabs the shrunken <b>{i.Target.Name}</b> and holds {GPPHim(i.Target)} in front of {GPPHis(i.Unit)} ass. <b>{i.Unit.Name}</b> lets the 'lucky' {GetRaceDescSingl(i.Target)} get a nice long view of {GPPHis(i.Unit)} assets before shoving {GPPHim(i.Target)} into {GPPHis(i.Unit)} hole.",
                priority: 10, conditional: s => Shrunk(s)),

            new EventString((i) => $"<b>{i.Unit.Name}</b> sits down on <b>{i.Target.Name}</b> and grinds {GPPHis(i.Unit)} butt on the ground. After {GPPHe(i.Unit)} stand{SIfSingular(i.Unit)} up, <b>{i.Target.Name}</b> is nowhere to be seen.",
                priority: 9, conditional: s => SizeDiff(s, 3)),
            new EventString((i) => $"<b>{i.Unit.Name}</b> sits down on the comparatively tiny <b>{i.Target.Name}</b> and clenches {GPPHis(i.Unit)} ass hard. After {GPPHe(i.Unit)} stand{SIfSingular(i.Unit)} up, <b>{i.Target.Name}</b> is nowhere to be seen.",
                priority: 9, conditional: s => SizeDiff(s, 3)),
            new EventString((i) => $"<b>{i.Unit.Name}</b> unceremoniously turns around and thwumps down on the miniscule <b>{i.Target.Name}</b>, making {GPPHim(i.Target)} slip up into {GPPHis(i.Unit)} anal depths.",
                priority: 9, conditional: s => SizeDiff(s, 3)),
            new EventString((i) => $"<b>{i.Unit.Name}</b> grabs little <b>{i.Target.Name}</b> and brings {GPPHim(i.Target)} to {GPPHis(i.Unit)} ass. Using {(ActorHumanoid(i) ? "a finger" : GPPHis(i.Unit) + " tongue")}, {GPPHe(i.Unit)} shove{SIfSingular(i.Unit)} the struggling {GetRaceDescSingl(i.Target)} inside.",
                priority: 9, conditional: s => SizeDiff(s, 3)),
            new EventString((i) => $"<b>{i.Unit.Name}</b> grabs the bite-sized <b>{i.Target.Name}</b> and brings {GPPHim(i.Target)} to {GPPHis(i.Unit)} ass, shoving {GPPHim(i.Target)} halfway inside {GPPHis(i.Unit)} hole. Using {(ActorHumanoid(i) ? "a finger" : GPPHis(i.Unit) + " tongue")}, {GPPHe(i.Unit)} shove{SIfSingular(i.Unit)} the struggling {GetRaceDescSingl(i.Target)} inside completely.",
                priority: 9, conditional: s => SizeDiff(s, 3)),
            new EventString((i) => $"<b>{i.Unit.Name}</b> {(ActorHumanoid(i) ? "reaches" : "leans")} down to grab <b>{i.Target.Name}</b> and holds {GPPHim(i.Target)} in front of {GPPHis(i.Unit)} ass. <b>{i.Unit.Name}</b> lets the 'lucky' {GetRaceDescSingl(i.Target)} get a nice long view of {GPPHis(i.Unit)} assets before shoving {GPPHim(i.Target)} into {GPPHis(i.Unit)} hole.",
                priority: 9, conditional: s => SizeDiff(s, 3)),
            new EventString((i) => $"<b>{i.Unit.Name}</b> only tries to immobilize the smaller <b>{i.Target.Name}</b> by sitting on {GPPHim(i.Target)}, but {GPPHe(i.Target)} end{SIfSingular(i.Target)} up deep inside {GPPHis(i.Unit)} ass.",
                priority: 9, conditional: s => SizeDiff(s, 3)),

            new EventString((i) => $"<b>{i.Unit.Name}</b> knocks over <b>{i.Target.Name}</b> and plants {GPPHis(i.Unit)} rump right on {GPPHis(i.Target)} face to let loose a blast of vile farts, numbing {GPPHis(i.Target)} senses. <b>{i.Target.Name}</b> barely has a grip on what's happening as {GPPHe(i.Target)} vanish{EsIfSingular(i.Target)} up the gassy tailhole.", priority: 9, conditional: s => Farts(s) && SizeDiff(s, 3)),
            new EventString((i) => $"<b>{i.Unit.Name}</b> plants {GPPHis(i.Unit)} rump on <b>{i.Target.Name}</b> and rubs {GPPHis(i.Unit)} filthy tailhole all over their comparatively tiny body. In the heat of the moment, the {GetRaceDescSingl(i.Unit)} lets lose all of {GPPHis(i.Unit)} flatulence upon <b>{i.Target.Name}</b>. When {GPPHe(i.Unit)} get{SIfSingular(i.Unit)} back up, {GPPHis(i.Unit)} thinks the little {GetRaceDescSingl(i.Target)} got vaporized by {GPPHis(i.Unit)} farts, until {GPPHe(i.Unit)} feel{SIfSingular(i.Unit)} a squirm inside {GPPHis(i.Unit)} ass.", priority: 9, conditional: s => Farts(s) && Scat(s) && SizeDiff(s, 3)),


            new EventString((i) => $"<b>{i.Unit.Name}</b> squats over <b>{i.Target.Name}</b> and stuffs {GPPHim(i.Target)} into {GPPHis(i.Unit)} ass. <b>{i.Target.Name}</b> takes notice on how stretchy and easy to slide through it is.", priority: 25, conditional: HasGreatEscape),
            new EventString((i) => $"\"Wanna go out the same way you went in?\"- asks <b>{i.Unit.Name}</b> {GPPHis(i.Unit)} prey smugly. <b>{i.Target.Name}</b> shrugs and dives up. if <b>{i.Unit.Name}</b> wants it, so be it.", priority: 25, conditional: HasGreatEscape),
            new EventString((i) => $"Not the one to reject free lodging, <b>{i.Target.Name}</b> climbs up <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> ass with vigor, prompting gasps of pleasure from his predator.", priority: 25, conditional: s => HasGreatEscape(s) && Cursed(s)),

            //Endo
            new EventString((i) => $"<b>{i.Target.Name}</b> blushes when <b>{i.Unit.Name}</b> turns aroud and sends {GPPHim(i.Target)} sliding up {GPPHis(i.Unit)} rectum.",
                priority: 12, conditional: s => Friendly(s) && Endo(s)),
            new EventString((i) => $"<b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> ass gently engulfs {GPPHis(i.Unit)} ally. For <b>{i.Target.Name}</b>, the noise of the battlefield is replaced with gently sloshing and groaning, as well as an all-encompassing heartbeat.",
                priority: 12, conditional: s => Friendly(s) && Endo(s)),
            new EventString((i) => $"After a moment of nervous hesitation, <b>{i.Target.Name}</b> comfortably slips into <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> anal folds.",
                priority: 12, conditional: s => Friendly(s) && Endo(s)),
            new EventString((i) => $"<b>{i.Target.Name}</b> finds {GPPHimself(i.Target)} thoroughly played with by <b>{i.Unit.Name}</b>, {GPPHis(i.Target)} ally, before being packed away into the safety of {GPPHis(i.Unit)} colon.",
                priority: 12, conditional: s => Friendly(s) && Endo(s)),
            new EventString((i) => $"<b>{i.Target.Name}</b> squirms and thrashes about, trying to free {GPPHimself(i.Target)} from the grasp of {GPPHis(i.Target)} vicious predator... and then {(Lewd(i) ? "moans in delight" : "giggles")} as the last of {GPPHim(i.Target)} slides past the sphincter.\n<b>{i.Unit.Name}</b> appreciates the act and caresses {GPPHis(i.Unit)} {i.preyLocation.ToSyn()}.",
                priority: 12, conditional: s => Friendly(s) && Endo(s)),
            new EventString((i) => $"<b>{i.Unit.Name}</b> has to reassure <b>{i.Target.Name}</b> that it's safe, before the latter finally dives head first up {GPPHis(i.Unit)} ass.",
                priority: 12, conditional: s => Friendly(s) && Endo(s)),
        };

        UnbirthMessages = new List<EventString>()
        {
            new EventString((i) => $"<b>{i.Target.Name}</b> lets out a distressed yelp as {GPPHeIs(i.Target)} shoved headfirst into <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> vagina!", priority: 8),
            new EventString((i) => $"<b>{i.Unit.Name}</b> casts aside {GPPHis(i.Unit)} weapon and grabs onto <b>{i.Target.Name}</b>, stuffing {GPPHim(i.Target)} into {GPPHis(i.Unit)} womb!", priority: 8),
            new EventString((i) => $"<b>{i.Unit.Name}</b> swiftly jumps on <b>{i.Target.Name}</b>, {GPPHis(i.Target)} form engulfed by <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> pussy!", priority: 8),
            new EventString((i) => $"<b>{i.Unit.Name}</b> shoves <b>{i.Target.Name}</b> onto the ground and shoves {GPPHim(i.Target)} legfirst into {GPPHis(i.Unit)} vulva!", priority: 8),
            new EventString((i) => $"<b>{i.Unit.Name}</b> overpowers <b>{i.Target.Name}</b> and swiftly shoves {GPPHim(i.Target)} into {GPPHis(i.Unit)} womb!", priority: 8),
            new EventString((i) => $"In the heat of {GPPHis(i.Unit)} needs, <b>{i.Unit.Name}</b> desides to use <b>{i.Target.Name}</b> as a living dildo – Unfortunately, {GPPHis(i.Unit)} toys have a tendency to disappear.", priority: 8, conditional: Lewd),

            //Shrunken Prey
            new EventString((i) => $"<b>{i.Unit.Name}</b> unceremoniously turns around and thwumps down on the shrunken <b>{i.Target.Name}</b>, making {GPPHim(i.Target)} slip up into {GPPHis(i.Unit)} wet folds.",
                priority: 9, conditional: Shrunk),

            //Size difference in general
            new EventString((i) => $"Effortlessly, <b>{i.Target.Name}</b> is trapped in the comparatively huge slobbery maw of <b>{i.Unit.Name}</b>. The next time light touches {GPPHis(i.Target)} body, it's only for {GPPHis(i.Unit)} massive tongue to squish {GPPHim(i.Target)} against {GPPHis(i.Unit)} aroused slit, repeatedly dragging {GPPHim(i.Target)} across its length, before {GPPHe(i.Target)} slip{SIfSingular(i.Target)} in with a <i>~shlup~</i>.",
                priority: 9, conditional: s => SizeDiff(s, 4) && !ActorHumanoid(s)),
            new EventString((i) => $"<b>{i.Unit.Name}</b> only tries to immobilize the smaller <b>{i.Target.Name}</b> by sitting on {GPPHim(i.Target)}, but {GPPHe(i.Target)} end{SIfSingular(i.Target)} up deep inside {GPPHis(i.Unit)} pussy.",
                priority: 9, conditional: s => SizeDiff(s, 3)),
            new EventString((i) => $"<b>{i.Unit.Name}</b> unceremoniously turns around and thwumps down on the much smaller <b>{i.Target.Name}</b>, making {GPPHim(i.Target)} slip up into {GPPHis(i.Unit)} wet folds.",
                priority: 9, conditional: s => SizeDiff(s, 3)),

            //Abakhanskya 
            new EventString((i) => $"Feeling lewd, and dominant, <b>{i.Unit.Name}</b> looks over a helpless <b>{i.Target.Name}</b>. It wasn't long until {GPPHeIsAbbr(i.Target)} grappled by the dragoness, and shoved in headfirst through her slit, <b>{i.Unit.Name}</b> cooing softly throughout.", actorRace: Race.Abakhanskya, priority: 10),
            new EventString((i) => $"<b>{i.Unit.Name}</b>, wishing to play with her food, spins herself about on the spot, grabbing her prey with her tail and sending <b>{i.Target.Name}</b> on a trip to her womb, for {GPPHis(i.Target)} demise.", actorRace: Race.Abakhanskya, priority: 10),
            new EventString((i) => $"<b>{i.Unit.Name}</b> feels an erotic itch needing to be scratched, and with <b>{i.Target.Name}</b> in front of her, it wasn't long until {GPPHeIsAbbr(i.Target)} used to scratch it. Up and through her slit {GPPHe(i.Target)} went, legs first!", actorRace: Race.Abakhanskya, priority: 10),
            new EventString((i) => $"After having sensed she had gained the advantage, <b>{i.Unit.Name}</b> seals <b>{i.Target.Name}</b> away lewdly, taking {GPPHim(i.Target)} in head first into her womb, gasping in delight as {GPPHe(i.Target)} passed through her slit.", actorRace: Race.Abakhanskya, priority: 10),
            new EventString((i) => $"<b>{i.Unit.Name}</b> stands perfectly still for a moment, menacingly overlooking her fearful prey. Quickly feigning an incoming swallow, she instead rotates to show her rump and sits down upon <b>{i.Target.Name}</b>, causing {GPPHim(i.Target)} to pass straight in to her womb.", actorRace: Race.Abakhanskya, priority: 10),

            //Slime Pred
            new EventString((i) => $"<b>{i.Unit.Name}</b> launches {GPPHimself(i.Unit)} at <b>{i.Target.Name}</b>, engulfing {GPPHim(i.Target)} in {GPPHis(i.Unit)} womb!", actorRace: Race.Slime, priority: 10),
            new EventString((i) => $"<b>{i.Unit.Name}</b> cascades over <b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> body, sucking {GPPHim(i.Target)} into {GPPHis(i.Unit)} womb!", actorRace: Race.Slime, priority: 10),

            //Slime Prey
            new EventString((i) => $"<b>{i.Unit.Name}</b> reaches for <b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> core, shoving it into {GPPHis(i.Unit)} vulva!", targetRace: Race.Slime, priority: 9),
            new EventString((i) => $"<b>{i.Unit.Name}</b> grabs <b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> viscous form and slurps {GPPHim(i.Target)} into {GPPHis(i.Unit)} pussy!", targetRace: Race.Slime, priority: 9),

            new EventString((i) => $"<b>{i.Unit.Name}</b> stuffs <b>{i.Target.Name}</b> into {GPPHis(i.Unit)} pussy.", priority: 25, conditional: HasGreatEscape),
            new EventString((i) => $"<b>{i.Unit.Name}</b> shoves <b>{i.Target.Name}</b> into {GPPHis(i.Unit)} pussy and goes back to battle, unbothered by cries of protest from within.", priority: 25, conditional: HasGreatEscape),
            new EventString((i) => $"After a short struggle <b>{i.Unit.Name}</b> is now sporting a fat belly, {GPPHis(i.Unit)} womb full of surprisingly stubborn <b>{i.Target.Name}</b>. \"Some people just refuse to admit they lost\" - sighs <b>{i.Unit.Name}</b>.", priority: 25, conditional: HasGreatEscape),
            new EventString((i) => $"\" Want to be my baby for a while?\" - smugly asks <b>{i.Unit.Name}</b> her half-unbirthed quarry. \"Sure, for a while\" - equally smugly answers <b>{i.Target.Name}</b>.", priority: 25, conditional: HasGreatEscape),
            new EventString((i) => $"You'd think the prospect of being melted into cum would shaken <b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> spirit, but {GetRaceDescSingl(i.Target)} is surprisingly calm, already calculating {GPPHis(i.Target)} escape.", priority: 25, conditional: HasGreatEscape),

            //Endo
            new EventString((i) => $"<b>{i.Target.Name}</b> blushes when <b>{i.Unit.Name}</b> humps {GPPHim(i.Target)} and sends {GPPHim(i.Target)} sliding up {GPPHis(i.Unit)} pussy.",
                priority: 12, conditional: s => Friendly(s) && Endo(s)),
            new EventString((i) => $"<b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> slit gently engulfs {GPPHis(i.Unit)} ally. For <b>{i.Target.Name}</b>, the noise of the battlefield is replaced with gently sloshing and throbbing, as well as an all-encompassing heartbeat.",
                priority: 12, conditional: s => Friendly(s) && Endo(s)),
            new EventString((i) => $"After a moment of nervous hesitation, <b>{i.Target.Name}</b> comfortably slips into <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> wet folds.",
                priority: 12, conditional: s => Friendly(s) && Endo(s)),
            new EventString((i) => $"<b>{i.Target.Name}</b> finds {GPPHimself(i.Target)} thoroughly played with by <b>{i.Unit.Name}</b>, {GPPHis(i.Target)} ally, before being packed away into the safety of {GPPHis(i.Unit)} womb.",
                priority: 12, conditional: s => Friendly(s) && Endo(s)),
            new EventString((i) => $"<b>{i.Target.Name}</b> squirms and thrashes about, trying to free {GPPHimself(i.Target)} from the grasp of {GPPHis(i.Target)} vicious predator... and then {(Lewd(i) ? "moans in delight" : "giggles")} as the last of {GPPHim(i.Target)} slide{SIfSingular(i.Target)} past the nether lips.\n<b>{i.Unit.Name}</b> appreciates the act and caresses {GPPHis(i.Unit)} {i.preyLocation.ToSyn()}.",
                priority: 12, conditional: s => Friendly(s) && Endo(s)),
            new EventString((i) => $"<b>{i.Unit.Name}</b> has to reassure <b>{i.Target.Name}</b> that it's safe, before the latter finally dives head first up {GPPHis(i.Unit)} cunt.",
                priority: 12, conditional: s => Friendly(s) && Endo(s)),
            new EventString((i) => $"<b>{i.Unit.Name}</b> grabs the shrunken <b>{i.Target.Name}</b> and brings {GPPHim(i.Target)} to {GPPHis(i.Unit)} pussy, shoving {GPPHim(i.Target)} halfway inside {GPPHis(i.Unit)} hole. \"Relax, it's me, {i.Unit.Name},\" {GPPHe(i.Unit)} reassures {GPPHim(i.Target)}. <b>{i.Target.Name}</b> relaxes as {GPPHis(i.Target)} comrade pushes {GPPHim(i.Target)} inside {GPPHis(i.Unit)} {i.preyLocation.ToSyn()} completely.",
                priority: 13, conditional: s => Friendly(s) && Endo(s) && Shrunk(s)),
        };

        TailVoreMessages = new List<EventString>()
        {
            new EventString((i) => $"<b>{i.Unit.Name}</b> catches <b>{i.Target.Name}</b> off guard and crams {GPPHim(i.Target)} down {GPPHis(i.Unit)} tail.", priority: 8),
            new EventString((i) => $"<b>{i.Unit.Name}</b> slurps up <b>{i.Target.Name}</b> with {GPPHis(i.Unit)} tail!", priority: 8),
            new EventString((i) => $"<b>{i.Unit.Name}</b> yanks <b>{i.Target.Name}</b> into {GPPHis(i.Unit)} hungry tail maw.", priority: 8),
            new EventString((i) => $"<b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> struggles are for naught as {GPPHe(i.Target)} vanishe{SIfSingular(i.Target)} into <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> ravenous tail.", priority: 8),

            new EventString((i) => $"<b>{i.Unit.Name}</b> blows <b>{i.Target.Name}</b> a kiss before sliding the tip of {GPPHis(i.Unit)} tail over <b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> head and slurping the {GetPreyDesc(i.Target)} {GetRaceDescSingl(i.Target)} up!", actorRace: Race.Succubus, priority: 9),
            new EventString((i) => $"<b>{i.Unit.Name}</b> distracts <b>{i.Target.Name}</b> with a tight sensual hug, just enough for {GPPHis(i.Unit)} to suck <b>{i.Target.Name}</b> into {GPPHis(i.Unit)} tail!", actorRace: Race.Succubus, priority: 9),
            new EventString((i) => $"<b>{i.Unit.Name}</b> flutters over <b>{i.Target.Name}</b> and causally drops {GPPHis(i.Unit)} tail down far enough to vaccum <b>{i.Target.Name}</b> into the hungry opening.", actorRace: Race.Succubus, priority: 9),
            new EventString((i) => $"<b>{i.Target.Name}</b> finds {GPPHimself(i.Target)} stunned with steamy thoughts when <b>{i.Unit.Name}</b> blows {GPPHim(i.Target)} a sensuous kiss; long enough for <b>{i.Unit.Name}</b> to plunge {GPPHim(i.Target)} into <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> hungry tail.", actorRace: Race.Succubus, priority: 9),
            new EventString((i) => $"<b>{i.Unit.Name}</b> flashes {GPPHis(i.Unit)} {(i.Unit.HasDick ? "dick" : "breasts")} at <b>{i.Target.Name}</b>, distracting {GPPHim(i.Target)} long enough to swallow the {GetPreyDesc(i.Target)} {GetRaceDescSingl(i.Target)} feet first with {GPPHis(i.Unit)} tail.", actorRace: Race.Succubus, priority: 9, conditional: s => Lewd(s) && TargetHumanoid(s)),
            new EventString((i) => $"<b>{i.Unit.Name}</b> seizes <b>{i.Target.Name}</b> in a tight hug, grinding {GPPHis(i.Unit)} {(i.Unit.HasDick ? "dick" : "breasts")} against <b>{i.Target.Name}</b> before pulling <b>{i.Target.Name}</b> down into the awaiting tail maw.", actorRace: Race.Succubus, priority: 9, conditional: s => Lewd(s) && TargetHumanoid(s)),
            new EventString((i) => $"<b>{i.Unit.Name}</b> slams {GPPHis(i.Unit)} {PreyLocStrings.ToSyn(PreyLocation.anal)} down on <b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> face, smothering <b>{i.Target.Name}</b> while {GPPHe(i.Unit)} devour{SIfSingular(i.Unit)} with <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> tail maw.", actorRace: Race.Succubus, priority: 9, conditional: s => Lewd(s) && TargetHumanoid(s)),

            new EventString((i) => $"<b>{i.Unit.Name}</b> coils {GPPHimself(i.Unit)} around <b>{i.Target.Name}</b>, feeding {GPPHim(i.Unit)} into the greedy hungry tail mouth!", actorRace: Race.Viper, priority: 9),
            new EventString((i) => $"<b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> tail lashes out with a loud crack and <b>{i.Target.Name}</b> promptly finds {GPPHimself(i.Target)} slithering into its long hot slimy depths.", actorRace: Race.Viper, priority: 9),

            new EventString((i) => $"<b>{i.Unit.Name}</b> grabs <b>{i.Target.Name}</b> and stuffs {GPPHim(i.Target)} down into an awaiting {PreyLocStrings.ToSyn(PreyLocation.womb)}. <b>{i.Target.Name}</b> quickly slides down it, not into a womb but into <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> voracious tail stomach.", actorRace: Race.Lamia, priority: 9, conditional: s => Lewd(s) && s.Unit.GetGender() == Gender.Female),
            new EventString((i) => $"<b>{i.Target.Name}</b> finds {GPPHimself(i.Target)} trapped in <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> crushing tail coils and the {GetPreyDesc(i.Target)} {GetRaceDescSingl(i.Target)} is devoured by the hungry tail maw.", actorRace: Race.Lamia, priority: 9),
            new EventString((i) => $"With a loud thud, <b>{i.Target.Name}</b> is knocked over by <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> lashing tail then reeled into its greedy tail maw!", actorRace: Race.Lamia, priority: 9),

            new EventString((i) => $"<b>{i.Unit.Name}</b> latches {GPPHis(i.Unit)} stinger into <b>{i.Target.Name}</b> and uses it like a straw to drink the {GetPreyDesc(i.Target)} {GetRaceDescSingl(i.Target)} down!", actorRace: Race.Bee, targetRace: Race.Slime, priority: 9),
            new EventString((i) => $"<b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> tail tip expands wide, engulfing <b>{i.Target.Name}</b> and trapping {GPPHim(i.Target)} in <b>{i.Unit.Name}</b> honey filled abdomen!", actorRace: Race.Bee, priority: 9),
            new EventString((i) => $"<b>{i.Unit.Name}</b> lunges {GPPHim(i.Unit)} stinger rapidly, jabbing it into <b>{i.Target.Name}</b>. While <b>{i.Target.Name}</b> reels from the pain, the stinger widens to a huge cone and {PreyLocStrings.GetOralVoreVPT()} {GPPHim(i.Target)}!", actorRace: Race.Bee, priority: 9),
            new EventString((i) => $"<b>{i.Unit.Name}</b> stings <b>{i.Target.Name}</b> and it swiftly swells wide enough to swallow up <b>{i.Target.Name}</b> whole!", actorRace: Race.Bee, priority: 9),
            new EventString((i) => $"<b>{i.Unit.Name}</b> throws {GPPHimself(i.Unit)} butt first at <b>{i.Target.Name}</b> with an armed tail stinger that wraps around <b>{i.Target.Name}</b> and swallows {GPPHim(i.Target)} upon impact!", actorRace: Race.Bee, priority: 9),

            new EventString((i) => $"<b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> world goes dark as <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> rapacious tail descends on <b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> head, engulfing {GPPHim(i.Target)} completely.", priority: 9, conditional: TargetHumanoid),
            new EventString((i) => $"<b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> tail tip expands around <b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> legs and reels {GPPHim(i.Target)} down greedily.", priority: 9, conditional: TargetHumanoid),
            new EventString((i) => $"<b>{i.Unit.Name}</b> pushes <b>{i.Target.Name}</b> over and snickers as {GPPHis(i.Unit)} tail consumes {GPPHis(i.Unit)} prey.", priority: 9, conditional: TargetHumanoid),

            new EventString((i) => $"<b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> struggling form is seen through <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> crop as they slide down into terror bird's waiting stomach", actorRace: Race.Terrorbird, priority: 10),
            new EventString((i) => $"<b>{i.Unit.Name}</b> is struggling to swallow <b>{i.Target.Name}</b> in a single bite, and so waits until {GetPreyDesc(i.Target)} {GetRaceDescSingl(i.Target)} tires themselves out", actorRace: Race.Terrorbird, priority: 10),
            new EventString((i) => $"A brief scream sounds through the battlefield and the only reminder of <b>{i.Target.Name}</b> ever being there is <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> bulging crop", actorRace: Race.Terrorbird, priority: 10),
            new EventString((i) => $"Every time <b>{i.Unit.Name}</b> opens their mouth, anyone can see <b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> terrified face, still struggling to get out of terror bird's esophagus", actorRace: Race.Terrorbird, priority: 10),

            new EventString((i) => $"<b>{i.Unit.Name}</b> stuffs <b>{i.Target.Name}</b> into {GPPHis(i.Unit)} {PreyLocStrings.ToSyn(i.preyLocation)}.", priority: 25, conditional: HasGreatEscape),
            new EventString((i) => $"<b>{i.Target.Name}</b> worries surprisingly little about being shoved into <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> {PreyLocStrings.ToSyn(i.preyLocation)}.", priority: 25, conditional: HasGreatEscape),
            new EventString((i) => $"<b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> {PreyLocStrings.ToSyn(i.preyLocation)} welcomes another guest, this one livelier than the most.", priority: 25, conditional: HasGreatEscape),


            //Endo
            new EventString((i) => $"<b>{i.Target.Name}</b> blushes when <b>{i.Unit.Name}</b> embraces {GPPHim(i.Target)} and sends {GPPHim(i.Target)} sliding into {GPPHis(i.Unit)} tail.",
                priority: 12, conditional: s => Friendly(s) && Endo(s)),
            new EventString((i) => $"<b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> tail slit gently engulfs {GPPHis(i.Unit)} ally. For <b>{i.Target.Name}</b>, the noise of the battlefield is replaced with gently sloshing and throbbing, as well as an all-encompassing heartbeat.",
                priority: 12, conditional: s => Friendly(s) && Endo(s)),
            new EventString((i) => $"After a moment of nervous hesitation, <b>{i.Target.Name}</b> comfortably slips into <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> tail folds.",
                priority: 12, conditional: s => Friendly(s) && Endo(s)),
            new EventString((i) => $"<b>{i.Target.Name}</b> finds {GPPHimself(i.Target)} thoroughly played with by <b>{i.Unit.Name}</b>, {GPPHis(i.Target)} ally, before being packed away into the safety of {GPPHis(i.Unit)} tail.",
                priority: 12, conditional: s => Friendly(s) && Endo(s)),
            new EventString((i) => $"<b>{i.Target.Name}</b> squirms and thrashes about, trying to free {GPPHimself(i.Target)} from the grasp of {GPPHis(i.Target)} vicious predator... and then {(Lewd(i) ? "moans in delight" : "giggles")} as the last of {GPPHim(i.Target)} vanish{EsIfSingular(i.Target)} into the tail.\n<b>{i.Unit.Name}</b> appreciates the act and caresses {GPPHis(i.Unit)} {i.preyLocation.ToSyn()}.",
                priority: 12, conditional: s => Friendly(s) && Endo(s)),
            new EventString((i) => $"<b>{i.Unit.Name}</b> has to reassure <b>{i.Target.Name}</b> that it's safe, before the latter finally dives head first into {GPPHis(i.Unit)} tail opening.",
                priority: 12, conditional: s => Friendly(s) && Endo(s)),
        };

        DigestionDeathMessages = global::DigestionDeathMessages.DigestionDeathMessagesList;

        AbsorptionMessages = new List<EventString>()
        {
            //generic
            new EventString((i) => $"<b>{i.Unit.Name}</b> enjoys the added weight of <b>{i.Target.Name}</b> on {GPPHis(i.Unit)} body.", priority: 8),
            new EventString((i) => $"The last of <b>{i.Target.Name}</b> is absorbed into <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> {i.preyLocation.ToSyn()}.", priority: 8),
            new EventString((i) => $"<b>{i.Target.Name}</b> is now completely gone, soon to be forgotten as just another meal.", priority: 8),
            new EventString((i) => $"<b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> nutrients are now all <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b>", priority: 8),

            //Weight gain generic
            new EventString((i) => $"<b>{i.Unit.Name}</b> smirks, noticing {GPPHis(i.Unit)} assets feel heavier after absorbing <b>{i.Target.Name}</b>.", priority: 8, conditional: WeightGain),
            new EventString((i) => $"<b>{i.Unit.Name}</b> slaps {GPPHis(i.Unit)} ass, enjoying its newfound roundness and wobbliness.", priority: 8, conditional: s => WeightGain(s) && ActorHumanoid(s)),
            new EventString((i) => $"<b>{i.Unit.Name}</b> checks {GPPHis(i.Unit)} new curves, wondering if <b>{AttractedWarrior(i.Unit)}</b> would find {GPPHim(i.Unit)} more attractive now.", priority: 8, conditional: s => WeightGain(s) && ReqOSW(s)),
            new EventString((i) => $"<b>{i.Unit.Name}</b> slaps {GPPHis(i.Unit)} newly made flab with a laugh ", actorRace: Race.Hippo, priority: 9, conditional: WeightGain),
            new EventString((i) => $"<b>{i.Unit.Name}</b> lifts {GPPHis(i.Unit)} belly and drops it with a bounce, it being pudgier after absorbing <b>{i.Target.Name}</b>.", priority: 8, conditional: s => WeightGain(s) && ActorHumanoid(s)),
            new EventString((i) => $"<b>{i.Unit.Name}</b> caresses {GPPHis(i.Unit)} {i.preyLocation.ToSyn()} now that <b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> substance has become part of it.", priority: 8, conditional: WeightGain),
            new EventString((i) => $"<b>{i.Unit.Name}</b> enjoys kneading {GPPHis(i.Unit)} {i.preyLocation.ToSyn()} with its added heft from <b>{i.Target.Name}</b>, but wishes <b>{PotentialNextPrey(i.Unit).Name}</b> would add to it even more", priority: 8, conditional: s => WeightGain(s) && (CanAddressPlayer(s) || PotentialNextPrey(s.Unit).Name != "You, the player")),

            //stomach-exclusive
            new EventString((i) => $"With <b>{i.Target.Name}</b> all sucked up by <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> intestines, {GPPHe(i.Unit)} immediately misses the sloshy feeling and looks around for {GPPHis(i.Unit)} next meal", priority: 9, conditional: InStomach),
            new EventString((i) => $"<b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> stomach flattens, having absorbed the last of <b>{i.Target.Name}</b>.", priority: 9, conditional: InStomach),
            new EventString((i) => $"<b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> stomach flattens, having absorbed the last of - what was {GPPHis(i.Target)} name, again?", priority: 9, conditional: InStomach),
            new EventString((i) => $"Gurgling gives way to silence, as last bits of <b>{i.Target.Name}</b> find themselves a better purpose - to provide <b>{i.Unit.Name}</b> with nutrients.", priority: 9, conditional: InStomach),
            new EventString((i) => $"<b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> belly finally contracts as if <b>{i.Target.Name}</b> never existed.", priority: 9, conditional: InStomach),
            new EventString((i) => $"<b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> belly finally contracts as if - what was {GPPHis(i.Target)} name, again? never existed.", priority: 9, conditional: InStomach),

            //Scat disposal
            new EventString((i) => $"<b>{i.Unit.Name}</b> enjoys feeling the weight of <b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> remains squeeze out of {GPPHis(i.Unit)} ass.", priority: 9, conditional: Scat),
            new EventString((i) => $"<b>{i.Unit.Name}</b> takes a lewd pleasure in <b>{i.Target.Name}</b> sliding out of {GPPHis(i.Unit)} rectum as several thick turds.", priority: 9, conditional: Scat),
            new EventString((i) => $"<b>{i.Unit.Name}</b> smirks, noticing {GPPHis(i.Unit)} ass feels heavier after absorbing and eliminating <b>{i.Target.Name}</b>.", priority: 9, conditional: Scat),
            new EventString((i) => $"<b>{i.Unit.Name}</b> takes a quick dump to purge <b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> foul remains from {GPPHis(i.Unit)} guts.", priority: 9, conditional: Scat),
            new EventString((i) => $"<b>{i.Unit.Name}</b> shits out a large, steamy batch of fresh manure. This wasn't the escape <b>{i.Target.Name}</b> had been looking for in {GPPHis(i.Unit)} dark stomach.", priority: 9, conditional: Scat),
            new EventString((i) => $"<b>{i.Unit.Name}</b> moans in disgust and pleasure, feeling <b>{i.Target.Name}</b> squeeze through {GPPHis(i.Unit)} colon and out of {GPPHis(i.Unit)} plump {PreyLocStrings.ToSyn(PreyLocation.anal)}.", priority: 9, conditional: Scat),
            new EventString((i) => $"<b>{i.Unit.Name}</b> presses down on {GPPHis(i.Unit)} {i.preyLocation.ToSyn()}, lolling out {GPPHis(i.Unit)} tongue in relief while letting loose one huge log of shit. There's <b>{i.Target.Name}</b>...", priority: 9, conditional: Scat),
            new EventString((i) => $"<b>{i.Target.Name}</b> is completely digested, dumped out as a gross scat pile behind <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> paws.", priority: 9, conditional: Scat),
            new EventString((i) => $"<b>{i.Target.Name}</b> finishes digesting in <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> bowels, and is promptly eliminated from {GPPHis(i.Unit)} body.", priority: 9, conditional: Scat),
            new EventString((i) => $"<b>{i.Unit.Name}</b> squats to deposit <b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> used-up remains in a messy pile below {GPPHis(i.Unit)} tail.", priority: 9, conditional: s => Scat(s) && ActorTail(s)),
            new EventString((i) => $"After squeezing out <b>{i.Target.Name}</b> as a nice, {GetRandomStringFrom("nasty", "creamy", "warm", "greasy")} {GetRandomStringFrom("log", "turd", "brown lump")}, <b>{i.Unit.Name}</b> winks at <b>{PotentialNextPrey(i.Unit).Name}</b>, as if to say \"You're next!\".\n...{GPPHis(i.Unit)} sphincter seems to do the same.", priority: 9, conditional: s => Scat(s) && (CanAddressPlayer(s) || PotentialNextPrey(s.Unit).Name != "You, the player")),
            new EventString((i) => $"After squeezing out ...What was {GPPHis(i.Target)} name, again? as a nice, {GetRandomStringFrom("nasty", "creamy", "warm", "greasy")} {GetRandomStringFrom("log", "turd", "brown lump")}, <b>{i.Unit.Name}</b> winks at <b>{PotentialNextPrey(i.Unit).Name}</b>, as if to say \"You're next!\".\n...{GPPHis(i.Unit)} sphincter seems to do the same.", priority: 9, conditional: s => Scat(s) && (CanAddressPlayer(s) || PotentialNextPrey(s.Unit).Name != "You, the player")),
            new EventString((i) => $"<b>{i.Unit.Name}</b> looks back at the vile mass of <b>{i.Target.Name}</b> that just slid out {GPPHis(i.Unit)} behind, then at <b>{PotentialNextPrey(i.Unit).Name}</b>, then back at {GPPHis(i.Unit)} {GetRandomStringFrom("dump", "shit", "crap")}, before licking {GPPHis(i.Unit)} lips at <b>{PotentialNextPrey(i.Unit).Name}.</b>", priority: 9, conditional: s => Scat(s) && (CanAddressPlayer(s) || PotentialNextPrey(s.Unit).Name != "You, the player")),
            new EventString((i) => $"<b>{i.Unit.Name}</b> is proud of the steaming mound of shit he turned <b>{i.Target.Name}</b> into, and desires to add <b>{PotentialNextPrey(i.Unit).Name}</b> on top while it's warm.", priority: 9, conditional: s => Scat(s) && (CanAddressPlayer(s) || PotentialNextPrey(s.Unit).Name != "You, the player")),
            new EventString((i) => $"<b>{i.Target.Name}</b> is nothing but {GetRandomStringFrom("a gross brown mass", "stinking turds", "fresh, warm shit", "disgusting waste")} that's now leaving <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> rectum", priority: 9, conditional: Scat),
            new EventString((i) => $"<b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> guts are done with <b>{i.Target.Name}</b>, leaving {GPPHim(i.Target)} to be disposed of as nothing but {GetRandomStringFrom("a gross brown pile", "stinking turds", "fresh, warm shit", "disgusting waste", "a creamy dump")}.", priority: 9, conditional: Scat),
            new EventString((i) => $"With a squint and a push from <b>{i.Unit.Name}</b>, <b>{i.Target.Name}</b> manages to achieve {GPPHis(i.Target)} final form as {InfoPanel.RaceSingular(i.Unit)} droppings.", priority: 9, conditional: Scat),
            new EventString((i) => $"<b>{i.Unit.Name}</b> grunts, and the filthy remains of {GPPHis(i.Unit)} now used-up prey splat onto the ground behind {GPPHim(i.Unit)}. <b>{i.Target.Name}</b> is completely unrecognizable.", priority: 9, conditional: Scat),
            new EventString((i) => $"All that's left of <b>{i.Target.Name}</b> is a {InfoPanel.RaceSingular(i.Target)}-sized load of <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> feces. Good fight.", priority: 9, conditional: Scat),
            new EventString((i) => $"<b>{i.Unit.Name}</b>: <i>~PFRRRRRT~</i>. <b>{i.Target.Name}</b>: <i>~Splat~</i>", priority: 9, conditional: s => Scat(s) && Farts(s)),
            new EventString((i) => $"A cacophonous fart out of <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> hind side heralds <b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> smelly return to the battlefield...", priority: 9, conditional: s => Scat(s) && Farts(s)),
            new EventString((i) => $"<b>{i.Unit.Name}</b> relieves {GPPHimself(i.Unit)}...\nThe obscene release of gas will have to suffice as <b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> eulogy.", priority: 9, conditional: s => Scat(s) && Farts(s)),
            new EventString((i) => $"With the amount of shit piling up under <b>{i.Unit.Name}</b>' ass, one could think <b>{i.Target.Name}</b> was mostly waste to begin with.", priority: 9, conditional: s => Scat(s)),
            new EventString((i) => $"<b>{i.Unit.Name}</b> exerts {GPPHimself(i.Unit)}, letting loose one juicy burst of gas, before having <b>{i.Target.Name}</b> emerge and drop out of that same hole to form a pile of {GetRandomStringFrom("nasty", "creamy", "warm", "greasy")} {GetRandomStringFrom("logs", "turds", "brown lumps")}", priority: 9, conditional: s => Scat(s) && Farts(s)),
            new EventString((i) => $"<b>{i.Target.Name}</b> has been reduced to {GetRandomStringFrom("shit", "waste", "a dump", "scat")} by <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> digestive system.", priority: 9, conditional: s => Scat(s)),
            new EventString((i) => $"Just like that, <b>{i.Target.Name}</b> has been casually shat onto the ground as mere {InfoPanel.RaceSingular(i.Unit)} waste, and <b>{i.Unit.Name}</b> certainly won't remember what {GPPHe(i.Unit)} ate.", priority: 9, conditional: s => Scat(s)),
            new EventString((i) => $"<b>{i.Unit.Name}</b> raises {GPPHis(i.Unit)} tail and closes {GPPHis(i.Unit)} eyes as <b>{i.Target.Name}</b> is finally set free - in a form that not even a mother could recognize.", priority: 9, conditional: s => Scat(s)),
            new EventString((i) => $"<b>{i.Unit.Name}</b> shits a load of <b>{i.Target.Name}</b> onto the floor", priority: 9, conditional: s => Scat(s)),
            new EventString((i) => $"<b>{i.Unit.Name}</b> thinks <b>{i.Target.Name}</b> looks better as this neat arrangement of {GetRaceDescSingl(i.Unit)} turds", priority: 9, conditional: s => Scat(s)),
            new EventString((i) => $"After a bit of a messy dump, <b>{i.Unit.Name}</b> wonders which turd could be that {InfoPanel.RaceSingular(i.Target)} leader, but can't quite tell.", priority: 9, conditional: s => Scat(s) && TargetLeader(s)),
            new EventString((i) => $"The friction from <b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> remains sliding past {GPPHis(i.Unit)} anal sphincter sends arousing tingles through <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> body.", priority: 9, conditional: s => Scat(s) && Lewd(s)),
            //Fart disposal
            new EventString((i) => $"<b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> sphincter blows <b>{i.Target.Name}</b> a smelly requiem. Looks like {GPPHis(i.Unit)} innards are done with their prey.", priority: 9, conditional: s => Farts(s) && InStomach(s)),
            new EventString((i) => $"<b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> rump is eager to announce that <b>{i.Target.Name}</b> is completely used up, and has a loud, satisfying fart to show for it.", priority: 9, conditional: s => Farts(s) && InStomach(s)),
            new EventString((i) => $"R.I.P <b>{i.Target.Name}</b>, Turn {Math.Max(State.World.Turn - (int)(Math.Pow(i.Target.Level, 1.5) / 2), 1)} - Turn {State.World.Turn}. Once Intrepid member of the {InfoPanel.RaceSingular(i.Target)} Empire military forces, now an assfull of {InfoPanel.RaceSingular(i.Unit)} farts" + (Scat(i) ? $" and shit" : $""), // Arbitrary formula arbitrarily seeking to correlate level with time alive in an arbitrary manner
                priority: 9, conditional: s => InStomach(s) && Farts(s) && TargetHumanoid(s) && !State.GameManager.PureTactical),
            new EventString((i) => $"<b>{i.Unit.Name}</b> passes gas – <b>{i.Target.Name}</b> passes on to the next life.", priority: 9, conditional: s => Farts(s) && !Scat(s) && InStomach(s)),
            new EventString((i) => $"<b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> ass vibrates loudly as it releases <b>{i.Target.Name}</b> as a gust of stink.", priority: 9, conditional: s => Farts(s) && !Scat(s) && InStomach(s)),
            new EventString((i) => $"<b>{i.Target.Name}</b> has become nothing but an obscene melody under <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> tail", priority: 9, conditional: s => Farts(s) && !Scat(s) && ActorTail(s) && InStomach(s)),
            //CV disposal
            new EventString((i) => $"<b>{i.Unit.Name}</b> sways {GPPHis(i.Unit)} hips side to side as what is left of <b>{i.Target.Name}</b> gushes from {GPPHis(i.Unit)} {PreyLocStrings.ToCockSyn()} onto the ground.", priority: 9, conditional: s => ActorHumanoid(s) && InBalls(s)),
            new EventString((i) => $"Not being able to hold anymore, <b>{i.Unit.Name}</b> orgasms, {GPPHis(i.Unit)} {PreyLocStrings.ToCockSyn()} expelling in spurts what once was <b>{i.Target.Name}</b>.", priority: 9, conditional: InBalls),
            new EventString((i) => $"With nothing to absorb anymore, <b>{i.Unit.Name}</b> cums out the puddle of <b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> remains.", priority: 9, conditional: InBalls),
            new EventString((i) => $"<b>{i.Unit.Name}</b> demonstrates {GPPHis(i.Unit)} virility to <b>{AttractedWarrior(i.Unit)}</b> by splattering <b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> remains across the ground in an impressive puddle.", priority: 9, conditional: s => InBalls(s) && ReqOSWLewd(s)),

            new EventString((i) => $"With a monstrous roar, <b>{i.Unit.Name}</b> ejaculates, {GPPHis(i.Unit)} cock spurting out the remains of <b>{i.Target.Name}</b> in thick globs.",
                actorRace: Race.Dragon, priority: 9, conditional: InBalls),

            new EventString((i) => $"<b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> dick spurts out the remains of <b>{i.Target.Name}</b>. Then {GPPHe(i.Unit)} proudly gazes at the puddle {GPPHis(i.Unit)} former servant made.",
                actorRace: Race.Dragon, targetRace: Race.Kobold, priority: 9, conditional: s => InBalls(s) && Friendly(s)),

            new EventString((i) => $"\"Attention {GetRaceDescSingl(i.Target)}, this is what has become of your 'mighty' leader,\" <b>{i.Unit.Name}</b> taunts as {GPPHe(i.Unit)} jerk{SIfSingular(i.Unit)} {GPPHimself(i.Unit)} to orgasm, splattering the remains of <b>{i.Target.Name}</b> all over the ground in an impressive puddle.",
                priority: 9, conditional: s => ActorLeader(s) && InBalls(s) && TargetLeader(s)),

            //BV disposal
            new EventString((i) => $"<b>{i.Unit.Name}</b> leans forward and moans, letting what's left of <b>{i.Target.Name}</b> pour out of {GPPHis(i.Unit)} nipples onto the ground.", priority: 9, conditional: InBreasts),
            new EventString((i) => $"<b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> breasts finally settle, the streams of milk pouring from them being only remnant of what happened to <b>{i.Target.Name}</b>.", priority: 9, conditional: InBreasts),
            new EventString((i) => $"<b>{i.Unit.Name}</b> basks in the triumph of {GPPHis(i.Unit)} mighty breasts as <b>{i.Target.Name}</b> is totally absorbed into them without a trace.", priority: 9, conditional: InBreasts),
            new EventString((i) => $"<b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> cleavage is absorbed into the <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> victorious {i.preyLocation.ToSyn()}. \"Mine are bigger and badder.\", <b>{i.Unit.Name}</b> scorns.", priority: 9, conditional: s => InBreasts(s) && TargetBoobs(s)),
            new EventString((i) => $"<b>{i.Unit.Name}</b> pants as {GPPHis(i.Unit)} {PreyLocStrings.ToBreastSynPlural()} squirt out the milky remains of <b>{i.Target.Name}</b>, making them wobble pleasurably in the process.", priority: 9, conditional: InBreasts),
            new EventString((i) => $"<b>{i.Unit.Name}</b> thrusts {GPPHis(i.Unit)} {PreyLocStrings.ToBreastSynPlural()} at <b>{AttractedWarrior(i.Unit)}</b>, then gives them a good squeeze to coat {GPPHis(i.Unit)} lover with fresh milk made from <b>{i.Target.Name}</b>.", priority: 9, conditional: s => InBreasts(s) && ReqOSWLewd(s)),
            new EventString((i) => $"<b>With a loud and tense grunt, <b>{i.Unit.Name}</b> squeezes out the undigested bones of <b>{i.Target.Name}</b> out of {GPPHis(i.Unit)} {i.preyLocation.ToSyn()}, the process well lubricated by gushing breast milk.", priority: 9, conditional: s => InBreasts(s) && BonesDisposal(s)),

            //Bone Disposal
            new EventString((i) => $"<b>{i.Unit.Name}</b> takes a lewd pleasure in <b>{i.Target.Name}</b> sliding out of {GPPHis(i.Unit)} {PreyLocStrings.ToSyn(PreyLocation.anal)} as clean bleached bones.", priority: 9, conditional: s => InStomach(s) && BonesDisposal(s)),
            new EventString((i) => $"<b>{i.Unit.Name}</b> moans in pleasure, feeling <b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> bones squeeze out of {GPPHis(i.Unit)} plump {PreyLocStrings.ToSyn(PreyLocation.anal)}.", priority: 9, conditional: s => InStomach(s) && BonesDisposal(s)),
            new EventString((i) => $"<b>{i.Target.Name}</b> finishes digesting in <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> bowels, and {GPPHis(i.Target)}'s skeletal remains are promptly eliminated from predator's body.", priority: 9, conditional: s => InStomach(s) && BonesDisposal(s)),

            //Slimes exclusive
            new EventString((i) => $"The dark lump in <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> translucent form disappears completely.", actorRace: Race.Slime, priority: 10),
            //Kangaroo pred (pouch vore)
            new EventString((i) => $"As <b>{i.Unit.Name}</b>'s {GetRandomStringFrom("lower torso", "pouch", "marsupium")} shrinks back to its original shape, <b>{i.Unit.Name}</b>'s pouchs entrance unseals, revealing nothing inside, as though <b>{i.Target.Name}</b> never existed.", actorRace: Race.Kangaroo, priority: 10),
            new EventString((i) => $"As <b>{i.Unit.Name}</b>'s {GetRandomStringFrom("lower torso", "pouch", "marsupium")} shrinks back to its original shape, <b>{i.Unit.Name}</b>'s pouchs entrance unseals, revealing it to be empty save for a pile of crumpled, but otherwise unharmed, clothes. There is no trace of <b>{i.Target.Name}</b>.", actorRace: Race.Kangaroo, priority: 10),
            new EventString((i) => $"<b>{i.Unit.Name}</b> slowly pulls open {GPPHis(i.Unit)} pouch, finding nothing inside at all.", actorRace: Race.Kangaroo, priority: 10),
            // Todo: Figure this one out
            //new EventString((i) => $"<b>{i.Unit.Name}</b> slowly pulls open {GPPHis(i.Unit)} pouch, finding nothing inside at all. <b>{i.Unit.Name}</b> then refoccuses {GPPHis(i.Unit)} attention on the {battlefield/battle<only used if non-surrendered enemies still remain on the field>}, rapidly forgetting that <b>{i.Target.Name}</b> had ever exsisted.", actorRace: Race.Kangaroos, priority: 10),
            new EventString((i) => $"<b>{i.Unit.Name}</b> slowly pulls open {GPPHis(i.Unit)} pouch, finding nothing inside at all. Wait, was that a handprint, pushing outward from the inside lining of the pouch? It's frankly too dark to tell, and even more frankly, <b>{i.Unit.Name}</b> doesn't care.", actorRace: Race.Kangaroo, priority: 10),
            new EventString((i) => $"<b>{i.Unit.Name}</b> slowly pulls open {GPPHis(i.Unit)} pouch, finding nothing inside at all, save a pile of crumpled clothes. <b>{i.Unit.Name}</b> pulls them out, and examines them for a moment, before saying \"wrong size,\" and casually tosses them over {GPPHis(i.Unit)} shoulder.", actorRace: Race.Kangaroo, priority: 10),
            new EventString((i) => $"<b>{i.Unit.Name}</b>'s pouch unseals, ready to hold more{GetRandomStringFrom(" occupants", " (temporary) occupants", " \"occupants\"")}.", actorRace: Race.Kangaroo, priority: 10),
            new EventString((i) => $"<b>{i.Unit.Name}</b>'s pouch has shrunk back down to normal size and shape, with no evidence that a {GetRaceDescSingl(i.Target)} had ever been inside.", actorRace: Race.Kangaroo, priority: 10),
            new EventString((i) => $"With <b>{i.Target.Name}</b> fully absorbed, <b>{i.Unit.Name}</b>'s pouch unseals, revealing not one hint as to what happened to its previous {GetRandomStringFrom("occupant", "tenant")}.", actorRace: Race.Kangaroo, priority: 10),
        };
        TransferMessages = new List<EventString>()
        {
            new EventString((i) => $"<b>{i.Unit.Name}</b> pumps the cum left of {GPPHis(i.Unit)} prey into <b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> mouth; {i.Target.Name} is happy with the result.",
                priority: 10, conditional: s => InStomach(s) && s.Prey.IsDead),
            new EventString((i) => $"<b>{i.Unit.Name}</b> pumps <b>{i.Prey.Name}</b> into <b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> {i.preyLocation.ToSyn()}; {i.Target.Name} is happy with the result.", priority: 8),

            //Very lewd
            new EventString((i) => $"<b>{i.Unit.Name}</b> beckons <b>{i.Target.Name}</b> over and tells {GPPHim(i.Target)} to start sucking. <b>{i.Target.Name}</b> is just too good with {GPPHis(i.Target)} mouth and <b>{i.Unit.Name}</b> {GetRandomStringFrom("climaxes", "orgasms", "goes over the edge", "cums", "ejaculates")} soon after, bloating out the {GetRaceDescSingl(i.Target)}'s belly as {GPPHis(i.Unit)} balls shrink down to normal.",
                priority: 11, conditional: s => InStomach(s) && Lewd(s)),
            new EventString((i) => $"<b>{i.Unit.Name}</b> calls <b>{i.Target.Name}</b> over and before long, <b>{i.Target.Name}</b> has pushed {GPPHim(i.Unit)} down and is riding {GPPHim(i.Unit)}. Soon thereafter, <b>{i.Unit.Name}</b> shoots <b>{i.Prey.Name}</b> into <b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> {i.preyLocation.ToSyn()}.",
                priority: 11, conditional: s => !InStomach(s) && Lewd(s) && !s.Prey.IsDead),
            new EventString((i) => $"<b>{i.Unit.Name}</b> calls <b>{i.Target.Name}</b> over and before long, <b>{i.Target.Name}</b> has pushed {GPPHim(i.Unit)} down and is riding {GPPHim(i.Unit)}. Soon thereafter, <b>{i.Unit.Name}</b> shoots {GPPHis(i.Unit)} load - freshly made from <b>{i.Prey.Name}</b> - into <b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> {i.preyLocation.ToSyn()}.",
                priority: 11, conditional: s => !InStomach(s) && Lewd(s) && s.Prey.IsDead),
            //Very lewd

            new EventString((i) => $"<b>{i.Target.Name}</b> wraps {GPPHis(i.Unit)} mouth around <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> member and begins servicing it. <b>{i.Unit.Name}</b> can't hold on any longer and blows {GPPHis(i.Unit)} load into <b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> mouth. <b>{i.Target.Name}</b> swallows most of it and {GPPHis(i.Target)} belly bloats out.",
                priority: 11, conditional: s => InStomach(s) && Lewd(s)),
        };
        VoreStealMessages = new List<EventString>()
        {
            // If you can make this not lewd, please advise, otherwise I may need to lock the whole mechanic behind lewd texts
            new EventString((i) => $"<b>{i.Unit.Name}</b> tackles <b>{i.Target.Name}</b> and rides {GPPHim(i.Target)} until {GPPHe(i.Target)} release{SIfSingular(i.Target)} <b>{i.Prey.Name}</b> into {GPPHis(i.Unit)} womb.",
                priority: 10, conditional: s => s.oldLocation == PreyLocation.balls && s.preyLocation == PreyLocation.womb),
            new EventString((i) => $"<b>{i.Target.Name}</b> can't bring {GPPHimself(i.Target)} to fight off <b>{i.Unit.Name}</b> when {GPPHe(i.Unit)} start{SIfSingular(i.Unit)} sucking on {GPPHis(i.Target)} {PreyLocStrings.ToBreastSynPlural()}; {i.Unit.Name} doesn't relent until <b>{i.Prey.Name}</b> is released from {GPPHis(i.Target)} {PreyLocStrings.ToBreastSynPlural()}.",
                priority: 10, conditional: s => (s.oldLocation == PreyLocation.breasts || s.oldLocation == PreyLocation.leftBreast || s.oldLocation == PreyLocation.rightBreast) && s.preyLocation == PreyLocation.stomach),
            new EventString((i) => $"<b>{i.Unit.Name}</b> knocks down <b>{i.Target.Name}</b> and begins sucking {GPPHis(i.Target)} rod. Astonished, {i.Target.Name} doesn't even realize it when {GPPHe(i.Target)} feed{SIfSingular(i.Target)} <b>{i.Prey.Name}</b> to {i.Unit.Name}.", priority: 8),
        };
        BreastFeedMessages = new List<EventString>()
        {
            new EventString((i) => $"<b>{i.Target.Name}</b> suckles on {(i.Unit == i.Target ? GPPHis(i.Target) + " own" : ApostrophizeWithOrWithoutS(i.Unit.Name))} full {PreyLocStrings.ToBreastSynPlural()}, eagerly gulping down a mouthful of {PreyLocStrings.ToFluid(PreyLocation.breasts)}.", priority: 8),
        };
        CumFeedMessages = new List<EventString>()
        {
            new EventString((i) => $"<b>{i.Target.Name}</b> lovingly fellates <b>{i.Unit.Name}</b>, and is rewarded with a mouthful of {GPPHis(i.Target)} lover's {PreyLocStrings.ToFluid(PreyLocation.balls)}.", priority: 10, conditional: s => ReqTargetCompatibleLewd(s)),
            new EventString((i) => $"<b>{i.Unit.Name}</b> tells <b>{i.Target.Name}</b> to open wide as {GPPHe(i.Unit)} thrust{SIfSingular(i.Unit)} {GPPHis(i.Unit)} cock into <b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> mouth and unloads {GPPHis(i.Unit)} {PreyLocStrings.ToFluid(PreyLocation.balls)}.", priority: 10, conditional: s => ReqTargetCompatibleLewd(s)),
            new EventString((i) => $"<b>{i.Target.Name}</b> deliriously beckons <b>{i.Unit.Name}</b> and begins playing with {GPPHis(i.Unit)} penis before feasting on {GPPHis(i.Unit)} {PreyLocStrings.ToFluid(PreyLocation.balls)}.", priority: 10, conditional: s => ReqTargetCompatibleLewd(s)),
            new EventString((i) => $"<b>{i.Unit.Name}</b> unloads {GPPHis(i.Unit)} {PreyLocStrings.ToFluid(PreyLocation.balls)} into <b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> mouth.", priority: 8),
        };

        GreatEscapeKeepMessages = new List<EventString>()
        {
            new EventString((i) => $"<b>{i.Unit.Name}</b> jiggles their {PreyLocStrings.ToSyn(i.preyLocation)} with <b>{i.Target.Name}</b> stashed inside. \"What are you waiting for in there?\" - {GPPHe(i.Unit)} asks, annoyed.", priority: 25, conditional: HasGreatEscape),
            new EventString((i) => $"<b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> {PreyLocStrings.ToSyn(i.preyLocation)} is quiet. Too quiet. \"Did you digest in there?\" - <b>{i.Unit.Name}</b> asks prodding {GPPHis(i.Unit)} {PreyLocStrings.ToSyn(i.preyLocation)}. Nope, <b>{i.Target.Name}</b> is still there.", priority: 25, conditional: HasGreatEscape),
            new EventString((i) => $"<b>{i.Unit.Name}</b> stretches, feeling the pleasant heaviness of <b>{i.Target.Name}</b> in {GPPHis(i.Unit)} {PreyLocStrings.ToSyn(i.preyLocation)}.", priority: 25, conditional: HasGreatEscape),
            new EventString((i) => $"<b>{i.Target.Name}</b> kicks the {PreyLocStrings.ToSyn(i.preyLocation)} holding {GPPHim(i.Target)} from the inside. <b>{i.Unit.Name}</b> considers getting rid of {GPPHis(i.Unit)} rude occupant right now.", priority: 25, conditional: HasGreatEscape),
            new EventString((i) => $"<b>{i.Target.Name}</b> curls up within claustrophobic confines of <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> {PreyLocStrings.ToSyn(i.preyLocation)}.", priority: 25, conditional: HasGreatEscape),
            new EventString((i) => $"<b>{i.Unit.Name}</b> has a hard time keeping <b>{i.Target.Name}</b> in {GPPHis(i.Unit)} {PreyLocStrings.ToSyn(i.preyLocation)}.", priority: 25, conditional: HasGreatEscape),
            new EventString((i) => $"<b>{i.Unit.Name}</b> moans, rubbing {GPPHis(i.Unit)} {PreyLocStrings.ToSyn(i.preyLocation)}. <b>{i.Target.Name}</b> proves to be a hardy catch", priority: 25, conditional: HasGreatEscape),
            new EventString((i) => $"<b>{i.Unit.Name}</b> listens to the gurgling of {GPPHis(i.Unit)} <b>{i.Target.Name}</b>-filled stomach. To {GPPHis(i.Unit)} dismay, there's little actual gurgling.", priority: 25, conditional: s => HasGreatEscape(s) && InStomach(s)),
            new EventString((i) => $"<b>{i.Unit.Name}</b> shows off {GPPHis(i.Unit)} belly to the others. <b>{i.Target.Name}</b> within said belly is waiting, having already devised plan to embarass {GPPHis(i.Target)} captor.", priority: 25, conditional: s => HasGreatEscape(s) && InStomach(s)),
            new EventString((i) => $"<b>{i.Unit.Name}</b> listens to the gurgling of {GPPHis(i.Unit)} <b>{i.Target.Name}</b>-filled stomach. To {GPPHis(i.Unit)} dismay, there's little actual gurgling.", priority: 25, conditional: s => HasGreatEscape(s) && InStomach(s)),
            new EventString((i) => $"<b>{i.Unit.Name}</b> gropes {GPPHis(i.Unit)} <b>{i.Target.Name}</b>-stuffed midsection. To {GPPHis(i.Unit)} annoyance, <b>{i.Target.Name}</b> still refuses to melt.", priority: 25, conditional: s => HasGreatEscape(s) && InWomb(s)),
            new EventString((i) => $"<b>{i.Unit.Name}</b> lets out a quiet gasp with each movement of surprisingly resilient <b>{i.Target.Name}</b> within {GPPHis(i.Unit)} womb.", priority: 25, conditional: s => HasGreatEscape(s) && InWomb(s)),
            new EventString((i) => $"<b>{i.Unit.Name}</b> sighs in resignation as <b>{i.Target.Name}</b> continues squirming within {GPPHis(i.Unit)} vagina. Looks like this annoyance isn't gonna melt away for a while.", priority: 25, conditional: s => HasGreatEscape(s) && InWomb(s)),
            new EventString((i) => $"<b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> full breasts jiggle as <b>{i.Target.Name}</b> struggles inside.", priority: 25, conditional: s => HasGreatEscape(s) && InBreasts(s)),
            new EventString((i) => $"\"Enjoying your time there?\" - <b>{i.Unit.Name}</b> asks the prisoner of {GPPHis(i.Unit)} bosom.", priority: 25, conditional: s => HasGreatEscape(s) && InBreasts(s)),
            new EventString((i) => $"<b>{i.Target.Name}</b> is tucked comfortably within <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> engorged breasts, the ideal environment to be plotting escape.", priority: 25, conditional: s => HasGreatEscape(s) && InBreasts(s)),
            new EventString((i) => $"<b>{i.Target.Name}</b> turns around within <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> breasts, getting {GPPHimself(i.Target)} a nice gulp of milk.", priority: 25, conditional: s => HasGreatEscape(s) && InBreasts(s)),
            new EventString((i) => $"<b>{i.Unit.Name}</b> moans in pleasure as <b>{i.Target.Name}</b> massages {GPPHis(i.Unit)} nutsack from the inside, trying to make {GPPHis(i.Target)} adversary cum {GPPHim(i.Target)} out.", priority: 25, conditional: s => HasGreatEscape(s) && InBalls(s)),
            new EventString((i) => $"<b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> balls slosh and quiver, <b>{i.Target.Name}</b> assuming more comfortable position within.", priority: 25, conditional: s => HasGreatEscape(s) && InBalls(s)),
            new EventString((i) => $"<b>{i.Unit.Name}</b> can't wait to humiliate {GPPHis(i.Unit)} prey by splattering its remains across the ground. <b>{i.Target.Name}</b> can't wait to humiliate {GPPHis(i.Target)} pred by coming out intact.", priority: 25, conditional: s => HasGreatEscape(s) && InBalls(s)),
            new EventString((i) => $"<b>Erin</b> lets out a horrified shriek as <b>{i.Unit.Name}</b> begins to rub at <b>Erin</b>'s nethers through their belly.",
                targetRace: Race.Erin, priority: 26, conditional: s => Lewd(s) && HasGreatEscape(s) && InStomach(s)),
            new EventString((i) => $"<b>{i.Unit.Name}</b> starts groping at the bulge that is <b>Erin</b>, squeezing and rubbing her most sensitive parts, trying to coerce an orgasm out of the terrified Nyangel.",
                targetRace: Race.Erin, priority: 26, conditional: s => Lewd(s) && HasGreatEscape(s)),
            new EventString((i) => $"Even though she should be terrified, the curse controlling <b>Erin</b>'s mind removes any doubt as the indigestible Nyangel begins fingering herself within <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> lewd {PreyLocStrings.ToSyn(i.preyLocation)}.",
                targetRace: Race.Erin, priority: 26, conditional: s => Cursed(s) && Lewd(s)),
            new EventString((i) => $"<b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> stomach isn't doing much to <b>Erin</b>, unlike the mind-altering spell bringing her closer and closer to another orgasm.",
                targetRace: Race.Erin, priority: 26, conditional: s => Cursed(s) && Lewd(s) && InStomach(s)),
            new EventString((i) => $"<b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> stinging acids burn away at <b>Erin</b>, causing her to scream in agony even though they fail to break her body down.",
                targetRace: Race.Erin, priority: 26, conditional: s => HardVore(s) && HasGreatEscape(s) && InStomach(s)),
            new EventString((i) => $"<b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> stomach tries to melt <b>Erin</b> down, but the Nyangel's body is staying intact.",
                targetRace: Race.Erin, priority: 26, conditional: s => HasGreatEscape(s) && InStomach(s)),
            new EventString((i) => $" \"Let me out! Please!\" <b>Erin</b> screams, to no avail, as <b>{i.Unit.Name}</b> seems content to keep the poor Nyangel in there.",
                targetRace: Race.Erin, priority: 26, conditional: HasGreatEscape),
            new EventString((i) => $"<b>Erin</b>'s body doesn't melt away, but her claustrophobia makes sure that her mind isn't staying intact.",
                targetRace: Race.Erin, priority: 26, conditional: HasGreatEscape),
            new EventString((i) => $"<b>Erin</b> squirms in <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> belly, hoping to escape the terrifyingly small space.",
                targetRace: Race.Erin, priority: 26, conditional: s => HasGreatEscape(s) && InStomach(s)),
            new EventString((i) => $"<b>Erin</b> doesn't make any move to resist as <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> acids try - and fail - to eat away at her flesh.",
                targetRace: Race.Erin, priority: 26, conditional: s => Cursed(s) && HasGreatEscape(s) && InStomach(s)),
        };

        GreatEscapeFleeMessages = new List<EventString>()
        {
            new EventString((i) => $"<b>{i.Unit.Name}</b> stretches and goes to pat {GPPHis(i.Unit)} full {PreyLocStrings.ToSyn(i.preyLocation)}, only to notice with horror the emptiness where <b>{i.Target.Name}</b> was supposed to be.", priority: 25),
            new EventString((i) => $"\"Curse you, <b>{i.Target.Name}</b>, you won't get away next time!\" - yells <b>{i.Unit.Name}</b>, shaking {GPPHis(i.Unit)} fist at the heavens.", priority: 25),
            new EventString((i) => $"Tired of not being to digest <b>{i.Target.Name}</b>, <b>{i.Unit.Name}</b> decides to let {GPPHim(i.Target)} out.", priority: 25),
            new EventString((i) => $"<b>{i.Target.Name}</b> slips out of <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> {PreyLocStrings.ToSyn(i.preyLocation)}.", priority: 25),
            new EventString((i) => $"Somehow, <b>{i.Target.Name}</b> managed to escape without anyone noticing, the only thing reminding <b>{i.Unit.Name}</b> of {GPPHim(i.Target)} being {GetRaceDescSingl(i.Unit)}'s aching butt.", priority: 25, conditional: InStomach),
            new EventString((i) => $"<b>{i.Target.Name}</b> pulls out a smoke bomb and lights it up. Amid the coughing fit, <b>{i.Unit.Name}</b> is too distracted to notice {GPPHis(i.Unit)} prey sneaking out and away.", priority: 25, conditional: InStomach),
            new EventString((i) => $"After the battle, <b>{i.Unit.Name}</b> decides to take a quick nap to digest <b>{i.Target.Name}</b>. Waking up, {GPPHe(i.Unit)} notice{SIfSingular(i.Unit)} in horror that {GPPHis(i.Unit)} belly is now completely empty, with no signs of <b>{i.Target.Name}</b> ever being there.", priority: 25, conditional: InStomach),
            new EventString((i) => $"Somehow, <b>{i.Target.Name}</b> managed to escape without anyone noticing, the only thing reminding <b>{i.Unit.Name}</b> of {GPPHim(i.Target)} being {GetRaceDescSingl(i.Unit)}'s aching pussy.", priority: 25, conditional: InWomb),
            new EventString((i) => $"<b>{ApostrophizeWithOrWithoutS(i.Target.Name)}</b> squirming around has driven <b>{i.Unit.Name}</b> to orgasm. The {GetRaceDescSingl(i.Target)} triumphantly slides out as {GPPHis(i.Target)} captor quivers on the ground.", priority: 25, conditional: InWomb),
            new EventString((i) => $"<b>{i.Unit.Name}</b>, tired of <b>{i.Target.Name}</b> hogging the valuable space within {GPPHis(i.Unit)} womb, decides to evict the freeloader.", priority: 25, conditional: InWomb),
            new EventString((i) => $"Somehow, <b>{i.Target.Name}</b> managed to escape without anyone noticing, the only thing reminding <b>{i.Unit.Name}</b> of {GPPHim(i.Target)} being {GetRaceDescSingl(i.Unit)}'s aching nipples.", priority: 25, conditional: InBreasts),
            new EventString((i) => $"<b>{i.Unit.Name}</b> shakes {GPPHis(i.Unit)} breasts trying to see if <b>{i.Target.Name}</b> has been digested already. Apparently not, as the sly prey frees {GPPHimself(i.Target)} from <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> bosom and runs away.'", priority: 25, conditional: InBreasts),
            new EventString((i) => $"Frustrated with <b>{i.Target.Name}</b> not digesting, <b>{i.Unit.Name}</b> squeezes {GPPHis(i.Unit)} breasts, evicting the freeloader into a nearby thorny bush.", priority: 25, conditional: InBreasts),
            new EventString((i) => $"Somehow, <b>{i.Target.Name}</b> managed to escape without anyone noticing, the only thing reminding <b>{i.Unit.Name}</b> of {GPPHim(i.Target)} being {GetRaceDescSingl(i.Unit)}'s aching cockhole.", priority: 25, conditional: InBalls),
            new EventString((i) => $"<b>{i.Target.Name}</b> pokes, prods and massages <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> ballsack from the inside, nearly bringing {GPPHim(i.Unit)} to orgasm several times. Frustrated, <b>{i.Unit.Name}</b> decides to take matters in {GPPHis(i.Unit)} own hands, soon unleashing a massive load of all the pent-up semen... in which {GPPHis(i.Unit)} prey escapes.", priority: 25, conditional: InBalls),
            new EventString((i) => $"Suddenly, <b>{ApostrophizeWithOrWithoutS(i.Unit.Name)}</b> cock expands to immense size and releases <b>{i.Target.Name}</b> back into the world. Before <b>{i.Unit.Name}</b> could react, his prey, slippery from cum covering it, escapes.", priority: 25, conditional: InBalls),
        };
    }
}