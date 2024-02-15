using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.UI;

internal static class TacticalUtilities
{
    private static Army[] _armies;
    private static Village _village;
    private static List<ActorUnit> _garrison;
    private static TacticalTileType[,] _tiles;
    private static bool[,] _blockedTiles;
    private static bool[,] _blockedClimberTiles;
    private static HirePanel _unitPickerUI;

    internal static TacticalMessageLog Log => State.GameManager.TacticalMode.Log;

    internal static List<ActorUnit> Units { get; private set; }

    internal static void ResetData()
    {
        _armies = null;
        _village = null;
        Units = null;
        _tiles = null;
        _garrison = null;
        _blockedTiles = null;
        _blockedClimberTiles = null;
    }

    internal static void ResetData(Army[] larmies, Village lvillage, List<ActorUnit> lunits, List<ActorUnit> lgarrison, TacticalTileType[,] ltiles, bool[,] lblockedTiles, bool[,] lblockedClimberTiles)
    {
        _armies = larmies;
        _village = lvillage;
        Units = lunits;
        _tiles = ltiles;
        _garrison = lgarrison;
        _blockedTiles = lblockedTiles;
        _blockedClimberTiles = lblockedClimberTiles;
        _unitPickerUI = State.GameManager.TacticalMode.UnitPickerUI;
    }

    // This is for fleeing units
    internal static void ProcessTravelingUnits(List<Unit> travelingUnits)
    {
        if (State.World.Villages == null)
        {
            return;
        }

        if (!RaceFuncs.IsMonster(travelingUnits[0].Side.ToRace()))
        {
            if (Config.TroopScatter)
            {
                foreach (var unit in travelingUnits.ToList())
                {
                    var villageList = State.World.Villages.Where(s => Equals(travelingUnits[0].Side, s.Side) && s != _village).ToList();
                    Village friendlyVillage;
                    if (villageList.Count() == 0) continue;
                    if (villageList.Count() == 1)
                        friendlyVillage = villageList[0];
                    else
                        friendlyVillage = villageList[State.Rand.Next(villageList.Count())];
                    var loc = StrategyPathfinder.GetPath(null, _armies[0], friendlyVillage.Position, 3, false, 999);
                    int turns = 9999;
                    int flightTurns = 9999;
                    Vec2I destination = null;
                    bool flyersExist = unit.HasTrait(TraitType.Pathfinder);
                    if (loc != null && loc.Count > 0)
                    {
                        destination = new Vec2I(loc.Last().X, loc.Last().Y);
                        turns = StrategyPathfinder.TurnsToReach(null, _armies[0], destination, Config.ArmyMp, false);
                        if (flyersExist) flightTurns = StrategyPathfinder.TurnsToReach(null, _armies[0], destination, Config.ArmyMp, true);
                    }

                    if (turns < 999)
                    {
                        if (flyersExist)
                            StrategicUtilities.CreateInvisibleTravelingArmy(unit, StrategicUtilities.GetVillageAt(destination), flightTurns);
                        else
                            StrategicUtilities.CreateInvisibleTravelingArmy(unit, StrategicUtilities.GetVillageAt(destination), turns);
                    }

                    travelingUnits.Remove(unit);
                }
            }
            else
            {
                var loc = StrategyPathfinder.GetPathToClosestObject(null, _armies[0], State.World.Villages.Where(s => Equals(travelingUnits[0].Side, s.Side) && s != _village).Select(s => s.Position).ToArray(), 3, 999, false);
                int turns = 9999;
                int flightTurns = 9999;
                Vec2I destination = null;
                bool flyersExist = travelingUnits.Where(s => s.HasTrait(TraitType.Pathfinder)).Count() > 0;
                if (loc != null && loc.Count > 0)
                {
                    destination = new Vec2I(loc.Last().X, loc.Last().Y);
                    turns = StrategyPathfinder.TurnsToReach(null, _armies[0], destination, Config.ArmyMp, false);
                    if (flyersExist) flightTurns = StrategyPathfinder.TurnsToReach(null, _armies[0], destination, Config.ArmyMp, true);
                }

                if (turns < 999)
                {
                    if (flyersExist) StrategicUtilities.CreateInvisibleTravelingArmy(travelingUnits.Where(s => s.HasTrait(TraitType.Pathfinder)).ToList(), StrategicUtilities.GetVillageAt(destination), flightTurns);
                    StrategicUtilities.CreateInvisibleTravelingArmy(travelingUnits.Where(s => s.HasTrait(TraitType.Pathfinder) == false).ToList(), StrategicUtilities.GetVillageAt(destination), turns);
                }
            }
        }
        else if (RaceFuncs.IsRebelOrBandit(travelingUnits[0].Side))
        {
            //Bandits and rebels that flee simply vanish
        }
        else
        {
            GenerateFleeingArmy(travelingUnits);
        }
    }


    private static void GenerateFleeingArmy(List<Unit> fleeingUnits)
    {
        if (fleeingUnits.Any() == false) return;
        if (Config.MonstersCanReform == false) return;
        for (int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {
                if (x == 0 && y == 0) continue;
                Vec2I loc = new Vec2I(_armies[0].Position.X + x, _armies[0].Position.Y + y);
                if (loc.X < 0 || loc.Y < 0 || loc.X >= Config.StrategicWorldSizeX || loc.Y >= Config.StrategicWorldSizeY) continue;
                if (StrategicUtilities.IsTileClear(loc))
                {
                    MonsterEmpire monsterEmp = (MonsterEmpire)State.World.GetEmpireOfSide(fleeingUnits[0].Side); // TODO incorrect cast exception happened here.
                    if (monsterEmp == null) return;
                    var army = new Army(monsterEmp, loc, fleeingUnits[0].Side);
                    army.RemainingMp = 0;
                    monsterEmp.Armies.Add(army);
                    army.Units.AddRange(fleeingUnits);
                    return;
                }
            }
        }
    }

    internal static void GenerateAnotherArmy(List<Unit> leftoverUnits)
    {
        if (leftoverUnits.Any() == false) return;
        if (Config.MonstersCanReform == false) return;
        for (int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {
                if (x == 0 && y == 0) continue;
                Vec2I loc = new Vec2I(_armies[0].Position.X + x, _armies[0].Position.Y + y);
                if (loc.X < 0 || loc.Y < 0 || loc.X >= Config.StrategicWorldSizeX || loc.Y >= Config.StrategicWorldSizeY) continue;
                if (StrategicUtilities.IsTileClear(loc))
                {
                    MonsterEmpire monsterEmp = (MonsterEmpire)State.World.GetEmpireOfSide(leftoverUnits[0].Side);
                    if (monsterEmp == null) return;
                    var army = new Army(monsterEmp, loc, leftoverUnits[0].Side);
                    army.RemainingMp = 0;
                    monsterEmp.Armies.Add(army);
                    army.Units.AddRange(leftoverUnits);
                    return;
                }
            }
        }
    }

    internal static void CleanVillage(int remainingAttackers)
    {
        bool monsterAttacker = RaceFuncs.IsMonster(_armies[0].Side.ToRace());
        //SpawnerInfo spawner = Config.SpawnerInfo((Race)armies[0]?.Side);
        SpawnerInfo spawner = Config.World.GetSpawner(_armies[0].Side.ToRace());
        Config.MonsterConquestType spawnerType;
        if (spawner != null)
            spawnerType = spawner.GetConquestType();
        else
            spawnerType = Config.MonsterConquest;
        //clean up missing garrison units
        if (_garrison != null)
        {
            foreach (ActorUnit garrison in _garrison)
            {
                if (garrison.Unit.IsDead || garrison.Fled || Equals(garrison.Unit.Side, _armies[0].Side))
                {
                    _village.VillagePopulation.RemoveHireable(garrison.Unit);
                }
            }
        }

        if (!Equals(_village.Side, _armies[0].Side) && remainingAttackers > 0 && (monsterAttacker == false || spawnerType != Config.MonsterConquestType.DevourAndDisperse))
        {
            _village.ChangeOwner(_armies[0].Side);
        }
        else if (remainingAttackers > 0 && monsterAttacker && spawnerType != Config.MonsterConquestType.DevourAndDisperse)
        {
            if (State.World.GetEmpireOfRace(_village.Race)?.IsAlly(_armies[0].EmpireOutside) ?? false) _village.ChangeOwner(_armies[0].Side);
        }

        if (monsterAttacker && remainingAttackers > 0 && _village.Empire.IsEnemy(_armies[0].EmpireOutside))
        {
            if (spawnerType == Config.MonsterConquestType.DevourAndDisperse)
            {
                _armies[0].RemainingMp = 1;
                State.GameManager.StrategyMode.Devour(_armies[0], Mathf.Min(2 * _armies[0].Units.Count, _village.Population - 2));
                _armies[0].Units = new List<Unit>();
            }
            else if (spawnerType == Config.MonsterConquestType.DevourAndHold)
            {
                if (_village.GetTotalPop() > _village.Maxpop / 2)
                {
                    _armies[0].RemainingMp = 1;
                    State.GameManager.StrategyMode.Devour(_armies[0], _village.GetTotalPop() - _village.Maxpop / 2);
                }
            }
            else //if (Config.MonsterConquest == Config.MonsterConquestType.CompleteDevourAndHold || Config.MonsterConquest == Config.MonsterConquestType.CompleteDevourAndMoveOn)
            {
                if (_village.GetTotalPop() > 0)
                {
                    _armies[0].RemainingMp = 1;
                    if (Config.MonsterConquestTurns > 1)
                    {
                        _armies[0].MonsterTurnsRemaining = Config.MonsterConquestTurns;
                    }
                    else
                        State.GameManager.StrategyMode.Devour(_armies[0], _village.GetTotalPop() / Config.MonsterConquestTurns);
                }
            }
        }
    }

    internal static bool IsUnitControlledByPlayer(Unit unit)
    {
        if (!Equals(GetMindControlSide(unit), Side.TrueNoneSide)) // Charmed units may fight for the player, but they are always AI controlled
            return false;
        Side defenderSide = State.GameManager.TacticalMode.GetDefenderSide();
        Side attackerSide = State.GameManager.TacticalMode.GetAttackerSide();
        bool aiDefender = State.GameManager.TacticalMode.AIDefender;
        bool aiAttacker = State.GameManager.TacticalMode.AIAttacker;
        if (State.GameManager.TacticalMode.CheatAttackerControl && Equals(unit.Side, attackerSide)) return true;
        if (State.GameManager.TacticalMode.CheatDefenderControl && Equals(unit.Side, defenderSide)) return true;

        if (State.GameManager.PureTactical)
        {
            return (!aiAttacker && Equals(attackerSide, unit.FixedSide)) || (!aiDefender && Equals(defenderSide, unit.FixedSide));
        }
        else
        {
            if (State.World.GetEmpireOfSide(unit.FixedSide) != null && State.World.GetEmpireOfSide(unit.FixedSide)?.StrategicAI == null)
            {
                return true;
            }

            bool prefSideHuman = (!aiDefender && Equals(defenderSide, GetPreferredSide(unit, defenderSide, attackerSide))) || (!aiAttacker && Equals(attackerSide, GetPreferredSide(unit, attackerSide, defenderSide)));
            bool currentSideHuman = (!aiDefender && Equals(defenderSide, unit.Side)) || (!aiAttacker && Equals(attackerSide, unit.Side));
            return prefSideHuman && currentSideHuman && !PlayerCanSeeTrueSide(unit); // "sleeping" infiltrators follow your orders while it doesn't go against their agenda.
        }
    }

    internal static bool AppropriateVoreTarget(ActorUnit pred, ActorUnit prey)
    {
        if (pred == prey) return false;
        if (Equals(pred.Unit.Side, prey.Unit.Side))
        {
            if (prey.Surrendered || pred.Unit.HasTrait(TraitType.Cruel) || Config.AllowInfighting || pred.Unit.HasTrait(TraitType.Endosoma) || !(Equals(prey.Unit.GetApparentSide(pred.Unit), pred.Unit.FixedSide) && Equals(prey.Unit.GetApparentSide(pred.Unit), pred.Unit.GetApparentSide())) || !Equals(GetMindControlSide(prey.Unit), Side.TrueNoneSide) || !Equals(GetMindControlSide(pred.Unit), Side.TrueNoneSide)) return true;
            return false;
        }

        return true;
    }

    public static Side GetPreferredSide(Unit actor, Side sideA, Side sideB) // If equally aligned with both, should default to A
    {
        Side effectiveActorSide = !Equals(GetMindControlSide(actor), Side.TrueNoneSide) ? GetMindControlSide(actor) : actor.FixedSide;
        if (State.GameManager.PureTactical)
        {
            return effectiveActorSide;
        }

        int aISideHostility = 0;
        int enemySideHostility = 0;
        if (!Equals(effectiveActorSide, sideA))
        {
            if (!Equals(effectiveActorSide, sideB))
            {
                switch (RelationsManager.GetRelation(effectiveActorSide, sideB).Type)
                {
                    case RelationState.Allied:
                    {
                        enemySideHostility = 1;
                        break;
                    }
                    case RelationState.Neutral:
                    {
                        enemySideHostility = 2;
                        break;
                    }
                    case RelationState.Enemies:
                    {
                        enemySideHostility = 3;
                        break;
                    }
                }
            }

            switch (RelationsManager.GetRelation(effectiveActorSide, sideA).Type)
            {
                case RelationState.Allied:
                {
                    aISideHostility = 1;
                    break;
                }
                case RelationState.Neutral:
                {
                    aISideHostility = 2;
                    break;
                }
                case RelationState.Enemies:
                {
                    aISideHostility = 3;
                    break;
                }
            }

            return enemySideHostility >= aISideHostility ? sideA : sideB;
        }
        else
        {
            return sideA;
        }
    }

    public static bool TreatAsHostile(ActorUnit actor, ActorUnit target)
    {
        if (actor == target) return false;
        if (ReferenceEquals(actor.Unit.Side, actor.Unit.FixedSide) && !(target.SidesAttackedThisBattle?.Contains(actor.Unit.FixedSide) ?? false) && Equals(target.Unit.Side, actor.Unit.Side) && Equals(GetMindControlSide(actor.Unit), Side.TrueNoneSide)) return false;
        Side friendlySide = actor.Unit.Side;
        Side defenderSide = State.GameManager.TacticalMode.GetDefenderSide();
        Side opponentSide = Equals(friendlySide, defenderSide) ? State.GameManager.TacticalMode.GetAttackerSide() : defenderSide;
        Side effectiveTargetSide = target.Unit.GetApparentSide(actor.Unit);
        Side effectiveActorSide = !Equals(GetMindControlSide(actor.Unit), Side.TrueNoneSide) ? GetMindControlSide(actor.Unit) : actor.Unit.FixedSide;
        if (Equals(GetMindControlSide(target.Unit), effectiveActorSide)) return false;
        if (State.GameManager.PureTactical)
        {
            return !Equals(effectiveTargetSide, effectiveActorSide);
        }

        if (Equals(effectiveActorSide, effectiveTargetSide)) return false;
        int aISideHostility = 0;
        int enemySideHostility = 0;
        Side preferredSide;
        Side unpreferredSide;
        if (!Equals(effectiveActorSide, friendlySide))
        {
            if (!Equals(effectiveActorSide, opponentSide))
            {
                switch (RelationsManager.GetRelation(effectiveActorSide, opponentSide).Type)
                {
                    case RelationState.Allied:
                    {
                        enemySideHostility = 1;
                        break;
                    }
                    case RelationState.Neutral:
                    {
                        enemySideHostility = 2;
                        break;
                    }
                    case RelationState.Enemies:
                    {
                        enemySideHostility = 3;
                        break;
                    }
                }
            }

            switch (RelationsManager.GetRelation(effectiveActorSide, friendlySide).Type)
            {
                case RelationState.Allied:
                {
                    aISideHostility = 1;
                    break;
                }
                case RelationState.Neutral:
                {
                    aISideHostility = 2;
                    break;
                }
                case RelationState.Enemies:
                {
                    aISideHostility = 3;
                    break;
                }
            }

            preferredSide = enemySideHostility >= aISideHostility ? friendlySide : opponentSide;
            unpreferredSide = Equals(preferredSide, friendlySide) ? opponentSide : friendlySide;
        }
        else
        {
            preferredSide = friendlySide;
            unpreferredSide = opponentSide;
        }

        int targetSideHostilityP = 0;
        int targetSideHostilityUp = 0;
        if (!Equals(preferredSide, effectiveTargetSide))
        {
            switch (RelationsManager.GetRelation(preferredSide, effectiveTargetSide).Type)
            {
                case RelationState.Allied:
                {
                    targetSideHostilityP = 1;
                    break;
                }
                case RelationState.Neutral:
                {
                    targetSideHostilityP = 2;
                    break;
                }
                case RelationState.Enemies:
                {
                    targetSideHostilityP = 3;
                    break;
                }
            }
        }

        if (!Equals(unpreferredSide, effectiveTargetSide))
        {
            switch (RelationsManager.GetRelation(unpreferredSide, effectiveTargetSide).Type)
            {
                case RelationState.Allied:
                {
                    targetSideHostilityUp = 1;
                    break;
                }
                case RelationState.Neutral:
                {
                    targetSideHostilityUp = 2;
                    break;
                }
                case RelationState.Enemies:
                {
                    targetSideHostilityUp = 3;
                    break;
                }
            }
        }

        return targetSideHostilityP >= targetSideHostilityUp || (target.SidesAttackedThisBattle?.Contains(preferredSide) ?? false) || (target.SidesAttackedThisBattle?.Contains(actor.Unit.FixedSide) ?? false);
    }

    public static bool SneakAttackCheck(Unit attacker, Unit target)
    {
        if (!Equals(GetMindControlSide(attacker), Side.TrueNoneSide)) return false;
        return Equals(attacker.GetApparentSide(target), target.GetApparentSide()) && attacker.IsInfiltratingSide(target.GetApparentSide());
    }

    public static Side GetMindControlSide(Unit unit)
    {
        if (unit.GetStatusEffect(StatusEffectType.Hypnotized) != null) return unit.GetStatusEffect(StatusEffectType.Hypnotized).Side;
        if (unit.GetStatusEffect(StatusEffectType.Charmed) != null) return unit.GetStatusEffect(StatusEffectType.Charmed).Side;
        return Side.TrueNoneSide;
    }
    // static public Side GetMindControlSide(Unit unit)
    // {
    //     if (unit.GetStatusEffect(StatusEffectType.Hypnotized) != null)
    //         return (int)(unit.GetStatusEffect(StatusEffectType.Hypnotized).Strength);
    //     if (unit.GetStatusEffect(StatusEffectType.Charmed) != null)
    //         return (int)(unit.GetStatusEffect(StatusEffectType.Charmed).Strength);
    //     return Race.none.ToSide();
    // }

    public static bool OpenTile(Vec2I vec, ActorUnit actor) => OpenTile(vec.X, vec.Y, actor);

    public static bool FreeSpaceAroundTarget(Vec2I targetLocation, ActorUnit actor)
    {
        for (int x = targetLocation.X - 1; x < targetLocation.X + 2; x++)
        {
            for (int y = targetLocation.Y - 1; y < targetLocation.Y + 2; y++)
            {
                if (x == targetLocation.X && y == targetLocation.Y) continue;
                if (OpenTile(x, y, actor))
                {
                    return true;
                }
            }
        }

        return false;
    }

    public static bool OpenTile(int x, int y, ActorUnit actor)
    {
        if (x < 0 || y < 0 || x > _tiles.GetUpperBound(0) || y > _tiles.GetUpperBound(1)) return false;
        if (_blockedTiles != null)
        {
            if (actor?.Unit.HasTrait(TraitType.NimbleClimber) ?? false)
            {
                if (x <= _blockedClimberTiles.GetUpperBound(0) || y <= _blockedClimberTiles.GetUpperBound(1))
                {
                    if (_blockedClimberTiles[x, y]) return false;
                }
            }
            else
            {
                if (x <= _blockedTiles.GetUpperBound(0) || y <= _blockedTiles.GetUpperBound(1))
                {
                    if (_blockedTiles[x, y]) return false;
                }
            }
        }

        if (TacticalTileInfo.CanWalkInto(_tiles[x, y], actor))
        {
            for (int i = 0; i < Units.Count; i++)
            {
                if (Units[i].Targetable == true && !Units[i].Hidden)
                {
                    if (Units[i].Position.X == x && Units[i].Position.Y == y)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        return false;
    }

    public static bool TileContainsMoreThanOneUnit(int x, int y)
    {
        if (x < 0 || y < 0 || x > _tiles.GetUpperBound(0) || y > _tiles.GetUpperBound(1)) return false;
        if (Units == null)
        {
            Debug.Log("This shouldn'targetPred have happened");
            return false;
        }

        int count = 0;
        for (int i = 0; i < Units.Count; i++)
        {
            if (Units[i].Targetable == true)
            {
                if (Units[i].Position.X == x && Units[i].Position.Y == y)
                {
                    count++;
                }
            }
        }

        return count > 1;
    }


    //public bool IsWalkable(int x, int y, Actor_Unit actor)
    //{
    //    return TacticalTileInfo.CanWalkInto(tiles[x, y], actor);
    //}


    public static bool FlyableTile(int x, int y)
    {
        if (x < 0 || y < 0 || x > _tiles.GetUpperBound(0) || y > _tiles.GetUpperBound(1)) return false;
        return true;
    }


    internal static void CheckKnockBack(ActorUnit attacker, ActorUnit target, ref float damage)
    {
        int xDiff = target.Position.X - attacker.Position.X;
        int yDiff = target.Position.Y - attacker.Position.Y;
        int direction = attacker.DiffToDirection(xDiff, yDiff);
        if (OpenTile(attacker.GetTile(target.Position, direction), target)) return;
        if (OpenTile(attacker.GetTile(target.Position, (direction + 1) % 8), target)) return;
        if (OpenTile(attacker.GetTile(target.Position, (direction + 7) % 8), target)) return;
        damage *= 1.2f;
        return;
    }

    internal static void KnockBack(ActorUnit attacker, ActorUnit target)
    {
        int xDiff = target.Position.X - attacker.Position.X;
        int yDiff = target.Position.Y - attacker.Position.Y;
        int direction = attacker.DiffToDirection(xDiff, yDiff);

        target.Movement += 1;
        if (target.Move(direction, _tiles))
            return;
        else if (target.Move((direction + 1) % 8, _tiles))
            return;
        else if (target.Move((direction + 7) % 8, _tiles)) return;
        target.Movement -= 1;

        return;
    }

    internal static PredatorComponent GetPredatorComponentOfUnit(Unit unit)
    {
        foreach (ActorUnit actor in Units)
        {
            if (actor.Unit == unit) return actor.PredatorComponent;
        }

        return null;
    }

    internal static ActorUnit FindPredator(ActorUnit searcher)
    {
        foreach (ActorUnit unit in Units)
        {
            if (unit.PredatorComponent?.IsActorInPrey(searcher) ?? false) return unit;
        }

        return null;
    }

    internal static void UpdateActorLocations()
    {
        foreach (ActorUnit unit in Units)
        {
            if (unit.UnitSprite == null) continue;
            Vec2I vec = unit.Position;
            Vector2 vector2 = new Vector2(vec.X, vec.Y);
            unit.UnitSprite.transform.position = vector2;
        }
    }


    internal static void RefreshUnitGraphicType()
    {
        if (Units == null) return;
        foreach (ActorUnit actor in Units)
        {
            if (!Equals(actor.Unit.Race, Race.Imp) && !Equals(actor.Unit.Race, Race.Lamia) && !Equals(actor.Unit.Race, Race.Tiger))
            {
                RaceFuncs.GetRace(actor.Unit).RandomCustomCall(actor.Unit);
            }
        }
    }

    internal static void UpdateVersion()
    {
        foreach (ActorUnit actor in Units)
        {
            actor.PredatorComponent?.UpdateVersion();
        }
    }

    internal static List<ActorUnit> UnitsWithinTiles(Vec2 target, int tiles)
    {
        List<ActorUnit> unitList = new List<ActorUnit>();
        foreach (ActorUnit actor in Units)
        {
            if (actor.Visible && actor.Targetable)
            {
                if (actor.Position.GetNumberOfMovesDistance(target) <= tiles)
                {
                    unitList.Add(actor);
                }
            }
        }

        return unitList;
    }

    internal static ActorUnit FindUnitToResurrect(ActorUnit caster)
    {
        ActorUnit actor = Units.Where(s => Equals(s.Unit.Side, caster.Unit.Side) && s.Unit.IsDead && s.Unit.Type != UnitType.Summon).OrderByDescending(s => s.Unit.Experience).FirstOrDefault();
        return actor;
    }

    internal static ActorUnit FindUnitToReanimate(ActorUnit caster)
    {
        ActorUnit actor = Units.Where(s => s.Unit.IsDead).OrderByDescending(s => s.Unit.Experience).FirstOrDefault();
        return actor;
    }


    internal static void CreateResurrectionPanel(Vec2I loc, Side side)
    {
        int children = _unitPickerUI.ActorFolder.transform.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            UnityEngine.Object.Destroy(_unitPickerUI.ActorFolder.transform.GetChild(i).gameObject);
        }

        ActorUnit[] list = Units.Where(s => Equals(s.Unit.Side, side) && s.Unit.IsDead && s.Unit.Type != UnitType.Summon).OrderByDescending(s => s.Unit.Experience).ToArray();
        foreach (ActorUnit actor in list)
        {
            GameObject obj = UnityEngine.Object.Instantiate(_unitPickerUI.HiringUnitPanel, _unitPickerUI.ActorFolder);
            UIUnitSprite sprite = obj.GetComponentInChildren<UIUnitSprite>();
            Text text = obj.transform.GetChild(3).GetComponent<Text>();
            text.text = $"Level: {actor.Unit.Level} Exp: {(int)actor.Unit.Experience}\n" +
                        $"Health : {100 * actor.Unit.HealthPct}%\n" +
                        $"Items: {actor.Unit.GetItem(0)?.Name} {actor.Unit.GetItem(1)?.Name}\n" +
                        $"Str: {actor.Unit.GetStatBase(Stat.Strength)} Dex: {actor.Unit.GetStatBase(Stat.Dexterity)} Agility: {actor.Unit.GetStatBase(Stat.Agility)}\n" +
                        $"Mind: {actor.Unit.GetStatBase(Stat.Mind)} Will: {actor.Unit.GetStatBase(Stat.Will)} Endurance: {actor.Unit.GetStatBase(Stat.Endurance)}\n";
            if (actor.Unit.Predator) text.text += $"Vore: {actor.Unit.GetStatBase(Stat.Voracity)} Stomach: {actor.Unit.GetStatBase(Stat.Stomach)}";
            actor.UpdateBestWeapons();
            sprite.UpdateSprites(actor);
            sprite.Name.text = actor.Unit.Name;
            Button button = obj.GetComponentInChildren<Button>();
            button.onClick.AddListener(() => Resurrect(loc, actor));
            button.onClick.AddListener(() => _unitPickerUI.gameObject.SetActive(false));
        }

        _unitPickerUI.ActorFolder.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 300 * (1 + list.Length / 3));
        _unitPickerUI.gameObject.SetActive(true);
    }

    internal static void CreateReanimationPanel(Vec2I loc, Unit unit)
    {
        int children = _unitPickerUI.ActorFolder.transform.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            UnityEngine.Object.Destroy(_unitPickerUI.ActorFolder.transform.GetChild(i).gameObject);
        }

        ActorUnit[] list = Units.Where(s => s.Unit.IsDead).OrderByDescending(s => s.Unit.Experience).ToArray();
        foreach (ActorUnit actor in list)
        {
            GameObject obj = UnityEngine.Object.Instantiate(_unitPickerUI.HiringUnitPanel, _unitPickerUI.ActorFolder);
            UIUnitSprite sprite = obj.GetComponentInChildren<UIUnitSprite>();
            Text genderText = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();
            Text expText = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>();
            GameObject equipRow = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(2).gameObject;
            GameObject statRow1 = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(3).gameObject;
            GameObject statRow2 = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(4).gameObject;
            GameObject statRow3 = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(5).gameObject;
            GameObject statRow4 = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(6).gameObject;
            Text traitList = obj.transform.GetChild(2).GetChild(0).GetChild(1).GetChild(0).GetChild(0).gameObject.GetComponent<Text>();
            Text hireButton = obj.transform.GetChild(2).GetChild(1).GetChild(0).gameObject.GetComponent<Text>();

            string gender;
            if (actor.Unit.GetGender() == Gender.Hermaphrodite)
                gender = "Herm";

            else
                gender = actor.Unit.GetGender().ToString();
            genderText.text = $"{gender}";
            traitList.text = RaceEditorPanel.TraitListToText(actor.Unit.GetTraits, true).Replace(", ", "\n");
            expText.text = $"Level {actor.Unit.Level} ({(int)actor.Unit.Experience} EXP)";
            if (actor.Unit.HasTrait(TraitType.Resourceful))
            {
                equipRow.transform.GetChild(2).gameObject.SetActive(true);
                equipRow.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = actor.Unit.GetItem(0)?.Name;
                equipRow.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = actor.Unit.GetItem(1)?.Name;
                equipRow.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = actor.Unit.GetItem(2)?.Name;
            }
            else
            {
                equipRow.transform.GetChild(2).gameObject.SetActive(false);
                equipRow.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = actor.Unit.GetItem(0)?.Name;
                equipRow.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = actor.Unit.GetItem(1)?.Name;
            }

            statRow1.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = actor.Unit.GetStatBase(Stat.Strength).ToString();
            statRow1.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = actor.Unit.GetStatBase(Stat.Dexterity).ToString();
            statRow2.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = actor.Unit.GetStatBase(Stat.Mind).ToString();
            statRow2.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = actor.Unit.GetStatBase(Stat.Will).ToString();
            statRow3.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = actor.Unit.GetStatBase(Stat.Endurance).ToString();
            statRow3.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = actor.Unit.GetStatBase(Stat.Agility).ToString();
            if (actor.PredatorComponent != null)
            {
                statRow4.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = actor.Unit.GetStatBase(Stat.Voracity).ToString();
                statRow4.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = actor.Unit.GetStatBase(Stat.Stomach).ToString();
            }
            else
                statRow4.SetActive(false);

            actor.UpdateBestWeapons();
            sprite.UpdateSprites(actor);
            sprite.Name.text = actor.Unit.Name;
            Button button = obj.GetComponentInChildren<Button>();
            button.onClick.AddListener(() =>
            {
                State.GameManager.SoundManager.PlaySpellCast(SpellList.Summon, actor);
                Reanimate(loc, actor, unit);
            });
            button.onClick.AddListener(() => _unitPickerUI.gameObject.SetActive(false));
        }

        _unitPickerUI.ActorFolder.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 300 * (1 + list.Length / 3));
        _unitPickerUI.gameObject.SetActive(true);
    }

    internal static void Resurrect(Vec2I loc, ActorUnit target)
    {
        var pred = FindPredator(target);
        if (pred != null) pred.PredatorComponent.FreeUnit(target, true);
        target.Position.X = loc.X;
        target.Position.Y = loc.Y;
        target.Unit.Health = target.Unit.MaxHealth * 3 / 4;
        target.Visible = true;
        target.Targetable = true;
        target.SelfPrey = null;
        target.Surrendered = false;
        UpdateActorLocations();
        if (target.UnitSprite != null)
        {
            target.UnitSprite.transform.rotation = Quaternion.Euler(0, 0, 0);
            target.UnitSprite.LevelText.gameObject.SetActive(true);
            target.UnitSprite.FlexibleSquare.gameObject.SetActive(true);
            target.UnitSprite.HealthBar.gameObject.SetActive(true);
        }
    }

    internal static void Reanimate(Vec2I loc, ActorUnit target, Unit caster)
    {
        var pred = FindPredator(target);
        if (pred != null) pred.PredatorComponent.FreeUnit(target, true);
        target.Position.X = loc.X;
        target.Position.Y = loc.Y;
        target.Unit.Health = target.Unit.MaxHealth * 3 / 4;
        target.Visible = true;
        target.Targetable = true;
        target.SelfPrey = null;
        target.Surrendered = false;
        target.SidesAttackedThisBattle = new List<Side>();
        target.Unit.Type = UnitType.Summon;
        State.GameManager.TacticalMode.HandleReanimationSideEffects(caster, target);
        if (!target.Unit.HasTrait(TraitType.Untamable)) target.Unit.FixedSide = caster.FixedSide;

        var actorCharm = caster.GetStatusEffect(StatusEffectType.Charmed) ?? caster.GetStatusEffect(StatusEffectType.Hypnotized);
        if (actorCharm != null)
        {
            target.Unit.ApplyStatusEffect(StatusEffectType.Charmed, actorCharm.Strength, actorCharm.Duration);
        }

        UpdateActorLocations();
        if (target.UnitSprite != null)
        {
            target.UnitSprite.transform.rotation = Quaternion.Euler(0, 0, 0);
            target.UnitSprite.LevelText.gameObject.SetActive(true);
            target.UnitSprite.FlexibleSquare.gameObject.SetActive(true);
            target.UnitSprite.HealthBar.gameObject.SetActive(true);
        }

        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{caster.Name}</b> brought back <b>{target.Unit.Name}</b> as a summon.");
    }

    internal static bool MeetsQualifier(List<AbilityTargets> targets, ActorUnit actor, ActorUnit target)
    {
        if (!Equals(target.Unit.GetApparentSide(), actor.Unit.FixedSide) && targets.Contains(AbilityTargets.Enemy)) return true;
        if ((Equals(target.Unit.GetApparentSide(), actor.Unit.GetApparentSide()) || Equals(target.Unit.GetApparentSide(), actor.Unit.FixedSide)) && targets.Contains(AbilityTargets.Ally)) return true;
        if ((Equals(target.Unit.GetApparentSide(), actor.Unit.GetApparentSide()) || Equals(target.Unit.GetApparentSide(), actor.Unit.FixedSide)) && target.Surrendered && targets.Contains(AbilityTargets.SurrenderedAlly)) return true;
        if (targets.Contains(AbilityTargets.Enemy) && Config.AllowInfighting) return true;
        if (targets.Contains(AbilityTargets.Self) && actor.Unit == target.Unit) return true;
        return false;
    }

    internal static ActorUnit GetActorAt(Vec2 location)
    {
        foreach (ActorUnit actor in Units)
        {
            if (actor == null) continue;
            if (actor.Position.X == location.X && actor.Position.Y == location.Y) return actor;
        }

        return null;
    }

    internal static ActorUnit GetActorOf(Unit unit)
    {
        return Units.FirstOrDefault(actor => actor.Unit == unit);
    }

    internal static void CreateEffect(Vec2I location, TileEffectType type, int areaOfEffect, float strength, int duration)
    {
        for (int x = location.X - areaOfEffect; x <= location.X + areaOfEffect; x++)
        {
            for (int y = location.Y - areaOfEffect; y <= location.Y + areaOfEffect; y++)
            {
                if (x < 0 || y < 0 || x > _tiles.GetUpperBound(0) || y > _tiles.GetUpperBound(1)) continue;
                Vec2 position = new Vec2(x, y);
                TileEffect effect = new TileEffect(duration, strength, type);
                State.GameManager.TacticalMode.ActiveEffects[position] = effect;
                switch (type)
                {
                    case TileEffectType.Fire:
                        State.GameManager.TacticalMode.EffectTileMap.SetTile(new Vector3Int(position.X, position.Y, 0), State.GameManager.TacticalMode.Pyre);
                        break;
                    case TileEffectType.IcePatch:
                        State.GameManager.TacticalMode.EffectTileMap.SetTile(new Vector3Int(position.X, position.Y, 0), State.GameManager.TacticalMode.Ice);
                        break;
                }
            }
        }
    }

    public static bool IsUnitControlledBySide(Unit unit, Side side)
    {
        if (!Equals(GetMindControlSide(unit), Side.TrueNoneSide)) // Charmed units may fight for a specific side, but for targeting purposes we'll consider them driven by separate forces
            return false;
        if (Equals(side, unit.FixedSide))
            return true;
        else if (State.GameManager.PureTactical) return false;
        if (unit.IsInfiltratingSide(side)) return true; // hidden and compliant
        return false;
    }

    public static bool PlayerCanSeeTrueSide(Unit unit)
    {
        if (!unit.HiddenFixedSide || Equals(unit.FixedSide, unit.Side)) return true;

        if (State.World.MainEmpires == null) return Equals(unit.FixedSide, !State.GameManager.TacticalMode.AIAttacker ? State.GameManager.TacticalMode.GetAttackerSide() : !State.GameManager.TacticalMode.AIDefender ? State.GameManager.TacticalMode.GetDefenderSide() : unit.FixedSide);

        if (StrategicUtilities.GetAllHumanSides().Count > 1) return false;
        if (StrategicUtilities.GetAllHumanSides().Count < 1) return true;


        if (Equals(State.GameManager.StrategyMode.LastHumanEmpire?.Side, unit.FixedSide)) return true;

        if (RelationsManager.GetRelation(unit.FixedSide, State.GameManager.StrategyMode.LastHumanEmpire.Side).Type == RelationState.Allied)
        {
            return true;
        }

        return false;
    }

    public static bool UnitCanSeeTrueSideOfTarget(Unit viewer, Unit target)
    {
        if (!target.HiddenFixedSide || Equals(target.FixedSide, target.Side)) return true;

        if (State.World.MainEmpires == null) return false;

        if (ReferenceEquals(target.FixedSide, viewer.FixedSide)) return true;

        if (RelationsManager.GetRelation(target.FixedSide, viewer.FixedSide).Type == RelationState.Allied)
        {
            return true;
        }
        // I was thinking about also giving units the insight of whatever side they're pretending to be on, but I think it' a good idea to "accidentally" kill "friendly" infiltrators
        // on the opposing side anyway, if you can get away with it. And you can, since by the current logic only obvious and direct betrayal uncovers someone's guise.
        // Well, and AOE, but that's due to cheese protection.

        return false;
    }

    internal static void ForceFeed(ActorUnit actor, ActorUnit targetPred)
    {
        float r = (float)State.Rand.NextDouble();
        if (targetPred.Unit.Predator)
        {
            PreyLocation preyLocation = PreyLocation.Stomach;
            var possibilities = new Dictionary<string, PreyLocation>();
            possibilities.Add("Maw", PreyLocation.Stomach);

            if (targetPred.Unit.CanAnalVore && State.RaceSettings.GetVoreTypes(targetPred.Unit.Race).Contains(VoreType.Anal)) possibilities.Add("Anus", PreyLocation.Anal);
            if (targetPred.Unit.CanBreastVore && State.RaceSettings.GetVoreTypes(targetPred.Unit.Race).Contains(VoreType.BreastVore)) possibilities.Add("Breast", PreyLocation.Breasts);
            if (targetPred.Unit.CanCockVore && State.RaceSettings.GetVoreTypes(targetPred.Unit.Race).Contains(VoreType.CockVore)) possibilities.Add("Cock", PreyLocation.Balls);
            if (targetPred.Unit.CanUnbirth && State.RaceSettings.GetVoreTypes(targetPred.Unit.Race).Contains(VoreType.Unbirth)) possibilities.Add("Pussy", PreyLocation.Womb);

            if (State.GameManager.TacticalMode.IsPlayerInControl && State.GameManager.CurrentScene == State.GameManager.TacticalMode && possibilities.Count > 1)
            {
                var box = State.GameManager.CreateOptionsBox();
                box.SetData($"Which way do you want to enter?", "Maw", () => targetPred.PredatorComponent.ForceConsume(actor, preyLocation), possibilities.Keys.ElementAtOrDefault(1), () => targetPred.PredatorComponent.ForceConsume(actor, possibilities.Values.ElementAtOrDefault(1)), possibilities.Keys.ElementAtOrDefault(2), () => targetPred.PredatorComponent.ForceConsume(actor, possibilities.Values.ElementAtOrDefault(2)), possibilities.Keys.ElementAtOrDefault(3), () => targetPred.PredatorComponent.ForceConsume(actor, possibilities.Values.ElementAtOrDefault(3)), possibilities.Keys.ElementAtOrDefault(4), () => targetPred.PredatorComponent.ForceConsume(actor, possibilities.Values.ElementAtOrDefault(4)));
                actor.Movement = 0;
            }
            else
            {
                preyLocation = possibilities.Values.ToList()[State.Rand.Next(possibilities.Count)];
                actor.Movement = 0;
                targetPred.PredatorComponent.ForceConsume(actor, preyLocation);
            }
        }
        else
        {
            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{actor.Unit.Name}</b> couldn't force feed {LogUtilities.GppHimself(actor.Unit)} to <b>{targetPred.Unit.Name}</b>.");
            actor.Movement = 0;
        }
    }

    internal static void AssumeForm(ActorUnit actor, ActorUnit target)
    {
        actor.ChangeRacePrey();
    }

    internal static void RevertForm(ActorUnit actor, ActorUnit target)
    {
        actor.RevertRace();
    }

    internal static void ShapeshifterPanel(ActorUnit selectedUnit)
    {
        //int children = UnitPickerUI.ActorFolder.transform.childCount;
        //for (int i = children - 1; i >= 0; i--)
        //{
        //    UnityEngine.Object.Destroy(UnitPickerUI.ActorFolder.transform.GetChild(i).gameObject);
        //}
        //foreach (Unit shape in selectedUnit.Unit.ShifterShapes)
        //{
        //    GameObject obj = UnityEngine.Object.Instantiate(UnitPickerUI.HiringUnitPanel, UnitPickerUI.ActorFolder);
        //    UIUnitSprite sprite = obj.GetComponentInChildren<UIUnitSprite>();
        //    Actor_Unit actor = new Actor_Unit(new Vec2i(0, 0), shape);
        //    sprite.UpdateSprites(actor);
        //    Text text = obj.transform.GetChild(3).GetComponent<Text>();
        //    text.text = 
        //        $"Items: {shape.GetItem(0)?.Name} {shape.GetItem(1)?.Name}" + (shape.HasTrait(Traits.Resourceful) ? $" { shape.GetItem(2)?.Name}" : "") + "\n" +
        //        $"Str: {shape.GetStatBase(Stat.Strength)} Dex: {shape.GetStatBase(Stat.Dexterity)} Agility: {shape.GetStatBase(Stat.Agility)}\n" +
        //        $"Mind: {shape.GetStatBase(Stat.Mind)} Will: {shape.GetStatBase(Stat.Will)} Endurance: {shape.GetStatBase(Stat.Endurance)}\n";
        //    if (shape.Predator)
        //        text.text += $"Vore: {shape.GetStatBase(Stat.Voracity)} Stomach: {shape.GetStatBase(Stat.Stomach)}";
        //    sprite.Name.text = InfoPanel.RaceSingular(shape);
        //    Button button = obj.GetComponentInChildren<Button>();
        //    button.GetComponentInChildren<Text>().text = "Transform";
        //    button.onClick.AddListener(() => selectedUnit.Shapeshift(shape));
        //    button.onClick.AddListener(() => UnitPickerUI.gameObject.SetActive(false));
        //}
        //UnitPickerUI.ActorFolder.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 300 * (1 + (selectedUnit.Unit.ShifterShapes.Count / 3)));
        //UnitPickerUI.GetComponentInChildren<HirePanel>().GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "Cancel";
        //UnitPickerUI.gameObject.SetActive(true);
    }

    internal static bool IsPreyEndoTargetForUnit(Prey preyUnit, Unit unit)
    {
        return unit.HasTrait(TraitType.Endosoma) && Equals(preyUnit.Unit.FixedSide, unit.GetApparentSide(preyUnit.Unit)) && preyUnit.Unit.IsDead == false;
    }
}