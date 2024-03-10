using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HoveringTooltip : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private RectTransform _rect;
    private int _remainingFrames = 0;

    private void Start()
    {
        _rect = GetComponent<RectTransform>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (_remainingFrames > 0)
            _remainingFrames--;
        else
            gameObject.SetActive(false);
    }

    public void UpdateInformationTraitsOnly(string[] words)
    {
        string description = GetTraitDescription(words);
        InfoUpdate(description);
    }

    public void UpdateInformationSpellsOnly(string[] words)
    {
        string description = GetSpellDescription(words);
        InfoUpdate(description);
    }

    public void UpdateInformationAIOnly(string[] words)
    {
        string description = GetAIDescription(words);
        InfoUpdate(description);
    }

    public void UpdateInformation(string[] words, Unit unit, ActorUnit actor)
    {
        string description = GetDescription(words, unit, actor);
        InfoUpdate(description);
    }

    public void UpdateInformation(Slider slider)
    {
        //_rect.sizeDelta = new Vector2(350, 80);
        string description = $"Slider Value: {Math.Round(slider.value, 3)}\nRight Click to type in the number.";
        InfoUpdate(description);
    }

    public void HoveringValidName()
    {
        string description = "Click to show the information for this consumed unit";
        InfoUpdate(description);
    }

    private string GetTraitDescription(string[] words)
    {
        if (Enum.TryParse(words[2], out TraitType trait))
        {
            return GetTraitData(trait);
        }

        return "";
    }

    private string GetSpellDescription(string[] words)
    {
        if (Enum.TryParse(words[2], out SpellType spell))
        {
            List<Spell> allSpells = SpellList.SpellDict.Select(s => s.Value).ToList();
            string complete = $"{words[0]} {words[1]} {words[2]} {words[3]} {words[4]}";
            for (int i = 0; i < allSpells.Count; i++)
            {
                if (words[2] == allSpells[i].SpellType.ToString() || (complete.Contains(allSpells[i].SpellType.ToString()) && allSpells[i].SpellType.ToString().Contains(words[2]))) //Ensures that the phrase contains the highlighed word
                {
                    return $"{allSpells[i].Description}\nRange: {allSpells[i].Range.Min}-{allSpells[i].Range.Max}\nMana Cost: {allSpells[i].ManaCost}\nTargets: {string.Join(", ", allSpells[i].AcceptibleTargets)}";
                }
            }
        }

        return "";
    }

    private string GetAIDescription(string[] words)
    {
        if (Enum.TryParse(words[2], out RaceAI ai))
        {
            return GetAIData(ai);
        }

        return "";
    }

    private string GetDescription(string[] words, Unit unit, ActorUnit actor = null)
    {
        if (int.TryParse(words[2], out int temp))
        {
            return "";
        }
        string STRDef = $"Affects melee accuracy and damage, also has a lesser impact on health, has minor effects on vore defense and vore escape\n{StatData(Stat.Strength, unit)}";
        string DEXDef = $"Affects ranged accuracy and damage, has minor effect on vore escape\n{StatData(Stat.Dexterity, unit)}";
        string VORDef = $"Affects vore odds, also has a minor effect on keeping prey down, also affects digestion damage to a minor degree\n{StatData(Stat.Voracity, unit)}";
        string AGIDef = $"Affects melee and ranged evasion and movement speed\n{StatData(Stat.Agility, unit)}\nMovement: {actor?.MaxMovement() ?? Mathf.Max(3 + ((int)Mathf.Pow(unit.GetStat(Stat.Agility) / 4, .8f)), 1)} tiles";
        string WLLDef = $"Affects vore defense, escape rate, mana capacity, and magic defense\n{StatData(Stat.Will, unit)}";
        string MNDDef = $"Affects spell damage, success odds, and duration with a minor amount of mana capacity\n{StatData(Stat.Mind, unit)}";
        string ENDDef = $"Affects total health, also reduces damage from acid, has a minor role in escape chance.\n{StatData(Stat.Endurance, unit)}";
        string STMDef = $"Affects stomach capacity and digestion rate.  Also helps keep prey from escaping.\n{StatData(Stat.Stomach, unit)}\n" +
                        (State.World?.ItemRepository == null ? $"" : $"{(!unit.Predator ? "" : $"Capacity: {(actor?.PredatorComponent != null ? $"{Math.Round(actor.PredatorComponent.GetBulkOfPrey(), 2)} / " : "")}{Math.Round(State.RaceSettings.GetStomachSize(unit.Race) * (unit.GetStat(Stat.Stomach) / 12f * unit.TraitBoosts.CapacityMult), 1)}")}");
        string LDRDef = $"Provides a stat boost for all friendly units\nStat value: {unit.GetStatBase(Stat.Leadership)}";

        if (Enum.TryParse(words[2], out Stat stat) && unit != null)
        {
            switch (stat)
            {
                case Stat.Strength:
                    return STRDef;
                case Stat.Dexterity:
                    return DEXDef;
                case Stat.Voracity:
                    return VORDef;
                case Stat.Agility:
                    return AGIDef;
                case Stat.Will:
                    return WLLDef;
                case Stat.Mind:
                    return MNDDef;
                case Stat.Endurance:
                    return ENDDef;
                case Stat.Stomach:
                    return STMDef;
                case Stat.Leadership:
                    return LDRDef;
            }
        }

        if (RaceFuncs.TryParse(words[2], out Race race))
        {
            if (unit == null) //Protector for the add a race screen
                return "";
            var racePar = RaceParameters.GetTraitData(unit);
            var bodySize = State.RaceSettings.GetBodySize(race);
            var stomachSize = State.RaceSettings.GetStomachSize(race);
            //return $"{race}\n{racePar.RaceDescription}\nBody Size: {State.RaceSettings.GetBodySize(race)}\nBase Stomach Size: {State.RaceSettings.GetStomachSize(race)}\nFavored Stat: {racePar.FavoredStat}\nDefault Traits:\n{State.RaceSettings.ListTraits(race)}";
            return $"{race}\n{racePar.RaceDescription}\nRace Body Size: {bodySize}\nCurrent Bulk: {actor?.Bulk()}\nBase Stomach Size: {stomachSize}\nFavored Stat: {State.RaceSettings.GetFavoredStat(race)}";
        }

        if (unit != null && words[2] == InfoPanel.RaceSingular(unit))
        {
            race = unit.Race;
            var racePar = RaceParameters.GetTraitData(unit);
            var bodySize = State.RaceSettings.GetBodySize(race);
            var stomachSize = State.RaceSettings.GetStomachSize(race);
            //return $"{race}\n{racePar.RaceDescription}\nBody Size: {State.RaceSettings.GetBodySize(race)}\nBase Stomach Size: {State.RaceSettings.GetStomachSize(race)}\nFavored Stat: {racePar.FavoredStat}\nDefault Traits:\n{State.RaceSettings.ListTraits(race)}";
            return $"{race}\n{racePar.RaceDescription}\nRace Body Size: {bodySize}\nCurrent Bulk: {actor?.Bulk()}\nBase Stomach Size: {stomachSize}\nFavored Stat: {State.RaceSettings.GetFavoredStat(race)}";
        }

        if (Enum.TryParse(words[2], out TraitType trait))
        {
            return GetTraitData(trait);
        }

        if (Enum.TryParse(words[2], out UnitType unitType))
        {
            switch (unitType)
            {
                case UnitType.Soldier:
                    return "A generic soldier, does anything and everything they are tasked with";
                case UnitType.Leader:
                    return "The leader of an empire, inspires friendly troops through leadership";
                case UnitType.Mercenary:
                    return "A hired mercenary from the mercenary camp";
                case UnitType.Summon:
                    return "A summoned unit.  It will vanish at the end of the battle";
                case UnitType.SpecialMercenary:
                    return "A unique mercenary, only one of each can exist in the world at once, can not retreat and will return to the merc camp if dismissed";
                case UnitType.Adventurer:
                    return "An adventurer, recruited not from the village population, but from an inn";
                case UnitType.Spawn:
                    return "A weaker unit, created under certain conditions";
            }
        }


        if (Enum.TryParse(words[2], out StatusEffectType effectType))
        {
            var effect = unit.GetLongestStatusEffect(effectType);
            if (effect != null)
            {
                switch (effectType)
                {
                    case StatusEffectType.Shielded:
                        return $"(Spell) Unit has resistance against incoming damage\nTurns Remaining: {effect.Duration}";
                    case StatusEffectType.Mending:
                        return $"(Spell) Unit heals a medium amount every turn.\nTurns Remaining: {effect.Duration}";
                    case StatusEffectType.Fast:
                        return $"(Spell) Unit moves faster\nTurns Remaining: {effect.Duration}";
                    case StatusEffectType.Valor:
                        return $"(Spell) Unit does additional damage\nTurns Remaining: {effect.Duration}";
                    case StatusEffectType.Predation:
                        return $"(Spell) Unit has increased voracity and stomach\nTurns Remaining: {effect.Duration}";
                    case StatusEffectType.Poisoned:
                        return $"(Spell) Unit is taking damage over time\nTurns Remaining: {effect.Duration}";
                    case StatusEffectType.WillingPrey:
                        return $"(Spell) Unit wants to be prey (is easier to eat, and less likely to escape)\nTurns Remaining: {effect.Duration}";
                    case StatusEffectType.Diminished:
                        return $"(Spell) Unit is tiny, with decreased stats and easy to eat\nTurns Remaining: {effect.Duration}";
                    case StatusEffectType.Enlarged:
                        return $"(Spell) Unit has increased size and stats\nTurns Remaining: {effect.Duration}";
                    case StatusEffectType.Clumsiness:
                        return $"Unit is easier to hit\nTurns Remaining: {effect.Duration}";
                    case StatusEffectType.Empowered:
                        return $"Unit's stats are temporarily boosted\nTurns Remaining: {effect.Duration}";
                    case StatusEffectType.Shaken:
                        return $"Unit's stats are temporarily lowered\nTurns Remaining: {effect.Duration}";
                    case StatusEffectType.Webbed:
                        return $"Unit only gets 1 AP per turn, and stats are temporarily lowered\nTurns Remaining: {effect.Duration}";
                    case StatusEffectType.Petrify:
                        return $"Unit cannot perform any actions, but is easy to hit, takes half damage from attacks and is bulky to eat.\nTurns Remaining: {effect.Duration}";
                    case StatusEffectType.Berserk:
                        return $"Unit is berserk, its strength and voracity are greatly increased for a brief period\nTurns Remaining: {effect.Duration}";
                    case StatusEffectType.Charmed:
                        return $"Unit fights for the unit that charmed it.";
                    case StatusEffectType.Sleeping:
                        return $"Unit is fast asleep and cannot perform any actions, are easy to hit and eat, and can't struggle.\nTurns Remaining: {effect.Duration}";
                    case StatusEffectType.Focus:
                        return $"Unit has its mind increased by {effect.Duration} + {effect.Duration}%. Lose 3 stacks when hit by an attack.";
                    case StatusEffectType.SpellForce:
                        return $"Unit has its mind increased by {effect.Duration} + {effect.Duration * 10}%, but its mana costs are increased by {effect.Duration * 10}%.";
                    case StatusEffectType.Staggering:
                        return $"Unit has lost balance, increasing damage taken by 20% and halving MP recovery. 1 stack removed per hit.\nCurrent Stacks: {effect.Duration}";
                    case StatusEffectType.Virus:
                        return $"(Spell) Unit is taking damage over time\nTurns Remaining: {effect.Duration}";
                    case StatusEffectType.DivineShield:
                        return $"(Spell) Unit was embraced by a divine being, providing damage mitigation\nTurns Remaining: {effect.Duration}";
                }
            }
        }


        if (State.World?.ItemRepository != null)
        {
            List<Item> allItems = State.World.ItemRepository.GetAllItems();
            string complete = $"{words[0]} {words[1]} {words[2]} {words[3]} {words[4]}";
            for (int i = 0; i < allItems.Count; i++)
            {
                if (words[2] == allItems[i].Name || (complete.Contains(allItems[i].Name) && allItems[i].Name.Contains(words[2]))) //Ensures that the phrase contains the highlighed word
                {
                    if (allItems[i] is Weapon weapon)
                    {
                        return $"{weapon.Description}\nDamage:{weapon.Damage}\nRange:{weapon.Range}\nAccuracy:{weapon.AccuracyModifier}";
                    }

                    if (allItems[i] is Accessory accessory)
                    {
                        return $"{accessory.Description}"; // \n+{accessory.StatBonus} to {(Stat)accessory.ChangedStat}";
                    }

                    if (allItems[i] is SpellBook book)
                    {
                        return $"{book.Description}\n{book.DetailedDescription()}";
                    }
                }
            }
        }

        {
            List<Spell> allSpells = SpellList.SpellDict.Select(s => s.Value).ToList();
            string complete = $"{words[0]} {words[1]} {words[2]} {words[3]} {words[4]}";
            for (int i = 0; i < allSpells.Count; i++)
            {
                if (words[2] == allSpells[i].Name || (complete.Contains(allSpells[i].Name) && allSpells[i].Name.Contains(words[2]))) //Ensures that the phrase contains the highlighed word
                {
                    return $"{allSpells[i].Description}\nRange: {allSpells[i].Range.Min}-{allSpells[i].Range.Max}\nMana Cost: {allSpells[i].ManaCost}\nTargets: {string.Join(", ", allSpells[i].AcceptibleTargets)}";
                }
            }
        }


        switch (words[2])
        {
            case "surrendered":
                return "This unit has surrendered, all units have a 100% chance to eat it, and it only costs 2 AP to eat it.";

            case "Imprinted":
                return $"This unit is imprinted in the village of {unit.SavedVillage.Name}, at level {unit.SavedCopy?.Level ?? 0} with {Math.Round(unit.SavedCopy?.Experience ?? 0)} exp.  " +
                       $"Unit will automatically resurrect there at that power, assuming the village is controlled by friendlies when the unit dies";

            case "STR":
                return STRDef;
            case "DEX":
                return DEXDef;
            case "MND":
                return MNDDef;
            case "WLL":
                return WLLDef;
            case "END":
                return ENDDef;
            case "AGI":
                return AGIDef;
            case "VOR":
                return VORDef;
            case "STM":
                return STMDef;
            case "LDR":
                return LDRDef;

            default:
                return "";
        }



    }
    
    string StatData(Stat stat2, Unit unit)
    {
        string leader = "";
        int leaderBonus = unit.GetLeaderBonus();
        if (leaderBonus > 0) leader = $"+{leaderBonus} from leader\n";
        string traits = "";
        int traitBonus = unit.GetTraitBonus(stat2);
        if (traitBonus > 0) traits = $"+{traitBonus} from traits\n";
        string effects = "";
        int effectBonus = unit.GetEffectBonus(stat2);
        if (effectBonus > 0)
            effects = $"+{effectBonus} from effects\n";
        else if (effectBonus < 0) effects = $"{effectBonus} from effects\n";
        return $"{unit.GetStatBase(stat2)} base {stat2}\n{leader}{traits}{effects}Final Stat: {unit.GetStat(stat2)}";
    }

    public static string GetTraitData(TraitType traitType)
    {
        Trait traitClass = TraitList.GetTrait(traitType);
        if (traitClass != null) return traitClass.Description;
        switch (traitType)
        {
            case TraitType.Resilient:
                return "Takes less damage from attacks";
            case TraitType.FastDigestion:
                return "Unit digests prey faster than normal";
            case TraitType.SlowDigestion:
                return "Unit digests prey slower than normal";
            case TraitType.Intimidating:
                return "Enemies within 1 tile get a penalty to accuracy against all targets";
            case TraitType.AdeptLearner:
                return "All stats are favored, randomly get 1 point in 2 different stats with level up";
            case TraitType.SlowBreeder:
                return "Race produces new population at a slower rate than normal";
            case TraitType.ProlificBreeder:
                return "Race produces new population at a faster rate than normal";
            case TraitType.Flight:
                return "Unit can pass through obstacles and other units in tactical mode.\nMust end turn on solid ground\nIf you try to take an action or end your turn in an invalid place, it will automatically undo your movement";
            case TraitType.Pounce:
                return "Unit spends a minimum of 2 AP to jump next to a target that is within 2-4 tiles (if there is space) and melee attack or vore it";
            case TraitType.Biter:
                return "A failed vore attack will result in an attack attempt with a weak melee weapon";
            case TraitType.Pathfinder:
                return "Passes through all terrain at a movement cost of 1.\nMore than half of the army has to have this trait to have an effect";
            case TraitType.AstralCall:
                return "Unit has a chance at the beginning of battle to summon a weaker unit from its race\nThey return to their own dimension after the battle";
            case TraitType.TentacleHarassment:
                return "Shifting tentacles distract and harass opponents within 1 tile.\nLowers enemy stats by a small amount";
            case TraitType.BornToMove:
                return "Experienced at carrying extra weight.\nUnit suffers no defense penalty and no movement penalty from units it is carrying.";
            case TraitType.Resourceful:
                return "Unit has an additional item slot";
            case TraitType.ForcefulBlow:
                return "Unit will knock enemy units back in melee (straight back or one diagonal to the side).\nIf a unit is blocked in those directions, it will take extra damage";
            case TraitType.NimbleClimber:
                return "Unit is a strong climber and can pass through trees unhindered";
            case TraitType.Dazzle:
                return "Units attacking this unit have to run a check based on the comparison of the will values, units failing the check will simply end their turn without attacking and suffer a stat penalty until their next turn (chance caps at 20% at 5x will, and is reflected in shown hit odds)";
            case TraitType.Charge:
                return "Unit has a signficant boost to movement speed for the first two turns of every battle";
            case TraitType.Feral:
                return "Unit can't use weapons or books, but does a considerable amount of melee damage (6 base) (AI will still try to buy stuff for them)";
            case TraitType.DualStomach:
                return "Unit has two stomachs, the second digests faster than the first and is harder to escape from (units slowly migrate to the second)";
            case TraitType.MadScience:
                return "Allows casting of a random spell for normal mana cost once per battle";
            case TraitType.ShunGokuSatsu:
                return "Allows usage of a powerful ability that does attacks and vore.  Can only be used every 3 turns";
            case TraitType.Eternal:
                return "(Cheat Trait) - This unit can never die.  If it is killed during a battle, it will be set to full hp and act as though it fled (will rejoin if the army wins, otherwise sets off for the closest town)";
            case TraitType.Revenant:
                return "(Cheat Trait) - This unit can never die from weapons or spells, though digestion can kill it permanently.  If it is 'killed' during a battle, it will be set to full hp and act as though it fled (will rejoin if the army wins, otherwise sets off for the closest town) Note that these don't give immunity to digestion conversion unlike the pure eternal trait";
            case TraitType.Reformer:
                return "(Cheat Trait) - This unit can never die from being digested, but spells and weapons can kill it permanently.  If it is killed during a battle, it will be set to full hp and act as though it fled (will rejoin if the army wins, otherwise sets off for the closest town) Note that these don't give immunity to digestion conversion unlike the pure eternal trait";
            case TraitType.LuckySurvival:
                return "Unit has an 80% chance of acting like an eternal unit on death (coming back to life after the battle), with a 20% chance of dying normally.";
            case TraitType.Replaceable:
                return "If the unit dies and its side wins, the unit will be replaced by a rather similar unit from the same race";
            case TraitType.Greedy:
                return "The unit will avoid giving up prey at all costs -- will not auto regurgitate friendlies regardless of settings, and the regurgitate command is disabled";
            case TraitType.RangedVore:
                return "Unit can perform vore actions at a range of up to 4 tiles, but chance of success drops per tile, but doesn't drop against flying units.";
            case TraitType.HeavyPounce:
                return "Pounce does extra melee damage based on the weight of prey, but defense is also lowered for one turn after pouncing based on the weight of prey (requires the pounce trait to be useable)";
            case TraitType.Cruel:
                return "Unit can attempt to eat non-surrendered allied units at normal odds";
            case TraitType.MetabolicSurge:
                return "Unit gains a burst of power after digesting a victim";
            case TraitType.EnthrallingDepths:
                return "Prey is afflicted with the Prey's Curse effect";
            case TraitType.FearsomeAppetite:
                return "Consuming a victim frightens nearby allies of the prey, temporarily reducing their stats";
            case TraitType.Endosoma:
                return "Can vore friendly units, friendly units that are vored take no digestion damage \nThey do not try to escape, but can be regurgitated or are freed at the end of battle\nHas 100% chance to eat allies, and only costs 2 AP, like eating surrendered units.  May cause battles to not automatically end if used with TheGreatEscape";
            case TraitType.TailStrike:
                return "An attack that does less damage, but attacks the tile and the 2 tiles adjacent to it that are within reach";
            case TraitType.HealingBelly:
                return "An accessory trait to endosoma that makes friendly prey receive healing each turn.  (Does nothing without the endosoma trait)\n(Cheat Trait)";
            case TraitType.Assimilate:
                return "If the unit has less than 5 traits, upon finishing absorption of an enemy unit, will take a random trait from them that the unit doesn't currently have.  If the unit has 5 traits, the random trait will replace this trait. Transferable via Endosoma.\n(Cheat Trait)";
            case TraitType.AdaptiveBiology:
                return "Upon finishing absorption of an enemy unit, will take a random trait from them that the unit doesn't currently have and add it to a list of 3 rotating traits.  If the list already has 3 rotating traits, the oldest trait is removed.  This can't trigger on the same kill as Assimilate. Transferable via Endosoma.\n(Cheat Trait)";
            case TraitType.KillerKnowledge:
                return "Every four weapon / spell kills (but not vore kills), the unit will get a permanent +1 to all stats\n(Cheat Trait)";
            case TraitType.PollenProjector:
                return "Allows using the pollen cloud ability once per battle, which attempts to inflict a few status effects in a 3x3 area for a small mana cost.  This trait also makes the unit immune to most of the statuses from this ability.";
            case TraitType.Webber:
                return "Allows using the web ability once per battle, which attempts to inflict the webbed status for 3 turns, which lowers AP to 1, and reduces stats.";
            case TraitType.Camaraderie:
                return "Prevents the unit from defecting to rejoin its race if that option is enabled.";
            case TraitType.RaceLoyal:
                return "Unit will defect to rejoin its race at every opportunity if that option is enabled.";
            case TraitType.WillingRace:
                return "Gives the whole race the willing prey spell effect permanently, which makes them easier to eat, and changes some of the dialogue.";
            case TraitType.InfiniteAssimilation:
                return "Upon finishing absorption of an enemy unit, will take a random trait from them that the unit doesn't currently have. This version has no cap, so it can be a little bit of a text mess. Transferable via Endosoma.\n(Cheat Trait)";
            case TraitType.GlueBomb:
                return "Gives access to a single use ability that applies the glued status effect to a 3x3 group.  Glued units are very slow, and it takes a while to get it off.";
            case TraitType.TasteForBlood:
                return "After digesting or killing someone, the unit will get a random positive buff for 5 turns.";
            case TraitType.PleasurableTouch:
                return "This unit's belly rub actions are more effective on others (doubled effect).";
            case TraitType.PoisonSpit:
                return "Allows using the poison spit ability once per battle, which does damage in a 3x3 and attempts to apply a strong short term poison as well.  This trait also makes the unit immune to poison damage.";
            case TraitType.DigestionConversion:
                return "When a unit finishes digesting someone, there's a 50% chance they will then convert to the predator's side, and be healed to half of their max life.  Can't convert leaders, summons, or units with saved copies.\n(Cheat Trait)";
            case TraitType.DigestionRebirth:
                return "When a unit finishes digesting someone, there's a 50% chance they will then convert to the predator's side and change race to the predator's race, and be healed to half of their max life.  Can't convert leaders, summons, or units with saved copies.\n(Cheat Trait)";
            case TraitType.SenseWeakness:
                return "Unit does more melee/ranged damage against targets with less health, and also increases for every negative status effect the target has.";
            case TraitType.BladeDance:
                return "Unit gains a stack of blade dance every time they successfully hit their opponent with melee, and lose a stack every time they are hit with melee.  Each stack gives +2 strength and +1 agility.";
            case TraitType.EssenceAbsorption:
                return "Every four vore digestions, the unit will get a permanent +1 to all stats\n(Cheat Trait)";
            case TraitType.AntPheromones:
                return "Unit will summon some friendly ants, depending on how many units have this trait.";
            case TraitType.Fearless:
                return "Unit can not flee nor surrender (also applies to auto-surrender).  If something does happen to make the unit surrender, it will automatically recover on the next turn.";
            case TraitType.Inedible:
                return "Unit can not be vored by other units.  (It makes their effective size so big that no one has the capacity to eat them)\n(Cheat Trait)";
            case TraitType.AllOutFirstStrike:
                return "Unit starts battle in a protected state, with high dodge rate.  On their first attack or vore attempt of the battle, they get a significant bonus to damage or vore chance.  After that they become vulnerable, and move slower and have a dodge penalty.";
            case TraitType.VenomousBite:
                return "A missed bite from the biter trait will also poison an enemy, and give them the shaken debuff.";
            case TraitType.Petrifier:
                return "Gives access to a single use ability that applies the petrified status effect to a target.  It prevents the target from acting, but also makes them resistant to damage and bulky to swallow.";
            case TraitType.VenomShock:
                return "Gives this unit significantly increased melee damage and vore odds against targets who are poisoned.";
            case TraitType.Tenacious:
                return "Unit gains a stack of tenacity every time they are hit or miss an attack, and lose five stacks every time they hit or vore an enemy.  Each stack gives +10% str/agi/vor.";
            case TraitType.PredConverter:
                return "This unit will always convert unbirthed prey to their side upon full digestion regardless of KuroTenko settings, putting this together with PredRebirther or PredGusher on same unit is not recommended";
            case TraitType.PredRebirther:
                return "This unit will always rebirth unbirthed prey as their race upon full absorption regardless of KuroTenko settings, putting this together with PredConverter or PredGusher on same unit is not recommended.";
            case TraitType.PredGusher:
                return "This unit will always turn unbirthed units into a sticky puddle, they will not be converted/rebirthed. (Basically cancels out certain game settings or traits)";
            case TraitType.SeductiveTouch:
                return "Unit's belly rub action can make enemies pause for a turn, or even switch sides, as long as they haven't taken damage for two turns.\n(Cheat Trait)";
            case TraitType.TheGreatEscape:
                return "This unit cannot be digested, but the battle will end if only units with this remain and they're all eaten.  The prey are assumed to escape sometime later, and are count as fled units. (Note that you'd need end of battle review checked to see the escape messages as they happen at the very end of battle)";
            case TraitType.Growth:
                return "Each absorbtion makes this unit grow in size, but the effect slowly degrades outside battle.\n(Cheat Trait)";
            case TraitType.PermanentGrowth:
                return "An accessory trait to Growth that makes growth gained permanent.  (Does nothing without the Growth trait)\n(Cheat Trait)";
            case TraitType.Berserk:
                return "If the unit is reduced below half health by an attack, will go berserk, greatly increasing its strength and voracity for 3 turns.\nCan only occur once per battle.";
            case TraitType.SynchronizedEvolution:
                return "Any trait this unit assimilates is received by all members of their race. (requires Assimilate or InfiniteAssimilation)\n(Cheat Trait)";
            case TraitType.Charmer:
                return "Allows the casting of the Charm spell once per battle";
            case TraitType.HypnoticGas:
                return "Can emit Gas that turns foes into subservient non-combatants that are easy to vore, use buff spells if they have any, and rub bellies. Units of identical alignment are unaffected.";
            case TraitType.ForceFeeder:
                return "Allows unit to attempt force-feeding itself to another unit at will.";
            case TraitType.Possession:
                return "Temporarily control a Pred unit while digesting inside\n (Cheat Hidded Trait)";
            case TraitType.Corruption:
                return "If a currupted unit is digested, the pred will build up corruption as a hidden status. Once corrupted prey with a stat total equal to that of the pred has been digested, they are under control of the side of the last-digested corrupted.\n(Hidden Trait)";
            case TraitType.Reanimator:
                return "Allows unit to use <b>Reanimate</b>, an attack that brings any unit back to life as the caster's summon, once per battle";
            case TraitType.Reincarnation:
                return "Soon after this unit dies, one of the new Units that come into being will be a reincarnation of them.\n(Hidden Trait)";
            case TraitType.Transmigration:
                return "Soon after this unit is digested, one of the new Units that come into being as the pred's race will be a reincarnation of them.\n(Hidden Trait)";
            case TraitType.InfiniteReincarnation:
                return "Soon after this unit dies, one of the new Units that come into being will be a reincarnation of them.\nReincarnations will also have this trait (Hidden Trait)(Cheat Trait)";
            case TraitType.InfiniteTransmigration:
                return "Soon after this unit is digested, one of the new Units that come into being as the pred's race will be a reincarnation of them.\nReincarnations will also have this trait (Hidden Trait)(Cheat Trait)";
            case TraitType.Untamable:
                return "No matter which army this unit is in, it is only ever truly aligned with its race. Only vore-based types of conversion are really effective\n(Hidden Trait)";
            case TraitType.Binder:
                return "Allows unit to either take control of any summon, or re-summon the most recently bound one once a battle.";
            case TraitType.Infiltrator:
                return "Armies fully consisting of infiltrators are invisible to the enemy. Using 'Exchange' on an enemy village or a Mercenary camp will infiltrate it (For Player villages, infiltrating as a Mercenary will be preferred, otherwise as recruitables).\nWill also use conventional changes of allignment to go undercover\n(Hidden Trait)";
            case TraitType.BookWormI:
                return "Unit generates with a random Tier 1 Book.";
            case TraitType.BookWormIi:
                return "Unit generates with a random Tier 2 Book.";
            case TraitType.BookWormIii:
                return "Unit generates with a random Tier 3-4 Book.";
            case TraitType.Temptation:
                return "Units that are put under a mindcontrol (e.g. Charm, Hypnosis) effect by this unit want to force-feed themselves to it or its close allies.";
            case TraitType.Infertile:
                return "Unit cannot contribute to village population growth.";
            case TraitType.HillImpedence:
                return "Unit treats all hills as impassable.\nMore than half of the army has to have this trait to have an effect";
            case TraitType.GrassImpedence:
                return "Unit treats grass as impassable.\nMore than half of the army has to have this trait to have an effect";
            case TraitType.MountainWalker:
                return "Unit can cross over mountains and broken cliffs (but not stop on one).\nAt least half of the army has to have this trait to have an effect";
            case TraitType.WaterWalker:
                return "Unit can cross over water (but not stop on it).\nAt least half of the army has to have this trait to have an effect";
            case TraitType.LavaWalker:
                return "Unit can cross over lava (but not stop on it).\nAt least half of the army has to have this trait to have an effect";
            case TraitType.SwampImpedence:
                return "Unit treats swamps as impassable.\nMore than half of the army has to have this trait to have an effect";
            case TraitType.ForestImpedence:
                return "Unit treats forests as impassable.\nMore than half of the army has to have this trait to have an effect";
            case TraitType.DesertImpedence:
                return "Unit treats deserts and sand hills as impassable.\nMore than half of the army has to have this trait to have an effect";
            case TraitType.SnowImpedence:
                return "Unit treats snow and snow hills as impassable.\nMore than half of the army has to have this trait to have an effect";
            case TraitType.VolcanicImpedence:
                return "Unit treats volcanic ground as impassable.\nMore than half of the army has to have this trait to have an effect";
            case TraitType.Donor:
                return "Upon being absorbed, this unit bestows all traits that are listed below \"Donor\" in its trait list.";
            case TraitType.Extraction:
                return "Every time digestion progresses, this unit steals one trait from each prey inside them, if only duplicates (or non-assimilable traits) remain, they are turned into exp. Absorbtion steals any that are left. Endoed units instead gain traits.\n(Cheat Trait)";
            //case Traits.Shapeshifter:
            //    return "Gives the ability to change into different races after acquiring them via absorbing, being reborn, reincarnating, being endoed or infiltrating. Also Allows Traversal of all terrain at normal speed.";
            //case Traits.Skinwalker:
            //    return "Gives the ability to change into specific units after absorbing them or being endoed or infiltrating. Or into the alternate selves acquired by being reborn or reincarnated. Also Allows Traversal of all terrain at normal speed.";
            case TraitType.BookEater:
                return "When this unit would equip a book, it is instead consumed and the spell becomes innate. Does not consume already equipped books, but does consume one if the unit would gain more than it could carry via BookWorm.";
            case TraitType.Whispers:
                return "When eaten, Predator is afflicted by Prey's curse, and has a chance to be charmed each round";
            case TraitType.TraitBorrower:
                return "While digesting, , Predator is able to use prey's normal traits";
            case TraitType.Changeling:
                return "While Absorbing a prey, Becomes that prey's Race until absorption";
            case TraitType.GreaterChangeling:
                return "While digesing a prey, Becomes that prey's Race until absorption";
            case TraitType.SpiritPossession:
                return "Units soul continues to possess pred after death";
            case TraitType.ForcedMetamorphosis:
                return "Pred Unit will gain the metamorphosis trait on Prey death";
            case TraitType.Metamorphosis:
                return "Unit changes Race upon digestion";
            case TraitType.MetamorphicConversion:
                return "Unit changes Race and side upon digestion";
            case TraitType.Perseverance:
                return "Unit heals after not taking damage for a 3 turns, scaling higer with each turn without damage thereafter.";
            case TraitType.ManaAttuned:
                return "Unit thrives on mana, uses 10% of their max mana every turn. Unit falls asleep for 2 turns if they don't have enough mana, but regenerate 50% max mana every turn they are asleep.";
            case TraitType.NightEye:
                return "Increases night time vision range by +1 in Tactical battles.";
            case TraitType.AccuteDodge:
                return "Unit gains +10% graze chance.";
            case TraitType.KeenEye:
                return "Unit gains +10% critical strike chance.";
            case TraitType.SpellBlade:
                return "Unit's weapon damage also scales with mind. (Half as effectively as weapons main stat)";
            case TraitType.ArcaneMagistrate:
                return "Unit gains 1 focus when it hits a spell, unit gains 4 more if the spell kills the target.";
        }

        return "<b>This trait needs a tooltip!</b>";
    }

    public static string GetAIData(RaceAI ai)
    {
        switch (ai)
        {
            case RaceAI.Standard:
                return "Straightforward battlers";
            case RaceAI.Hedonist:
                return "Will try to find the time for massaging any prey-filled parts on their comrades or their own body.\nDon't be fooled – this is deceptively efficient.";
            case RaceAI.ServantRace:
                return "Acts Subservient towards units of the most powerful race on their side, flocking to rub those individuals.\n" +
                       "Racial superiority is based on eminence.";
            //case RaceAI.NonCombatant:
            //    return "Won't use weapons or offensive spells, but supports combatants with beneficial spells and bodily services.";
        }

        return "<b>This AI needs a tooltip!</b>";
    }

    internal void UpdateInformationDefaultTooltip(int value)
    {

        string description = DefaultTooltips.Tooltip(value);
        InfoUpdate(description, true);
    }

    internal void InfoUpdate(string description, bool linger = false)
    {
        if (description == "")
        {
            gameObject.SetActive(false);
            return;
        }
        gameObject.SetActive(true);
        _remainingFrames = linger ? 999 : 3;
        _text.text = description;
        float xAdjust = 10;
        float exceeded = Input.mousePosition.x + (_rect.rect.width * Screen.width / 1920) - Screen.width;
        if (exceeded > 0)
            xAdjust = -exceeded;
        transform.position = Input.mousePosition + new Vector3(xAdjust, 0, 0);
    }
}