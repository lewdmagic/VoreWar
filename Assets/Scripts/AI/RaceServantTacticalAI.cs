using System.Collections.Generic;
using System.Linq;

public class RaceServantTacticalAI : HedonistTacticalAI
{
    public RaceServantTacticalAI(List<ActorUnit> actors, TacticalTileType[,] tiles, Side aiSide, bool defendingVillage = false) : base(actors, tiles, aiSide, defendingVillage)
    {
    }

    protected override List<PotentialTarget> GetListOfPotentialRubTargets(ActorUnit actor, Vec2I position, int moves)
    {
        List<PotentialTarget> targets = new List<PotentialTarget>();
        Race masterRace = GetStrongestFriendlyRaceOnBattlefield(actor);

        if (Equals(actor.Unit.Race, masterRace)) return targets; // Don't serve your own race

        List<ActorUnit> masters = Actors.Where(a => Equals(a.Unit.Race, masterRace)).ToList();

        foreach (ActorUnit unit in masters)
        {
            if (unit.Targetable == true && unit.Unit.Predator && !TacticalUtilities.TreatAsHostile(actor, unit) && Equals(TacticalUtilities.GetMindControlSide(unit.Unit), Side.TrueNoneSide) && !unit.Surrendered && unit.PredatorComponent?.PreyCount > 0 && !unit.ReceivedRub)
            {
                int distance = unit.Position.GetNumberOfMovesDistance(position);
                if (distance - 1 + actor.MaxMovement() / 3 <= moves)
                {
                    if (distance > 1 && TacticalUtilities.FreeSpaceAroundTarget(unit.Position, actor) == false) continue;
                    targets.Add(new PotentialTarget(unit, 100, distance, 4, 100 - (unit == actor ? 100 - unit.Unit.HealthPct + 10 : 100 - unit.Unit.HealthPct)));
                }
            }
        }

        return targets.OrderByDescending(t => t.Utility).ToList();
    }

    private Race GetStrongestFriendlyRaceOnBattlefield(ActorUnit unit)
    {
        return Actors.Where(a => !TacticalUtilities.TreatAsHostile(unit, a)).OrderByDescending(a => State.RaceSettings.Get(a.Unit.Race).PowerAdjustment == 0 ? RaceParameters.GetTraitData(a.Unit).PowerAdjustment : State.RaceSettings.Get(a.Unit.Race).PowerAdjustment).First().Unit.Race;
    }
}