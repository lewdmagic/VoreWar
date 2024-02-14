using OdinSerializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using static LogUtilities;
using Random = UnityEngine.Random;

public class TacticalMessageLog
{
    [OdinSerialize]
    private List<EventLog> _events;

    private List<EventLog> Events { get => _events; set => _events = value; }

    public bool ShowOdds = false;
    public bool ShowHealing = true;
    public bool ShowSpells = true;
    public bool ShowMisses = true;
    public bool ShowWeaponCombat = true;
    public bool ShowInformational = true;
    public bool ShowPureFluff = true;

    public bool SimpleText = false;

    private Unit _defaultPrey;

    public TacticalMessageLog()
    {
        _defaultPrey = new Unit(Race.Human);
        _defaultPrey.DefaultBreastSize = -1;
        _defaultPrey.DickSize = -1;
        _defaultPrey.Name = "[Redacted]";
        Events = new List<EventLog>();
    }


    private class SpellLog : EventLog
    {
        [OdinSerialize]
        private SpellType _spellType;

        internal SpellType SpellType { get => _spellType; set => _spellType = value; }
    }

    internal enum MessageLogEvent
    {
        Hit,
        Miss,
        Devour,
        Unbirth,
        CockVore,
        BreastVore,
        BellyRub,
        BreastRub,
        BallMassage,
        Feed,
        Birth,
        TransferFail,
        TransferSuccess,
        Resist,
        Kill,
        Digest,
        Absorb,
        Escape,
        Freed,
        Regurgitated,
        Heal,
        NewTurn,
        LowHealth,
        FeedCum,
        RandomDigestion = 25, //Done because of a removed category
        Miscellaneous,
        PartialEscape,
        TailVore,
        AnalVore,
        Dazzle,
        SpellHit,
        SpellMiss,
        SpellKill,
        CurseExpires,
        DiminishmentExpires,
        TailRub,
        Suckle,
        SuckleFail,
        VoreStealFail,
        VoreStealSuccess,
        GreatEscapeKeep,
        GreatEscapeFlee,
        ManualRegurgitation,
    }

    public void RefreshListing()
    {
        StringBuilder sb = new StringBuilder();
        var validEvents = Events.Where(s => EventValid(s));
        List<EventLog> last200;

        if (validEvents.Count() > 200)
        {
            last200 = validEvents.ToList().GetRange(validEvents.Count() - 200, 200);
        }
        else
            last200 = validEvents.ToList();

        foreach (EventLog action in last200)
        {
            sb.AppendLine(EventDescription(action));
        }

        State.GameManager.TacticalMode.LogUI.Text.text = sb.ToString();
        State.GameManager.TacticalMode.LogUI.Text.transform.Translate(new Vector3(0, 30000, 0));
    }

    internal string DebugDump()
    {
        StringBuilder sb = new StringBuilder();

        foreach (EventLog action in Events)
        {
            sb.AppendLine(EventDescription(action));
        }

        return sb.ToString();
    }

    private bool EventValid(EventLog test)
    {
        switch (test.Type)
        {
            case MessageLogEvent.Heal:
                if (ShowHealing == false) return false;
                break;
            case MessageLogEvent.Miss:
                if (ShowMisses == false || ShowWeaponCombat == false) return false;
                break;
            case MessageLogEvent.Resist:
            case MessageLogEvent.Dazzle:
            case MessageLogEvent.SpellMiss:
                if (ShowMisses == false || ShowSpells == false) return false;
                break;
            case MessageLogEvent.SpellHit:
                if (ShowSpells == false) return false;
                break;
            case MessageLogEvent.LowHealth:
            case MessageLogEvent.Absorb:
            case MessageLogEvent.NewTurn:
                if (ShowInformational == false) return false;
                break;
            case MessageLogEvent.RandomDigestion:
            case MessageLogEvent.GreatEscapeKeep:
                if (ShowPureFluff == false) return false;
                break;
            case MessageLogEvent.Hit:
                if (ShowWeaponCombat == false) return false;
                break;
        }

        return true;
    }

    public void Clear()
    {
        Events.Clear();
        State.GameManager.TacticalMode.LogUI.Text.text = "";
    }


    private void UpdateListing()
    {
        if (Events.Count > 4000) Events.RemoveRange(0, 400);
        if (State.GameManager.TacticalMode.TurboMode) return;
        if (EventValid(Events.Last()) == false) return;
        State.GameManager.TacticalMode.LogUI.Text.text += EventDescription(Events.Last()) + "\n";
        if (State.GameManager.TacticalMode.LogUI.Text.text.Length > 10000)
        {
            State.GameManager.TacticalMode.LogUI.Text.text = State.GameManager.TacticalMode.LogUI.Text.text.Substring(1000);
        }

        State.GameManager.TacticalMode.TacticalLogUpdated = true;
    }

    private string EventDescription(EventLog action)
    {
        string odds = "";
        if (ShowOdds && action.Odds > 0) odds = $" ({Math.Round(action.Odds * 100f, 2)}% success)";
        string msg;
        switch (action.Type)
        {
            case MessageLogEvent.Hit:
                return $"<b>{action.Unit.Name}</b> hit <b>{action.Target.Name}</b> with a {GetWeaponTrueName(action.Weapon, action.Unit)} for <color=red>{action.Damage}</color> points of damage.{odds}";
            case MessageLogEvent.Miss:
                msg = GenerateMissMessage(action);
                msg = msg += odds;
                return msg;
            case MessageLogEvent.Devour:
                msg = GenerateSwallowMessage(action);
                msg = msg += odds;
                return msg;
            case MessageLogEvent.Unbirth:
                msg = GenerateUbSwallowMessage(action);
                msg = msg += odds;
                return msg;
            case MessageLogEvent.CockVore:
                msg = GenerateCvSwallowMessage(action);
                msg = msg += odds;
                return msg;
            case MessageLogEvent.BreastVore:
                msg = GenerateBvSwallowMessage(action);
                msg = msg += odds;
                return msg;
            case MessageLogEvent.TailVore:
                msg = GenerateTVSwallowMessage(action);
                msg = msg += odds;
                return msg;
            case MessageLogEvent.AnalVore:
                msg = GenerateAvSwallowMessage(action);
                msg = msg += odds;
                return msg;
            case MessageLogEvent.BellyRub:
                return GenerateBellyRubMessage(action);
            case MessageLogEvent.BreastRub:
                return GenerateBreastRubMessage(action);
            case MessageLogEvent.TailRub:
                return GenerateTailRubMessage(action);
            case MessageLogEvent.BallMassage:
                return GenerateBallMassageMessage(action);
            case MessageLogEvent.TransferSuccess:
                return GetStoredMessage(StoredLogTexts.MessageTypes.TransferMessages, action);
            case MessageLogEvent.VoreStealSuccess:
                return GetStoredMessage(StoredLogTexts.MessageTypes.VoreStealMessages, action);
            //return $"<b>{action.Target.Name}</b> gently pushes down <b>{action.Unit.Name}</b> as {GPPHe(action.Target)} straddles {GPPHim(action.Unit)}. As {GPPHe(action.Target)} rides {GPPHim(action.Unit)}, {GPPHe(action.Unit)} cums, shooting {GPPHis(action.Unit)} prey straight into {GPPHis(action.Target)} {action.preyLocation.ToSyn()}.{odds}";
            case MessageLogEvent.TransferFail:
                return $"<b>{action.Unit.Name}</b> is a bit too quick, and {GppHis(action.Unit)} prey gets partially released.";
            case MessageLogEvent.VoreStealFail:
                if (action.OldLocation == PreyLocation.Breasts || action.OldLocation == PreyLocation.LeftBreast || action.OldLocation == PreyLocation.RightBreast)
                    return $"<b>{action.Target.Name}</b> shoves <b>{action.Unit.Name}</b> off of {GppHim(action.Target)} before {GppHe(action.Unit)} can suck <b>{action.Prey.Name}</b> out of {GppHis(action.Target)} breasts.";
                else
                    return $"<b>{action.Target.Name}</b> shoves <b>{action.Unit.Name}</b> off of {GppHim(action.Target)} before {GppHe(action.Unit)} can suck <b>{action.Prey.Name}</b> out of {GppHis(action.Target)} balls.";
            case MessageLogEvent.Feed:
                return GetStoredMessage(StoredLogTexts.MessageTypes.BreastFeedMessages, action);
            case MessageLogEvent.FeedCum:
                return GetStoredMessage(StoredLogTexts.MessageTypes.CumFeedMessages, action);
            case MessageLogEvent.Suckle:
                if (action.PreyLocation == PreyLocation.Breasts || action.PreyLocation == PreyLocation.LeftBreast || action.PreyLocation == PreyLocation.RightBreast)
                    return $"<b>{action.Unit.Name}</b> hugs <b>{action.Target.Name}</b>, pinning {GppHis(action.Target)} arms to {GppHis(action.Target)} sides as {GppHe(action.Unit)} starts sucking on {GppHis(action.Target)} breasts!{odds}";
                else
                    return $"<b>{action.Unit.Name}</b> knocks down <b>{action.Target.Name}</b> and begins sucking {GppHis(action.Target)} rod.{odds}";
            case MessageLogEvent.SuckleFail:
                if (action.PreyLocation == PreyLocation.Breasts || action.PreyLocation == PreyLocation.LeftBreast || action.PreyLocation == PreyLocation.RightBreast)
                    return $"<b>{action.Unit.Name}</b> hugs <b>{action.Target.Name}</b>, but {GppHe(action.Target)} breaks free from {GppHis(action.Unit)} hold before {action.Unit.Name} can do anything!{odds}";
                else
                    return $"<b>{action.Unit.Name}</b> tries to knock down <b>{action.Target.Name}</b>, but {action.Target.Name} stands {GppHis(action.Target)} ground!{odds}";
            case MessageLogEvent.Birth:
                return $"With a loud grunt, <b>{action.Unit.Name}</b> pushes <b>{action.Target.Name}</b> from {GppHis(action.Unit)} womb, and breathes a sigh of relief.{odds}";
            case MessageLogEvent.Resist:
                return $"<b>{action.Unit.Name}</b> tried to vore <b>{action.Target.Name}</b>, but was fought off.{odds}";
            case MessageLogEvent.Kill:
                return GenerateKillMessage(action);
            case MessageLogEvent.Digest:
                return GenerateDigestionDeathMessage(action);
            case MessageLogEvent.Absorb:
                return GenerateAbsorptionMessage(action);
            case MessageLogEvent.Escape:
                return GenerateEscapeMessage(action, odds);
            case MessageLogEvent.PartialEscape:
                return $"<b>{action.Target.Name}</b> escaped from <b>{action.Unit.Name}</b>'s second stomach, only to find {GppHimself(action.Target)} back in the first stomach.{odds}";
            case MessageLogEvent.Freed:
                return $"<b>{action.Target.Name}</b> was freed because <b>{action.Unit.Name}</b> died.";
            //$"<b>{action.Target.Name}</b> sees insides of {action.preyLocation.ToSyn()} around him melting, only to find {GPPHimself(action.Target)} <b>{action.Unit.Name}</b>'s {action.preyLocation.ToSyn()}{odds}"
            case MessageLogEvent.Regurgitated:
                return $"<b>{action.Unit.Name}</b> hears {GppHis(action.Unit)} comrade's plea for help and regurgitates <b>{action.Target.Name}</b>.";
            case MessageLogEvent.Heal:
                if (new[] { "breastfeeding", "cumfeeding" }.Contains(action.Message))
                {
                    string message = "";
                    if (action.Damage != 0 && action.Bonus == 0)
                        message = $"<b>{action.Unit.Name}</b> <color=blue>healed {action.Damage}</color> from the milk.";
                    else if (action.Damage != 0 && action.Bonus != 0)
                        message = $"<b>{action.Unit.Name}</b> <color=blue>healed {action.Damage}</color> and <color=blue>gained {action.Bonus} experience</color> from the milk.";
                    else
                        message = $"<b>{action.Unit.Name}</b> <color=blue>gained {action.Bonus} experience</color> from the milk.";
                    if (action.Extra == "honey")
                        message = message.Replace("milk", "honey");
                    else if (action.Message == "cumfeeding") message = message.Replace("milk", "cum");
                    return message;
                }

                return $"<b>{action.Unit.Name}</b> <color=blue>healed {action.Damage}</color> from absorbing {GppHis(action.Unit)} prey.";
            case MessageLogEvent.NewTurn:
                return action.Message;
            case MessageLogEvent.LowHealth:
                return GenerateDigestionLowHealthMessage(action);
            case MessageLogEvent.Miscellaneous:
                return action.Message;
            case MessageLogEvent.RandomDigestion:
                return GenerateRandomDigestionMessage(action);
            case MessageLogEvent.Dazzle:
                return $"<b>{action.Unit.Name}</b> was dazzled by <b>{action.Target.Name}</b>, the distraction wasting {GppHis(action.Unit)} turn.{odds}";
            case MessageLogEvent.SpellHit:
                msg = GenerateSpellHitMessage((SpellLog)action);
                msg = msg += odds;
                return msg;
            case MessageLogEvent.SpellMiss:
                msg = GenerateSpellMissMessage((SpellLog)action);
                msg = msg += odds;
                return msg;
            case MessageLogEvent.SpellKill:
                string spellName = SpellList.SpellDict[((SpellLog)action).SpellType].Name;
                return $"<b>{action.Unit.Name}</b> killed <b>{action.Target.Name}</b> with the {spellName} spell.";
            case MessageLogEvent.CurseExpires:
                return GenerateCurseExpiringMessage(action);
            case MessageLogEvent.DiminishmentExpires:
                return GenerateDiminshmentExpiringMessage(action);
            case MessageLogEvent.GreatEscapeKeep:
                return GenerateGreatEscapeKeepMessage(action);
            case MessageLogEvent.GreatEscapeFlee:
                return GenerateGreatEscapeFleeMessage(action);
            case MessageLogEvent.ManualRegurgitation:
                return GenerateRegurgitationMessage(action);
            // return $"<b>{action.Unit.Name}</b> triggers my test message by regurgitating <b>{action.Target.Name}</b>.";
            default:
                return string.Empty;
        }
    }

    private string GenerateBreastRubMessage(EventLog action)
    {
        if (SimpleText) return $"<b>{action.Unit.Name}</b> massages {(action.Unit == action.Target ? GppHis(action.Target) : "<b>" + action.Target.Name + "</b>'s")} full breasts.";
        return GetStoredMessage(StoredLogTexts.MessageTypes.BreastRubMessages, action);
    }

    private string GenerateTailRubMessage(EventLog action)
    {
        if (SimpleText)
        {
            if (Equals(action.Unit.Race, Race.Terrorbird))
                return $"<b>{action.Unit.Name}</b> massages {(action.Unit == action.Target ? GppHis(action.Target) : "<b>" + action.Target.Name + "</b>'s")} filled crop.";
            else
                return $"<b>{action.Unit.Name}</b> massages {(action.Unit == action.Target ? GppHis(action.Target) : "<b>" + action.Target.Name + "</b>'s")} stuffed tail.";
        }

        return GetStoredMessage(StoredLogTexts.MessageTypes.TailRubMessages, action);
    }


    private string GenerateBallMassageMessage(EventLog action)
    {
        if (SimpleText) return $"<b>{action.Unit.Name}</b> massages {(action.Unit == action.Target ? GppHis(action.Target) : "<b>" + action.Target.Name + "</b>'s")} full scrotum.";
        return GetStoredMessage(StoredLogTexts.MessageTypes.BallMassageMessages, action);
    }

    private string GenerateSpellHitMessage(SpellLog action)
    {
        var spell = SpellList.SpellDict[action.SpellType];
        switch (action.SpellType)
        {
            case SpellType.Shield:
                return $"<b>{action.Unit.Name}</b> buffed <b>{action.Target.Name}</b> with the {spell.Name} spell.";
            //$"<b>{action.Target.Name}</b> feels <b>{action.Unit.Name}</b>'s protective spell enveloping {GPPHim(action.Unit)}"
            case SpellType.Mending:
                return $"<b>{action.Unit.Name}</b> buffed <b>{action.Target.Name}</b> with the {spell.Name} spell.";
            case SpellType.Speed:
                return $"<b>{action.Unit.Name}</b> buffed <b>{action.Target.Name}</b> with the {spell.Name} spell.";
            case SpellType.Valor:
                return $"<b>{action.Unit.Name}</b> buffed <b>{action.Target.Name}</b> with the {spell.Name} spell.";
            case SpellType.Predation:
                return $"<b>{action.Unit.Name}</b> buffed <b>{action.Target.Name}</b> with the {spell.Name} spell.";
            case SpellType.Poison:
                return $"<b>{action.Unit.Name}</b> inflicted <b>{action.Target.Name}</b> with the {spell.Name} spell.";
            case SpellType.PreysCurse:
                return $"<b>{action.Unit.Name}</b> debuffed <b>{action.Target.Name}</b> with the {spell.Name} spell.";
            case SpellType.Maw:
                return $"<b>{action.Unit.Name}</b> consumed <b>{action.Target.Name}</b> with the {spell.Name} spell.";
            case SpellType.Diminishment:
                return $"<b>{action.Unit.Name}</b> shrunk <b>{action.Target.Name}</b> with the {spell.Name} spell.";
            case SpellType.GateMaw:
                return $"<b>{action.Unit.Name}</b> consumed <b>{action.Target.Name}</b> with the {spell.Name} spell.";
            case SpellType.Enlarge:
                return $"<b>{action.Unit.Name}</b> enlarged <b>{action.Target.Name}</b> with the {spell.Name} spell.";
            case SpellType.Resurrection:
                return $"<b>{action.Unit.Name}</b> resurrected <b>{action.Target.Name}</b>.";
            case SpellType.AlraunePuff:
                return $"<b>{action.Unit.Name}</b>'s pollen cloud affected <b>{action.Target.Name}</b>.";
            case SpellType.ViperPoison:
                return $"<b>{action.Unit.Name}</b>'s poison spot poisoned <b>{action.Target.Name}</b>.";
            case SpellType.Web:
                return $"<b>{action.Unit.Name}</b> webbed <b>{action.Target.Name}</b>.";
            case SpellType.GlueBomb:
                return $"<b>{action.Unit.Name}</b>'s glue bomb affected <b>{action.Target.Name}</b>.";
            case SpellType.Petrify:
                return $"<b>{action.Unit.Name}</b> petrified <b>{action.Target.Name}</b>.";
            case SpellType.DivinitysEmbrace:
                return $"<b>{action.Unit.Name}</b> buffed <b>{action.Target.Name}</b> with the {spell.Name} spell.";
            default:
                return $"<b>{action.Unit.Name}</b> hit <b>{action.Target.Name}</b> with the {spell.Name} spell, dealing <color=red>{action.Damage}</color> damage.";
        }
    }

    private string GenerateSpellMissMessage(SpellLog action)
    {
        if (action.SpellType == SpellType.None) return "";
        var spell = SpellList.SpellDict[action.SpellType];
        return $"<b>{action.Unit.Name}</b> failed to affect <b>{action.Target.Name}</b> with the {spell.Name} spell.";
    }

    private string GenerateMissMessage(EventLog action)
    {
        int rand = Random.Range(0, 3);
        switch (rand)
        {
            case 0:
                return $"<b>{action.Unit.Name}</b> missed <b>{action.Target.Name}</b> with {GppHis(action.Unit)} {GetWeaponTrueName(action.Weapon, action.Unit)}.";
            case 1:
            {
                if (action.Weapon.Range > 1)
                    return $"<b>{action.Unit.Name}</b> took a shot at <b>{action.Target.Name}</b> with {GppHis(action.Unit)} {GetWeaponTrueName(action.Weapon, action.Unit)}, but missed.";
                else
                    return $"<b>{action.Unit.Name}</b> struck at <b>{action.Target.Name}</b> with {GppHis(action.Unit)} {GetWeaponTrueName(action.Weapon, action.Unit)}, but went wide.";
            }
            default:
                return $"<b>{action.Target.Name}</b> dodged <b>{action.Unit.Name}</b>'s attempted attack with {GppHis(action.Unit)} {GetWeaponTrueName(action.Weapon, action.Unit)}.";
        }
    }

    /// <summary>
    ///     Generates a message for the tactical log when a unit dies through damage from a weapon.
    /// </summary>
    private string GenerateKillMessage(EventLog action)
    {
        int rand = Random.Range(0, 6);
        switch (rand)
        {
            case 0:
                return $"<b>{action.Unit.Name}</b> killed <b>{action.Target.Name}</b> with {GppHis(action.Unit)} {GetWeaponTrueName(action.Weapon, action.Unit)}.";
            case 1:
                return $"<b>{action.Target.Name}</b> was brought down by <b>{action.Unit.Name}</b>'s {GetWeaponTrueName(action.Weapon, action.Unit)}.";
            case 2:
                return $"<b>{action.Target.Name}</b>'s fight was brought to an end by <b>{action.Unit.Name}</b>'s {GetWeaponTrueName(action.Weapon, action.Unit)}.";
            case 3:
                if (action.Weapon.Range > 1)
                    return $"<b>{action.Target.Name}</b> was struck down by an accurate hit of <b>{action.Unit.Name}'s</b> {GetWeaponTrueName(action.Weapon, action.Unit)}.";
                else
                    return $"<b>{action.Unit.Name}</b> struck <b>{action.Target.Name}</b> down with a skilled strike of {GppHis(action.Unit)} {GetWeaponTrueName(action.Weapon, action.Unit)}.";
            case 4:
                return $"<b>{action.Target.Name}</b> was slain by <b>{action.Unit.Name}</b> wielding {GppHis(action.Unit)} {GetWeaponTrueName(action.Weapon, action.Unit)}.";
            default:
                return $"<b>{action.Unit.Name}</b> put an end to <b>{action.Target.Name}</b> with {GppHis(action.Unit)} {GetWeaponTrueName(action.Weapon, action.Unit)}.";
        }
    }

    private string GenerateSwallowMessage(EventLog action) // Oral vore devouring messages.
    {
        if (SimpleText) return $"<b>{action.Unit.Name}</b> ate <b>{action.Target.Name}</b>.";

        return GetStoredMessage(StoredLogTexts.MessageTypes.SwallowMessages, action);
    }

    private string GenerateBvSwallowMessage(EventLog action)
    {
        if (SimpleText) return $"<b>{action.Unit.Name}</b> breast vores <b>{action.Target.Name}</b>.";
        return GetStoredMessage(StoredLogTexts.MessageTypes.BreastVoreMessages, action);
    }

    private string GenerateCurseExpiringMessage(EventLog action)
    {
        if (action.PreyLocation == PreyLocation.Stomach || action.PreyLocation == PreyLocation.Stomach2)
        {
            return GetRandomStringFrom(
                $"As the curse comes to an end, <b>{action.Target.Name}</b> tries and figures out where {GppHeIs(action.Target)} and begins to scream in horror as {GppHe(action.Target)} realize{SIfSingular(action.Target)} what’s happened.",
                $"<b>{action.Unit.Name}</b> begins to worry as the curse on {GppHis(action.Unit)} meal wears off. Surprisingly though, <b>{action.Target.Name}</b> continues to massage {GppHis(action.Unit)} belly walls with enthusiasm.",
                $"<b>{action.Unit.Name}</b>’s tummy goes from a smooth, gentle surface to a sudden mass of angry rippling as {GppHis(action.Unit)} previously willing prey realizes {GppHe(action.Target)} {HasHave(action.Target)} been tricked.",
                $"<b>{action.Target.Name}</b> confusedly asks where {GppHeIs(action.Target)} as the spell breaks. <b>{action.Unit.Name}</b> tells {GppHim(action.Target)} {GppHeIsAbbr(action.Target)} exactly where {GppHeIsAbbr(action.Target)} meant to be as {GppHe(action.Unit)} lovingly embraces {GppHis(action.Unit)} swollen stomach."
            );
        }
        else
        {
            return GetRandomStringFrom(
                $"As the curse comes to an end, <b>{action.Target.Name}</b> tries and figures out where {GppHeIs(action.Target)} and begins to scream in horror as {GppHe(action.Target)} realize{SIfSingular(action.Target)} what’s happened.",
                $"<b>{action.Target.Name}</b> confusedly asks where {GppHeIs(action.Target)} as the spell breaks. <b>{action.Unit.Name}</b> tells {GppHim(action.Target)} {GppHeIsAbbr(action.Target)} exactly where {GppHeIsAbbr(action.Target)} meant to be as {GppHe(action.Unit)} lovingly embraces {GppHis(action.Unit)} swollen stomach."
            );
        }
    }

    private string GenerateDiminshmentExpiringMessage(EventLog action)
    {
        if (action.PreyLocation == PreyLocation.Stomach || action.PreyLocation == PreyLocation.Stomach2)
        {
            return GetRandomStringFrom(
                $"<b>{action.Unit.Name}</b>’s stomach expands violently as {GppHis(action.Unit)} previously diminutive prey reverts to {GppHis(action.Target)} regular size.",
                $"<b>{action.Unit.Name}</b> falls onto the ground as {GppHis(action.Unit)} belly is suddenly filled with a full-sized {action.Target.Race}. {GppHe(action.Unit)} rubs {GppHis(action.Unit)} engorged gut before standing once more.",
                $"<b>{action.Unit.Name}</b> had been eagerly waiting for {GppHis(action.Unit)} tiny meal to revert to its regular size. When {GppHis(action.Unit)} gut finally expands, the air is filled with {GppHis(action.Unit)} cries of pleasure and a great sloshing.",
                $"<b>{action.Unit.Name}</b>’s tummy nearly bursts as <b>{action.Target.Name}</b> reverts to {GppHis(action.Target)} usual size."
            );
        }
        else
        {
            return GetRandomStringFrom(
                $"<b>{action.Unit.Name}</b>’s {PreyLocStrings.ToSyn(action.PreyLocation)} expands violently as {GppHis(action.Unit)} previously diminutive prey reverts to {GppHis(action.Target)} regular size.",
                $"<b>{action.Unit.Name}</b> had been eagerly waiting for {GppHis(action.Unit)} tiny meal to revert to its regular size. When {GppHis(action.Unit)} {PreyLocStrings.ToSyn(action.PreyLocation)} finally expands, the air is filled with {GppHis(action.Unit)} cries of pleasure and a great sloshing."
            );
        }
    }

    private string GenerateEscapeMessage(EventLog action, string odds)
    {
        if (SimpleText)
        {
            return $"<b>{action.Target.Name}</b> escaped from <b>{action.Unit.Name}</b>'s {action.PreyLocation.ToSyn()}.{odds}";
        }

        if (action.PreyLocation == PreyLocation.Stomach)
        {
            if (RaceFuncs.IsHumanoid(action.Target.Race)) // Prey Humanoid
            {
                if (RaceFuncs.IsHumanoid(action.Unit.Race)) // Pred Humanoid
                    return GetRandomStringFrom(
                        $"From within <b>{action.Unit.Name}</b>’s gurgling gut, <b>{action.Target.Name}</b> remembers all the loved ones that would miss {GppHim(action.Target)} and with this incentive forces {GppHis(action.Target)} way out.{odds}",
                        $"<b>{action.Unit.Name}</b>’s stomach finds something particularly disagreeable with how <b>{action.Target.Name}</b> tastes. With a wretched gag, <b>{action.Target.Name}</b> is expelled from <b>{action.Unit.Name}</b>’s tummy.{odds}",
                        $"The rampant indigestion caused by <b>{action.Target.Name}</b>’s incessant struggles causes <b>{action.Unit.Name}</b> to reluctantly release {GppHis(action.Unit)} stubborn prey.{odds}",
                        $"<b>{action.Target.Name}</b>’s determination proves greater than the strength of <b>{action.Unit.Name}</b>’s constitution as {GppHe(action.Target)} free{SIfSingular(action.Target)} {GppHimself(action.Unit)} from {GppHis(action.Unit)} fleshy prison.{odds}",
                        $"<b>{action.Target.Name}</b> claws {GppHis(action.Target)} way up <b>{action.Unit.Name}</b>’s throat and is able to pull {GppHimself(action.Target)} free.{odds}",
                        $"<b>{action.Unit.Name}</b> can feel the tip of a weapon stabbing at {GppHis(action.Unit)} insides. Panicking, the worried predator spits <b>{action.Target.Name}</b> up quickly.{odds}",
                        $"<b>{action.Target.Name}</b> tricks {GppHis(action.Target)} would-be predator with a heartfelt sob story. <b>{action.Unit.Name}</b> believes it and naïvely lets the clever prey climb out of {GppHis(action.Unit)} gullet.{odds}",
                        $"<b>{action.Target.Name}</b> becomes terrified as the acids begin to tear into {GppHis(action.Target)} flesh and in a sudden bout of panic forces <b>{action.Unit.Name}</b> to throw {GppHim(action.Target)} up.{odds}",
                        $"<b>{action.Unit.Name}</b> relaxes and arrogantly pats {GppHis(action.Unit)} swollen belly while taunting {GppHis(action.Unit)} prey; {GppHeIsAbbr(action.Unit)} taken by surprise as <b>{action.Target.Name}</b> uses the moment of relaxation to fight {GppHis(action.Target)} way out.{odds}",
                        $"<b>{action.Unit.Name}</b> watches with concern as {GppHis(action.Unit)} belly suddenly lets out an angry roar. <b>{action.Target.Name}</b> had kept a number of inedible herbs for just this occasion and as they break down they force the belly to expel its contents.{odds}"
                    );
                else // Pred Feral
                    return GetRandomStringFrom(
                        $"From within <b>{action.Unit.Name}</b>’s gurgling gut, <b>{action.Target.Name}</b> remembers all the loved ones that would miss {GppHim(action.Target)} and with this incentive forces {GppHis(action.Target)} way out.{odds}",
                        $"<b>{action.Unit.Name}</b>’s stomach finds something particularly disagreeable with how <b>{action.Target.Name}</b> tastes. With a wretched gag, <b>{action.Target.Name}</b> is expelled from <b>{action.Unit.Name}</b>’s tummy.{odds}",
                        $"The rampant indigestion caused by <b>{action.Target.Name}</b>’s incessant struggles causes <b>{action.Unit.Name}</b> to reluctantly release {GppHis(action.Unit)} stubborn prey.{odds}",
                        $"<b>{action.Target.Name}</b>’s determination proves greater than the strength of <b>{action.Unit.Name}</b>’s constitution as {GppHe(action.Target)} free{SIfSingular(action.Target)} {GppHimself(action.Unit)} from {GppHis(action.Unit)} fleshy prison.{odds}",
                        $"<b>{action.Target.Name}</b> claws {GppHis(action.Target)} way up <b>{action.Unit.Name}</b>’s throat and is able to pull {GppHimself(action.Target)} free.{odds}",
                        $"<b>{action.Unit.Name}</b> can feel the tip of a weapon stabbing at {GppHis(action.Unit)} insides. Panicking, the worried predator spits <b>{action.Target.Name}</b> up quickly.{odds}",
                        $"<b>{action.Target.Name}</b> becomes terrified as the acids begin to tear into {GppHis(action.Target)} flesh and in a sudden bout of panic forces <b>{action.Unit.Name}</b> to throw {GppHim(action.Target)} up.{odds}",
                        $"<b>{action.Unit.Name}</b> watches with concern as {GppHis(action.Unit)} belly suddenly lets out an angry roar. <b>{action.Target.Name}</b> had kept a number of inedible herbs for just this occasion and as they break down they force the belly to expel its contents.{odds}"
                    );
            }
            else // Prey Feral
            {
                if (RaceFuncs.IsHumanoid(action.Unit.Race)) // Pred Humanoid
                    return GetRandomStringFrom(
                        $"<b>{action.Unit.Name}</b>’s stomach finds something particularly disagreeable with how <b>{action.Target.Name}</b> tastes. With a wretched gag, <b>{action.Target.Name}</b> is expelled from <b>{action.Unit.Name}</b>’s tummy.{odds}",
                        $"The rampant indigestion caused by <b>{action.Target.Name}</b>’s incessant struggles causes <b>{action.Unit.Name}</b> to reluctantly release {GppHis(action.Unit)} stubborn prey.{odds}",
                        $"<b>{action.Target.Name}</b>’s determination proves greater than the strength of <b>{action.Unit.Name}</b>’s constitution as {GppHe(action.Target)} free{SIfSingular(action.Target)} {GppHimself(action.Unit)} from {GppHis(action.Unit)} fleshy prison.{odds}",
                        $"<b>{action.Target.Name}</b> claws {GppHis(action.Target)} way up <b>{action.Unit.Name}</b>’s throat and is able to pull {GppHimself(action.Target)} free.{odds}",
                        $"<b>{action.Target.Name}</b> becomes terrified as the acids begin to tear into {GppHis(action.Target)} flesh and in a sudden bout of panic forces <b>{action.Unit.Name}</b> to throw {GppHim(action.Target)} up.{odds}",
                        $"<b>{action.Unit.Name}</b> relaxes and arrogantly pats {GppHis(action.Unit)} swollen belly while taunting {GppHis(action.Unit)} prey; {GppHeIsAbbr(action.Unit)} taken by surprise as <b>{action.Target.Name}</b> uses the moment of relaxation to fight {GppHis(action.Target)} way out.{odds}",
                        $"<b>{action.Target.Name}</b>'s survival instincts take over, letting {GppHim(action.Target)} channel a burst of near supernatural strength and setting {GppHim(action.Target)} free.{odds}",
                        $"<b>{action.Target.Name}</b>'s natural built-in weapons proove too much to leave {GppHim(action.Target)} contained. The irritated gut soon sets {GppHim(action.Target)} free.{odds}"
                    );
                else // Pred Feral
                    return GetRandomStringFrom(
                        $"<b>{action.Unit.Name}</b>’s stomach finds something particularly disagreeable with how <b>{action.Target.Name}</b> tastes. With a wretched gag, <b>{action.Target.Name}</b> is expelled from <b>{action.Unit.Name}</b>’s tummy.{odds}",
                        $"The rampant indigestion caused by <b>{action.Target.Name}</b>’s incessant struggles causes <b>{action.Unit.Name}</b> to reluctantly release {GppHis(action.Unit)} stubborn prey.{odds}",
                        $"<b>{action.Target.Name}</b>’s determination proves greater than the strength of <b>{action.Unit.Name}</b>’s constitution as {GppHe(action.Target)} free{SIfSingular(action.Target)} {GppHimself(action.Unit)} from {GppHis(action.Unit)} fleshy prison.{odds}",
                        $"<b>{action.Target.Name}</b> claws {GppHis(action.Target)} way up <b>{action.Unit.Name}</b>’s throat and is able to pull {GppHimself(action.Target)} free.{odds}",
                        $"<b>{action.Target.Name}</b> becomes terrified as the acids begin to tear into {GppHis(action.Target)} flesh and in a sudden bout of panic forces <b>{action.Unit.Name}</b> to throw {GppHim(action.Target)} up.{odds}",
                        $"<b>{action.Target.Name}</b>'s survival instincts take over, letting {GppHim(action.Target)} channel a burst of near supernatural strength and setting {GppHim(action.Target)} free.{odds}",
                        $"<b>{action.Target.Name}</b>'s natural built-in weapons proove too much to leave {GppHim(action.Target)} contained. The irritated gut soon sets {GppHim(action.Target)} free.{odds}"
                    );
            }
        }
        else
        {
            if (action.PreyLocation == PreyLocation.Breasts && Equals(action.Unit.Race, Race.Kangaroo))
            {
                return GetRandomStringFrom(
                    $"Just when all hope seemed lost, <b>{action.Target.Name}</b> manages to pry <b>{action.Unit.Name}</b>'s pouch entrance open, and clambers out, taking large breaths of fresh air. {odds}",
                    $"In the chaos of battle, <b>{action.Unit.Name}</b> leans over, causing a crease to appear in {GppHis(action.Unit)} pouch, forcing the pouch's entrance to unseal ever-so-slightly. <b>{action.Target.Name}</b> seizes the opportunity, clawing {GppHis(action.Target)} way out, and taking several deep, victorious gulps of real air.",
                    $"Rather suddenly, a blade pokes out of <b>{action.Unit.Name}</b>'s pouch's entrance, a knife or dagger of some kind. \"Let me out right now, or I'll carve your whole stupid pouch off,\" <b>{action.Target.Name}</b> angrily demands. <b>{action.Unit.Name}</b>, who would rather not be mutilated, caves and quickly pushes <b>{action.Target.Name}</b> out.",
                    $"Rather suddenly, a blade pokes out of <b>{action.Unit.Name}</b>'s pouch's entrance, a knife or dagger of some kind. \"Let me out right now, or I'll carve your whole stupid pouch off,\" <b>{action.Target.Name}</b> angrily demands. Seeing no other option, <b>{action.Unit.Name}</b> opens {GppHis(action.Unit)} pouch, and <b>{action.Target.Name}</b> quickly jumps out.",
                    $"In the chaos of battle, <b>{action.Unit.Name}</b> leans over, allowing <b>{action.Target.Name}</b> an opportunity to escape, which {GppHe(action.Target)} take{SIfSingular(action.Target)} quite happily.",
                    $"As a blade pokes out of <b>{action.Unit.Name}</b>'s pouch's entrance, <b>{action.Target.Name}</b> demands to be let out. Within moments, <b>{action.Unit.Name}</b> complies."
                );
            }

            return GetRandomStringFrom(
                $"<b>{action.Target.Name}</b> escaped from <b>{action.Unit.Name}</b>'s {action.PreyLocation.ToSyn()}.{odds}",
                $"From within <b>{action.Unit.Name}</b>’s {action.PreyLocation.ToSyn()}, <b>{action.Target.Name}</b> remembers all the loved ones that would miss {GppHim(action.Target)}, and with this incentive forces {GppHis(action.Target)} way out.{odds}",
                $"<b>{action.Target.Name}</b>’s determination proves greater than the strength of <b>{action.Unit.Name}</b>’s constitution as {GppHe(action.Target)} free{SIfSingular(action.Target)} {GppHimself(action.Target)} from {GppHis(action.Target)} fleshy prison.{odds}",
                $"<b>{action.Unit.Name}</b> can feel the tip of a weapon stabbing at {GppHis(action.Unit)} insides. Panicking, the worried predator spits <b>{action.Target.Name}</b> up quickly.{odds}",
                $"<b>{action.Target.Name}</b> tricks {GppHis(action.Target)} would-be predator with a heartfelt sob story. <b>{action.Unit.Name}</b> believes it and naïvely lets the clever prey climb back out.{odds}"
            );
        }
    }

    private string GenerateRegurgitationMessage(EventLog action)
    {
        if (SimpleText)
        {
            return $"<b>{action.Unit.Name}</b> regurgitates <b>{action.Target.Name}</b>.";
        }

        List<string> possibleLines = new List<string>();
        if (Equals(action.Unit.Race, Race.Slime))
        {
            possibleLines.Add($"As <b>{action.Unit.Name}</b> moves forward, {GppHis(action.Unit)} slimey body contorts, leaving behind <b>{action.Target.Name}</b>, covered in goo, but otherwise alive.");
            if (!Equals(action.Target.Race, Race.Slime))
                possibleLines.Add($"For a moment, <b>{action.Unit.Name}</b> appears to be undergoing mitosis, splitting in half. Then, one half pulls itself off a slightly freaked out <b>{action.Target.Name}</b>, the other becoming <b>{action.Unit.Name}</b> once again.");
            else
                possibleLines.Add($"For a moment, <b>{action.Unit.Name}</b> appears to be undergoing mitosis, splitting in half. Then, one half begins to shift slightly as <b>{action.Target.Name}</b> becomes a seperate slime once more.");
            return GetRandomStringFrom(possibleLines.ToArray());
        }

        possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("regurgitated", "released", "freed", "pushed out")} <b>{action.Target.Name}</b>{GetRandomStringFrom("", $"from {GppHis(action.Unit)} {PreyLocStrings.ToSyn(action.PreyLocation)}")}.");
        possibleLines.Add($"<b>{action.Unit.Name}</b> decides to eject <b>{action.Target.Name}</b> from {GppHis(action.Unit)} {PreyLocStrings.ToSyn(action.PreyLocation)}.");
        possibleLines.Add($"As <b>{action.Unit.Name}</b> hears a gurgle{GetRandomStringFrom("", $" eminate from {GppHis(action.Unit)} {PreyLocStrings.ToSyn(action.PreyLocation)}")}, {GppHe(action.Unit)} force{SIfSingular(action.Unit)} <b>{action.Target.Name}</b> out, not wishing to digest {GppHim(action.Target)}.");
        if (action.PreyLocation == PreyLocation.Stomach || action.PreyLocation == PreyLocation.Anal)
        {
            possibleLines.Add($"<b>{action.Target.Name}</b> was released from <b>{action.Unit.Name}</b>'s stomach.");
            possibleLines.Add($"With a great heave, <b>{action.Unit.Name}</b> {GetRandomStringFrom("vomits out", "spits out", "pukes up", "coughs up")} a still living <b>{action.Target.Name}</b>.");
            if (Config.Scat) possibleLines.Add($"As <b>{action.Unit.Name}</b>'s guts grumble, and the bulge <b>{action.Target.Name}</b> makes seems to move downwards, <b>{action.Unit.Name}</b> is briefly worried that {GppHe(action.Unit)} killed <b>{action.Target.Name}</b>. <b>{action.Unit.Name}</b> hurridly does a series of clenches to force <b>{action.Target.Name}</b> out, and is relieved when, instead of shit, a perfectly healthy <b>{action.Target.Name}</b> slides out {GppHis(action.Unit)} anus.");
            switch (action.PreyLocation)
            {
                case PreyLocation.Stomach:
                    possibleLines.Add($"<b>{action.Unit.Name}</b>, not wishing to {(action.Unit.HasTrait(TraitType.Endosoma) && Equals(action.Unit.Side, action.Target.Side) ? $"carry around <b>{action.Target.Name}</b> any longer" : $"digest <b>{action.Target.Name}</b>")}, sticks a finger in {GppHis(action.Unit)} throat and {GetRandomStringFrom("vomits out", "throws up", "coughs up")} {GetRandomStringFrom($"<b>{action.Target.Name}</b>", $"the {GetRaceDescSingl(action.Target)}")}.");
                    possibleLines.Add($"<b>{action.Target.Name}</b> was released back out <b>{action.Unit.Name}</b>'s mouth.");
                    possibleLines.Add($"<b>{action.Unit.Name}</b> pushes up on the bulge <b>{action.Target.Name}</b> makes in {GppHis(action.Unit)} belly. It isn't long before <b>{action.Target.Name}</b> is pushed back out {GetRandomStringFrom($"the way {GppHe(action.Target)} came in", $"<b>{action.Unit.Name}</b>'s mouth")}.");
                    possibleLines.Add($"<b>{action.Unit.Name}</b> pushes down on the bulge <b>{action.Target.Name}</b> makes in {GppHis(action.Unit)} belly. For a moment, this appears to do nothing aside from cause <b>{action.Target.Name}</b> some discomfort. Then, <b>{action.Target.Name}</b> emerges intact from {GetRandomStringFrom($"<b>{action.Unit.Name}</b>", $"the {GetRaceDescSingl(action.Unit)}")}'s ass!");
                    possibleLines.Add($"<b>{action.Unit.Name}</b> pushes down on the bulge <b>{action.Target.Name}</b> makes in {GppHis(action.Unit)} belly. For a moment, this appears to do nothing aside from cause <b>{action.Target.Name}</b> some discomfort. Then, <b>{action.Target.Name}</b> emerges intact from {GetRandomStringFrom($"<b>{action.Unit.Name}</b>", $"the {GetRaceDescSingl(action.Unit)}")}'s ass! Having completed a full tour through <b>{action.Unit.Name}</b>'s body, <b>{action.Target.Name}</b> simply stands there, confused.");
                    break;
                case PreyLocation.Anal:
                    possibleLines.Add($"<b>{action.Target.Name}</b> was released back out <b>{action.Unit.Name}</b>'s asshole.");
                    possibleLines.Add($"<b>{action.Unit.Name}</b> pushes down on the bulge <b>{action.Target.Name}</b> makes in {GppHis(action.Unit)} gut. It isn't long before <b>{action.Target.Name}</b> is pushed back out {GetRandomStringFrom($"the way {GppHe(action.Target)} came in", $"<b>{action.Unit.Name}</b>'s anus")}.");
                    possibleLines.Add($"<b>{action.Unit.Name}</b> pushes up on the bulge <b>{action.Target.Name}</b> makes in {GppHis(action.Unit)} gut. It isn't long before <b>{action.Target.Name}</b>'s face appears in the back of <b>{action.Unit.Name}</b>'s throat, before being promptly spat all the way out.");
                    possibleLines.Add($"<b>{action.Unit.Name}</b> pushes up on the bulge <b>{action.Target.Name}</b> makes in {GppHis(action.Unit)} gut. It isn't long before <b>{action.Target.Name}</b>'s face appears in the back of <b>{action.Unit.Name}</b>'s throat, before being promptly spat all the way out. Having made it all the way through <b>{action.Unit.Name}</b> going the wrong way, {GetRandomStringFrom($"<b>{action.Target.Name}</b>", $"the {GetRaceDescSingl(action.Target)}")} shudders, usure what to do next.");
                    possibleLines.Add($"As <b>{action.Unit.Name}</b> clenches, <b>{action.Target.Name}</b> can feel {GppHimself(action.Target)} being pulled back down into {GetRandomStringFrom($"<b>{action.Target.Name}</b>", $"the {GetRaceDescSingl(action.Target)}")}'s intestines. It isn't long before {GppHeIs(action.Target)} pushed back out <b>{action.Unit.Name}</b>'s {GetRandomStringFrom("butt", "ass", "asshole", "anus", "rectum")}, smelly but alive.");
                    break;
                default:
                    return $"What the hell happened? The prey was in the stomach somewhere and now they're not. Message Scarabyte on Discord, please.";
            }
        }
        else if (action.PreyLocation == PreyLocation.Balls)
        {
            possibleLines.Add($"<b>{action.Unit.Name}</b> reaches down and strokes {GppHis(action.Unit)} throbbing cock. Once <b>{action.Unit.Name}</b> climaxes, alongside the expected cum emerges <b>{action.Target.Name}</b>{GetRandomStringFrom($"", $", sticky and wet but otherwise unharmed")}.");
            possibleLines.Add($"<b>{action.Unit.Name}</b> faps <b>{action.Target.Name}</b> out of {GppHis(action.Unit)} {PreyLocStrings.ToSyn(action.PreyLocation)}.");
            possibleLines.Add($"<b>{action.Unit.Name}</b> presses upwards on the underside of {GppHis(action.Unit)} {PreyLocStrings.ToSyn(action.PreyLocation)}, forcing <b>{action.Target.Name}</b> {GetRandomStringFrom($"out", $"to re-emerge from {GppHis(action.Unit)} {PreyLocStrings.ToCockSyn()}")}.");
            possibleLines.Add($"<b>{action.Target.Name}</b> was released from <b>{action.Unit.Name}</b>'s balls.");
            possibleLines.Add($"As <b>{action.Unit.Name}</b> clenches, {GppHis(action.Unit)} balls shrink inwards, showing the whole of <b>{action.Target.Name}</b>'s trapped form. Slowly, that form moves upwards, sliding up <b>{action.Unit.Name}</b>'s cock, before <b>{action.Target.Name}</b> is extruded from the tip.");
            possibleLines.Add($"After nearly tripping on {GppHis(action.Unit)} own engorged {PreyLocStrings.ToSyn(action.PreyLocation)}, <b>{action.Unit.Name}</b> decides enough is enough, and quickly {GetRandomStringFrom("faps", "forces", "pushes", "cums")} <b>{action.Target.Name}</b> out{GetRandomStringFrom("", $", not even checking if <b>{action.Target.Name}</b> survived or if {GppHe(action.Target)} became a puddle of {GetRandomStringFrom($"{GetRaceDescSingl(action.Unit)} {GetRandomStringFrom("jizz", "cum", "spunk")}", $"{GetRandomStringFrom("jizz", "cum", "spunk")}", $"{GetRaceDescSingl(action.Target)} batter")}")}.");
        }
        else if (action.PreyLocation == PreyLocation.Womb)
        {
            possibleLines.Add($"<b>{action.Unit.Name}</b> reaches down and rubs {GppHis(action.Target)} soaking vagina. Once <b>{action.Unit.Name}</b> climaxes, alongside the expected {PreyLocStrings.ToFluid(action.PreyLocation)} emerges <b>{action.Target.Name}</b>{GetRandomStringFrom($"", $", sticky and wet but otherwise unharmed")}.");
            possibleLines.Add($"<b>{action.Unit.Name}</b> decides to \"rebirth\" <b>{action.Target.Name}</b> into this world, sliding {GppHim(action.Target)} out of {GppHis(action.Unit)} {PreyLocStrings.ToSyn(action.PreyLocation)}.");
            possibleLines.Add($"<b>{action.Unit.Name}</b> decides to push <b>{action.Target.Name}</b> back out of {GppHis(action.Unit)} {PreyLocStrings.ToSyn(action.PreyLocation)}, silently {GetRandomStringFrom($"hop", $"pray")}ing that {GppHe(action.Unit)}'ll get to stick {GetRandomStringFrom($"<b>{action.Target.Name}</b>", $"{GppHim(action.Target)}")} right back in.");
            possibleLines.Add($"<b>{action.Target.Name}</b> was released from <b>{action.Unit.Name}</b>'s womb.");
        }
        else if (action.PreyLocation == PreyLocation.Breasts || action.PreyLocation == PreyLocation.LeftBreast || action.PreyLocation == PreyLocation.RightBreast)
        {
            if (action.PreyLocation == PreyLocation.Breasts)
            {
                possibleLines.Add($"<b>{action.Unit.Name}</b> squeezes {GppHis(action.Unit)} {GetRandomStringFrom("squirming", "wriggling")} boobs, pushing out large amounts of milk, and one very wet <b>{action.Target.Name}</b>.");
                possibleLines.Add($"After {GppHis(action.Unit)} full breasts nearly tips {GppHim(action.Unit)} over, <b>{action.Unit.Name}</b> decides to release <b>{action.Target.Name}</b>, in the process regaining {GppHis(action.Unit)} balance.");
                possibleLines.Add($"<b>{action.Target.Name}</b> was released from <b>{action.Unit.Name}</b>'s breasts.");
            }
            else
            {
                possibleLines.Add($"<b>{action.Unit.Name}</b> squeezes {GppHis(action.Unit)} {GetRandomStringFrom("squirming", "wriggling")} {(action.PreyLocation == PreyLocation.LeftBreast ? "left" : "right")} boob, pushing out large amounts of milk, and one very wet <b>{action.Target.Name}</b>.");
                possibleLines.Add($"After giving {GppHimself(action.Unit)} a hearty slap on {GppHis(action.Unit)} {(action.PreyLocation == PreyLocation.LeftBreast ? "left" : "right")} {GetRandomStringFrom("boob", "breast", "titty")}, <b>{action.Unit.Name}</b> sees <b>{action.Target.Name}</b>'s head poke out of {GppHis(action.Unit)} nipple! After a moment, <b>{action.Unit.Name}</b> sighs and pulls <b>{action.Target.Name}</b> all the way out.");
                possibleLines.Add($"After {GppHis(action.Unit)} full breast nearly tips {GppHim(action.Unit)} over, <b>{action.Unit.Name}</b> decides to release <b>{action.Target.Name}</b>, in the process regaining {GppHis(action.Unit)} balance.");
                possibleLines.Add($"<b>{action.Target.Name}</b> was released from <b>{action.Unit.Name}</b>'s {(action.PreyLocation == PreyLocation.LeftBreast ? "left" : "right")} breast.");
            }
        }

        return GetRandomStringFrom(possibleLines.ToArray());
    }

    private string GenerateBellyRubMessage(EventLog action)
    {
        return GetStoredMessage(StoredLogTexts.MessageTypes.BellyRubMessages, action);
    }

    private string GenerateUbSwallowMessage(EventLog action)
    {
        if (SimpleText) return $"<b>{action.Unit.Name}</b> unbirths <b>{action.Target.Name}</b>.";
        return GetStoredMessage(StoredLogTexts.MessageTypes.UnbirthMessages, action);
    }

    private string GenerateTVSwallowMessage(EventLog action)
    {
        if (SimpleText) return $"<b>{action.Unit.Name}</b> tail vores <b>{action.Target.Name}</b>.";
        return GetStoredMessage(StoredLogTexts.MessageTypes.TailVoreMessages, action);
    }

    private string GenerateAvSwallowMessage(EventLog action)
    {
        if (SimpleText) return $"<b>{action.Unit.Name}</b> anal vores <b>{action.Target.Name}</b>.";
        return GetStoredMessage(StoredLogTexts.MessageTypes.AnalVoreMessages, action);
    }

    private string GenerateCvSwallowMessage(EventLog action)
    {
        if (SimpleText) return $"<b>{action.Unit.Name}</b> cock vores <b>{action.Target.Name}</b>.";
        return GetStoredMessage(StoredLogTexts.MessageTypes.CockVoreMessages, action);
    }

    private string GenerateRandomDigestionMessage(EventLog action)
    {
        return GetStoredMessage(StoredLogTexts.MessageTypes.RandomDigestionMessages, action);
    }

    private string GenerateDigestionLowHealthMessage(EventLog action)
    {
        if (Config.Scat && (action.PreyLocation == PreyLocation.Stomach || action.PreyLocation == PreyLocation.Stomach2) && State.Rand.Next(5) == 0)
        {
            return GetRandomStringFrom(
                $"<b>{action.Target.Name}</b> is well on {GppHis(action.Target)} way to becoming <b>{action.Unit.Name}</b>'s poop.",
                $"<b>{action.Target.Name}</b> is increasingly falling apart into a foul mess, waiting to be flushed into <b>{action.Unit.Name}</b>'s intestines.",
                $"<b>{action.Target.Name}</b> doesn’t have the fortitude left to resist {GppHis(action.Target)} destiny as a {GetRaceDescSingl(action.Unit)}'s next bowel movement anymore.",
                $"<b>{action.Unit.Name}</b> can feel <b>{action.Target.Name}</b>'s struggles getting weaker, kindly reminding {GppHim(action.Target)} that if {GppHe(action.Target)} fail{SIfSingular(action.Target)} to escape {GppHeIs(action.Target)} getting melted into turds.",
                $"<b>{action.Unit.Name}</b>’s {action.PreyLocation.ToSyn()} rumbles ominously while telling <b>{action.Target.Name}</b> that {GppHe(action.Unit)} will enjoy shitting {GppHim(action.Target)} out later.");
        }

        if (Config.HardVoreDialog && Random.Range(0, 5) == 0)
        {
            string loc = action.PreyLocation.ToSyn();
            string locs = loc.EndsWith("s") ? "" : "s";
            GetRandomStringFrom($"<b>{action.Unit.Name}</b> hears {GppHis(action.Unit)} {loc} gurgle{locs} intensely. {Capitalize(GppHe(action.Unit))} feels <b>{action.Target.Name}</b> begin to slip under {GppHis(action.Unit)} turbulent acids.",
                $"<b>{action.Unit.Name}</b>'s {loc} glurt{locs} and blort{locs}, {GppHis(action.Unit)} {GetPredDesc(action.Target)} prey starting to break down. <b>{action.Target.Name}</b> seems doomed.");
        }

        if (action.PreyLocation == PreyLocation.Breasts && Equals(action.Unit.Race, Race.Kangaroo))
        {
            return GetRandomStringFrom(
                $"With so little air in <b>{action.Unit.Name}</b>'s pouch, <b>{action.Target.Name}</b>'s mind has gone a little fuzzy. {Capitalize(GppHeIs(action.Target))} now talking to {GppHimself(action.Target)}.",
                $"With no breathable air left in <b>{action.Unit.Name}</b>'s pouch, <b>{action.Target.Name}</b>'s vision begins to grow dark and fuzzy around the edges. The end of <b>{action.Target.Name}</b> is nigh.",
                $"The lack of air within <b>{action.Unit.Name}</b>'s pouch has taken its toll on <b>{action.Target.Name}</b>, whose struggles have begun to slow.",
                $"As the O2 levels in <b>{action.Unit.Name}</b>'s pouch drop to critically low levels, <b>{action.Target.Name}</b> begins to hallucinate. Rather than continue to struggle, <b>{action.Target.Name}</b> decides that a better use of their little remaining oxygen is in having a conversation with these hallucinations.",
                $"<b>{action.Target.Name}</b>'s breathing has now replaced most of the O2 in <b>{action.Unit.Name}</b>'s pouch with CO2. With the air mixture so inhospitable, <b>{action.Target.Name}</b> falls into a coughing fit. As <b>{action.Unit.Name}</b>'s fellow soldiers look at {GppHim(action.Unit)}, <b>{action.Unit.Name}</b> blushes, and smacks {GppHis(action.Unit)} pouch a few times, hoping to {GetRandomStringFrom("rob", "drain")} <b>{action.Target.Name}</b> of the last of {GppHis(action.Target)} strength."
            );
        }

        int ran = Random.Range(0, 9);
        switch (ran)
        {
            case 0:
                string loc = action.PreyLocation.ToSyn();
                return $"<b>{action.Target.Name}</b> feels weak; <b>{action.Unit.Name}</b>'s {loc + (loc.EndsWith("s") ? " are" : " is")} overwhelming.";
            case 1:
                return $"<b>{action.Target.Name}</b> is about to give up fighting <b>{action.Unit.Name}</b>'s {action.PreyLocation.ToSyn()}.";
            case 2:
                return $"<b>{action.Target.Name}</b> is fading in the {GetPredDesc(action.Unit)} {GetRaceDescSingl(action.Unit)}'s {action.PreyLocation.ToSyn()}. <b>{action.Unit.Name}</b> licks {GppHis(action.Unit)} lips smugly, feeling it happen.";
            case 3:
                return $"The struggles of <b>{action.Target.Name}</b> become weaker, {GppHis(action.Target)} death imminent.";
            case 4:
                return $"<b>{action.Target.Name}</b> feels {GppHis(action.Target)} body becoming soft and pliable.";
            case 5:
                return $"<b>{action.Target.Name}</b> clearly doesn’t have the strength to avoid {GppHis(action.Target)} messy fate anymore.";
            case 6:
                return $"<b>{action.Target.Name}</b> whimpers, realizing {GppHis(action.Target)} gurgly doom has arrived as <b>{action.Unit.Name}</b>'s {action.PreyLocation.ToSyn()} readies to contract one last time.";
            case 7:
                return $"<b>{action.Unit.Name}</b> can feel <b>{action.Target.Name}</b> submitting to {GppHis(action.Unit)} {action.PreyLocation.ToSyn()}, licking {GppHis(action.Unit)} lips in satisfaction.";
            default:
                return $"<b>{action.Target.Name}</b> has no strength left. Fears death in the {action.PreyLocation.ToSyn()} of <b>{action.Unit.Name}</b>.";
        }
    }

    private string GenerateDigestionDeathMessage(EventLog action)
    {
        if (SimpleText) return $"<b>{action.Unit.Name}</b> digested <b>{action.Target.Name}</b>.";

        return GetStoredMessage(StoredLogTexts.MessageTypes.DigestionDeathMessages, action);
    }

    private string GenerateGreatEscapeKeepMessage(EventLog action)
    {
        if (SimpleText) return $"<b>{action.Unit.Name}</b> held <b>{action.Target.Name}</b> without digesting {GppHim(action.Target)}.";

        return GetStoredMessage(StoredLogTexts.MessageTypes.GreatEscapeKeep, action);
    }

    private string GenerateGreatEscapeFleeMessage(EventLog action)
    {
        if (SimpleText) return $"<b>{action.Unit.Name}</b>'s prey <b>{action.Target.Name}</b> managed to escape and flee the map.";

        return GetStoredMessage(StoredLogTexts.MessageTypes.GreatEscapeFlee, action);
    }

    private string GenerateAbsorptionMessage(EventLog action)
    {
        if (SimpleText) return $"<b>{action.Unit.Name}</b> finished absorbing the leftover nutrients from <b>{action.Target.Name}</b>.";
        return GetStoredMessage(StoredLogTexts.MessageTypes.AbsorptionMessages, action);
    }

    private string GetStoredMessage(StoredLogTexts.MessageTypes msgType, EventLog action)
    {
        List<EventString> list = StoredLogTexts.Redirect(msgType);
        IEnumerable<EventString> messages = list.Where(s => (Equals(s.ActorRace, action.Unit.Race) || s.ActorRace == null) && (Equals(s.TargetRace, action.Target.Race) || s.TargetRace == null) &&
                                                            s.Conditional(action));

        if (messages.Any() == false)
        {
            return $"Couldn't find matching message {action.Unit.Name} {action.Type} {action.Target?.Name ?? ""}";
        }

        int priority = messages.Max(s => s.Priority);
        if (priority == 9 && State.Rand.Next(2) == 0) priority = 8;

        EventString[] array = messages.Where(s => s.Priority == priority).ToArray();
        return array[State.Rand.Next(array.Length)].GetString(action);
    }

    public void RegisterHit(Unit attacker, Unit defender, Weapon weapon, int damage, float odds)
    {
        Events.Add(new EventLog
        {
            Type = MessageLogEvent.Hit,
            Unit = attacker,
            Damage = damage,
            Target = defender,
            Weapon = weapon,
            Odds = odds
        });
        UpdateListing();
    }

    public void RegisterMiss(Unit attacker, Unit defender, Weapon weapon, float odds)
    {
        Events.Add(new EventLog
        {
            Type = MessageLogEvent.Miss,
            Unit = attacker,
            Target = defender,
            Weapon = weapon,
            Odds = odds
        });
        UpdateListing();
    }

    public void RegisterVore(Unit predator, Unit prey, float odds)
    {
        Events.Add(new EventLog
        {
            Type = MessageLogEvent.Devour,
            Unit = predator,
            Target = prey,
            Odds = odds,
            PreyLocation = PreyLocation.Stomach,
        });
        UpdateListing();
    }

    public void RegisterUnbirth(Unit predator, Unit prey, float odds)
    {
        Events.Add(new EventLog
        {
            Type = MessageLogEvent.Unbirth,
            Unit = predator,
            Target = prey,
            Odds = odds,
            PreyLocation = PreyLocation.Womb,
        });
        UpdateListing();
    }

    public void RegisterCockVore(Unit predator, Unit prey, float odds)
    {
        Events.Add(new EventLog
        {
            Type = MessageLogEvent.CockVore,
            Unit = predator,
            Target = prey,
            Odds = odds,
            PreyLocation = PreyLocation.Balls,
        });
        UpdateListing();
    }

    public void RegisterBreastVore(Unit predator, Unit prey, float odds)
    {
        Events.Add(new EventLog
        {
            Type = MessageLogEvent.BreastVore,
            Unit = predator,
            Target = prey,
            Odds = odds,
            PreyLocation = PreyLocation.Breasts,
        });
        UpdateListing();
    }

    public void RegisterTailVore(Unit predator, Unit prey, float odds)
    {
        Events.Add(new EventLog
        {
            Type = MessageLogEvent.TailVore,
            Unit = predator,
            Target = prey,
            Odds = odds,
            PreyLocation = PreyLocation.Tail,
        });
        UpdateListing();
    }

    public void RegisterAnalVore(Unit predator, Unit prey, float odds)
    {
        Events.Add(new EventLog
        {
            Type = MessageLogEvent.AnalVore,
            Unit = predator,
            Target = prey,
            Odds = odds,
            PreyLocation = PreyLocation.Anal,
        });
        UpdateListing();
    }

    public void RegisterBellyRub(Unit rubber, Unit target, Unit prey, float odds)
    {
        Events.Add(new EventLog
        {
            Type = MessageLogEvent.BellyRub,
            Unit = rubber,
            Target = target,
            Prey = prey ?? _defaultPrey,
            Odds = odds,
            PreyLocation = PreyLocation.Stomach,
        });
        UpdateListing();
    }

    public void RegisterBreastRub(Unit rubber, Unit target, Unit prey, float odds)
    {
        Events.Add(new EventLog
        {
            Type = MessageLogEvent.BreastRub,
            Unit = rubber,
            Target = target,
            Prey = prey ?? _defaultPrey,
            Odds = odds,
            PreyLocation = PreyLocation.Breasts,
        });
        UpdateListing();
    }

    public void RegisterTailRub(Unit predator, Unit target, Unit prey, float odds)
    {
        Events.Add(new EventLog
        {
            Type = MessageLogEvent.TailRub,
            Unit = predator,
            Target = prey,
            Prey = prey ?? _defaultPrey,
            Odds = odds,
            PreyLocation = PreyLocation.Tail,
        });
        UpdateListing();
    }

    public void RegisterBallMassage(Unit rubber, Unit target, Unit prey, float odds)
    {
        Events.Add(new EventLog
        {
            Type = MessageLogEvent.BallMassage,
            Unit = rubber,
            Target = target,
            Prey = prey ?? _defaultPrey,
            Odds = odds
        });
        UpdateListing();
    }

    public void RegisterFeed(Unit predator, Unit target, Unit prey, float odds)
    {
        Events.Add(new EventLog
        {
            Type = MessageLogEvent.Feed,
            Unit = predator,
            Target = target,
            Prey = prey ?? _defaultPrey,
            Odds = odds
        });
        UpdateListing();
    }

    public void RegisterCumFeed(Unit predator, Unit target, Unit prey, float odds)
    {
        Events.Add(new EventLog
        {
            Type = MessageLogEvent.FeedCum,
            Unit = predator,
            Target = target,
            Prey = prey ?? _defaultPrey,
            Odds = odds
        });
        UpdateListing();
    }

    public void RegisterSuckle(Unit user, Unit target, PreyLocation location, float odds)
    {
        Events.Add(new EventLog
        {
            Type = MessageLogEvent.Suckle,
            Unit = user,
            Target = target,
            PreyLocation = location,
            Odds = odds
        });
        UpdateListing();
    }

    public void RegisterSuckleFail(Unit user, Unit target, PreyLocation location, float odds)
    {
        Events.Add(new EventLog
        {
            Type = MessageLogEvent.SuckleFail,
            Unit = user,
            Target = target,
            PreyLocation = location,
            Odds = odds
        });
        UpdateListing();
    }

    public void RegisterBirth(Unit predator, Unit prey, float odds)
    {
        Events.Add(new EventLog
        {
            Type = MessageLogEvent.Birth,
            Unit = predator,
            Target = prey,
            Odds = odds
        });
        UpdateListing();
    }

    public void RegisterTransferSuccess(Unit donor, Unit recipient, Unit donation, float odds, PreyLocation loc)
    {
        Events.Add(new EventLog
        {
            Type = MessageLogEvent.TransferSuccess,
            Unit = donor,
            Target = recipient,
            Prey = donation,
            Odds = odds,
            PreyLocation = loc,
        });
        UpdateListing();
    }

    public void RegisterTransferFail(Unit predator, Unit prey, float odds)
    {
        Events.Add(new EventLog
        {
            Type = MessageLogEvent.TransferFail,
            Unit = predator,
            Target = prey,
            Odds = odds,
        });
        UpdateListing();
    }

    public void RegisterVoreStealSuccess(Unit donor, Unit recipient, Unit donation, float odds, PreyLocation loc, PreyLocation oldLoc)
    {
        Events.Add(new EventLog
        {
            Type = MessageLogEvent.VoreStealSuccess,
            Unit = recipient,
            Target = donor,
            Prey = donation,
            Odds = odds,
            PreyLocation = loc,
            OldLocation = oldLoc,
        });
        UpdateListing();
    }

    public void RegisterVoreStealFail(Unit donor, Unit recipient, Unit donation, PreyLocation oldLoc)
    {
        Events.Add(new EventLog
        {
            Type = MessageLogEvent.VoreStealFail,
            Unit = recipient,
            Target = donor,
            Prey = donation,
            OldLocation = oldLoc,
        });
        UpdateListing();
    }

    public void RegisterResist(Unit predator, Unit prey, float odds)
    {
        Events.Add(new EventLog
        {
            Type = MessageLogEvent.Resist,
            Unit = predator,
            Target = prey,
            Odds = odds
        });
        UpdateListing();
    }

    public void RegisterDazzle(Unit attacker, Unit target, float odds)
    {
        Events.Add(new EventLog
        {
            Type = MessageLogEvent.Dazzle,
            Unit = attacker,
            Target = target,
            Odds = odds
        });
        UpdateListing();
    }

    public void RegisterSpellHit(Unit attacker, Unit target, SpellType type, int damage, float odds)
    {
        Events.Add(new SpellLog
        {
            Type = MessageLogEvent.SpellHit,
            Unit = attacker,
            Target = target,
            Damage = damage,
            SpellType = type,
            Odds = odds
        });
        UpdateListing();
    }

    public void RegisterSpellMiss(Unit attacker, Unit target, SpellType type, float odds)
    {
        Events.Add(new SpellLog
        {
            Type = MessageLogEvent.SpellMiss,
            Unit = attacker,
            Target = target,
            SpellType = type,
            Odds = odds
        });
        UpdateListing();
    }

    public void RegisterSpellKill(Unit attacker, Unit target, SpellType type)
    {
        Events.Add(new SpellLog
        {
            Type = MessageLogEvent.SpellKill,
            Unit = attacker,
            Target = target,
            SpellType = type
        });
        UpdateListing();
    }


    public void RegisterKill(Unit attacker, Unit defender, Weapon weapon)
    {
        Events.Add(new EventLog
        {
            Type = MessageLogEvent.Kill,
            Unit = attacker,
            Target = defender,
            Weapon = weapon
        });
        UpdateListing();
    }

    public void RegisterDigest(Unit predator, Unit prey, PreyLocation loc)
    {
        Events.Add(new EventLog
        {
            Type = MessageLogEvent.Digest,
            Unit = predator,
            Target = prey,
            PreyLocation = loc,
        });
        UpdateListing();
    }

    public void RegisterAbsorb(Unit predator, Unit prey, PreyLocation loc)
    {
        Events.Add(new EventLog
        {
            Type = MessageLogEvent.Absorb,
            Unit = predator,
            Target = prey,
            PreyLocation = loc,
        });
        UpdateListing();
    }

    public void RegisterPartialEscape(Unit predator, Unit prey, PreyLocation loc)
    {
        Events.Add(new EventLog
        {
            Type = MessageLogEvent.PartialEscape,
            Unit = predator,
            Target = prey,
            PreyLocation = loc,
        });
        UpdateListing();
    }

    public void RegisterEscape(Unit predator, Unit prey, PreyLocation loc)
    {
        Events.Add(new EventLog
        {
            Type = MessageLogEvent.Escape,
            Unit = predator,
            Target = prey,
            PreyLocation = loc,
        });
        UpdateListing();
    }

    public void RegisterFreed(Unit predator, Unit prey, PreyLocation loc)
    {
        Events.Add(new EventLog
        {
            Type = MessageLogEvent.Freed,
            Unit = predator,
            Target = prey,
            PreyLocation = loc,
        });
        UpdateListing();
    }

    public void RegisterRegurgitated(Unit predator, Unit prey, PreyLocation loc)
    {
        Events.Add(new EventLog
        {
            Type = MessageLogEvent.Regurgitated,
            Unit = predator,
            Target = prey,
            PreyLocation = loc,
        });
        UpdateListing();
    }

    public void RegisterHeal(Unit unit, int[] amount, string type = "absorb", string extra = "none")
    {
        Events.Add(new EventLog
        {
            Type = MessageLogEvent.Heal,
            Unit = unit,
            Damage = amount[0],
            Bonus = amount[1],
            Message = type,
            Extra = extra
        });
        UpdateListing();
    }

    public void RegisterNearDigestion(Unit predator, Unit prey, PreyLocation loc)
    {
        Events.Add(new EventLog
        {
            Type = MessageLogEvent.LowHealth,
            Unit = predator,
            Target = prey,
            PreyLocation = loc,
        });
        UpdateListing();
    }

    public void RegisterCurseExpiration(Unit predator, Unit prey, PreyLocation loc)
    {
        Events.Add(new EventLog
        {
            Type = MessageLogEvent.CurseExpires,
            Unit = predator,
            Target = prey,
            PreyLocation = loc,
        });
        UpdateListing();
    }

    public void RegisterDiminishmentExpiration(Unit predator, Unit prey, PreyLocation loc)
    {
        Events.Add(new EventLog
        {
            Type = MessageLogEvent.DiminishmentExpires,
            Unit = predator,
            Target = prey,
            PreyLocation = loc,
        });
        UpdateListing();
    }

    public void LogDigestionRandom(Unit predator, Unit prey, PreyLocation loc)
    {
        Events.Add(new EventLog
        {
            Type = MessageLogEvent.RandomDigestion,
            Unit = predator,
            Target = prey,
            PreyLocation = loc,
        });
        UpdateListing();
    }

    public void LogGreatEscapeKeep(Unit predator, Unit prey, PreyLocation loc)
    {
        Events.Add(new EventLog
        {
            Type = MessageLogEvent.GreatEscapeKeep,
            Unit = predator,
            Target = prey,
            PreyLocation = loc,
        });
        UpdateListing();
    }

    public void LogGreatEscapeFlee(Unit predator, Unit prey, PreyLocation loc)
    {
        Events.Add(new EventLog
        {
            Type = MessageLogEvent.GreatEscapeFlee,
            Unit = predator,
            Target = prey,
            PreyLocation = loc,
        });
        UpdateListing();
    }

    public void RegisterNewTurn(string name, int amount)
    {
        Events.Add(new EventLog
        {
            Type = MessageLogEvent.NewTurn,
            Message = $"Turn {amount} - {name}"
        });
        UpdateListing();
    }

    public void RegisterMiscellaneous(string str)
    {
        Events.Add(new EventLog
        {
            Type = MessageLogEvent.Miscellaneous,
            Message = str,
        });
        UpdateListing();
    }

    public void RegisterRegurgitate(Unit predator, Unit prey, PreyLocation loc)
    {
        Events.Add(new EventLog
        {
            Type = MessageLogEvent.ManualRegurgitation,
            Unit = predator,
            Target = prey,
            PreyLocation = loc,
        });
        UpdateListing();
    }
}