using OdinSerializer;
using System.Collections.Generic;
using System.Linq;

internal class GoblinAI : IStrategicAI
{
    [OdinSerialize]
    private Empire _empire;

    private Empire Empire { get => _empire; set => _empire = value; }

    private List<PathNode> _path;
    private Army _pathIsFor;

    public GoblinAI(Empire empire)
    {
        this.Empire = empire;
    }

    public bool RunAI()
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
                if (StrategicUtilities.ArmyAt(newLoc) != null)
                {
                    _path = null;
                    army.RemainingMp = 0;
                    continue;
                }

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
        foreach (Army army in Empire.Armies)
        {
            var closeVillages = State.World.Villages.Where(s => s.Position.GetNumberOfMovesDistance(army.Position) < 4 && s.GetTotalPop() > 0);
            foreach (Village village in closeVillages)
            {
                var emp = State.World.GetEmpireOfSide(village.Side);
                if (emp != null) emp.AddGold(10);
            }
        }

        return false;
    }

    public bool TurnAI()
    {
        if (Config.GoblinCaravans == false)
        {
            Empire.SpendGold(Empire.Gold);
            return false;
        }

        bool foundSpot = false;
        int highestExp = State.GameManager.StrategyMode.ScaledExp;
        int baseXp = (int)(highestExp * .6f);
        if (Empire.Gold < 10000) Empire.AddGold(10000);
        double mapFactor = (Config.StrategicWorldSizeX + Config.StrategicWorldSizeY) / 20;

        if (State.Rand.NextDouble() < (mapFactor - Empire.Armies.Count) / 10)
        {
            int x = 0;
            int y = 0;

            for (int i = 0; i < 10; i++)
            {
                x = State.Rand.Next(Config.StrategicWorldSizeX);
                y = State.Rand.Next(Config.StrategicWorldSizeY);

                if (StrategicUtilities.IsTileClear(new Vec2I(x, y)))
                {
                    foundSpot = true;
                    break;
                }
            }

            if (foundSpot)
            {
                var army = new Army(Empire, new Vec2I(x, y), Empire.Side);
                Empire.Armies.Add(army);

                int num = 0;
                int average = 0;
                foreach (Empire emp in State.World.MainEmpires.Where(s => RaceFuncs.IsMainRaceOrMerc(s.Side) && s.KnockedOut == false))
                {
                    num++;
                    average += emp.MaxArmySize;
                }

                int count = 0;
                if (num > 0)
                {
                    count = State.Rand.Next(3 * average / num / 4, average / num);
                }
                else
                    count = State.Rand.Next(12, 16);

                army.BountyGoods = new BountyGoods((int)(15 * count * (.75f + State.Rand.NextDouble() / 2)));

                for (int i = 0; i < count; i++)
                {
                    Unit unit = new NpcUnit(1, State.Rand.Next(2) == 0, 2, Empire.Side, Empire.ReplacedRace, RandXp(baseXp), true);
                    if (State.Rand.Next(4) == 0)
                    {
                        if (unit.Level < 3)
                            unit.SetItem(State.World.ItemRepository.GetRandomBook(1, 2), 1);
                        else if (unit.Level < 6)
                            unit.SetItem(State.World.ItemRepository.GetRandomBook(1, 3), 1);
                        else if (unit.Level < 9)
                            unit.SetItem(State.World.ItemRepository.GetRandomBook(1, 4), 1);
                        else
                            unit.SetItem(State.World.ItemRepository.GetRandomBook(2, 4), 1);
                    }

                    army.Units.Add(unit);
                    StrategicUtilities.SetAIClass(unit);
                }
            }
        }

        foreach (Army army in Empire.Armies)
        {
            foreach (Unit unit in army.Units)
            {
                if (unit.Experience < .5f * baseXp)
                {
                    unit.SetExp(3 + unit.Experience * 1.1f);
                }

                StrategicUtilities.SpendLevelUps(unit);
            }
        }

        return foundSpot;


        int RandXp(int exp)
        {
            if (exp < 1) exp = 1;
            return (int)(exp * .8f) + State.Rand.Next(10 + (int)(exp * .4));
        }
    }

    private void GenerateTaskForArmy(Army army)
    {
        if (army.Destination != null)
        {
            if (army.Position.Matches(army.Destination))
                army.Destination = null;
            else
            {
                SetPath(army, army.Destination);
                return;
            }
        }

        List<Side> preferredSides = new List<Side>();
        foreach (var relation in State.World.Relations[Empire.Side])
        {
            if (relation.Value.Type == RelationState.Neutral && State.World.GetEmpireOfSide(relation.Key)?.VillageCount > 0) preferredSides.Add(relation.Key);
            if (relation.Value.Type == RelationState.Allied && State.World.GetEmpireOfSide(relation.Key)?.VillageCount > 0)
            {
                preferredSides.Add(relation.Key);
                preferredSides.Add(relation.Key);
            }
        }

        if (preferredSides.Count == 0)
        {
            foreach (var relation in State.World.Relations[Empire.Side])
            {
                if (State.World.GetEmpireOfSide(relation.Key)?.VillageCount > 0) preferredSides.Add(relation.Key);
            }
        }

        var villages = State.World.Villages.Where(s => preferredSides.Contains(s.Side)).ToArray();
        if (villages.Count() == 0)
        {
            army.RemainingMp = 0;
            return;
        }

        for (int i = 0; i < 8; i++)
        {
            Village village = villages[State.Rand.Next(villages.Length)];
            if (village.GetTotalPop() < 4) continue;
            if (MoveToNearVillage(army, village)) break;
        }
    }

    private bool MoveToNearVillage(Army army, Village village)
    {
        int x = 0;
        int y = 0;
        bool foundSpot = false;
        for (int i = 0; i < 10; i++)
        {
            x = village.Position.X + State.Rand.Next(-2, 3);
            y = village.Position.Y + State.Rand.Next(-2, 3);
            if (StrategicUtilities.IsTileClear(new Vec2I(x, y)))
            {
                foundSpot = true;
                break;
            }
        }

        if (foundSpot == false) return false;
        army.Destination = new Vec2I(x, y);
        return true;
    }

    private void SetPath(Army army, Vec2I targetPosition)
    {
        if (targetPosition != null)
        {
            _pathIsFor = army;
            _path = StrategyPathfinder.GetMonsterPath(Empire, army, targetPosition, army.RemainingMp, army.MovementMode == MovementMode.Flight);
            return;
        }

        army.RemainingMp = 0;
    }
}