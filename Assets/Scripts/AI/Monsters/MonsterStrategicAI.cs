using OdinSerializer;
using System;
using System.Collections.Generic;
using System.Linq;


internal class MonsterStrategicAI : IStrategicAI
{
    [OdinSerialize]
    private MonsterEmpire _empire;

    private MonsterEmpire Empire { get => _empire; set => _empire = value; }

    private List<PathNode> _path;
    private Army _pathIsFor;

    public MonsterStrategicAI(MonsterEmpire empire)
    {
        this.Empire = empire;
    }

    public bool RunAI()
    {
        return GiveOrder();
    }

    internal bool GiveOrder()
    {
        foreach (Army army in Empire.Armies.ToList())
        {
            if (army.RemainingMp < 1) continue;
            if (_path != null && _pathIsFor == army)
            {
                if (_path.Count == 0)
                {
                    GenerateTaskForArmy(army);
                    return true;
                }

                Vec2I newLoc = new Vec2I(_path[0].X, _path[0].Y);
                Vec2I position = army.Position;
                _path.RemoveAt(0);

                if (army.MoveTo(newLoc))
                    StrategicUtilities.StartBattle(army);
                else if (position == army.Position) army.RemainingMp = 0; //This prevents the army from wasting time trying to move into a forest with 1 mp repeatedly
                return true;
            }
            else
            {
                GenerateTaskForArmy(army);
                if (_path == null || _path.Count == 0) army.RemainingMp = 0;
                return true;
            }
        }

        foreach (Army army in Empire.Armies)
        {
            foreach (Unit unit in army.Units)
            {
                StrategicUtilities.SpendLevelUps(unit);
            }
        }

        _path = null;
        return false;
    }

    public bool TurnAI()
    {
        SpawnerInfo spawner = Config.World.GetSpawner(Empire.Race);
        if (spawner == null) return false;
        Empire.MaxArmySize = spawner.MaxArmySize;
        Empire.Team = spawner.Team;
        int highestExp = State.GameManager.StrategyMode.ScaledExp;
        int baseXp = (int)(highestExp * spawner.ScalingFactor / 100);

        if (spawner.GetConquestType() == Config.MonsterConquestType.CompleteDevourAndRepopulateFortify)
            Empire.MaxGarrisonSize = spawner.MaxArmySize;
        else
            Empire.MaxGarrisonSize = 0;


        //if (empire.Gold < 10000) empire.AddGold(8000);
        for (int j = 0; j < spawner.SpawnAttempts; j++)
        {
            if (spawner.MaxArmies == 0 || (Config.NightMonsters && !State.World.IsNight)) break;
            if (Empire.Armies.Count() < (int)Math.Max(spawner.MaxArmies * Config.OverallMonsterCapModifier, 1) && State.Rand.NextDouble() < spawner.SpawnRate * Config.OverallMonsterSpawnRateModifier)
            {
                int x = 0;
                int y = 0;
                bool foundSpot = false;
                var spawners = State.GameManager.StrategyMode.Spawners.Where(s => Equals(s.Race, Empire.Race)).ToArray();
                for (int i = 0; i < 10; i++)
                {
                    if (spawners != null && spawners.Length > 0)
                    {
                        int num = State.Rand.Next(spawners.Length);
                        x = spawners[num].Location.X + State.Rand.Next(-2, 3);
                        y = spawners[num].Location.Y + State.Rand.Next(-2, 3);
                    }
                    else
                    {
                        x = State.Rand.Next(Config.StrategicWorldSizeX);
                        y = State.Rand.Next(Config.StrategicWorldSizeY);
                    }

                    if (StrategicUtilities.IsTileClear(new Vec2I(x, y)))
                    {
                        foundSpot = true;
                        break;
                    }
                }

                if (foundSpot == false) continue;
                var army = new Army(Empire, new Vec2I(x, y), Empire.Side);
                Empire.Armies.Add(army);

                army.RemainingMp = 0;

                int count;
                if (spawner.MinArmySize > spawner.MaxArmySize)
                    count = spawner.MaxArmySize;
                else
                    count = State.Rand.Next(spawner.MinArmySize, spawner.MaxArmySize + 1);

                if (count <= 0) continue;

                if (Equals(Empire.ReplacedRace, Race.Wyvern))
                {
                    for (int i = 0; i < count; i++)
                    {
                        if (spawner.AddOnRace && State.Rand.Next(4) == 0)
                            army.Units.Add(new Unit(Empire.Side, Race.YoungWyvern, RandXp(baseXp), true));
                        else
                            army.Units.Add(new Unit(Empire.Side, Race.Wyvern, RandXp(baseXp), true));
                    }
                }
                else if (Equals(Empire.ReplacedRace, Race.FeralShark))
                {
                    for (int i = 0; i < count; i++)
                    {
                        if (spawner.AddOnRace && State.Rand.Next(2) == 0)
                            army.Units.Add(new Unit(Empire.Side, Race.DarkSwallower, RandXp(baseXp), true));
                        else
                            army.Units.Add(new Unit(Empire.Side, Race.FeralShark, RandXp(baseXp), true));
                    }
                }
                else if (Equals(Empire.ReplacedRace, Race.Harvester))
                {
                    for (int i = 0; i < count; i++)
                    {
                        if (spawner.AddOnRace && State.Rand.Next(3) == 0)
                            army.Units.Add(new Unit(Empire.Side, Race.Collector, RandXp(baseXp), true));
                        else
                            army.Units.Add(new Unit(Empire.Side, Race.Harvester, RandXp(baseXp), true));
                    }
                }
                else if (Equals(Empire.ReplacedRace, Race.Dragon))
                {
                    army.Units.Add(new Unit(Empire.Side, Race.Dragon, RandXp(baseXp), true));
                    for (int i = 1; i < count; i++)
                    {
                        Unit unit = new Unit(Empire.Side, Race.Kobold, RandXp(baseXp), true);
                        if (unit.BestSuitedForRanged())
                            unit.Items[0] = State.World.ItemRepository.GetItem(ItemType.CompoundBow);
                        else
                            unit.Items[0] = State.World.ItemRepository.GetItem(ItemType.Axe);
                        army.Units.Add(unit);
                    }
                }
                else if (Equals(Empire.ReplacedRace, Race.RockSlug))
                {
                    const float rockFraction = .08f;
                    const float spitterFraction = .25f;
                    const float coralFraction = .25f;
                    int rockCount = Math.Max((int)(rockFraction * count), 1);
                    int spitterCount = Math.Max((int)(spitterFraction * count), 1);
                    int coralCount = Math.Max((int)(coralFraction * count), 1);
                    int springCount = count - rockCount - spitterCount - coralCount;
                    for (int i = 0; i < rockCount; i++) army.Units.Add(new Unit(Empire.Side, Race.RockSlug, RandXp(baseXp), true));
                    for (int i = 0; i < spitterCount; i++) army.Units.Add(new Unit(Empire.Side, Race.SpitterSlug, RandXp(baseXp), true));
                    for (int i = 0; i < coralCount; i++) army.Units.Add(new Unit(Empire.Side, Race.CoralSlug, RandXp(baseXp), true));
                    for (int i = 0; i < springCount; i++) army.Units.Add(new Unit(Empire.Side, Race.SpringSlug, RandXp(baseXp), true));
                }
                else if (Equals(Empire.ReplacedRace, Race.Compy))
                {
                    for (int i = 0; i < count; i++)
                    {
                        if (spawner.AddOnRace && State.Rand.Next(2) == 0)
                            army.Units.Add(new Unit(Empire.Side, Race.Raptor, RandXp(baseXp), true));
                        else
                            army.Units.Add(new Unit(Empire.Side, Race.Compy, RandXp(baseXp), true));
                    }
                }
                else if (Equals(Empire.ReplacedRace, Race.Monitor))
                {
                    for (int i = 0; i < count; i++)
                    {
                        if (spawner.AddOnRace && State.Rand.Next(5) == 0)
                            army.Units.Add(new Unit(Empire.Side, Race.Komodo, RandXp(baseXp), true));
                        else
                            army.Units.Add(new Unit(Empire.Side, Race.Monitor, RandXp(baseXp), true));
                    }
                }
                else if (Equals(Empire.ReplacedRace, Race.FeralLion))
                {
                    army.Units.Add(new Leader(Empire.Side, Race.FeralLion, RandXp(baseXp * 2)));
                    for (int i = 1; i < count; i++)
                    {
                        army.Units.Add(new Unit(Empire.Side, Race.FeralLion, RandXp(baseXp), true));
                    }
                }
                else
                {
                    for (int i = 0; i < count; i++)
                    {
                        army.Units.Add(new Unit(Empire.Side, Empire.ReplacedRace, RandXp(baseXp), true));
                    }
                }

                if (Config.MonstersDropSpells)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        if (State.Rand.Next(3) == 0)
                        {
                            if (army.Units[0].Level < 3)
                                army.ItemStock.AddItem((ItemType)State.World.ItemRepository.GetRandomBookType(1, 2));
                            else if (army.Units[0].Level < 5)
                                army.ItemStock.AddItem((ItemType)State.World.ItemRepository.GetRandomBookType(1, 3));
                            else if (army.Units[0].Level < 7)
                                army.ItemStock.AddItem((ItemType)State.World.ItemRepository.GetRandomBookType(1, 4));
                            else if (army.Units[0].Level < 9)
                                army.ItemStock.AddItem((ItemType)State.World.ItemRepository.GetRandomBookType(2, 4));
                            else
                                army.ItemStock.AddItem((ItemType)State.World.ItemRepository.GetRandomBookType(2, 4));
                        }
                    }
                }


                foreach (Unit unit in army.Units)
                {
                    StrategicUtilities.SetAIClass(unit);
                }
            }
        }


        foreach (Army army in Empire.Armies)
        {
            if (army.Units.Count == 0) continue;
            foreach (Unit unit in army.Units)
            {
                if (unit.Experience < .5f * baseXp)
                {
                    unit.SetExp(3 + unit.Experience * 1.1f);
                }

                StrategicUtilities.SpendLevelUps(unit);
            }

            if (army.Units.Count > 0 && army.InVillageIndex != -1 && Equals(State.World.Villages[army.InVillageIndex].Race, army.Units[0].Race))
            {
                Village village = State.World.Villages[army.InVillageIndex];
                for (int i = 0; i < 8; i++)
                {
                    if (army.Units.Count() >= spawner.MaxArmySize || village.Population < 5) break;
                    army.Units.Add(new Unit(Empire.Side, Empire.ReplacedRace, RandXp(baseXp), true));
                    village.SubtractPopulation(1);
                }
            }
        }

        return true;

        int RandXp(int exp)
        {
            if (exp < 1) exp = 1;
            return (int)(exp * .8f) + State.Rand.Next(10 + (int)(exp * .4));
        }
    }


    private void GenerateTaskForArmy(Army army)
    {
        _pathIsFor = army;

        SpawnerInfo spawner = Config.World.GetSpawner(Empire.Race);
        Config.MonsterConquestType spawnerType;
        if (spawner != null)
            spawnerType = spawner.GetConquestType();
        else
            spawnerType = Config.MonsterConquest;

        if (army.InVillageIndex != -1)
        {
            bool inAlliedVillage = false;
            bool inEnemyVillage = false;
            bool inOwnVillage = false;
            if (State.World.Villages[army.InVillageIndex].Empire.IsEnemy(Empire))
                inEnemyVillage = true;
            else if (Equals(State.World.Villages[army.InVillageIndex].Side, Empire.Side))
                inOwnVillage = true;
            else
                inAlliedVillage = true;

            if ((spawnerType != Config.MonsterConquestType.CompleteDevourAndMoveOn || State.World.Villages[army.InVillageIndex].Population > 0) &&
                (inEnemyVillage || (inOwnVillage && army.Units.Where(s => Equals(s.Race, State.World.Villages[army.InVillageIndex].Race)).Any() == false)))
            {
                if (spawnerType == Config.MonsterConquestType.CompleteDevourAndMoveOn || spawnerType == Config.MonsterConquestType.CompleteDevourAndRepopulate || spawnerType == Config.MonsterConquestType.CompleteDevourAndRepopulateFortify || spawnerType == Config.MonsterConquestType.CompleteDevourAndHold)
                {
                    if (army.MonsterTurnsRemaining <= 1)
                        State.GameManager.StrategyMode.Devour(army, State.World.Villages[army.InVillageIndex].GetTotalPop());
                    else
                    {
                        State.GameManager.StrategyMode.Devour(army, State.World.Villages[army.InVillageIndex].GetTotalPop() / army.MonsterTurnsRemaining);
                        army.MonsterTurnsRemaining--;
                    }
                }

                if (spawnerType == Config.MonsterConquestType.DevourAndHold)
                {
                    Village village = State.World.Villages[army.InVillageIndex];
                    if (village.GetTotalPop() > village.Maxpop / 2)
                    {
                        State.GameManager.StrategyMode.Devour(army, village.GetTotalPop() - village.Maxpop / 2);
                    }
                }

                army.RemainingMp = 0;
                return;
            }

            if (inAlliedVillage == false && (spawnerType == Config.MonsterConquestType.CompleteDevourAndRepopulate || spawnerType == Config.MonsterConquestType.CompleteDevourAndRepopulateFortify))
            {
                army.RemainingMp = 0;
                return;
            }
        }

        if (Config.NightMoveMonsters && !State.World.IsNight) //DayNight Modification (zero's out monster AP when NOT night)
        {
            army.RemainingMp = 0;
            return;
        }

        Attack(army, spawner.Confidence);
    }


    private void Attack(Army army, float maxDefenderStrength)
    {
        Village[] villages = State.World.Villages;

        List<Vec2I> potentialTargets = new List<Vec2I>();
        List<int> potentialTargetValue = new List<int>();

        foreach (Army hostileArmy in StrategicUtilities.GetAllHostileArmies(Empire, true))
        {
            if (!hostileArmy.Units.All(u => u.HasTrait(TraitType.Infiltrator)) && StrategicUtilities.ArmyPower(hostileArmy) < maxDefenderStrength * StrategicUtilities.ArmyPower(army) && hostileArmy.InVillageIndex == -1)
            {
                potentialTargets.Add(hostileArmy.Position);
                potentialTargetValue.Add(0);
            }
        }

        SpawnerInfo spawner = Config.World.GetSpawner(Empire.Race);
        Config.MonsterConquestType spawnerType;
        if (spawner != null)
            spawnerType = spawner.GetConquestType();
        else
            spawnerType = Config.MonsterConquest;
        if (spawnerType == Config.MonsterConquestType.IgnoreTowns)
        {
            SetClosestMonsterPath(army, potentialTargets.ToArray());
            return;
        }

        for (int i = 0; i < State.World.Villages.Length; i++)
        {
            if (villages[i].Empire.IsEnemy(Empire))
            {
                if (StrategicUtilities.TileThreat(villages[i].Position) < maxDefenderStrength * StrategicUtilities.ArmyPower(army) && villages[i].Population > 0)
                {
                    potentialTargets.Add(villages[i].Position);
                    potentialTargetValue.Add(-8);
                }
            }
        }

        SetClosestPathWithPriority(army, potentialTargets.ToArray(), potentialTargetValue.ToArray());
    }

    private void SetPath(Army army, Vec2I targetPosition)
    {
        if (targetPosition != null)
        {
            _path = StrategyPathfinder.GetArmyPath(Empire, army, targetPosition, army.RemainingMp, army.MovementMode == MovementMode.Flight);
            return;
        }

        army.RemainingMp = 0;
    }

    private void SetClosestMonsterPath(Army army, Vec2I[] targetPositions, int maxDistance = 999)
    {
        if (targetPositions != null && targetPositions.Length > 1)
        {
            _path = StrategyPathfinder.GetMonsterPathToClosestObject(Empire, army, targetPositions, army.RemainingMp, maxDistance, army.MovementMode == MovementMode.Flight);
            return;
        }
        else if (targetPositions.Length == 1)
        {
            if (targetPositions[0] != null)
            {
                _path = StrategyPathfinder.GetMonsterPath(Empire, army, targetPositions[0], army.RemainingMp, army.MovementMode == MovementMode.Flight);
                return;
            }

            army.RemainingMp = 0;
        }
        else
            army.RemainingMp = 0;
    }

    private void SetClosestPathWithPriority(Army army, Vec2I[] targetPositions, int[] targetPriorities, int maxDistance = 999)
    {
        if (targetPositions != null && targetPositions.Length > 1)
        {
            _path = StrategyPathfinder.GetPathToClosestObject(Empire, army, targetPositions, army.RemainingMp, maxDistance, army.MovementMode == MovementMode.Flight, targetPriorities);
            return;
        }
        else if (targetPositions.Length == 1)
        {
            SetPath(army, targetPositions[0]);
        }
        else
            army.RemainingMp = 0;
    }
}