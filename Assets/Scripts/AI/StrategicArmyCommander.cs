using System;
using System.Collections.Generic;
using System.Linq;

public enum AIMode
{
    Default,
    Sneak,
    Heal,
    Resupply,
    //HuntStrong,
    //HeavyTraining
}


internal class StrategicArmyCommander
{
    private Village[] Villages => State.World.Villages;
    private Empire _empire;
    private int _maxArmySize;

    private bool _smarterAI;

    private List<PathNode> _path;
    private Army _pathIsFor;

    internal float StrongestArmyRatio { get; private set; }

    private Side AISide => _empire.Side;

    public StrategicArmyCommander(Empire empire, int maxSize, bool smarterAI)
    {
        this._empire = empire;
        _maxArmySize = maxSize;
        this._smarterAI = smarterAI;
    }

    internal void UpdateStrongestArmyRatio()
    {
        if (_empire.Armies.Count < 2) return;
        var strongestArmy = _empire.Armies.OrderByDescending(s => s.ArmyPower).FirstOrDefault();
        if (strongestArmy == null) return;

        var strongestEnemy = StrategicUtilities.GetAllHostileArmies(_empire).OrderByDescending(s => s.ArmyPower).FirstOrDefault();
        if (strongestEnemy != null)
        {
            StrongestArmyRatio = (float)(strongestArmy.ArmyPower / strongestEnemy.ArmyPower);
        }
    }

    internal void ResetPath() => _pathIsFor = null; //If there is only one army, this forces it to generate a new path each turn

    internal bool GiveOrder()
    {
        foreach (Army army in _empire.Armies.ToList())
        {
            if (army.RemainingMp < 1) continue;
            DevourCheck(army);
            if (army.RemainingMp < 1) continue;
            if (_path != null && _pathIsFor == army)
            {
                if (army.InVillageIndex > -1)
                {
                    UpdateEquipmentAndRecruit(army);
                }

                if (_path.Count == 0)
                {
                    GenerateTaskForArmy(army);
                    return true;
                }

                Vec2I newLoc = new Vec2I(_path[0].X, _path[0].Y);
                Vec2I position = army.Position;
#if UNITY_EDITOR
                if (newLoc.GetNumberOfMovesDistance(position) != 1)
                {
                    UnityEngine.Debug.LogWarning($"Army tried to move from {position.X} {position.Y} to {newLoc.X} {newLoc.Y}");
                }

#endif
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

        SpendExpAndRecruit(); //At the end of the turn, restock troops
        return false;
    }

    internal void SpendExpAndRecruit()
    {
        foreach (Army army in _empire.Armies)
        {
            var infiltrators = new List<Unit>();
            foreach (Unit unit in army.Units)
            {
                StrategicUtilities.SpendLevelUps(unit);
                if (unit.HasTrait(TraitType.Infiltrator) && unit.Type != UnitType.Leader && Equals(unit.FixedSide, army.Side)) infiltrators.Add(unit);
            }

            infiltrators.ForEach(u => StrategicUtilities.TryInfiltrateRandom(army, u));

            if (army.InVillageIndex > -1)
            {
                UpdateEquipmentAndRecruit(army);
            }

            if (army.AIMode == AIMode.Resupply && army.Units.Count == _maxArmySize && StrategicUtilities.NumberOfDesiredUpgrades(army) == 0) army.AIMode = AIMode.Default;
        }
    }


    private void GenerateTaskForArmy(Army army)
    {
        _path = null;
        _pathIsFor = army;
        UpdateArmyStatus(army);
        switch (army.AIMode)
        {
            case AIMode.Default:
                Attack(army, 1.6f);
                break;
            case AIMode.Sneak:
                Attack(army, 0.8f);
                break;
            case AIMode.Heal:
                if (army.InVillageIndex == -1)
                {
                    if (NavigateToFriendlyVillage(army, false)) break;
                    Attack(army, 1);
                    break;
                }
                else
                {
                    DevourCheck(army);
                    army.RemainingMp = 0;
                    break;
                }
            case AIMode.Resupply:
                if (_empire.Income < 20) army.AIMode = AIMode.Default;

                Village villageArmyIsIn = null;
                if (army.InVillageIndex != -1)
                {
                    villageArmyIsIn = State.World.Villages[army.InVillageIndex];
                }


                if (army.InVillageIndex != -1 && _empire.Gold > 4500 && Config.AICanHireSpecialMercs && MercenaryHouse.UniqueMercs.Count > 0 && army.Units.Count() < _empire.MaxArmySize && NavigateToMercenaries(army, (int)(3f * army.GetMaxMovement())))
                {
                    break;
                }

                if (army.InVillageIndex != -1 && _empire.Gold > 1500 && army.Units.Count() < _empire.MaxArmySize && NavigateToMercenaries(army, (int)(2f * army.GetMaxMovement())))
                {
                    break;
                }


                if (army.InVillageIndex != -1 && _empire.Gold > 500 && army.Units.Count() < _empire.MaxArmySize && NavigateToMercenaries(army, (int)(1f * army.GetMaxMovement())))
                {
                    break;
                }

                MercenaryHouse mercHouseArmyIsIn = StrategicUtilities.GetMercenaryHouseAt(army.Position);
                if (mercHouseArmyIsIn != null)
                {
                    if (Config.AICanHireSpecialMercs)
                    {
                        foreach (var merc in MercenaryHouse.UniqueMercs.OrderByDescending(s => s.Cost))
                        {
                            HireSpecialMerc(army, merc);
                        }
                    }

                    foreach (var merc in mercHouseArmyIsIn.Mercenaries.OrderByDescending(s => s.Unit.Experience / s.Cost))
                    {
                        HireMerc(army, mercHouseArmyIsIn, merc);
                    }
                }

                if (villageArmyIsIn == null || villageArmyIsIn.GetTotalPop() < 12)
                {
                    if (NavigateToFriendlyVillage(army, army.Units.Count != _maxArmySize)) break;
                    Attack(army, 1);
                    break;
                }

                UpdateEquipmentAndRecruit(army);
                if (army.Units.Count == _maxArmySize && StrategicUtilities.NumberOfDesiredUpgrades(army) == 0)
                    army.AIMode = AIMode.Default;
                else
                    army.RemainingMp = 0;
                break;
            //       case AIMode.HuntStrong:
            //           {
            //               AttackStrongestArmy(army);
            //               break;
            //           }
            //       case AIMode.HeavyTraining:
            //           {
            //if (army.InVillageIndex > -1)
            //{
            //	Village trainingVillage = State.World.Villages[army.InVillageIndex];
            //	var maxTrainLevel = trainingVillage.NetBoosts.MaximumTrainingLevelAdd;
            //	if (trainingVillage != null && maxTrainLevel > 0)
            //	{
            //		Train(army);
            //		double highestPower = 0;
            //		foreach (Army hostileArmy in StrategicUtilities.GetAllHostileArmies(AITeam))
            //		{
            //			double p = hostileArmy.ArmyPower;
            //			if (p > highestPower)
            //			{
            //				highestPower = p;
            //			}

            //		}
            //		if (highestPower * 1.3f + 256 < army.ArmyPower)
            //			army.AIMode = AIMode.HuntStrong;
            //	}
            //	else
            //		NavigateToTrainArmy(army);
            //}
            //               break;
            //           }
        }
    }

    private void HireSpecialMerc(Army army, MercenaryContainer merc)
    {
        if (_empire.Gold >= merc.Cost * 2)
        {
            if (army.Units.Count < army.MaxSize)
            {
                army.Units.Add(merc.Unit);
                merc.Unit.Side = army.Side;
                _empire.SpendGold(merc.Cost);
                MercenaryHouse.UniqueMercs.Remove(merc);
            }
        }
    }

    private void HireMerc(Army army, MercenaryHouse house, MercenaryContainer merc)
    {
        if (_empire.Gold >= merc.Cost)
        {
            if (army.Units.Count < army.MaxSize)
            {
                army.Units.Add(merc.Unit);
                merc.Unit.Side = army.Side;
                _empire.SpendGold(merc.Cost);
                house.Mercenaries.Remove(merc);
                MercenaryHouse.UniqueMercs.Remove(merc);
            }
        }
    }

    //void Train(Army army)
    //{
    //    if (army.RemainingMP > 0)
    //    {
    //        for (int i = 5 - 1; i >= 0; i--)
    //        {
    //var trainingCost = army.TrainingGetCost(i);
    //            if (empire.Gold > trainingCost)
    //            {
    //                State.World.Stats.SpentGoldOnArmyTraining(trainingCost, empire.Side);
    //                army.Train(i);
    //                army.RemainingMP = 0;
    //                return;
    //            }
    //        }

    //    }
    //}

    //void NavigateToTrainArmy(Army army)
    //{
    //    Vec2i[] locations = State.World.Villages.Where(s => s.Side == army.Side && s.NetBoosts.MaximumTrainingLevelAdd > 0).Select(s => s.Position).ToArray();
    //    if (locations != null && locations.Length > 0)
    //    {
    //        SetClosestPath(army, locations);
    //    }
    //    else
    //    {
    //        army.AIMode = AIMode.Sneak;
    //        return;
    //    }


    //}

    //void AttackStrongestArmy(Army army)
    //{
    //    Vec2i targetPosition = null;
    //    double highestPower = 0;
    //    foreach (Army hostileArmy in StrategicUtilities.GetAllHostileArmies(AITeam))
    //    {
    //        double p = hostileArmy.ArmyPower;
    //        if (p > highestPower)
    //        {
    //            highestPower = p;
    //            targetPosition = hostileArmy.Position;
    //        }
    //        if (p > army.ArmyPower * 1.1f)
    //            army.AIMode = AIMode.HeavyTraining;
    //    }

    //    SetPath(army, targetPosition);
    //}

    private void Attack(Army army, float maxDefenderStrength)
    {
        foreach (Army hostileArmy in StrategicUtilities.GetAllHostileArmies(_empire).Where(s => s.ArmyPower > 2 * army.ArmyPower).Where(s => s.Position.GetNumberOfMovesDistance(army.Position) < 4 && !s.Units.All(u => u.HasTrait(TraitType.Infiltrator))))
        {
            Vec2I[] closeVillagePositions = Villages.Where(s => s.Position.GetNumberOfMovesDistance(army.Position) < 7 && StrategicUtilities.ArmyAt(s.Position) == null).Select(s => s.Position).ToArray();
            if (closeVillagePositions != null && closeVillagePositions.Length > 0)
            {
                int oldMp = army.RemainingMp; //If there's no close town, then ignore it, instead of eating remaining MP
                SetClosestPath(army, closeVillagePositions, 6);
                if (_path != null) return;
                army.RemainingMp = oldMp;
            }
        }

        Vec2I capitalPosition = _empire.CapitalCity?.Position ?? army.Position; //Shouldn't really ever be null, but just in case

        List<Vec2I> potentialTargets = new List<Vec2I>();
        List<int> potentialTargetValue = new List<int>();

        for (int i = 0; i < State.World.Villages.Length; i++)
        {
            if (Villages[i].Empire.IsEnemy(_empire))
            {
                if (StrategicUtilities.TileThreat(Villages[i].Position) < maxDefenderStrength * StrategicUtilities.ArmyPower(army))
                {
                    potentialTargets.Add(Villages[i].Position);
                    int value = Equals(Villages[i].Race, _empire.ReplacedRace) ? 45 : State.World.GetEmpireOfRace(Villages[i].Race)?.IsAlly(_empire) ?? false ? 40 : 35;
                    if (Villages[i].GetTotalPop() == 0) value = 30;
                    value -= Villages[i].Position.GetNumberOfMovesDistance(capitalPosition) / 3;
                    potentialTargetValue.Add(value);
                }
            }
        }

        foreach (Army hostileArmy in StrategicUtilities.GetAllHostileArmies(_empire))
        {
            if (!hostileArmy.Units.All(u => u.HasTrait(TraitType.Infiltrator)) && StrategicUtilities.ArmyPower(hostileArmy) < maxDefenderStrength * StrategicUtilities.ArmyPower(army) && hostileArmy.InVillageIndex == -1)
            {
                potentialTargets.Add(hostileArmy.Position);
                if (RaceFuncs.IsMonstersOrUniqueMercsOrRebelsOrBandits(hostileArmy.Side) || Equals(hostileArmy.Side, Race.Goblin.ToSide())) //If Monster
                    potentialTargetValue.Add(12);
                else
                    potentialTargetValue.Add(42 - hostileArmy.Position.GetNumberOfMovesDistance(capitalPosition) / 3);
            }
        }

        foreach (ClaimableBuilding claimable in State.World.Claimables)
        {
            if (claimable.Owner == null || _empire.IsEnemy(claimable.Owner))
            {
                Army defender = StrategicUtilities.ArmyAt(claimable.Position);
                if (defender != null && StrategicUtilities.ArmyPower(defender) > maxDefenderStrength * StrategicUtilities.ArmyPower(army)) continue;
                potentialTargets.Add(claimable.Position);
                int value = 38;
                value -= claimable.Position.GetNumberOfMovesDistance(capitalPosition) / 3;
                potentialTargetValue.Add(value);
            }
        }

        SetClosestPathWithPriority(army, potentialTargets.ToArray(), potentialTargetValue.ToArray());
    }


    private void UpdateArmyStatus(Army army)
    {
        var healthPct = army.GetHealthPercentage();
        if (healthPct < 60) army.AIMode = AIMode.Heal;

        if (army.InVillageIndex != -1 && healthPct < 80) army.AIMode = AIMode.Heal;

        if (army.AIMode == AIMode.Heal && healthPct > 95)
            if ((army.InVillageIndex > -1 && StrategicUtilities.NumberOfDesiredUpgrades(army) > 0) == false)
                army.AIMode = AIMode.Default;

        float need = 32 * (((float)_maxArmySize - army.Units.Count()) / _maxArmySize) + StrategicUtilities.NumberOfDesiredUpgrades(army);
        if (need > 4 && _empire.Gold >= 40 && _empire.Income > 25)
        {
            var path = StrategyPathfinder.GetPathToClosestObject(_empire, army, Villages.Where(s => Equals(s.Side, army.Side)).Select(s => s.Position).ToArray(), army.RemainingMp, 5, army.MovementMode == MovementMode.Flight);
            if (path != null && path.Count() < need / 2) army.AIMode = AIMode.Resupply;
        }
    }

    private bool NavigateToMercenaries(Army army, int maxRange)
    {
        Vec2I[] mercPositions = StrategicUtilities.GetUnoccupiedMercCamp(_empire).Select(s => s.Position).ToArray();
        if (mercPositions == null || mercPositions.Count() < 1)
        {
            return false;
        }
        else
        {
            SetClosestPath(army, mercPositions, maxRange);
            if (_path == null) return false;
            return true;
        }
    }


    private bool NavigateToFriendlyVillage(Army army, bool canRecruitFrom)
    {
        Vec2I[] villagePositions = StrategicUtilities.GetUnoccupiedFriendlyVillages(_empire).Select(s => s.Position).ToArray();
        if (villagePositions == null || villagePositions.Count() < 1)
        {
            return false;
        }
        else
        {
            SetClosestPath(army, villagePositions);
            return true;
        }
    }

    private void DevourCheck(Army army)
    {
        if (army.GetHealthPercentage() > 88) return;
        if (army.Units.Where(s => s.Predator && 100 * s.HealthPct < 70).Any() == false) return;
        if (army.RemainingMp < 1) return;
        if (army.InVillageIndex > -1)
        {
            Village village = State.World.Villages[army.InVillageIndex];
            int range;
            int minimumheal;
            //Could check the relative strength but it's probably fine for now.
            if (village.Empire.IsAlly(_empire))
            {
                minimumheal = 7;
                range = 8;
                if (village.GetTotalPop() < 22)
                {
                    minimumheal = 9;
                }
            }
            else
            {
                minimumheal = 2;
                range = 20;
            }

            if (StrategicUtilities.EnemyArmyWithinXTiles(army, range))
            {
                State.GameManager.StrategyMode.Devour(army, Math.Min(army.GetDevourmentCapacity(minimumheal), village.GetTotalPop() - 3)); //Don't completely devour villages
            }
        }
    }

    private void UpdateEquipmentAndRecruit(Army army)
    {
        Village village = State.World.Villages[army.InVillageIndex];
        army.ItemStock.SellAllWeaponsAndAccessories(_empire);
        StrategicUtilities.UpgradeUnitsIfAtLeastLevel(army, village, 4);
        if (army.Units.Count != _maxArmySize)
        {
            int goldPerTroop = _empire.Gold / (_maxArmySize - army.Units.Count());
            for (int i = 0; i < _maxArmySize; i++)
            {
                if (_smarterAI && _empire.Gold > 40)
                    RecruitUnitAndEquip(army, village, 2);
                else if (goldPerTroop > 40 && army.Units.Count < _maxArmySize && village.GetTotalPop() > 3 && _empire.Income > 15)
                    RecruitUnitAndEquip(army, village, 2);
                else if (_empire.Gold > 16 && army.Units.Count < _maxArmySize && village.GetTotalPop() > 3 && _empire.Income > 5)
                    RecruitUnitAndEquip(army, village, 1);
                else
                    break;
            }

            if (army.AIMode == AIMode.Resupply && army.Units.Count() == _maxArmySize) army.AIMode = AIMode.Default;
        }
    }


    internal Unit RecruitUnitAndEquip(Army army, Village village, int tier)
    {
        if (village.GetTotalPop() < 4) return null;
        if (army.Units.Count >= army.MaxSize) return null;
        if (_empire.Leader?.Health <= 0) return ResurrectLeader(army, village);
        if (tier == 2 && _empire.Gold < Config.ArmyCost + State.World.ItemRepository.GetItem(ItemType.CompoundBow).Cost) return null;
        if (tier == 1 && _empire.Gold < Config.ArmyCost + State.World.ItemRepository.GetItem(ItemType.Bow).Cost) return null;
        Unit unit = village.RecruitAIUnit(_empire, army);
        if (unit == null) //Catches army size
            return null;
        if (unit.HasTrait(TraitType.Infiltrator) && !unit.IsInfiltratingSide(unit.Side))
        {
            unit.OnDiscard = () =>
            {
                village.VillagePopulation.AddHireable(unit);
                UnityEngine.Debug.Log(unit.Name + " is returning to " + village.Name);
            };
        }

        if (unit.FixedGear == false)
        {
            if (unit.HasTrait(TraitType.Feral))
            {
                Shop.BuyItem(_empire, unit, State.World.ItemRepository.GetItem(ItemType.Gauntlet));
            }
            else if (unit.Items[0] == null)
            {
                if (tier == 1)
                {
                    if (unit.BestSuitedForRanged())
                        Shop.BuyItem(_empire, unit, State.World.ItemRepository.GetItem(ItemType.Bow));
                    else
                        Shop.BuyItem(_empire, unit, State.World.ItemRepository.GetItem(ItemType.Mace));
                }
                else if (tier == 2)
                {
                    if (unit.BestSuitedForRanged())
                        Shop.BuyItem(_empire, unit, State.World.ItemRepository.GetItem(ItemType.CompoundBow));
                    else
                        Shop.BuyItem(_empire, unit, State.World.ItemRepository.GetItem(ItemType.Axe));
                }
            }
        }

        StrategicUtilities.SetAIClass(unit, .1f);

        StrategicUtilities.SpendLevelUps(unit);
        army.RefreshMovementMode();
        return unit;
    }

    private Unit ResurrectLeader(Army army, Village village)
    {
        _empire.SpendGold(100);
        _empire.Leader.Side = AISide;
        _empire.Leader.FixedSide = AISide;
        _empire.Leader.Type = UnitType.Leader;
        _empire.Leader.LeaderLevelDown();
        _empire.Leader.Health = _empire.Leader.MaxHealth;
        if (village.GetStartingXp() > _empire.Leader.Experience)
        {
            _empire.Leader.SetExp(village.GetStartingXp());
            StrategicUtilities.SpendLevelUps(_empire.Leader);
        }

        army.Units.Add(_empire.Leader);
        army.RefreshMovementMode();
        State.World.Stats.ResurrectedLeader(_empire.Side);
        if (Config.LeadersRerandomizeOnDeath)
        {
            _empire.Leader.TotalRandomizeAppearance();
            _empire.Leader.ReloadTraits();
            _empire.Leader.InitializeTraits();
        }

        return _empire.Leader;
    }

    private void SetPath(Army army, Vec2I targetPosition, int maxDistance)
    {
        if (targetPosition != null)
        {
            _path = StrategyPathfinder.GetArmyPath(_empire, army, targetPosition, army.RemainingMp, army.MovementMode == MovementMode.Flight, maxDistance);
            return;
        }

        army.RemainingMp = 0;
    }

    private void SetClosestPath(Army army, Vec2I[] targetPositions, int maxDistance = 999)
    {
        if (targetPositions != null && targetPositions.Length > 1)
        {
            _path = StrategyPathfinder.GetPathToClosestObject(_empire, army, targetPositions, army.RemainingMp, maxDistance, army.MovementMode == MovementMode.Flight);
            return;
        }
        else if (targetPositions.Length == 1)
        {
            SetPath(army, targetPositions[0], maxDistance);
        }
        else
            army.RemainingMp = 0;
    }

    private void SetClosestPathWithPriority(Army army, Vec2I[] targetPositions, int[] targetPriorities, int maxDistance = 999)
    {
        if (targetPositions != null && targetPositions.Length > 1)
        {
            _path = StrategyPathfinder.GetPathToClosestObject(_empire, army, targetPositions, army.RemainingMp, maxDistance, army.MovementMode == MovementMode.Flight, targetPriorities);
            return;
        }
        else if (targetPositions.Length == 1)
        {
            SetPath(army, targetPositions[0], maxDistance);
        }
        else
            army.RemainingMp = 0;
    }
}