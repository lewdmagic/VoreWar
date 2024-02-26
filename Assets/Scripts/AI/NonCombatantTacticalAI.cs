using System.Collections.Generic;
using System.Linq;

public class NonCombatantTacticalAI : RaceServantTacticalAI
{
    public NonCombatantTacticalAI(List<ActorUnit> actors, TacticalTileType[,] tiles, Side aiSide, bool defendingVillage = false) : base(actors, tiles, aiSide, defendingVillage)
    {
    }

    protected override void GetNewOrder(ActorUnit actor)
    {
        FoundPath = false;
        DidAction = false; // Very important fix: surrounded retreaters sometimes just skipped doing attacks because this was never set to false in or before "fightwithoutmoving"

        Path = null;
        List<ActorUnit> masters = Actors.Where(a => RaceAIType.Dict[State.RaceSettings.GetRaceAI(a.Unit.Race)] != typeof(NonCombatantTacticalAI) && !TacticalUtilities.TreatAsHostile(actor, a)).ToList();
        if ((Retreating && actor.Unit.Type != UnitType.Summon && actor.Unit.Type != UnitType.SpecialMercenary && actor.Unit.HasTrait(TraitType.Fearless) == false && Equals(TacticalUtilities.GetMindControlSide(actor.Unit), Side.TrueNoneSide) && Equals(TacticalUtilities.GetPreferredSide(actor.Unit, AISide, EnemySide), AISide))
            || masters.Count == 0)
        {
            int retreatY;
            if (State.GameManager.TacticalMode.IsDefender(actor) == false)
                retreatY = Config.TacticalSizeY - 1;
            else
                retreatY = 0;
            if (actor.Position.Y == retreatY)
            {
                State.GameManager.TacticalMode.AttemptRetreat(actor, true);
                actor.Movement = 0;
                return;
            }

            WalkToYBand(actor, retreatY);
            if (Path == null || Path.Path.Count == 0)
            {
                actor.Movement = 0;
            }

            return;
        }

        //do action

        int spareMp = CheckActionEconomyOfActorFromPositionWithAP(actor, actor.Position, actor.Movement);
        int thirdMovement = actor.MaxMovement() / 3;
        if (spareMp >= thirdMovement)
        {
            RunBellyRub(actor, spareMp);
            if (Path != null) return;
            if (DidAction) return;
        }

        if (actor.Unit.GetStatusEffect(StatusEffectType.Temptation) != null && (State.Rand.Next(2) == 0 || actor.Unit.GetStatusEffect(StatusEffectType.Temptation).Duration <= 3))
        {
            RunForceFeed(actor);
        }

        TryResurrect(actor);

        RunSpells(actor);
        if (Path != null) return;

        RunBellyRub(actor, actor.Movement);
        if (FoundPath || DidAction) return;
        //Search for surrendered targets outside of vore range
        //If no path to any targets, will sit out its turn

        actor.ClearMovement();
    }

    protected override List<PotentialTarget> GetListOfPotentialRubTargets(ActorUnit actor, Vec2I position, int moves)
    {
        List<PotentialTarget> targets = new List<PotentialTarget>();

        List<ActorUnit> masters = Actors.Where(a => RaceAIType.Dict[State.RaceSettings.GetRaceAI(a.Unit.Race)] != typeof(NonCombatantTacticalAI) && Equals(TacticalUtilities.GetMindControlSide(a.Unit), Side.TrueNoneSide)).ToList();

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

    protected override void RunSpells(ActorUnit actor)
    {
        if (actor.Unit.UseableSpells == null || actor.Unit.UseableSpells.Any() == false) return;
        var friendlySpells = actor.Unit.UseableSpells.Where(sp => sp != SpellList.Resurrection && sp != SpellList.Reanimate && sp != SpellList.Bind && sp.ManaCost <= actor.Unit.Mana && sp.AcceptibleTargets.Contains(AbilityTargets.Ally)).ToList();

        if (friendlySpells == null || friendlySpells.Any() == false) return;

        if (friendlySpells.Any() == false) return;

        Spell spell = friendlySpells[State.Rand.Next(friendlySpells.Count())];

        if ((spell == SpellList.Charm || spell == SpellList.HypnoGas) && !Equals(TacticalUtilities.GetMindControlSide(actor.Unit), Side.TrueNoneSide)) // Charmed units should not use charm. Trust me.
            return;
        if (spell.ManaCost > actor.Unit.Mana) return;

        if (State.GameManager.TacticalMode.IsOnlyOneSideVisible()) return;

        List<PotentialTarget> targets = GetListOfPotentialSpellTargets(actor, spell, actor.Position);
        if (!targets.Any()) return;
        ActorUnit reserveTarget = targets[0].Actor;
        while (targets.Any())
        {
            if (targets[0].Distance <= spell.Range.Max)
            {
                if (spell.TryCast(actor, targets[0].Actor)) DidAction = true;
                return;
            }
            else
            {
                if (targets[0].Actor.Position.GetNumberOfMovesDistance(actor.Position) <= actor.Movement + spell.Range.Max) //discard the clearly impossible
                {
                    MoveToAndAction(actor, targets[0].Actor.Position, spell.Range.Max, actor.Movement, () => spell.TryCast(actor, targets[0].Actor));
                    if (FoundPath && Path.Path.Count() < actor.Movement)
                        return;
                    else
                    {
                        FoundPath = false;
                        Path = null;
                    }
                }
            }

            targets.RemoveAt(0);
        }
    }

    protected override int CheckActionEconomyOfActorFromPositionWithAP(ActorUnit actor, Vec2I position, int ap)
    {
        int apRequired = -1;

        apRequired = CheckResurrect(actor, position, ap);
        if (apRequired > 0) return ap - apRequired;


        apRequired = CheckSpells(actor, position, ap);
        if (apRequired > 0) return ap - apRequired;

        // Everything else is less important than belly rubs.
        return ap;
    }
}