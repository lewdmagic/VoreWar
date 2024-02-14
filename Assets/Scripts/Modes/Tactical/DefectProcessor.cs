using System.Collections.Generic;


internal class DefectProcessor
{
    internal Army Attacker;
    internal Army Defender;
    internal Village Village;

    internal int DefectedAttackers;
    internal int DefectedDefenders;
    internal int DefectedGarrison;

    internal int NewAttackers;
    internal int NewDefenders;
    //internal int newGarrison;

    internal List<ActorUnit> ExtraDefenders;
    internal List<ActorUnit> ExtraAttackers;

    public DefectProcessor(Army attacker, Army defender, Village village)
    {
        this.Attacker = attacker;
        this.Defender = defender;
        this.Village = village;
        ExtraAttackers = new List<ActorUnit>();
        ExtraDefenders = new List<ActorUnit>();
    }

    internal void DefectReport()
    {
        if (DefectedAttackers == 0 && DefectedDefenders == 0 && DefectedGarrison == 0) return;
        string msg = DefectedAttackers > 0 ? $"{DefectedAttackers} attackers have defected to rejoin their race\n" : "";
        msg += DefectedDefenders > 0 || DefectedGarrison > 0 ? $"{DefectedDefenders + DefectedGarrison} defenders have defected to rejoin their race\n" : "";
        State.GameManager.CreateMessageBox(msg);
    }

    internal void AttackerDefectCheck(ActorUnit actor, Race otherRace)
    {
        if (!Equals(actor.Unit.Race, otherRace) || actor.Unit.ImmuneToDefections || actor.Unit.HasTrait(TraitType.Camaraderie) || (actor.Unit.HasFixedSide() && !actor.Unit.HasTrait(TraitType.Infiltrator))) return;
        if (actor.Unit.HasTrait(TraitType.RaceLoyal) || State.Rand.NextDouble() < .15f - .05f * (actor.Unit.GetStat(Stat.Will) - 10) / 10)
        {
            DefectedAttackers++;

            Attacker.Units.Remove(actor.Unit);
            if (Defender != null && Defender.Units.Count < Defender.MaxSize)
            {
                actor.Unit.Side = Defender.Side;
                Defender.Units.Add(actor.Unit);
            }
            else
            {
                actor.Unit.Side = State.GameManager.TacticalMode.GetDefenderSide();
                NewDefenders++;
                ExtraDefenders.Add(actor);
            }
        }
    }

    internal void DefenderDefectCheck(ActorUnit actor, Race otherRace)
    {
        if (!Equals(actor.Unit.Race, otherRace) || actor.Unit.ImmuneToDefections || actor.Unit.HasTrait(TraitType.Camaraderie) || (actor.Unit.HasFixedSide() && !actor.Unit.HasTrait(TraitType.Infiltrator))) return;
        if (actor.Unit.HasTrait(TraitType.RaceLoyal) || State.Rand.NextDouble() < .15f - .05f * (actor.Unit.GetStat(Stat.Will) - 10) / 10)
        {
            actor.Unit.Side = Attacker.Side;
            DefectedDefenders++;
            Defender.Units.Remove(actor.Unit);
            if (Attacker.Units.Count < Attacker.MaxSize)
            {
                Attacker.Units.Add(actor.Unit);
            }
            else
            {
                NewAttackers++;
                ExtraAttackers.Add(actor);
            }
        }
    }

    internal void GarrisonDefectCheck(ActorUnit actor, Race otherRace)
    {
        if (!Equals(actor.Unit.Race, otherRace) || actor.Unit.ImmuneToDefections || actor.Unit.HasTrait(TraitType.Camaraderie) || (actor.Unit.HasFixedSide() && !actor.Unit.HasTrait(TraitType.Infiltrator))) return;
        if (actor.Unit.HasTrait(TraitType.RaceLoyal) || State.Rand.NextDouble() < (2 - Village.Happiness / 66) * .15f - .05f * (actor.Unit.GetStat(Stat.Will) - 10) / 10)
        {
            actor.Unit.Side = Attacker.Side;
            Village.GetRecruitables().Remove(actor.Unit);
            DefectedGarrison++;
            if (Attacker.Units.Count < Attacker.MaxSize)
            {
                Attacker.Units.Add(actor.Unit);
            }
            else
            {
                NewAttackers++;
                ExtraAttackers.Add(actor);
            }
        }
    }
}