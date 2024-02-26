using System.Collections.Generic;

public class StandardTacticalAI : TacticalAI
{
    public StandardTacticalAI(List<ActorUnit> actors, TacticalTileType[,] tiles, Side aiSide, bool defendingVillage = false) : base(actors, tiles, aiSide, defendingVillage)
    {
    }

    protected override void GetNewOrder(ActorUnit actor)
    {
        FoundPath = false;
        DidAction = false; // Very important fix: surrounded retreaters sometimes just skipped doing attacks because this was never set to false in or before "fightwithoutmoving"

        Path = null;
        if (Retreating && actor.Unit.Type != UnitType.Summon && actor.Unit.Type != UnitType.SpecialMercenary && actor.Unit.HasTrait(TraitType.Fearless) == false && Equals(TacticalUtilities.GetMindControlSide(actor.Unit), Side.TrueNoneSide) && (Equals(TacticalUtilities.GetPreferredSide(actor.Unit, AISide, EnemySide), AISide) || OnlyForeignTroopsLeft))
        {
            int retreatY;
            if (State.GameManager.TacticalMode.IsDefender(actor) == false)
                retreatY = Config.TacticalSizeY - 1;
            else
                retreatY = 0;
            if (actor.Position.Y == retreatY)
            {
                State.GameManager.TacticalMode.AttemptRetreat(actor, true);
                FightWithoutMoving(actor);
                actor.Movement = 0;
                return;
            }

            WalkToYBand(actor, retreatY);
            if (Path == null || Path.Path.Count == 0)
            {
                FightWithoutMoving(actor);
                actor.Movement = 0;
            }

            return;
        }
        //do action


        if (actor.Unit.HasTrait(TraitType.Pounce) && actor.Movement >= 2)
        {
            RunVorePounce(actor);
            if (Path != null) return;
            if (DidAction) return;
        }

        RunPred(actor);
        if (DidAction || FoundPath) return;

        TryResurrect(actor);
        TryReanimate(actor);

        RunBind(actor);

        if (State.Rand.Next(2) == 0 || actor.Unit.HasWeapon == false) RunSpells(actor);
        if (Path != null) return;
        if (actor.Unit.HasTrait(TraitType.Pounce) && actor.Movement >= 2)
        {
            if (IsRanged(actor) == false)
            {
                RunMeleePounce(actor);
                if (DidAction) return;
            }
        }

        if (FoundPath || DidAction) return;
        if (IsRanged(actor))
            RunRanged(actor);
        else
            RunMelee(actor);
        if (FoundPath || DidAction) return;
        //Search for surrendered targets outside of vore range
        //If no path to any targets, will sit out its turn
        RunPred(actor, true);
        if (FoundPath || DidAction) return;
        actor.ClearMovement();
    }
}