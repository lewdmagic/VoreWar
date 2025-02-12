﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public struct Range
{
    internal int Min;
    internal int Max;

    public Range(int min, int max)
    {
        Min = min;
        Max = max;
    }

    public Range(int max)
    {
        Min = 0;
        Max = max;
    }
}

public enum AbilityTargets
{
    Enemy,
    Ally,
    Self,
    SurrenderedAlly,
    Tile,
}

internal static class SpellList
{
    internal static readonly DamageSpell Fireball;
    internal static readonly DamageSpell PowerBolt;
    internal static readonly DamageSpell LightningBolt;
    internal static readonly StatusSpell Shield;
    internal static readonly StatusSpell Mending;
    internal static readonly StatusSpell Speed;
    internal static readonly StatusSpell Valor;
    internal static readonly StatusSpell Predation;
    internal static readonly StatusSpell Charm;

    internal static readonly DamageSpell IceBlast;

    internal static readonly DamageSpell Pyre;

    //static internal readonly Spell Warp;
    //static internal readonly DamageSpell MagicWall;
    internal static readonly StatusSpell Poison;

    //Quicksand
    internal static readonly StatusSpell PreysCurse;
    internal static readonly StatusSpell Enlarge;
    internal static readonly Spell Maw;
    internal static readonly Spell Summon;

    internal static readonly StatusSpell Diminishment;

    //Raze
    internal static readonly Spell GateMaw;
    internal static readonly Spell Reanimate;
    internal static readonly Spell Resurrection;

    internal static readonly Spell AmplifyMagic;
    internal static readonly Spell Evocation;
    internal static readonly DamageSpell ManaFlux;
    internal static readonly DamageSpell UnstableMana;
    internal static readonly DamageSpell ManaExpolsion;

    internal static readonly StatusSpell AlraunePuff;
    internal static readonly StatusSpell Web;
    internal static readonly StatusSpell GlueBomb;
    internal static readonly StatusSpell Petrify;
    internal static readonly StatusSpell HypnoGas;
    internal static readonly StatusSpell Whispers;
    internal static readonly Spell ForceFeed;
    internal static readonly Spell AssumeForm;
    internal static readonly Spell RevertForm;
    internal static readonly Spell Bind;

    internal static readonly DamageSpell ViperPoisonDamage;
    internal static readonly StatusSpell ViperPoisonStatus;
    internal static readonly StatusSpell ViralInfection;
    internal static readonly StatusSpell DivinitysEmbrace;

    internal static Dictionary<SpellType, Spell> SpellDict;


    static SpellList()
    {
        SpellDict = new Dictionary<SpellType, Spell>();
        Fireball = new DamageSpell()
        {
            Name = "Fireball",
            Id = "fireball",
            SpellType = SpellType.Fireball,
            Description = "Deals damage in an area",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy, AbilityTargets.Tile },
            Range = new Range(6),
            AreaOfEffect = 1,
            Tier = 1,
            Resistable = true,
            DamageType = DamageType.Fire,
            Damage = (a, t) => 5 + a.Unit.GetStat(Stat.Mind) / 10,
            OnExecute = (a, t) =>
            {
                a.CastOffensiveSpell(Fireball, t);
                TacticalGraphicalEffects.CreateFireBall(a.Position, t.Position, t);
            },
            OnExecuteTile = (a, l) =>
            {
                a.CastOffensiveSpell(Fireball, null, l);
                TacticalGraphicalEffects.CreateFireBall(a.Position, l, null);
            },
        };
        SpellDict[SpellType.Fireball] = Fireball;

        PowerBolt = new DamageSpell()
        {
            Name = "Power Bolt",
            Id = "power-bolt",
            SpellType = SpellType.PowerBolt,
            Description = "Deals damage to a single target, not resistable",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy },
            Range = new Range(6),
            AreaOfEffect = 0,
            Tier = 1,
            Resistable = false,
            Damage = (a, t) => 1 + a.Unit.GetStat(Stat.Mind) / 5,
            OnExecute = (a, t) =>
            {
                a.CastOffensiveSpell(PowerBolt, t);
                TacticalGraphicalEffects.CreateGenericMagic(a.Position, t.Position, t);
            },
        };
        SpellDict[SpellType.PowerBolt] = PowerBolt;

        LightningBolt = new DamageSpell()
        {
            Name = "Lightning Bolt",
            Id = "lightning-bolt",
            SpellType = SpellType.LightningBolt,
            Description = "Deals damage to a single target, long range",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy },
            Range = new Range(40),
            AreaOfEffect = 0,
            Tier = 1,
            Resistable = true,
            Damage = (a, t) => 5 + a.Unit.GetStat(Stat.Mind) / 10,
            OnExecute = (a, t) =>
            {
                a.CastOffensiveSpell(LightningBolt, t);
                TacticalGraphicalEffects.CreateGenericMagic(a.Position, t.Position, t);
            },
        };
        SpellDict[SpellType.LightningBolt] = LightningBolt;

        Shield = new StatusSpell()
        {
            Name = "Shield",
            Id = "shield",
            SpellType = SpellType.Shield,
            Description = "Causes target to take reduced damage",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Ally },
            Range = new Range(6),
            Duration = (a, t) => 2 + a.Unit.GetStat(Stat.Mind) / 10,
            Effect = (a, t) => .25f,
            Type = StatusEffectType.Shielded,
            Tier = 1,
            Resistable = false,
            OnExecute = (a, t) =>
            {
                a.CastStatusSpell(Shield, t);
                TacticalGraphicalEffects.CreateGenericMagic(a.Position, t.Position, t, TacticalGraphicalEffects.SpellEffectIcon.Buff);
            },
        };
        SpellDict[SpellType.Shield] = Shield;

        Mending = new StatusSpell()
        {
            Name = "Mending",
            Id = "mending",
            SpellType = SpellType.Mending,
            Description = "Heals target over time",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Ally },
            Range = new Range(4),
            Duration = (a, t) => 1 + a.Unit.GetStat(Stat.Mind) / 10,
            Effect = (a, t) => a.Unit.GetStat(Stat.Mind),
            Type = StatusEffectType.Mending,
            Tier = 1,
            Resistable = false,
            OnExecute = (a, t) =>
            {
                a.CastStatusSpell(Mending, t);
                TacticalGraphicalEffects.CreateGenericMagic(a.Position, t.Position, t, TacticalGraphicalEffects.SpellEffectIcon.Heal);
            },
        };
        SpellDict[SpellType.Mending] = Mending;

        Speed = new StatusSpell()
        {
            Name = "Speed",
            Id = "speed",
            SpellType = SpellType.Speed,
            Description = "Makes target faster",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Ally },
            Range = new Range(4),
            Duration = (a, t) => 3 + a.Unit.GetStat(Stat.Mind) / 10,
            Effect = (a, t) => .3f + a.Unit.GetStat(Stat.Mind) / 1000,
            Type = StatusEffectType.Fast,
            Tier = 1,
            Resistable = false,
            OnExecute = (a, t) =>
            {
                a.CastStatusSpell(Speed, t);
                TacticalGraphicalEffects.CreateGenericMagic(a.Position, t.Position, t, TacticalGraphicalEffects.SpellEffectIcon.Buff);
            },
        };
        SpellDict[SpellType.Speed] = Speed;

        Valor = new StatusSpell()
        {
            Name = "Valor",
            Id = "valor",
            SpellType = SpellType.Valor,
            Description = "Target does more damage",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Ally },
            Range = new Range(6),
            Duration = (a, t) => 2 + a.Unit.GetStat(Stat.Mind) / 10,
            Effect = (a, t) => .25f,
            Type = StatusEffectType.Valor,
            Tier = 1,
            Resistable = false,
            OnExecute = (a, t) =>
            {
                a.CastStatusSpell(Valor, t);
                TacticalGraphicalEffects.CreateGenericMagic(a.Position, t.Position, t, TacticalGraphicalEffects.SpellEffectIcon.Buff);
            },
        };
        SpellDict[SpellType.Valor] = Valor;

        Predation = new StatusSpell()
        {
            Name = "Predation",
            Id = "predation",
            SpellType = SpellType.Predation,
            Description = "Target is better at vore",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Ally },
            Range = new Range(6),
            Duration = (a, t) => 2 + a.Unit.GetStat(Stat.Mind) / 10,
            Effect = (a, t) => a.Unit.GetStat(Stat.Mind) / 4,
            Type = StatusEffectType.Predation,
            Tier = 1,
            Resistable = false,
            OnExecute = (a, t) =>
            {
                a.CastStatusSpell(Predation, t);
                TacticalGraphicalEffects.CreateGenericMagic(a.Position, t.Position, t, TacticalGraphicalEffects.SpellEffectIcon.Buff);
            },
        };
        SpellDict[SpellType.Predation] = Predation;

        IceBlast = new DamageSpell()
        {
            Name = "Ice Blast",
            Id = "ice-blast",
            SpellType = SpellType.IceBlast,
            Description = "Area of effect cold damage and lays down ice",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy, AbilityTargets.Tile },
            Range = new Range(6),
            AreaOfEffect = 1,
            Tier = 2,
            Resistable = true,
            Damage = (a, t) => 5 + a.Unit.GetStat(Stat.Mind) / 10,
            OnExecute = (a, t) =>
            {
                a.CastOffensiveSpell(IceBlast, t);
                TacticalUtilities.CreateEffect(t.Position, TileEffectType.IcePatch, 1, 2, 10 + a.Unit.GetStat(Stat.Mind) / 5);
                TacticalGraphicalEffects.CreateIceBlast(t.Position);
            },
            OnExecuteTile = (a, l) =>
            {
                a.CastOffensiveSpell(IceBlast, null, l);
                TacticalUtilities.CreateEffect(l, TileEffectType.IcePatch, 1, 2, 10 + a.Unit.GetStat(Stat.Mind) / 5);
                TacticalGraphicalEffects.CreateIceBlast(l);
            },
        };
        SpellDict[SpellType.IceBlast] = IceBlast;

        Pyre = new DamageSpell()
        {
            Name = "Pyre",
            Id = "pyre",
            SpellType = SpellType.Pyre,
            Description = "Deals damage in an area, sets the ground on fire for a few turns",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy, AbilityTargets.Tile },
            Range = new Range(6),
            AreaOfEffect = 1,
            Tier = 2,
            Resistable = true,
            DamageType = DamageType.Fire,
            Damage = (a, t) => 7 + a.Unit.GetStat(Stat.Mind) / 10,
            OnExecute = (a, t) =>
            {
                a.CastOffensiveSpell(Pyre, t);
                TacticalUtilities.CreateEffect(t.Position, TileEffectType.Fire, 1, 1 + a.Unit.GetStat(Stat.Mind) / 30, 4);
            },
            OnExecuteTile = (a, l) =>
            {
                a.CastOffensiveSpell(Pyre, null, l);
                TacticalUtilities.CreateEffect(l, TileEffectType.Fire, 1, 1 + a.Unit.GetStat(Stat.Mind) / 30, 4);
            },
        };
        SpellDict[SpellType.Pyre] = Pyre;

        //Warp = new Spell() //Implemented this and forgot it was supposed to be target and then location, only the caster makes it highly situational
        //{
        //    Name = "Warp",
        //    SpellType = SpellTypes.Warp,
        //    Description = "Teleports the Unit to the target tile",
        //    AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Tile },
        //    Range = new Range(16),
        //    Tier = 2,
        //    Resistable = false,
        //    OnExecuteTile = (a, loc) =>
        //    {
        //        if (TacticalUtilities.OpenTile(loc, null) && a.CastSpell(Warp, null))
        //        {
        //            a.Position.x = loc.x;
        //            a.Position.y = loc.y;
        //            TacticalUtilities.UpdateActorLocations();
        //        }               
        //    },
        //};
        //SpellDict[SpellTypes.Warp] = Warp;

        Poison = new StatusSpell()
        {
            Name = "Poison",
            Id = "poison",
            SpellType = SpellType.Poison,
            Description = "Deals damage over time, can not kill targets",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy },
            Range = new Range(8),
            Duration = (a, t) => 4 + a.Unit.GetStat(Stat.Mind) / 5,
            Effect = (a, t) => 1 + a.Unit.GetStat(Stat.Mind) / 20,
            Type = StatusEffectType.Poisoned,
            Tier = 2,
            Resistable = true,
            OnExecute = (a, t) =>
            {
                if (a.CastStatusSpell(Poison, t)) TacticalGraphicalEffects.CreateGenericMagic(a.Position, t.Position, t, TacticalGraphicalEffects.SpellEffectIcon.Poison);
            },
        };
        SpellDict[SpellType.Poison] = Poison;

        PreysCurse = new StatusSpell()
        {
            Name = "Prey's Curse",
            Id = "preys-curse",
            SpellType = SpellType.PreysCurse,
            Description = "Makes target into a semi-willing prey, increasing vore chance on them (and changing some flavor text)",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy },
            Range = new Range(6),
            Duration = (a, t) => 2 + a.Unit.GetStat(Stat.Mind) / 10,
            Effect = (a, t) => 0,
            Type = StatusEffectType.WillingPrey,
            Tier = 3,
            Resistable = true,
            OnExecute = (a, t) =>
            {
                if (a.CastStatusSpell(PreysCurse, t)) TacticalGraphicalEffects.CreateGenericMagic(a.Position, t.Position, t, TacticalGraphicalEffects.SpellEffectIcon.Debuff);
            },
        };
        SpellDict[SpellType.PreysCurse] = PreysCurse;

        Maw = new Spell()
        {
            Name = "Maw",
            Id = "maw",
            SpellType = SpellType.Maw,
            Description = "Attempts to teleport the target into caster's belly, adding caster's Power to Voracity.",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy },
            Range = new Range(4),
            Tier = 3,
            Resistable = true,
            OnExecute = (a, t) => a.CastMawWithLocation(Maw, t),
        };
        SpellDict[SpellType.Maw] = Maw;

        Charm = new StatusSpell()
        {
            Name = "Charm",
            Id = "charm",
            SpellType = SpellType.Charm,
            Description = "Has a chance to temporarily make an enemy fight for the caster.",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy },
            Range = new Range(6),
            Duration = (a, t) => 2 + a.Unit.GetStat(Stat.Mind) / 10,
            Effect = (a, t) => 0f, // the unit will act to the best of its knowledge
            EffectSide = (a, t) => a.Unit.GetApparentSide(t.Unit), // the unit will act to the best of its knowledge
            Type = StatusEffectType.Charmed,
            Tier = 3,
            Resistable = true,
            OnExecute = (a, t) =>
            {
                a.CastStatusSpell(Charm, t);
                TacticalGraphicalEffects.CreateHeartProjectile(a.Position, t.Position, t);
            },
        };
        SpellDict[SpellType.Charm] = Charm;

        Summon = new Spell()
        {
            Name = "Summon",
            Id = "summon",
            SpellType = SpellType.Summon,
            Description = "Summons a random monster at 50 % of caster’s experience (Available monsters depend on what monsters are available to hire as mercs, or set to spawn, so that monsters you aren't interested in don't spawn.)",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Tile },
            Range = new Range(4),
            Tier = 3,
            Resistable = false,
            OnExecuteTile = (a, loc) =>
            {
                if (TacticalUtilities.OpenTile(loc, null) && a.CastSpell(Summon, null))
                {
                    var AvailableRaces = new List<Race>();
                    foreach (Race race in RaceFuncs.RaceEnumerable())
                    {
                        if (RaceFuncs.IsMonster(race) && (Config.World.GetValue($"Merc {race}") || (Config.World.GetSpawnerWithoutGeneration(race)?.Enabled ?? false))) AvailableRaces.Add(race);
                    }

                    AvailableRaces.Remove(Race.Dragon);
                    if (AvailableRaces.Any() == false) AvailableRaces.Add(Race.Vagrant);
                    Race summonRace = AvailableRaces[State.Rand.Next(AvailableRaces.Count())];
                    Unit unit = new Unit(a.Unit.Side, summonRace, (int)(a.Unit.Experience * .50f), true, UnitType.Summon);
                    var actorCharm = a.Unit.GetStatusEffect(StatusEffectType.Charmed) ?? a.Unit.GetStatusEffect(StatusEffectType.Hypnotized);
                    if (actorCharm != null)
                    {
                        unit.ApplyStatusEffect(StatusEffectType.Charmed, actorCharm.Strength, actorCharm.Duration, actorCharm.Side);
                    }

                    StrategicUtilities.SpendLevelUps(unit);
                    State.GameManager.TacticalMode.AddUnitToBattle(unit, loc);
                    State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{a.Unit.Name}</b> has summoned the {InfoPanel.RaceSingular(unit)} <b>{unit.Name}</b>.");
                }
            },
        };
        SpellDict[SpellType.Summon] = Summon;


        Reanimate = new Spell()
        {
            Name = "Reanimate",
            Id = "Reanimate",
            SpellType = SpellType.Reanimate,
            Description = "Brings back any unit that died this battle as a summon under the caster's control",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Tile },
            Range = new Range(4),
            Tier = 3,
            Resistable = false,
            OnExecuteTile = (a, loc) =>
            {
                var target = TacticalUtilities.FindUnitToReanimate(a);
                if (target != null)
                {
                    if (TacticalUtilities.OpenTile(loc, null) && a.CastSpell(Reanimate, null))
                    {
                        if (State.GameManager.TacticalMode.IsPlayerInControl && State.GameManager.CurrentScene == State.GameManager.TacticalMode)
                        {
                            TacticalUtilities.CreateReanimationPanel(loc, a.Unit);
                        }
                        else
                        {
                            State.GameManager.SoundManager.PlaySpellCast(Summon, a);
                            TacticalUtilities.Reanimate(loc, target, a.Unit);
                        }
                    }
                }
            },
        };
        SpellDict[SpellType.Reanimate] = Reanimate;

        Enlarge = new StatusSpell()
        {
            Name = "Enlarge",
            Id = "enlarge",
            SpellType = SpellType.Enlarge,
            Description = "Makes target unit 20% bigger, increasing stats and increasing body size and stomach size",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Ally },
            Range = new Range(4),
            Tier = 3,
            Resistable = false,
            Duration = (a, t) => 2 + a.Unit.GetStat(Stat.Mind) / 15,
            Type = StatusEffectType.Enlarged,
            Effect = (a, t) => .2f,
            OnExecute = (a, t) =>
            {
                a.CastStatusSpell(Enlarge, t);
                TacticalGraphicalEffects.CreateGenericMagic(a.Position, t.Position, t, TacticalGraphicalEffects.SpellEffectIcon.Buff);
            },
        };
        SpellDict[SpellType.Enlarge] = Enlarge;


        Diminishment = new StatusSpell()
        {
            Name = "Diminishment",
            Id = "diminishment",
            SpellType = SpellType.Diminishment,
            Description = "Reduces target in size greatly, cutting all its statistics (including size) by 75%.",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy },
            Range = new Range(6),
            Duration = (a, t) => 1 + a.Unit.GetStat(Stat.Mind) / 20,
            Effect = (a, t) => .75f,
            Type = StatusEffectType.Diminished,
            Tier = 4,
            Resistable = true,
            ResistanceMult = 1.5f,
            OnExecute = (a, t) => a.CastStatusSpell(Diminishment, t),
        };
        SpellDict[SpellType.Diminishment] = Diminishment;

        GateMaw = new Spell()
        {
            Name = "Gate Maw",
            Id = "gate-maw",
            SpellType = SpellType.GateMaw,
            Description = "Attempts to teleport all targets in radius into caster's belly, adding caster's Power to Voracity. If caster doesn't have enough stomach space, targets are unaffected.",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy, AbilityTargets.Tile },
            Range = new Range(4),
            Tier = 4,
            AreaOfEffect = 1,
            Resistable = true,
            OnExecute = (a, t) => a.CastMawWithLocation(GateMaw, t),
            OnExecuteTile = (a, l) => a.CastMawWithLocation(GateMaw, null, l),
        };
        SpellDict[SpellType.GateMaw] = GateMaw;


        Resurrection = new Spell()
        {
            Name = "Resurrection",
            Id = "resurrection",
            SpellType = SpellType.Resurrection,
            Description = "Resurrects the highest exp unit that died during this battle",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Tile },
            Range = new Range(4),
            Tier = 4,
            Resistable = false,
            OnExecuteTile = (a, loc) =>
            {
                var target = TacticalUtilities.FindUnitToResurrect(a);
                if (target != null)
                {
                    if (TacticalUtilities.OpenTile(loc, null) && a.CastSpell(Resurrection, null))
                    {
                        if (State.GameManager.TacticalMode.IsPlayerInControl && State.GameManager.CurrentScene == State.GameManager.TacticalMode)
                        {
                            TacticalUtilities.CreateResurrectionPanel(loc, a.Unit.Side);
                        }
                        else
                        {
                            TacticalUtilities.Resurrect(loc, target);
                        }
                    }
                }
            },
        };
        SpellDict[SpellType.Resurrection] = Resurrection;

        ViperPoisonDamage = new DamageSpell()
        {
            Name = "Viper Spit",
            Id = "viperDamage",
            SpellType = SpellType.ViperDamage,
            Description = "(Designed as part of the Viper spit spell)",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy, AbilityTargets.Tile },
            Range = new Range(6),
            AreaOfEffect = 1,
            Tier = 0,
            Resistable = true,
            DamageType = DamageType.Poison,
            Damage = (a, t) => 5 + a.Unit.GetStat(Stat.Mind) / 10,
            OnExecute = (a, t) => { },
            OnExecuteTile = (a, l) => { },
        };
        SpellDict[SpellType.ViperDamage] = ViperPoisonDamage;

        ViperPoisonStatus = new StatusSpell()
        {
            Name = "Poison Spit",
            Id = "viper-poison",
            SpellType = SpellType.ViperPoison,
            Description = "Deals damage and applies a strong short lived poison in a 3x3 area",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy, AbilityTargets.Tile },
            Range = new Range(6),
            Duration = (a, t) => 4,
            Effect = (a, t) => 2 + a.Unit.GetStat(Stat.Mind) / 10,
            AreaOfEffect = 1,
            Type = StatusEffectType.Poisoned,
            Tier = 0,
            Resistable = true,
            ResistanceMult = 1f,
            OnExecute = (a, t) =>
            {
                a.CastStatusSpell(ViperPoisonStatus, t);
                a.Unit.RestoreMana(2);
                a.CastOffensiveSpell(ViperPoisonDamage, t);
                TacticalGraphicalEffects.CreatePoisonCloud(t.Position);
            },
            OnExecuteTile = (a, loc) =>
            {
                a.CastStatusSpell(ViperPoisonStatus, null, loc);
                a.Unit.RestoreMana(2);
                a.CastOffensiveSpell(ViperPoisonDamage, null, loc);
                TacticalGraphicalEffects.CreatePoisonCloud(loc);
            }
        };
        SpellDict[SpellType.ViperPoison] = ViperPoisonStatus;

        AlraunePuff = new StatusSpell()
        {
            Name = "Pollen Cloud",
            Id = "alraune-spell",
            SpellType = SpellType.AlraunePuff,
            Description = "Applies a variety of status effects in a 3x3 area",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy, AbilityTargets.Tile },
            Range = new Range(8),
            Duration = (a, t) => 3,
            Effect = (a, t) => .15f,
            AreaOfEffect = 1,
            Type = StatusEffectType.Shaken,
            Tier = 0,
            Resistable = true,
            ResistanceMult = 1f,
            Alraune = true,
            OnExecute = (a, t) =>
            {
                a.CastStatusSpell(AlraunePuff, t);
                TacticalGraphicalEffects.CreatePollenCloud(t.Position);
            },
            OnExecuteTile = (a, loc) =>
            {
                a.CastStatusSpell(AlraunePuff, null, loc);
                TacticalGraphicalEffects.CreatePollenCloud(loc);
            }
        };
        SpellDict[SpellType.AlraunePuff] = AlraunePuff;

        Web = new StatusSpell()
        {
            Name = "Web",
            Id = "web",
            SpellType = SpellType.Web,
            Description = "Webs the target, lowering their movement to 1 for a few turns and lowering their stats",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy },
            Range = new Range(4),
            Duration = (a, t) => 3,
            Effect = (a, t) => 1,
            Type = StatusEffectType.Webbed,
            Tier = 0,
            Resistable = true,
            ResistanceMult = 0.55f,
            OnExecute = (a, t) =>
            {
                a.CastStatusSpell(Web, t);
                TacticalGraphicalEffects.CreateSpiderWeb(a.Position, t.Position, t);
            },
        };
        SpellDict[SpellType.Web] = Web;

        GlueBomb = new StatusSpell()
        {
            Name = "Glue Bomb",
            Id = "glueBomb",
            SpellType = SpellType.GlueBomb,
            Description = "Glues a 3x3 area, greatly slowing them down for a significant number of turns.",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy, AbilityTargets.Tile },
            Range = new Range(8),
            Duration = (a, t) => 8,
            Effect = (a, t) => 1,
            AreaOfEffect = 1,
            Type = StatusEffectType.Glued,
            Tier = 0,
            Resistable = true,
            ResistanceMult = 0.45f,
            OnExecute = (a, t) =>
            {
                a.CastStatusSpell(GlueBomb, t);
                TacticalGraphicalEffects.CreateGlueBomb(a.Position, t.Position);
            },
            OnExecuteTile = (a, loc) =>
            {
                a.CastStatusSpell(GlueBomb, null, loc);
                TacticalGraphicalEffects.CreateGlueBomb(a.Position, loc);
            }
        };
        SpellDict[SpellType.GlueBomb] = GlueBomb;

        Petrify = new StatusSpell()
        {
            Name = "Petrify",
            Id = "petrify",
            SpellType = SpellType.Petrify,
            Description = "Petrifies the target, preventing them from taking any action or dodging, but reducing damage taken and making them harder to eat.",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy },
            Range = new Range(4),
            Duration = (a, t) => 4,
            Effect = (a, t) => 1,
            Type = StatusEffectType.Petrify,
            Tier = 0,
            Resistable = true,
            ResistanceMult = .9f,
            OnExecute = (a, t) => { a.CastStatusSpell(Petrify, t); },
        };
        SpellDict[SpellType.Petrify] = Petrify;

        HypnoGas = new StatusSpell()
        {
            Name = "Hypnotic Gas",
            Id = "hypno-fart",
            SpellType = SpellType.HypnoGas,
            Description = "Applies Hypnotized in a 4x4 area near the caster. Hypnotized units become noncombatants that serve the caster's side.",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Tile, AbilityTargets.Enemy },
            Range = new Range(1),
            Duration = (a, t) => 5,
            Effect = (a, t) => 0f,
            EffectSide = (a, t) => a.Unit.GetApparentSide(t.Unit),
            AreaOfEffect = 1,
            Type = StatusEffectType.Hypnotized,
            Tier = 0,
            Resistable = true,
            ResistanceMult = 0.5f,
            OnExecute = (a, t) =>
            {
                a.CastStatusSpell(HypnoGas, t, null, Stat.Voracity);
                if (Config.FartOnAbsorb)
                {
                    a.SetPredMode(PreyLocation.Anal);
                    State.GameManager.SoundManager.PlayFart(a);
                }
                else if (Config.BurpFraction > 0)
                {
                    a.SetPredMode(PreyLocation.Stomach);
                    State.GameManager.SoundManager.PlayBurp(a);
                }

                TacticalGraphicalEffects.CreateGasCloud(t.Position);
            },
            OnExecuteTile = (a, loc) =>
            {
                a.CastStatusSpell(HypnoGas, null, loc, Stat.Voracity);
                if (Config.FartOnAbsorb)
                {
                    a.SetPredMode(PreyLocation.Anal);
                    State.GameManager.SoundManager.PlayFart(a);
                }
                else if (Config.BurpFraction > 0)
                {
                    a.SetPredMode(PreyLocation.Stomach);
                    State.GameManager.SoundManager.PlayBurp(a);
                }

                TacticalGraphicalEffects.CreateGasCloud(loc);
            }
        };
        SpellDict[SpellType.HypnoGas] = HypnoGas;

        Bind = new Spell()
        {
            Name = "Bind/Resummon",
            Id = "Bind",
            SpellType = SpellType.Bind,
            Description = "Allows to either take control of any summon, or re-summon the most recently bound one by targeting an empty space.",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy, AbilityTargets.Ally, AbilityTargets.Tile },
            Range = new Range(4),
            Tier = 2,
            Resistable = false,
            OnExecute = (a, t) => a.CastBind(t),
            OnExecuteTile = (a, l) => a.SummonBound(l),
        };
        SpellDict[SpellType.Bind] = Bind;

        ForceFeed = new Spell()
        {
            Name = "Force Feed",
            Id = "force-feed",
            SpellType = SpellType.ForceFeed,
            Description = "",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy },
            Range = new Range(1),
            Tier = 0,
            Resistable = false,
            OnExecute = (a, t) => { TacticalUtilities.ForceFeed(a, t); },
        };
        SpellDict[SpellType.ForceFeed] = ForceFeed;

        RevertForm = new Spell()
        {
            Name = "Revert Form",
            Id = "revert-form",
            SpellType = SpellType.RevertForm,
            Description = "",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Self },
            Range = new Range(1),
            Tier = 0,
            Resistable = false,
            OnExecute = (a, t) => { TacticalUtilities.RevertForm(a, t); },
        };
        SpellDict[SpellType.RevertForm] = RevertForm;

        AssumeForm = new Spell()
        {
            Name = "Assume Form",
            Id = "assume-form",
            SpellType = SpellType.AssumeForm,
            Description = "Assumes the form of a random Prey",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Self },
            Range = new Range(1),
            Tier = 0,
            Resistable = false,
            OnExecute = (a, t) => { TacticalUtilities.AssumeForm(a, t); },
        };
        SpellDict[SpellType.AssumeForm] = AssumeForm;


        Whispers = new StatusSpell()
        {
            Name = "Whispers",
            Id = "whispers-spell",
            SpellType = SpellType.Whispers,
            Description = "Applies Charm, Prey's Curse, and Temptation for 3 rounds",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy },
            Range = new Range(1),
            Duration = (a, t) => 3,
            Effect = (a, t) => 0f,
            EffectSide = (a, t) => a.Unit.GetApparentSide(t.Unit),
            Type = StatusEffectType.Charmed,
            Tier = 3,
            Resistable = true,
            OnExecute = (a, t) => { a.CastStatusSpell(Whispers, t); },
        };
        SpellDict[SpellType.Whispers] = Whispers;

        ViralInfection = new StatusSpell()
        {
            Name = "ViralInfection",
            Id = "viralinfection",
            SpellType = SpellType.ViralInfection,
            Description = "Applies virus to enemy, dealing damage over time.",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Ally, AbilityTargets.Enemy, AbilityTargets.Self },
            Range = new Range(8),
            Duration = (a, t) => 4 + a.Unit.GetStat(Stat.Mind) / 5,
            Effect = (a, t) => 1 + a.Unit.GetStat(Stat.Mind) / 10,
            Type = StatusEffectType.Virus,
            Tier = 2,
            Resistable = false,
            OnExecute = (a, t) =>
            {
                if (a.CastStatusSpell(ViralInfection, t)) TacticalGraphicalEffects.CreateGenericMagic(a.Position, t.Position, t, TacticalGraphicalEffects.SpellEffectIcon.Poison);
            },
        };
        SpellDict[SpellType.ViralInfection] = ViralInfection;

        AmplifyMagic = new Spell()
        {
            Name = "Amplify Magic",
            Id = "amplify-magic",
            SpellType = SpellType.AmplifyMagic,
            Description = "Grant nearby units stacks of Focus, scaling with will.",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Ally },
            Range = new Range(1),
            Tier = 0,
            AreaOfEffect = 1,
            Resistable = false,
            IsFree = true,
            OnExecute = (a, t) =>
            {
                a.CastSpell(AmplifyMagic, t);
                int amt = a.Unit.GetStat(Stat.Will) / 25;
                foreach (var ally in TacticalUtilities.UnitsWithinTiles(t.Position, 1).Where(s => s.Unit.IsDead == false && Equals(s.Unit.Side, a.Unit.Side)))
                {
                    ally.Unit.AddFocus(amt > 1 ? amt : 1);
                    TacticalGraphicalEffects.CreateGenericMagic(a.Position, ally.Position, ally, TacticalGraphicalEffects.SpellEffectIcon.Buff);
                }
            }
        };
        SpellDict[SpellType.AmplifyMagic] = AmplifyMagic;

        DivinitysEmbrace = new StatusSpell()
        {
            Name = "Divinity's Embrace",
            Id = "divinitysembrace",
            SpellType = SpellType.DivinitysEmbrace,
            Description = "Shower an ally in divine light, providing instant healing as well as damage mitigation for a few turns.",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Ally },
            Range = new Range(4),
            Duration = (a, t) => 2 + a.Unit.GetStat(Stat.Mind) / 10,
            Effect = (a, t) => .25f,
            Type = StatusEffectType.DivineShield,
            Tier = 3,
            Resistable = false,
            OnExecute = (a, t) =>
            {
                a.CastStatusSpell(DivinitysEmbrace, t);
                TacticalGraphicalEffects.CreateGenericMagic(a.Position, t.Position, t, TacticalGraphicalEffects.SpellEffectIcon.Buff);
                t.Damage(-5 - a.Unit.GetStat(Stat.Mind));
            },
        };
        SpellDict[SpellType.DivinitysEmbrace] = DivinitysEmbrace;

        Evocation = new Spell()
        {
            Name = "Evocation",
            Id = "evocation",
            SpellType = SpellType.Evocation,
            Description = "Draw all nearby Unit's Focus onto a target, inspiring it. Grants the target equivalent SpellForce stacks and half as much MP.",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Ally },
            Range = new Range(1),
            Tier = 0,
            AreaOfEffect = 1,
            Resistable = false,
            IsFree = true,
            OnExecute = (a, t) =>
            {
                a.CastSpell(Evocation, a);
                int stacks = 0;
                foreach (var ally in TacticalUtilities.UnitsWithinTiles(t.Position, 1).Where(s => s.Unit.GetStatusEffect(StatusEffectType.Focus) != null))
                {
                    for (int i = 0; i < ally.Unit.GetStatusEffect(StatusEffectType.Focus).Duration; i++)
                    {
                        t.Unit.AddSpellForce();
                        stacks++;
                    }

                    ally.Unit.StatusEffects.Remove(ally.Unit.GetStatusEffect(StatusEffectType.Focus));
                    TacticalGraphicalEffects.CreateGenericMagic(ally.Position, t.Position, t);
                }

                t.Movement += stacks / 2;
                TacticalGraphicalEffects.CreateGenericMagic(a.Position, t.Position, t, TacticalGraphicalEffects.SpellEffectIcon.PurplePlus);
            }
        };
        SpellDict[SpellType.Evocation] = Evocation;

        ManaFlux = new DamageSpell()
        {
            Name = "Mana Flux",
            Id = "mana-flux",
            SpellType = SpellType.ManaFlux,
            Description = "Deals damage which is increased by missing mana.",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy },
            Range = new Range(6),
            Tier = 1,
            AreaOfEffect = 0,
            Damage = (a, t) => a.Unit.GetStat(Stat.Mind) / 5 + (a.Unit.MaxMana - a.Unit.Mana) / 10,
            Resistable = true,
            OnExecute = (a, t) =>
            {
                a.CastOffensiveSpell(ManaFlux, t);
                TacticalGraphicalEffects.CreateGenericMagic(a.Position, t.Position, t);
            }
        };
        SpellDict[SpellType.ManaFlux] = ManaFlux;
        UnstableMana = new DamageSpell()
        {
            Name = "Unstable Mana",
            Id = "unstable-mana",
            SpellType = SpellType.UnstableMana,
            Description = "Deals damage increased by unit's current mana, then uses it all. The target explodes, dealing half of their current mana as damage around them.",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy },
            Range = new Range(6),
            Tier = 0,
            AreaOfEffect = 0,
            Damage = (a, t) => a.Unit.Mana / 4 + a.Unit.GetStat(Stat.Mind) / 5,
            Resistable = true,
            OnExecute = (a, t) =>
            {
                int curr = t.Unit.Health;
                a.CastOffensiveSpell(UnstableMana, t);
                bool didSpellHit = curr != t.Unit.Health;
                TacticalGraphicalEffects.CreateHugeMagic(a.Position, t.Position, t, didSpellHit);
                if (didSpellHit)
                {
                    a.CastOffensiveSpell(ManaExpolsion, t);
                }

                a.Unit.SpendMana(a.Unit.Mana);
            }
        };
        SpellDict[SpellType.UnstableMana] = UnstableMana;

        ManaExpolsion = new DamageSpell()
        {
            Name = "Mana Expolsion",
            Id = "mana-expolsion",
            SpellType = SpellType.ManaExpolsion,
            Description = "(Second half of Unstable Mana Spell)",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy },
            Range = new Range(6),
            Tier = 0,
            AreaOfEffect = 1,
            Damage = (a, t) => t.Unit.Mana / 2,
            Resistable = true,
            ResistanceMult = 0.2f,
            OnExecute = (a, t) => { }
        };
        SpellDict[SpellType.ManaExpolsion] = ManaExpolsion;
    }
}


public class Spell
{
    internal string Name;
    internal string Id;
    internal string Description;
    internal List<AbilityTargets> AcceptibleTargets;
    internal SpellType SpellType;
    internal Range Range;
    internal int Tier;

    /// <summary>The number of tiles away a target is affected (i.e. 1 = 9 total tiles)</summary>
    internal int AreaOfEffect;

    internal Action<ActorUnit, Vec2I> OnExecuteTile;
    internal bool Resistable;
    internal float ResistanceMult = 1;
    internal Action<ActorUnit, ActorUnit> OnExecute;
    internal bool IsFree = false;


    internal bool TryCast(ActorUnit actor, Vec2I location)
    {
        if ((actor.Unit.Mana >= ManaCost || IsFree) && actor.Movement > 0)
        {
            int startMana = actor.Unit.Mana;
            OnExecuteTile(actor, location);
            if ((actor.Unit.SingleUseSpells?.Contains(SpellType) ?? false) && actor.Unit.Mana != startMana)
            {
                actor.Unit.SingleUseSpells.Remove(SpellType);
                actor.Unit.UpdateSpells();
            }

            return true;
        }

        return false;
    }

    internal bool TryCast(ActorUnit actor, ActorUnit target)
    {
        if ((actor.Unit.Mana >= ManaCost || IsFree) && actor.Movement > 0)
        {
            int startMana = actor.Unit.Mana;
            OnExecute(actor, target);
            if ((actor.Unit.SingleUseSpells?.Contains(SpellType) ?? false) && actor.Unit.Mana != startMana)
            {
                actor.Unit.SingleUseSpells.Remove(SpellType);
                actor.Unit.UpdateSpells();
            }

            return true;
        }

        return false;
    }

    internal int ManaCost => 2 * (int)Math.Pow(2, Tier);
}

//May redo this with interfaces later on

internal class DamageSpell : Spell
{
    internal Func<ActorUnit, ActorUnit, int> Damage;
    internal DamageType DamageType = DamageType.Generic;
}

internal class StatusSpell : Spell
{
    internal Func<ActorUnit, ActorUnit, int> Duration;
    internal Func<ActorUnit, ActorUnit, float> Effect;

    /// <summary>
    ///     BAD SOLUTION. TEMPORARY(tm)
    /// </summary>
    internal Func<ActorUnit, ActorUnit, Side> EffectSide;

    internal StatusEffectType Type;
    internal bool Alraune = false;
}

//class MultiTargetSpell : Spell
//{
//    internal Range SecondRange;
//}