﻿using System;
using System.Collections.Generic;

internal abstract class Trait
{
    internal string Description;
}

internal abstract class VoreTrait : Trait, IVoreCallback
{
    public virtual int ProcessingPriority => 0;

    public abstract bool IsPredTrait { get; }

    public virtual bool OnAbsorption(Prey preyUnit, ActorUnit predUnit, PreyLocation location) => true;

    public virtual bool OnDigestion(Prey preyUnit, ActorUnit predUnit, PreyLocation location) => true;

    public virtual bool OnDigestionKill(Prey preyUnit, ActorUnit predUnit, PreyLocation location) => true;

    public virtual bool OnFinishAbsorption(Prey preyUnit, ActorUnit predUnit, PreyLocation location) => true;

    public virtual bool OnFinishDigestion(Prey preyUnit, ActorUnit predUnit, PreyLocation location) => true;

    public virtual bool OnRemove(Prey preyUnit, ActorUnit predUnit, PreyLocation location) => true;

    public virtual bool OnSwallow(Prey preyUnit, ActorUnit predUnit, PreyLocation location) => true;
}

internal class PermanentBoosts
{
    internal float ExpRequired = 1.0f;
    internal float ExpGain = 1.0f;
    internal float ExpGainFromVore = 1.0f;
    internal float ExpGainFromAbsorption = 1.0f;
    internal float PassiveHeal = 1.0f;
    internal float CapacityMult = 1;
    internal DirectionalStat Incoming = new DirectionalStat();
    internal DirectionalStat Outgoing = new DirectionalStat();
    internal float FlatHitReduction = 1.0f;
    internal float SpeedLossFromWeightMultiplier = 1.0f;
    internal float DodgeLossFromWeightMultiplier = 1.0f;
    internal float BulkMultiplier = 1.0f;
    internal float SpeedMultiplier = 1.0f;
    internal int MinSpeed = 1;
    internal int SpeedBonus = 0;
    internal int MeleeAttacks = 1;
    internal int RangedAttacks = 1;
    internal int VoreAttacks = 1;
    internal int SpellAttacks = 1;
    internal float ManaMultiplier = 1.0f;
    internal int VoreMinimumOdds = 0;
    internal int TurnCanFlee = 8;
    internal int DigestionImmunityTurns = 0;
    internal int HealthRegen = 0;
    internal int OnLevelUpBonusToAllStats = 0;
    internal int OnLevelUpBonusToGiveToTwoRandomStats = 0;
    internal bool OnLevelUpAllowAnyStat = false;
    internal float Scale = 1f;
    internal float StatMult = 1f;
    internal float VirtualDexMult = 1;
    internal float VirtualStrMult = 1;
    internal float FireDamageTaken = 1;
    internal float GrowthDecayRate = 1;
    internal int SightRangeBoost = 0;
}

internal class DirectionalStat
{
    internal float ChanceToEscape = 1;
    internal float MeleeDamage = 1;
    internal float RangedDamage = 1;
    internal float MagicDamage = 1;
    internal float DigestionRate = 1;
    internal float AbsorptionRate = 1;
    internal float Nutrition = 1;
    internal int ManaAbsorbHundreths = 0;

    /// <summary>Positive favors defender, negative favors attacker</summary>
    internal float MeleeShift = 0;

    internal float RangedShift = 0;
    internal float MagicShift = 0;
    internal float VoreOddsMult = 1;
    internal float GrowthRate = 1;

    internal float CritRateShift = 0;
    internal float CritDamageMult = 1;
    internal float GrazeRateShift = 0;
    internal float GrazeDamageMult = 1;
}


internal interface IStatBoost
{
    int StatBoost(Unit unit, Stat stat);
}

internal interface IAttackStatusEffect
{
    void ApplyStatusEffect(ActorUnit actor, ActorUnit target, bool ranged, int damage);
}

internal interface IVoreDefenseOdds
{
    void VoreDefense(ActorUnit defender, ref float voreMult);
}

internal interface IVoreAttackOdds
{
    void VoreAttack(ActorUnit attacker, ref float voreMult);
}

internal interface IPhysicalDefenseOdds
{
    void PhysicalDefense(ActorUnit defender, ref float defMult);
}

internal interface IProvidesSingleSpell
{
    List<SpellType> GetSingleSpells(Unit unit);
}

internal interface IProvidesMultiSpell
{
    List<SpellType> GetMultiSpells(Unit unit);
}

internal interface INoAutoEscape
{
    bool CanEscape(Prey preyUnit, ActorUnit predUnit);
}

internal abstract class AbstractBooster : Trait
{
    internal Action<PermanentBoosts> Boost;
}

internal class Booster : AbstractBooster
{
    public Booster(string desc, Action<PermanentBoosts> boost)
    {
        Description = desc;
        Boost = boost;
    }
}

internal abstract class VoreTraitBooster : AbstractBooster, IVoreCallback
{
    public virtual int ProcessingPriority => 0;

    public abstract bool IsPredTrait { get; }

    public virtual bool OnAbsorption(Prey preyUnit, ActorUnit predUnit, PreyLocation location) => true;

    public virtual bool OnDigestion(Prey preyUnit, ActorUnit predUnit, PreyLocation location) => true;

    public virtual bool OnDigestionKill(Prey preyUnit, ActorUnit predUnit, PreyLocation location) => true;

    public virtual bool OnFinishAbsorption(Prey preyUnit, ActorUnit predUnit, PreyLocation location) => true;

    public virtual bool OnFinishDigestion(Prey preyUnit, ActorUnit predUnit, PreyLocation location) => true;

    public virtual bool OnRemove(Prey preyUnit, ActorUnit predUnit, PreyLocation location) => true;

    public virtual bool OnSwallow(Prey preyUnit, ActorUnit predUnit, PreyLocation location) => true;
}


internal static class TraitList
{
    internal static Trait GetTrait(TraitType traitType)
    {
        _traits.TryGetValue(traitType, out Trait retTrait);
        return retTrait;
    }

    private static Dictionary<TraitType, Trait> _traits = new Dictionary<TraitType, Trait>()
    {
        [TraitType.Frenzy] = new Frenzy(),
        [TraitType.ThrillSeeker] = new ThrillSeeker(),
        [TraitType.PackStrength] = new PackStat(Stat.Strength),
        [TraitType.PackDexterity] = new PackStat(Stat.Dexterity),
        [TraitType.PackDefense] = new PackStat(Stat.Agility),
        [TraitType.PackWill] = new PackStat(Stat.Will),
        [TraitType.PackMind] = new PackStat(Stat.Mind),
        [TraitType.PackVoracity] = new PackStat(Stat.Voracity),
        [TraitType.PackStomach] = new PackStat(Stat.Stomach),
        [TraitType.PackTactics] = new PackTactics(),
        [TraitType.Paralyzer] = new Paralyzer(),
        [TraitType.BoggingSlime] = new BoggingSlime(),
        [TraitType.Vampirism] = new Vampirism(),
        [TraitType.Stinger] = new Stinger(),
        [TraitType.DefensiveStance] = new DefensiveStance(),
        [TraitType.Ravenous] = new Ravenous(),
        [TraitType.Possession] = new Possession(),
        [TraitType.UnpleasantDigestion] = new UnpleasantDigestion(),
        [TraitType.Parasite] = new Parasite(),
        [TraitType.Whispers] = new Whispers(),
        [TraitType.Metamorphosis] = new Metamorphosis(),
        [TraitType.Changeling] = new Changeling(),
        [TraitType.GreaterChangeling] = new GreaterChangeling(),
        [TraitType.Symbiote] = new Symbiote(),
        [TraitType.TraitBorrower] = new TraitBorrower(),
        [TraitType.CreateSpawn] = new CreateSpawn(),
        [TraitType.SpiritPossession] = new SpiritPossession(),
        [TraitType.ForcedMetamorphosis] = new ForcedMetamorphosis(),
        [TraitType.MetamorphicConversion] = new MetamorphicConversion(),
        [TraitType.Tempered] = new Booster("Reduces damage taken from ranged attacks.\nIncreases damage taken from melee attacks", (s) =>
        {
            s.Incoming.RangedDamage *= .7f;
            s.Incoming.MeleeDamage *= 1.3f;
            s.VirtualDexMult *= 1.1f;
        }),
        [TraitType.GelatinousBody] = new Booster("Takes less damage from attacks, but is easier to vore", (s) =>
        {
            s.Incoming.RangedDamage *= .75f;
            s.Incoming.MeleeDamage *= 0.8f;
            s.Incoming.VoreOddsMult *= 1.15f;
        }),
        [TraitType.MetalBody] = new Booster("Provides vore resistance, and their remains are only worth half as much healing", (s) =>
        {
            s.Incoming.VoreOddsMult *= .7f;
            s.Outgoing.Nutrition *= .5f;
        }),
        [TraitType.EasyToVore] = new Booster("Unit is easier to vore than normal", (s) => s.Incoming.VoreOddsMult *= 1.25f),
        [TraitType.Defenseless] = new Booster("Unit is incredibly easy to vore", (s) => s.Incoming.VoreOddsMult *= 1000),
        [TraitType.BornToMove] = new Booster("Experienced at carrying extra weight.\nUnit suffers a smaller defense penalty and no movement penalty from units it is carrying.", (s) =>
        {
            s.SpeedLossFromWeightMultiplier = 0;
            s.DodgeLossFromWeightMultiplier = 0.2f;
        }),
        [TraitType.Maul] = new Booster("Allows unit to attack twice in melee.\nEach attack uses half of max mp", (s) =>
        {
            s.MeleeAttacks += 1;
            s.VirtualStrMult *= 1.8f;
        }),
        [TraitType.DoubleAttack] = new Booster("Allows unit to attack twice in melee or ranged.\nEach attack uses half of max mp", (s) =>
        {
            s.MeleeAttacks += 1;
            s.RangedAttacks += 1;
        }),
        [TraitType.Nauseous] = new Booster("This unit has a higher than average chance of having prey escape", (s) => s.Outgoing.ChanceToEscape *= 2),
        [TraitType.EscapeArtist] = new Booster("Unit is much more likely to escape from predators", (s) => s.Incoming.ChanceToEscape *= 2),
        [TraitType.Submissive] = new Booster("Unit does not try to escape", (s) => s.Incoming.ChanceToEscape *= 0),
        [TraitType.EvasiveBattler] = new Booster("This unit can flee from battles on its fourth turn", (s) => s.TurnCanFlee = 4),
        [TraitType.Prey] = new Booster("Unit can not vore other units.\nReceives 15% more exp, and heals twice as fast in towns", (s) =>
        {
            s.ExpGain *= 1.15f;
            s.PassiveHeal *= 2;
        }),
        [TraitType.Clever] = new Booster("Requires less experience to level up", (s) => s.ExpRequired *= 0.7f),
        [TraitType.Foolish] = new Booster("Requires additional experience to level up", (s) => s.ExpRequired *= 1.4f),
        [TraitType.StrongMelee] = new Booster("Does additional damage in melee", (s) =>
        {
            s.Outgoing.MeleeDamage *= 1.2f;
            s.VirtualStrMult *= 1.2f;
        }),
        [TraitType.WeakAttack] = new Booster("Does reduced damage in melee", (s) =>
        {
            s.Outgoing.MeleeDamage *= 0.8f;
            s.VirtualDexMult *= 1.2f;
        }),
        [TraitType.FastDigestion] = new Booster("Does additional acid damage to prey", (s) => s.Outgoing.DigestionRate *= 2f),
        [TraitType.SlowDigestion] = new Booster("Does reduced acid damage to prey", (s) => s.Outgoing.DigestionRate *= 0.5f),
        [TraitType.Tasty] = new Booster("Provides additonal healing when consumed", (s) => s.Outgoing.Nutrition *= 2f),
        [TraitType.Disgusting] = new Booster("Provides very little healing when consumed", (s) => s.Outgoing.Nutrition *= 0.2f),
        [TraitType.ArtfulDodge] = new Booster("Flat 10% chance to dodge all attacks or vore attacks", (s) => s.FlatHitReduction *= 0.9f),
        [TraitType.AcidResistant] = new Booster("Takes less damage from acid, and gets a 1 turn grace period before taking damage when prey", (s) =>
        {
            s.Incoming.DigestionRate *= 0.5f;
            s.DigestionImmunityTurns += 1;
        }),
        [TraitType.SoftBody] = new Booster("Unit takes additional acid damage", (s) => s.Incoming.DigestionRate *= 2f),
        [TraitType.KeenReflexes] = new Booster("Gains a significant amount of dodge chance against ranged attacks", (s) => s.Incoming.RangedShift += 1.5f),
        [TraitType.StrongGullet] = new Booster("Allows unit to do 2 vore attempts.\nEach vore uses half of max mp", (s) => s.VoreAttacks += 1),
        [TraitType.AdeptLearner] = new Booster("Allows picking of any stat on level up and gives +1 to 2 random stats on level up", (s) =>
        {
            s.OnLevelUpBonusToGiveToTwoRandomStats += 1;
            s.OnLevelUpAllowAnyStat = true;
        }),
        [TraitType.Lethargic] = new Booster("Unit is significantly less likely to escape from predators", (s) => s.Incoming.ChanceToEscape *= 0.5f),
        [TraitType.AcidImmunity] = new Booster("Unit is immune to acid damage.  To avoid never ending battles, the immunity only lasts for 20 turns in the same belly", (s) => s.DigestionImmunityTurns += 20),
        [TraitType.Large] = new Booster("Unit is larger than normal", (s) => s.Scale *= 1.5f),
        [TraitType.Small] = new Booster("Unit is smaller than normal", (s) => s.Scale *= 2.0f / 3.0f),
        [TraitType.MagicResistance] = new Booster("Unit is harder to hit with magic", (s) => s.Incoming.MagicShift += 0.2f),
        [TraitType.MagicProwess] = new Booster("Unit's spells are more accurate", (s) => s.Outgoing.MagicShift -= 0.2f),
        [TraitType.FastAbsorption] = new Booster("Unit absorbs dead prey quickly", (s) => s.Outgoing.AbsorptionRate *= 2f),
        [TraitType.SlowAbsorption] = new Booster("Unit absorbs dead prey slowly", (s) => s.Outgoing.AbsorptionRate *= 0.5f),
        [TraitType.IronGut] = new Booster("This unit is hard to escape from", (s) => s.Outgoing.ChanceToEscape *= 0.5f),
        [TraitType.SteadyStomach] = new Booster("Unit keeps prey down slightly better than average", (s) => s.Outgoing.ChanceToEscape *= 0.85f),
        [TraitType.Bulky] = new Booster("Unit is bulkier than normal (increased size, making them harder to swallow, without actually giving other bonuses or making them bigger like the scale traits)", (s) => s.BulkMultiplier *= 1.75f),
        [TraitType.SlowMovement] = new Booster("Unit is slower than normal, but suffers reduced prey penalties to speed.", (s) =>
        {
            s.SpeedMultiplier *= 0.75f;
            s.MinSpeed = 2;
            s.SpeedLossFromWeightMultiplier = 0.5f;
        }),
        [TraitType.VerySlowMovement] = new Booster("Unit is considerably slower than normal, but suffers no prey penalties to speed.", (s) =>
        {
            s.SpeedMultiplier *= 0.65f;
            s.MinSpeed = 2;
            s.SpeedLossFromWeightMultiplier = 0;
        }),
        [TraitType.Toxic] = new Booster("Melee attackers have a chance to be poisoned on hitting this unit, and it provides no healing when absorbed.", (s) => s.Outgoing.Nutrition = 0),
        [TraitType.HardSkin] = new Booster("Its skin is hard and ranged weapons have a harder time penetrating it.", (s) =>
        {
            s.Incoming.RangedDamage *= .75f;
            s.VirtualDexMult *= 1.1f;
        }),
        [TraitType.QuickShooter] = new Booster("Unit is quite adept with ranged weapons and is able to attack twice with them.", (s) =>
        {
            s.RangedAttacks += 1;
            s.VirtualDexMult *= 2.2f;
        }),
        [TraitType.FastCaster] = new Booster("Unit is quite adept with spells and is able to cast two spells per turn.", (s) => s.SpellAttacks += 1),
        [TraitType.RangedIneptitude] = new Booster("Unit's ranged weapons do less damage than normal", (s) =>
        {
            s.Outgoing.RangedDamage *= 0.8f;
            s.VirtualStrMult *= 1.2f;
        }),
        [TraitType.KeenShot] = new Booster("Unit's ranged weapons do additional damage", (s) =>
        {
            s.Outgoing.RangedDamage *= 1.2f;
            s.VirtualDexMult *= 1.2f;
        }),
        [TraitType.HotBlooded] = new Booster("Unit is used to the heat and takes significantly less damage from fire spells", (s) => s.FireDamageTaken *= .25f),
        [TraitType.FocusedDevelopment] = new Booster("Allows picking of any stat on level up", (s) => { s.OnLevelUpAllowAnyStat = true; }),
        [TraitType.ManaRich] = new Booster("Unit has higher mana cap, units absorbing this unit will recover some of their mana", (s) =>
        {
            s.ManaMultiplier *= 1.5f;
            s.Outgoing.ManaAbsorbHundreths += 40;
        }),
        [TraitType.ManaDrain] = new Booster("This unit will recover some mana while absorbing others, but it also receives less health", (s) =>
        {
            s.Incoming.ManaAbsorbHundreths += 40;
            s.Incoming.Nutrition *= .6f;
        }),
        [TraitType.CursedMark] = new Booster("This unit gets various combat boosts (increased damage, accuracy and damage resistance), but is slower and significantly easier to eat and keep down, and whoever digests this unit inherits this trait (applied at time of death)", (s) =>
        {
            s.Outgoing.MeleeDamage *= 1.4f;
            s.Outgoing.RangedDamage *= 1.4f;
            s.Outgoing.MagicDamage *= 1.4f;
            s.Outgoing.MeleeShift -= 1;
            s.Outgoing.RangedShift -= 1;
            s.Outgoing.MagicShift -= 1;
            s.Incoming.MeleeDamage *= .75f;
            s.Incoming.RangedDamage *= .75f;
            s.Incoming.VoreOddsMult *= 2.5f;
            s.SpeedMultiplier *= .8f;
            s.Incoming.ChanceToEscape *= .25f;
        }),
        [TraitType.Slippery] = new Booster("This unit is harder to eat, but has a hard time escaping once eaten", (s) =>
        {
            s.Incoming.VoreOddsMult *= .8f;
            s.Incoming.ChanceToEscape *= .4f;
        }),
        [TraitType.HealingBlood] = new Booster("This unit heals 2 hp per turn (and fully outside of battles), but is also worth a lot more healing to its predator", (s) =>
        {
            s.HealthRegen += 2;
            s.Outgoing.Nutrition *= 3f;
        }),


        [TraitType.LightningSpeed] = new Booster("Unit moves very fast, and can perform actions many times per turn. \n(Cheat Trait)", (s) =>
        {
            s.SpeedBonus += 10;
            s.MeleeAttacks += 5;
            s.RangedAttacks += 5;
            s.VoreAttacks += 5;
            s.SpellAttacks += 5;
        }),
        [TraitType.DivineBloodline] = new Booster("Descendent from supernatural beings, this individual is a force of nature. \n(Cheat Trait) - gives damage reduction, extra attacks, stat bonuses on level up)", (s) =>
        {
            s.Incoming.DigestionRate *= 0.25f;
            s.DigestionImmunityTurns += 5;
            s.Incoming.RangedDamage *= .75f;
            s.Incoming.MeleeDamage *= 0.75f;
            s.PassiveHeal *= 10;
            s.VoreAttacks += 2;
            s.MeleeAttacks += 2;
            s.RangedAttacks += 2;
            s.SpellAttacks += 2;
            s.OnLevelUpBonusToAllStats += 3;
            s.OnLevelUpBonusToGiveToTwoRandomStats += 7;
            s.OnLevelUpAllowAnyStat = true;
        }),
        [TraitType.GeneEater] = new Booster("Capable of absorbing the best genes from devoured prey. Gains significant bonus experience from absorbing prey. \n(Cheat Trait)", (s) =>
        {
            s.ExpGainFromVore *= 5.0f;
            s.ExpGainFromAbsorption *= 50.0f;
        }),
        [TraitType.InstantDigestion] = new Booster("Instantly digests non-acid-immune prey. \n(Cheat Trait)", (s) => s.Outgoing.DigestionRate *= 1000000f),
        [TraitType.InstantAbsorption] = new Booster("Instantly absorbs dead prey. \n(Cheat Trait)", (s) => s.Outgoing.AbsorptionRate *= 1000000f),
        [TraitType.Inescapable] = new Booster("Prey can never escape. \n(Cheat Trait)", (s) => s.Outgoing.ChanceToEscape /= 1000f), //Handled seperately now.  
        [TraitType.Irresistable] = new Booster("Prey is always devoured. \n(Cheat Trait)", (s) => s.Outgoing.VoreOddsMult *= 1000000f),
        [TraitType.Titanic] = new Booster("Unit is practically a giant. (x3 size). \n(Cheat Trait)", (s) => s.Scale *= 3f),
        [TraitType.Colossal] = new Booster("Unit is far larger than normal (x2.5 size). \n(Cheat Trait)", (s) => s.Scale *= 2.5f),
        [TraitType.Huge] = new Booster("Unit is considerably larger than normal (x2 size). \n(Cheat Trait)", (s) => s.Scale *= 2f),
        [TraitType.Tiny] = new Booster("Unit is far smaller than normal. \n(Cheat Trait)", (s) => s.Scale /= 3.0f),
        [TraitType.AdaptiveTactics] = new Booster("Unit earns double the normal amount of experience from actions.\n(Cheat Trait)", (s) => s.ExpGain *= 2),
        [TraitType.SlowMetabolism] = new Booster("Unit digests and absorbs prey very slowly (effectively the slow digestion and slow absorbtion traits combined, but is also slower than those)", (s) =>
        {
            s.Outgoing.AbsorptionRate *= 0.25f;
            s.Outgoing.DigestionRate *= 0.25f;
        }),
        [TraitType.LightFrame] = new Booster("Unit can melee attack twice in a turn, though it loses this ability while it contains any prey.  Unit also takes 25% more damage from all sources", (s) =>
        {
            s.Incoming.MeleeDamage *= 1.25f;
            s.Incoming.RangedDamage *= 1.25f;
            s.Incoming.MagicDamage *= 1.25f;
            s.VirtualStrMult *= 1.7f;
        }),
        [TraitType.Featherweight] = new Booster("Unit moves slightly faster (+1 AP) and gets a melee/vore dodge bonus, but takes extra damage from melee.", (s) =>
        {
            s.SpeedBonus += 1;
            s.Incoming.MeleeShift += .75f;
            s.Incoming.VoreOddsMult *= 0.75f;
            s.Incoming.MeleeDamage *= 1.2f;
        }),
        [TraitType.PeakCondition] = new Booster("Unit is at the height of their physical condition (+50% all stats)", (s) => s.StatMult *= 1.5f),
        [TraitType.Fit] = new Booster("Unit is in better shape than the average unit (+20% all stats)", (s) => s.StatMult *= 1.2f),
        [TraitType.Illness] = new Booster("Unit is sick and is in poor shape (-20% all stats)", (s) => s.StatMult *= 0.8f),
        [TraitType.Diseased] = new Booster("Unit is in terrible shape, and is only a shadow of its former self (-50% all stats)", (s) => s.StatMult *= 0.5f),
        [TraitType.StretchyInsides] = new Booster("This unit can hold twice as many units as normal.", (s) => s.CapacityMult *= 2),
        [TraitType.UnlimitedCapacity] = new Booster("This unit effectively has no limit on consumed prey.\n(Cheat Trait)", (s) => s.CapacityMult *= 1000),
        [TraitType.Clumsy] = new Booster("This unit moves slower, and has slightly reduced melee accuracy and damage.", (s) =>
        {
            s.SpeedMultiplier *= 0.85f;
            s.Outgoing.MeleeDamage *= .9f;
            s.Outgoing.MeleeShift += .3f;
            s.VirtualDexMult *= 1.1f;
        }),
        [TraitType.Honeymaker] = new Booster("When feeding another unit, more exprience is given and prey is absorbed slower.", (s) => { s.Incoming.RangedDamage *= 1.0f; }),
        [TraitType.WetNurse] = new Booster("When feeding another unit, more health is healed and less AP is consumed.", (s) => { s.Incoming.RangedDamage *= 1.0f; }),
        [TraitType.HighlyAbsorbable] = new Booster("This unit's body gets absorbed more readily. (+50% absorb speed)", (s) => { s.Incoming.AbsorptionRate *= 1.5f; }),
        [TraitType.IdealSustenance] = new Booster("This unit's body gets absorbed efficiently and very fast.(+50% nutrition, +100% absorb speed)", (s) =>
        {
            s.Outgoing.Nutrition *= 1.5f;
            s.Incoming.AbsorptionRate = 2.0f;
        }),
        [TraitType.DoubleGrowth] = new Booster("Unit grows twice as much from absorbing prey (Requires the Growth trait)", (s) => { s.Incoming.GrowthRate *= 2f; }),
        [TraitType.IncreasedGrowth] = new Booster("Unit grows 50% more from absorbing prey (Requires the Growth trait)", (s) => { s.Incoming.GrowthRate *= 1.5f; }),
        [TraitType.MinorGrowth] = new Booster("Unit grows 50% less from absorbing prey (Requires the Growth trait)", (s) => { s.Incoming.GrowthRate *= 0.5f; }),
        [TraitType.SlowedGrowth] = new Booster("Unit grows 20% less from absorbing prey (Requires the Growth trait)", (s) => { s.Incoming.GrowthRate *= 0.8f; }),
        [TraitType.FleetingGrowth] = new Booster("Unit loses its gained growth more quickly (Requires the Growth trait)", (s) => { s.GrowthDecayRate *= 2f; }),
        [TraitType.PersistentGrowth] = new Booster("Unit loses its gained growth less quickly (Requires the Growth trait)", (s) => { s.GrowthDecayRate *= 0.5f; }),
        [TraitType.ProteinRich] = new Booster("Absorbing this unit yields more (x2) healing and (with the growth trait) more growth than usual (+80%)", (s) =>
        {
            s.Outgoing.GrowthRate *= 1.5f;
            s.Outgoing.Nutrition *= 2f;
        }),
        [TraitType.EfficientGuts] = new Booster("Unit receives 50% more healing from absorbing prey", (s) => { s.Incoming.Nutrition *= 1.5f; }),
        [TraitType.WastefulProcessing] = new Booster("Unit can't get as much healing out of prey, but they are done with it quicker. (+50% absorb speed, -50% nutrition)", (s) =>
        {
            s.Incoming.Nutrition *= 0.5f;
            s.Outgoing.AbsorptionRate *= 1.5f;
        }),
        [TraitType.TightNethers] = new Booster("This unit can only take much smaller units into their nethers, but their prey will not enlarge while inside their genitals.", (s) => { s.Incoming.RangedDamage *= 1.0f; }),
        [TraitType.NightEye] = new Booster("Increases night time vision range by +1 in Tactical battles and by +1 in stratigic if half of the units in an army have this trait.", (s) => { s.SightRangeBoost += 1; }),
        [TraitType.KeenEye] = new Booster("Unit has the chance to deal increase damage when attacking.", (s) => { s.Outgoing.CritRateShift += 0.1f; }),
        [TraitType.AccuteDodge] = new Booster("Unit has the chance to minimise recieved damage when being attacked. (Excludes spells and vore damage).", (s) => { s.Outgoing.GrazeRateShift += 0.1f; }),
        [TraitType.ViralDigestion] = new ViralDigestion(),
        [TraitType.AwkwardShape] = new Booster("This unit has a very strange body type, making them harder to swallow and providing less sustenance as prey.", (s) =>
        {
            s.Incoming.VoreOddsMult *= 0.75f;
            s.Outgoing.Nutrition *= 0.25f;
        }),
    };
}

internal class Frenzy : Trait, IStatBoost
{
    public Frenzy() => Description = "Unit's stats increase for every kill it gets.\nResets at the end of the battle";

    public int StatBoost(Unit unit, Stat stat)
    {
        int ret = 0;
        if (unit.EnemiesKilledThisBattle > 0)
        {
            ret = (int)(unit.GetStatBase(stat) * 0.1f * unit.EnemiesKilledThisBattle);
        }

        return ret;
    }
}

internal class ThrillSeeker : Trait, IStatBoost
{
    public ThrillSeeker() => Description = "Stats increase as health falls";

    public int StatBoost(Unit unit, Stat stat)
    {
        int ret = 0;
        ret = (int)(unit.GetStatBase(stat) * (0.11f - 0.11f * unit.GetHealthPctWithoutUpdating()));
        if (ret < 0) return 0;
        return ret;
    }
}

internal class PackStat : Trait, IStatBoost
{
    private Stat _usedStat;

    public PackStat(Stat usedStat)
    {
        Description = $"Boosts {usedStat} if friendly units are nearby.\nEffect is capped at 2 friendly units";
        this._usedStat = usedStat;
    }

    public int StatBoost(Unit unit, Stat stat)
    {
        int ret = 0;
        if (stat == _usedStat)
        {
            ret = Math.Min(unit.NearbyFriendlies, 2) * (1 + unit.Level) / 2;
        }

        return ret;
    }
}

internal class PackTactics : Trait, IStatBoost
{
    public PackTactics()
    {
        Description = $"A combination of all the other pack stat traits";
    }

    public int StatBoost(Unit unit, Stat stat)
    {
        int ret = 0;
        if (stat != Stat.Endurance && stat != Stat.Leadership)
        {
            ret = Math.Min(unit.NearbyFriendlies, 2) * (1 + unit.Level) / 2;
        }

        return ret;
    }
}

internal class Paralyzer : Trait, IAttackStatusEffect
{
    public Paralyzer() => Description = "Unit has a small chance to stun an enemy for 1 turn when it attacks";

    public void ApplyStatusEffect(ActorUnit actor, ActorUnit target, bool ranged, int damage)
    {
        if (State.Rand.Next(10 + 3 * target.TimesParalyzed) == 0) target.Paralyzed = true;
    }
}

internal class BoggingSlime : Trait, IAttackStatusEffect
{
    public BoggingSlime() => Description = "Attacks from this unit have a chance to slime enemy units for 1 turn.\n(effect lowers mp to 50 % of max)\nDoes not stack.";

    public void ApplyStatusEffect(ActorUnit actor, ActorUnit target, bool ranged, int damage)
    {
        if (State.Rand.Next(10) < 4) target.Slimed = true;
    }
}

internal class Vampirism : Trait, IAttackStatusEffect
{
    public Vampirism() => Description = "Melee Attacks from this unit will heal the attacker by a small amount (12% of damage done, to a minimum of 1).";

    public void ApplyStatusEffect(ActorUnit actor, ActorUnit target, bool ranged, int damage)
    {
        if (ranged == false)
        {
            int heal = Math.Max((int)(damage * .12f), 1);
            actor.Unit.Heal(heal);
        }
    }
}

internal class Stinger : Trait, IAttackStatusEffect
{
    public Stinger() => Description = "Melee attacks from this unit have a chance to poison enemy units. Poison deals damage over time but doesn't kill enemy units.\nDoes not stack with itself or the poison spell.";

    public void ApplyStatusEffect(ActorUnit actor, ActorUnit target, bool ranged, int damage)
    {
        if (ranged) return;
        if (State.Rand.Next(10) < 2)
        {
            damage = Math.Max(damage / 2, 3);
            target.Unit.ApplyStatusEffect(StatusEffectType.Poisoned, damage, 3);
        }
    }
}

internal class DefensiveStance : Trait, IVoreDefenseOdds, IPhysicalDefenseOdds
{
    public DefensiveStance() => Description = "Unit gets a bonus to defense if it has at least 1 mp remaining";

    public void PhysicalDefense(ActorUnit defender, ref float defMult) => defMult += defender.Movement > 0 ? 0.8f : 0;

    public void VoreDefense(ActorUnit defender, ref float voreMult) => voreMult *= defender.Movement > 0 ? 1.333f : 1;
}


internal class Ravenous : Trait, IVoreAttackOdds
{
    public Ravenous() => Description = "Unit gets a bonus to vore chance if no units are in its stomach";

    public void VoreAttack(ActorUnit attacker, ref float voreMult)
    {
        if (attacker.PredatorComponent.Fullness == 0)
        {
            voreMult *= 1.75f;
        }
    }
}

internal class UnpleasantDigestion : VoreTrait
{
    public UnpleasantDigestion()
    {
        Description = "While digesting, Prey deals damage to predator";
    }

    public override bool IsPredTrait => false;

    public override bool OnDigestion(Prey preyUnit, ActorUnit predUnit, PreyLocation location)
    {
        predUnit.Damage(1);
        return true;
    }
}

internal class Whispers : VoreTrait, IProvidesSingleSpell
{
    public Whispers()
    {
        Description = "Predator has a chance to be charmed and afflicted by Prey's curse each round";
    }

    public override bool IsPredTrait => false;

    public List<SpellType> GetSingleSpells(Unit unit) => new List<SpellType> { SpellList.Whispers.SpellType };

    public override bool OnDigestion(Prey preyUnit, ActorUnit predUnit, PreyLocation location)
    {
        if (!Equals(predUnit.Unit.FixedSide, preyUnit.Unit.FixedSide)) preyUnit.Actor.CastStatusSpell(SpellList.Whispers, predUnit);
        return true;
    }
}

internal class Parasite : VoreTraitBooster
{
    public Parasite()
    {
        Description = "A parasite prey will give the host CreateSpawn and set infection after digestion, host Takes minor damage on prey absorption and major damage when creating spawn";
        Boost = (s) => s.Incoming.ChanceToEscape *= 0;
    }

    public override bool IsPredTrait => false;

    public override bool OnFinishDigestion(Prey preyUnit, ActorUnit predUnit, PreyLocation location)
    {
        predUnit.Infected = true;
        predUnit.InfectedRace = preyUnit.Unit.DetermineSpawnRace();
        if (!preyUnit.Unit.HasSharedTrait(TraitType.Parasite)) predUnit.InfectedRace = preyUnit.Unit.HiddenUnit.DetermineSpawnRace();
        predUnit.InfectedSide = preyUnit.Unit.FixedSide;
        return true;
    }
}

internal class Metamorphosis : VoreTrait, INoAutoEscape
{
    public Metamorphosis()
    {
        Description = "Unit changes Race upon digestion";
    }

    public override bool IsPredTrait => false;

    public override int ProcessingPriority => 50;

    public bool CanEscape(Prey preyUnit, ActorUnit predUnit)
    {
        return false;
    }

    public override bool OnFinishDigestion(Prey preyUnit, ActorUnit predUnit, PreyLocation location)
    {
        Race conversionRace = preyUnit.Unit.HiddenUnit.DetermineSpawnRace();
        preyUnit.Unit.Health = preyUnit.Unit.MaxHealth;
        preyUnit.Unit.GiveExp(predUnit.Unit.Experience / 2);
        if (!Equals(preyUnit.Unit.Race, conversionRace))
        {
            preyUnit.ChangeRace(conversionRace);
        }

        preyUnit.Unit.RemoveTrait(TraitType.Metamorphosis); //no metamorphosis loops
        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"{preyUnit.Unit.Name} changed form within {predUnit.Unit.Name}'s body.");
        predUnit.PredatorComponent.UpdateFullness();
        return false;
    }
}

internal class MetamorphicConversion : Metamorphosis
{
    public MetamorphicConversion()
    {
        Description = "Unit changes Race and side upon digestion";
    }

    public override bool IsPredTrait => false;

    public override int ProcessingPriority => 49;

    public override bool OnFinishDigestion(Prey preyUnit, ActorUnit predUnit, PreyLocation location)
    {
        preyUnit.ChangeSide(predUnit.Unit.FixedSide);
        base.OnFinishDigestion(preyUnit, predUnit, location);
        preyUnit.Unit.RemoveTrait(TraitType.MetamorphicConversion);
        return false;
    }
}

internal class Possession : VoreTraitBooster, INoAutoEscape
{
    public Possession()
    {
        Description = "If a possession unit is eaten, the pred will be possessed as a hidden status. " +
                      "Once possessed prey's stat total plus the Preds corruption is equal to that of the pred, they are under control of the side of the last-eaten possessed.";
        Boost = (s) => s.Incoming.ChanceToEscape *= 0;
    }

    public override bool IsPredTrait => false;

    public override bool OnRemove(Prey preyUnit, ActorUnit predUnit, PreyLocation location)
    {
        if (!preyUnit.Unit.IsDead)
        {
            predUnit.RemovePossession(preyUnit.Actor);
        }

        return true;
    }

    public override bool OnDigestionKill(Prey preyUnit, ActorUnit predUnit, PreyLocation location)
    {
        predUnit.RemovePossession(preyUnit.Actor);
        return true;
    }

    public override bool OnDigestion(Prey preyUnit, ActorUnit predUnit, PreyLocation location)
    {
        predUnit.CheckPossession(preyUnit.Actor);
        return true;
    }

    public override bool OnSwallow(Prey preyUnit, ActorUnit predUnit, PreyLocation location)
    {
        if (!preyUnit.Unit.IsDead)
        {
            predUnit.AddPossession(preyUnit.Actor);
        }

        return true;
    }

    public bool CanEscape(Prey preyUnit, ActorUnit predUnit)
    {
        return !predUnit.CheckPossession(preyUnit.Actor);
    }
}

internal class Changeling : VoreTrait, IProvidesMultiSpell
{
    public Changeling()
    {
        Description = "While absorbing a prey, Becomes that prey's Race";
    }

    public override bool IsPredTrait => true;

    public override int ProcessingPriority => 51;

    public virtual bool TargetIsDead => true;

    public override bool OnRemove(Prey preyUnit, ActorUnit predUnit, PreyLocation location)
    {
        if (preyUnit == predUnit.PredatorComponent.Template)
        {
            predUnit.RevertRace();
            predUnit.PredatorComponent.ChangeRaceAuto(TargetIsDead, false);
            return false;
        }

        return true;
    }

    public override bool OnDigestionKill(Prey preyUnit, ActorUnit predUnit, PreyLocation location)
    {
        predUnit.PredatorComponent.TryChangeRace(preyUnit);
        return true;
    }

    public List<SpellType> GetMultiSpells(Unit unit) => new List<SpellType> { SpellList.AssumeForm.SpellType, SpellList.RevertForm.SpellType };
}

internal class GreaterChangeling : Changeling
{
    public GreaterChangeling()
    {
        Description = "While digesting a prey, Becomes that prey's Race";
    }

    public override bool IsPredTrait => true;

    public override int ProcessingPriority => 50;

    public override bool TargetIsDead => false;

    public override bool OnSwallow(Prey preyUnit, ActorUnit predUnit, PreyLocation location)
    {
        return !predUnit.PredatorComponent.TryChangeRace(preyUnit);
    }
}

internal class Symbiote : VoreTrait
{
    public Symbiote()
    {
        Description = "Shares generic traits with pred";
    }

    public override bool IsPredTrait => false;

    public override int ProcessingPriority => 100;

    public override bool OnSwallow(Prey preyUnit, ActorUnit predUnit, PreyLocation location)
    {
        foreach (TraitType trait in preyUnit.Unit.GetTraits)
        {
            if (trait != TraitType.Prey && trait != TraitType.Small) predUnit.PredatorComponent.ShareTrait(trait, preyUnit);
        }

        predUnit.Unit.ReloadTraits();
        predUnit.Unit.InitializeTraits();
        predUnit.ReloadSpellTraits();
        return true;
    }

    public override bool OnRemove(Prey preyUnit, ActorUnit predUnit, PreyLocation location)
    {
        foreach (TraitType trait in preyUnit.SharedTraits) predUnit.Unit.RemoveSharedTrait(trait);
        predUnit.PredatorComponent.RefreshSharedTraits();
        predUnit.Unit.InitializeTraits();
        predUnit.ReloadSpellTraits();
        return true;
    }
}

internal class TraitBorrower : VoreTrait
{
    public TraitBorrower()
    {
        Description = "borrows generic traits from prey";
    }

    public override bool IsPredTrait => true;

    public override int ProcessingPriority => 100;

    public override bool OnSwallow(Prey preyUnit, ActorUnit predUnit, PreyLocation location)
    {
        foreach (TraitType trait in preyUnit.Unit.GetTraits)
        {
            if (trait != TraitType.Prey && trait != TraitType.Small) predUnit.PredatorComponent.ShareTrait(trait, preyUnit);
        }

        predUnit.Unit.ReloadTraits();
        predUnit.Unit.InitializeTraits();
        predUnit.ReloadSpellTraits();
        return true;
    }

    public override bool OnRemove(Prey preyUnit, ActorUnit predUnit, PreyLocation location)
    {
        foreach (TraitType trait in preyUnit.SharedTraits) predUnit.Unit.RemoveSharedTrait(trait);
        predUnit.PredatorComponent.RefreshSharedTraits();
        predUnit.Unit.InitializeTraits();
        predUnit.ReloadSpellTraits();
        return true;
    }
}

internal class CreateSpawn : VoreTrait
{
    public CreateSpawn()
    {
        Description = "creates a spawn unit on prey Absorption";
    }

    public override bool IsPredTrait => true;

    public override int ProcessingPriority => 0;

    public override bool OnFinishAbsorption(Prey preyUnit, ActorUnit predUnit, PreyLocation location)
    {
        Race spawnRace = predUnit.Unit.DetermineSpawnRace();
        if (!predUnit.Unit.HasSharedTrait(TraitType.CreateSpawn)) spawnRace = predUnit.Unit.HiddenUnit.DetermineSpawnRace();
        // use source race IF changeling already had this ability before transforming
        predUnit.PredatorComponent.CreateSpawn(spawnRace, predUnit.Unit.Side, predUnit.Unit.Experience / 2);
        return true;
    }
}

internal class SpiritPossession : Possession
{
    public SpiritPossession()
    {
        Description = "Units soul continues to possess pred after death";
        Boost = (s) => s.Incoming.ChanceToEscape *= 0;
    }

    public override bool IsPredTrait => false;

    public override bool OnDigestionKill(Prey preyUnit, ActorUnit predUnit, PreyLocation location)
    {
        var possessed = predUnit.CheckPossession(preyUnit.Actor);
        predUnit.RemovePossession(preyUnit.Actor);
        if (possessed)
        {
            if (!Equals(predUnit.Unit.Side, preyUnit.Unit.Side)) State.GameManager.TacticalMode.SwitchAlignment(predUnit);
            //TODO: This game needs some form of true fusion mechanic. 
            //this is an approximation of fusion with the result taking the appearance of the pred, and the side of the prey
            if (Equals(predUnit.Unit.Side, preyUnit.Unit.Side)) predUnit.Unit.FixedSide = Side.TrueNoneSide;
            predUnit.Unit.Name = preyUnit.Unit.Name;
            predUnit.Unit.GiveExp(preyUnit.Unit.Experience);
        }

        return true;
    }
}

internal class ForcedMetamorphosis : VoreTraitBooster, INoAutoEscape
{
    public ForcedMetamorphosis()
    {
        Description = "Pred Unit will gain the metamorphosis trait on Prey death";
        Boost = (s) => s.Incoming.ChanceToEscape *= 0;
    }

    public override int ProcessingPriority => 10;

    public override bool IsPredTrait => false;

    public bool CanEscape(Prey preyUnit, ActorUnit predUnit)
    {
        return false;
    }

    public override bool OnDigestionKill(Prey preyUnit, ActorUnit predUnit, PreyLocation location)
    {
        //TODO: Make this a status effect instead
        if (Equals(predUnit.Unit.FixedSide, preyUnit.Unit.FixedSide) && Equals(predUnit.Unit.FixedSide, predUnit.Unit.Side))
            predUnit.Unit.AddPermanentTrait(TraitType.Metamorphosis);
        else
            predUnit.Unit.AddPermanentTrait(TraitType.MetamorphicConversion);
        predUnit.Unit.SpawnRace = preyUnit.Unit.HiddenUnit.DetermineSpawnRace();
        return true;
    }
}

internal class ViralDigestion : VoreTrait
{
    public ViralDigestion()
    {
        Description = "This unit has powerful viruses within them, which cause any prey to take additional damage for a few turns even after escaping.";
    }

    public override bool IsPredTrait => true;

    public override bool OnRemove(Prey preyUnit, ActorUnit predUnit, PreyLocation location)
    {
        preyUnit.Unit.ApplyStatusEffect(StatusEffectType.Virus, 3, 3);
        return true;
    }
}