using OdinSerializer;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Army
{
    [OdinSerialize]
    private List<StrategicTileType> _impassables = new List<StrategicTileType>()
        { StrategicTileType.Mountain, StrategicTileType.SnowMountain, StrategicTileType.Water, StrategicTileType.Lava, StrategicTileType.Ocean, StrategicTileType.BrokenCliffs };
    
    internal List<StrategicTileType> Impassables { get => _impassables; set => _impassables = value; }
    
    internal enum TileAction
    {
        Impassible,
        OneMp,
        TwoMp,
        Attack,
        AttackTwoMp
    }

    [OdinSerialize]
    private Empire _empireOutside;

    internal Empire EmpireOutside { get => _empireOutside; set => _empireOutside = value; }

    [OdinSerialize]
    private AIMode _aIMode;

    public AIMode AIMode { get => _aIMode; set => _aIMode = value; } //May eventually split this out, but we'll see

    [OdinSerialize]
    private Side _side;

    public Side Side { get => _side; set => _side = value; }

    [OdinSerialize]
    private List<Unit> _units;

    public List<Unit> Units { get => _units; set => _units = value; }

    [OdinSerialize]
    private Vec2I _position;

    public Vec2I Position
    {
        get => _position;
        private set
        {
            _position = value;
            if (_position.X != 0 && _position.Y != 0) GetTileHealRate();
        }
    }

    [OdinSerialize]
    private string _name;

    public string Name { get => _name; set => _name = value; }

    public bool JustCreated = false;

    [OdinSerialize]
    private int _remainingMp;

    public int RemainingMp { get => _remainingMp; set => _remainingMp = value; }

    [OdinSerialize]
    public int InVillageIndex { get; private set; } = -1;

    [OdinSerialize]
    private MovementMode _movementMode = MovementMode.Standard;

    internal MovementMode MovementMode { get => _movementMode; set => _movementMode = value; }

    [OdinSerialize]
    private BountyGoods _bountyGoods;

    internal BountyGoods BountyGoods { get => _bountyGoods; set => _bountyGoods = value; }

    [OdinSerialize]
    private int _monsterTurnsRemaining;

    internal int MonsterTurnsRemaining { get => _monsterTurnsRemaining; set => _monsterTurnsRemaining = value; }

    public bool DevourThisTurn { get; private set; } = false;



    [OdinSerialize]
    private Vec2I _destination;

    public Vec2I Destination { get => _destination; set => _destination = value; }

    [OdinSerialize]
    private float _healRate;

    public float HealRate { get => _healRate; set => _healRate = value; }

    [OdinSerialize]
    private ItemStock _itemStock;

    internal ItemStock ItemStock
    {
        get
        {
            if (_itemStock == null) _itemStock = new ItemStock();
            return _itemStock;
        }
        set => _itemStock = value;
    }


    internal double ArmyPower => StrategicUtilities.ArmyPower(this);
    internal int MaxSize => EmpireOutside.MaxArmySize;

    [OdinSerialize]
    private int _bannerStyle = 0;

    internal int BannerStyle { get => _bannerStyle; set => _bannerStyle = value; }

    internal SpriteRenderer Sprite;

    public void SetEmpire(Empire empire) => this.EmpireOutside = empire;

    internal MultiStageBanner Banner { get; set; }

    public Unit LeaderIfInArmy() => Units.Where(s => s.Type == UnitType.Leader).FirstOrDefault();

    internal void SetPosition(Vec2I pos)
    {
        Position = new Vec2I(pos.X, pos.Y);
    }


    public Army(Empire empireOutside, Vec2I p, Side side)
    {
        this.EmpireOutside = empireOutside;
        Side = side;
        HealRate = 0.02f;
        RemainingMp = Config.ArmyMp;
        Position = p;
        Units = new List<Unit>();
        JustCreated = true;

        NameArmy(empireOutside);
        if (empireOutside.Side != null && RaceFuncs.IsPlayableRace(empireOutside.Side.ToRace()))
        {
            BannerStyle = empireOutside.BannerType;
        }

        if (State.World.Turn == 1 && Config.FirstTurnArmiesIdle) RemainingMp = 0;
    }

    internal void NameArmy(Empire empire)
    {
        string newName;
        if (State.World?.Villages != null)
        {
            NameGenerator nameGen = State.NameGen;
            newName = nameGen.GetArmyName(empire.Race, StrategicUtilities.GetVillageAt(Position));
        }
        else
        {
            newName = State.NameGen.GetArmyName(empire.Race, null);
        }

        empire.ArmiesCreated++;
        if (newName == "")
            Name = $"{empire.Name} Army {empire.ArmiesCreated}";
        else
            Name = newName;
    }

    public void Refresh()
    {
        foreach (Unit unit in Units)
        {
            unit.HealPercentage(HealRate);
            unit.RestoreManaPct(.6f);
            if (Config.WeightGain && Config.WeightLoss && unit.Predator)
            {
                if (unit.BodySize > 0 && State.Rand.NextDouble() < Config.WeightLossFractionBody) unit.BodySize--;
                if (unit.DefaultBreastSize > 0 && State.Rand.NextDouble() < Config.WeightLossFractionBreasts) unit.SetDefaultBreastSize(unit.DefaultBreastSize - 1);
                if (unit.DickSize > 0 && State.Rand.NextDouble() < Config.WeightLossFractionDick) unit.DickSize--;
            }

            if (unit.HasTrait(TraitType.Growth) && unit.BaseScale > 1 && !unit.HasTrait(TraitType.PermanentGrowth))
            {
                float extremum = -(Config.GrowthDecayOffset - Config.GrowthDecayIncreaseRate - 1 / 2 * Config.GrowthDecayIncreaseRate);
                if (unit.BaseScale > extremum)
                    unit.BaseScale -= extremum - extremum * (1 - Config.GrowthDecayOffset - Config.GrowthDecayIncreaseRate * (extremum - 1)); // force the decay function to be monotonous
                else
                    unit.BaseScale = Math.Max(1, unit.BaseScale * (1 - Config.GrowthDecayOffset - Config.GrowthDecayIncreaseRate * (unit.BaseScale - 1))); // default decayIncreaseRate = 0.04f
            }
        }

        RefreshMovementMode();

        if (EmpireOutside.StrategicAI != null)
        {
            SortSpells();
        }

        RemainingMp = GetMaxMovement();
        DevourThisTurn = false;
        GetTileHealRate();
        ProcessInVillageOnTurn();
    }

    public int GetMaxMovement()
    {
        return Config.ArmyMp;
    }

    public void RefreshMovementMode()
    {
        int flying = 0;
        int noHill = 0;
        int yesLava = 0;
        int noSnow = 0;
        int yesMountain = 0;
        int noVolcanic = 0;
        int yesWater = 0;
        int noDesert = 0;
        int noSwamp = 0;
        int noForest = 0;
        int noGrass = 0;
        //int aquatic = 0;

        foreach (Unit unit in Units)
        {
            if (unit.HasTrait(TraitType.Pathfinder)) flying++;
            if (unit.HasTrait(TraitType.HillImpedence)) noHill++;
            if (unit.HasTrait(TraitType.LavaWalker)) yesLava++;
            if (unit.HasTrait(TraitType.SnowImpedence)) noSnow++;
            if (unit.HasTrait(TraitType.MountainWalker)) yesMountain++;
            if (unit.HasTrait(TraitType.VolcanicImpedence)) noVolcanic++;
            if (unit.HasTrait(TraitType.WaterWalker)) yesWater++;
            if (unit.HasTrait(TraitType.DesertImpedence)) noDesert++;
            if (unit.HasTrait(TraitType.SwampImpedence)) noSwamp++;
            if (unit.HasTrait(TraitType.ForestImpedence)) noForest++;
            if (unit.HasTrait(TraitType.GrassImpedence)) noGrass++;
            //if (unit.HasTrait(Traits.Shapeshifter) || unit.HasTrait(Traits.Skinwalker))
            //{
            //    yesLava++; yesMountain++; yesWater++; flying++;
            //}

            //else if (unit.HasTrait(Traits.Aquatic))
            //    aquatic++;
        }

        if (noHill > 0 && noHill > Units.Count / 2)
        {
            if (!Impassables.Contains(StrategicTileType.Hills))
            {
                Impassables.Add(StrategicTileType.Hills);
            }

            if (!Impassables.Contains(StrategicTileType.SnowHills))
            {
                Impassables.Add(StrategicTileType.SnowHills);
            }

            if (!Impassables.Contains(StrategicTileType.SandHills))
            {
                Impassables.Add(StrategicTileType.SandHills);
            }
        }
        else
        {
            Impassables.Remove(StrategicTileType.Hills);
            Impassables.Remove(StrategicTileType.SnowHills);
            Impassables.Remove(StrategicTileType.SandHills);
        }

        if (noSnow > 0 && noSnow > Units.Count / 2)
        {
            if (!Impassables.Contains(StrategicTileType.Snow))
            {
                Impassables.Add(StrategicTileType.Snow);
            }

            if (!Impassables.Contains(StrategicTileType.SnowHills))
            {
                Impassables.Add(StrategicTileType.SnowHills);
            }
        }
        else
        {
            Impassables.Remove(StrategicTileType.Snow);
            Impassables.Remove(StrategicTileType.SnowHills);
        }

        if (yesLava > 0 && yesLava >= Units.Count / 2)
            Impassables.Remove(StrategicTileType.Lava);
        else if (!Impassables.Contains(StrategicTileType.Lava)) Impassables.Add(StrategicTileType.Lava);

        if (yesMountain > 0 && yesMountain >= Units.Count / 2)
        {
            Impassables.Remove(StrategicTileType.Mountain);
            Impassables.Remove(StrategicTileType.SnowMountain);
            Impassables.Remove(StrategicTileType.BrokenCliffs);
        }
        else
        {
            if (!Impassables.Contains(StrategicTileType.SnowMountain)) Impassables.Add(StrategicTileType.SnowMountain);
            if (!Impassables.Contains(StrategicTileType.Mountain)) Impassables.Add(StrategicTileType.Mountain);
            if (!Impassables.Contains(StrategicTileType.BrokenCliffs)) Impassables.Add(StrategicTileType.BrokenCliffs);
        }

        if (noVolcanic > 0 && noVolcanic > Units.Count / 2)
        {
            if (!Impassables.Contains(StrategicTileType.Volcanic)) Impassables.Add(StrategicTileType.Volcanic);
        }
        else
            Impassables.Remove(StrategicTileType.Volcanic);

        if (yesWater > 0 && yesWater >= Units.Count / 2)
        {
            Impassables.Remove(StrategicTileType.Ocean);
            Impassables.Remove(StrategicTileType.Water);
        }
        else
        {
            if (!Impassables.Contains(StrategicTileType.Ocean)) Impassables.Add(StrategicTileType.Ocean);
            if (!Impassables.Contains(StrategicTileType.Water)) Impassables.Add(StrategicTileType.Water);
        }

        if (noDesert > 0 && noDesert > Units.Count / 2)
        {
            if (!Impassables.Contains(StrategicTileType.Desert))
            {
                Impassables.Add(StrategicTileType.Desert);
            }

            if (!Impassables.Contains(StrategicTileType.SandHills))
            {
                Impassables.Add(StrategicTileType.SandHills);
            }
        }
        else
        {
            Impassables.Remove(StrategicTileType.Desert);
            Impassables.Remove(StrategicTileType.SandHills);
        }

        if (noSwamp > 0 && noSwamp > Units.Count / 2)
        {
            if (!Impassables.Contains(StrategicTileType.Swamp))
            {
                Impassables.Add(StrategicTileType.Swamp);
            }

            if (!Impassables.Contains(StrategicTileType.PurpleSwamp))
            {
                Impassables.Add(StrategicTileType.PurpleSwamp);
            }
        }
        else
        {
            Impassables.Remove(StrategicTileType.Swamp);
            Impassables.Remove(StrategicTileType.PurpleSwamp);
        }

        if (noForest > 0 && noForest > Units.Count / 2)
        {
            if (!Impassables.Contains(StrategicTileType.Forest))
            {
                Impassables.Add(StrategicTileType.Forest);
            }

            if (!Impassables.Contains(StrategicTileType.SnowTrees))
            {
                Impassables.Add(StrategicTileType.SnowTrees);
            }
        }
        else
        {
            Impassables.Remove(StrategicTileType.Forest);
            Impassables.Remove(StrategicTileType.SnowTrees);
        }

        if (noGrass > 0 && noGrass > Units.Count / 2)
        {
            if (!Impassables.Contains(StrategicTileType.Grass)) Impassables.Add(StrategicTileType.Grass);
        }
        else
            Impassables.Remove(StrategicTileType.Grass);


        if (flying > 0 && flying >= Units.Count / 2)
            MovementMode = MovementMode.Flight;
        //else if (aquatic >= Units.Count / 2)
        //    movementMode = MovementMode.Aquatic;
        else
            MovementMode = MovementMode.Standard;
    }

    /// <summary>
    ///     Removes dead troops, and deletes the army itself if needed
    /// </summary>
    public void Prune()
    {
        foreach (Unit unit in Units.ToList())
        {
            if (unit.Type == UnitType.Summon)
            {
                Units.Remove(unit);
            }

            if (unit.IsDead)
            {
                Units.Remove(unit);
                State.World.Stats.SoldiersLost(1, unit.Side);
            }
        }

        if (Units.Count == 0)
        {
            EmpireOutside.Armies.Remove(this);
            if (JustCreated) EmpireOutside.ArmiesCreated--;
        }
    }

    public int GetHealthPercentage()
    {
        float hp = 0;
        float maxhp = 0;
        foreach (Unit unit in Units)
        {
            hp += unit.Health;
            maxhp += unit.MaxHealth;
        }

        float pct = 100 * hp / maxhp;
        return (int)pct;
    }

    public int GetAbsHealth()
    {
        int hp = 0;
        for (int i = 0; i < Units.Count; i++)
        {
            if (Units[i] != null)
            {
                hp += Units[i].Health;
            }
        }

        return hp;
    }

    public bool MoveTo(Vec2I pos)
    {
        return CheckMove(pos);
    }

    public bool Move(int direction)
    {
        Vec2I pos = GetPos(direction);
        return CheckMove(pos);
    }

    private Vec2I GetPos(int i)
    {
        switch (i)
        {
            case 0:
                return new Vec2I(Position.X, Position.Y + 1);
            case 1:
                return new Vec2I(Position.X + 1, Position.Y + 1);
            case 2:
                return new Vec2I(Position.X + 1, Position.Y);
            case 3:
                return new Vec2I(Position.X + 1, Position.Y - 1);
            case 4:
                return new Vec2I(Position.X, Position.Y - 1);
            case 5:
                return new Vec2I(Position.X - 1, Position.Y - 1);
            case 6:
                return new Vec2I(Position.X - 1, Position.Y);
            case 7:
                return new Vec2I(Position.X - 1, Position.Y + 1);
        }

        return Position;
    }

    private bool CheckMove(Vec2I pos)
    {
        TileAction act = CheckTileForActionType(pos);

        if (act == TileAction.Impassible)
        {
            return false;
        }

        if (act == TileAction.TwoMp)
        {
            if (RemainingMp > 1)
            {
                if (Banner != null && Banner.gameObject.activeSelf)
                    State.GameManager.StrategyMode.Translator?.SetTranslator(Banner.transform, Position, pos, Config.StrategicAIMoveDelay, State.GameManager.StrategyMode.IsPlayerTurn);
                else if (Sprite != null) State.GameManager.StrategyMode.Translator?.SetTranslator(Sprite.transform, Position, pos, Config.StrategicAIMoveDelay, State.GameManager.StrategyMode.IsPlayerTurn);
                if (State.GameManager.StrategyMode.IsPlayerTurn) State.GameManager.StrategyMode.UndoMoves.Add(new StrategicMoveUndo(this, RemainingMp, new Vec2I(Position.X, Position.Y)));
                Position = pos;
                StrategicUtilities.TryClaim(pos, EmpireOutside);
                RemainingMp -= 2;
                return false;
            }

            return false;
        }

        if ((act == TileAction.Attack && RemainingMp > 0) || (act == TileAction.AttackTwoMp && RemainingMp > 1))
        {
            RemainingMp = 0;
            if (Banner != null && Banner.gameObject.activeSelf)
                State.GameManager.StrategyMode.Translator?.SetTranslator(Banner.transform, Position, pos, Config.StrategicAIMoveDelay, State.GameManager.StrategyMode.IsPlayerTurn);
            else if (Sprite != null) State.GameManager.StrategyMode.Translator?.SetTranslator(Sprite.transform, Position, pos, Config.StrategicAIMoveDelay, State.GameManager.StrategyMode.IsPlayerTurn);
            State.GameManager.StrategyMode.UndoMoves.Clear();
            Position = pos;
            return true;
        }

        if (act == TileAction.OneMp && RemainingMp > 0)
        {
            if (State.GameManager.StrategyMode.IsPlayerTurn) State.GameManager.StrategyMode.UndoMoves.Add(new StrategicMoveUndo(this, RemainingMp, new Vec2I(Position.X, Position.Y)));
            RemainingMp -= 1;
            if (Banner != null && Banner.gameObject.activeSelf)
                State.GameManager.StrategyMode.Translator?.SetTranslator(Banner.transform, Position, pos, Config.StrategicAIMoveDelay, State.GameManager.StrategyMode.IsPlayerTurn);
            else if (Sprite != null) State.GameManager.StrategyMode.Translator?.SetTranslator(Sprite.transform, Position, pos, Config.StrategicAIMoveDelay, State.GameManager.StrategyMode.IsPlayerTurn);
            Position = pos;
            StrategicUtilities.TryClaim(pos, EmpireOutside);
            return false;
        }

        return false;
    }

    internal TileAction CheckTileForActionType(Vec2I p)
    {
        bool flyer = MovementMode == MovementMode.Flight;
        foreach (Army army in StrategicUtilities.GetAllArmies())
        {
            if (p.Matches(army._position))
            {
                if (army.EmpireOutside.IsEnemy(EmpireOutside) == false)
                {
                    return TileAction.Impassible;
                }
                else
                {
                    if (StrategicTileInfo.WalkCost(p.X, p.Y) == 2 && flyer == false) return TileAction.AttackTwoMp;
                    return TileAction.Attack;
                }
            }
        }

        for (int i = 0; i < State.World.Villages.Length; i++)
        {
            if (p.Matches(State.World.Villages[i].Position))
            {
                if (State.World.Villages[i].Empire.IsAlly(EmpireOutside))
                {
                    return TileAction.OneMp;
                }
                else if (State.World.Villages[i].Empire.IsEnemy(EmpireOutside))
                {
                    return TileAction.Attack;
                }
                else
                    return TileAction.Impassible;
            }
        }

        if (StrategicTileInfo.WalkCost(p.X, p.Y) == 2 && flyer == false)
        {
            return TileAction.TwoMp;
        }

        if (StrategyPathfinder.CanEnter(p, this) == false)
        {
            return TileAction.Impassible;
        }

        return TileAction.OneMp;
    }

    internal void UpdateInVillage()
    {
        InVillageIndex = -1;
        for (int i = 0; i < State.World.Villages.Length; i++)
        {
            if (_position.Matches(State.World.Villages[i].Position))
            {
                InVillageIndex = i;
                State.World.Villages[i].ItemStock.TransferAllItems(ItemStock);
            }
        }
    }

    internal void GetTileHealRate()
    {
        UpdateInVillage();

        HealRate = 0.02f;
        if (InVillageIndex > -1)
        {
            HealRate = State.World.Villages[InVillageIndex].Healrate();
        }
    }

    internal void ProcessInVillageOnTurn()
    {
        UpdateInVillage();
        if (InVillageIndex > -1)
        {
            var village = State.World.Villages[InVillageIndex];

            var minExp = village.GetStartingXp();
            foreach (Unit unit in Units)
            {
                if (unit.Experience < minExp)
                {
                    unit.SetExp(minExp);
                }
            }

            foreach (Unit unit in Units)
            {
                unit.AddTraits(village.GetTraitsToAdd());
            }
        }
    }

    public int GetDevourmentCapacity(int minimumHealAmount)
    {
        int cap = 0;

        for (int i = 0; i < Units.Count; i++)
        {
            if (Units[i].Predator == false) continue;
            cap += (10 - minimumHealAmount + Units[i].MaxHealth - Units[i].Health) / 10;
        }

        return cap;
    }

    public void DevourHeal(int numprey)
    {
        int remainingPrey = numprey;
        Unit target;
        if (numprey > 0)
        {
            RemainingMp = 0;
            DevourThisTurn = true;
            State.GameManager.StrategyMode.UndoMoves.Clear();
        }

        while (remainingPrey > 0)
        {
            target = Units.Where(s => s.Predator).OrderByDescending(l => l.MaxHealth - l.Health).ThenBy(l => State.Rand.Next()).FirstOrDefault();
            if (target == null) break;
            target.Feed();
            remainingPrey--;
        }
    }


    internal int TrainingGetCost(int level)
    {
        int cost = 2;
        for (int i = 1; i <= level; i++)
        {
            cost += 12 * i;
        }

        cost *= Units.Count;
        return cost;
    }

    internal int TrainingGetExpValue(int level)
    {
        int xpGain = (int)Mathf.Ceil(Config.ExperiencePerLevel / 2f);
        xpGain += ((int)Mathf.Ceil(Config.ExperiencePerLevel / 2f) + Config.AdditionalExperiencePerLevel) * level;
        return xpGain;
    }

    internal void Train(int level)
    {
        int xpGain = TrainingGetExpValue(level);
        int cost = TrainingGetCost(level);

        if (EmpireOutside.Gold >= cost)
        {
            foreach (Unit unit in Units)
            {
                unit.GiveExp(xpGain);
            }

            EmpireOutside.SpendGold(cost);
            State.World.Stats.SpentGoldOnArmyTraining(cost, Side);
        }
    }

    internal void SortSpells()
    {
        var spellbookTypes = ItemStock.GetAllSpellBooks();
        if (spellbookTypes.Count > 0)
        {
            int casters = Units.Where(s => s.AIClass == AIClass.MagicMelee || (s.AIClass == AIClass.MagicRanged && s.FixedGear == false)).Count();
            {
                int shortfall = Units.Count / 2 - casters;
                if (shortfall > 0)
                {
                    var possibleUnits = Units.Where(s => (s.AIClass != AIClass.MagicMelee && s.AIClass != AIClass.MagicRanged) || s.FixedGear == false).ToList();
                    int possibleCount = Math.Min(Math.Min(shortfall, spellbookTypes.Count), possibleUnits.Count);
                    for (int i = 0; i < possibleCount; i++)
                    {
                        if (possibleUnits[i].GetBestRanged() != null)
                            possibleUnits[i].AIClass = AIClass.MagicRanged;
                        else
                            possibleUnits[i].AIClass = AIClass.MagicMelee;
                    }
                }
            }
        }

        if (spellbookTypes.Count == 0) return;
        List<SpellBook> books = new List<SpellBook>();
        foreach (var bookType in spellbookTypes)
        {
            books.Add((SpellBook)State.World.ItemRepository.GetItem(bookType));
        }

        books.OrderByDescending(s => s.Tier);

        var magicUsers = Units.Where(s => (s.FixedGear == false && s.AIClass == AIClass.MagicMelee) || s.AIClass == AIClass.MagicRanged).OrderByDescending(s => s.GetStatBase(Stat.Mind));


        foreach (Unit unit in magicUsers)
        {
            if (books.Count == 0) break;
            if (unit.Items[1] == null)
            {
                if (ItemStock.TakeItem(State.World.ItemRepository.GetItemType(books[0])))
                {
                    unit.SetItem(books[0], 1);
                    books.RemoveAt(0);
                }
            }
            else if (unit.Items[1] is SpellBook == false)
            {
                if (ItemStock.TakeItem(State.World.ItemRepository.GetItemType(books[0])))
                {
                    ItemStock.AddItem(State.World.ItemRepository.GetItemType(unit.Items[1]));
                    unit.SetItem(books[0], 1);
                    books.RemoveAt(0);
                }
            }
            else
            {
                if (unit.Items[1] is SpellBook book)
                {
                    if (books[0].Tier > book.Tier)
                    {
                        if (ItemStock.TakeItem(State.World.ItemRepository.GetItemType(books[0])))
                        {
                            ItemStock.AddItem(State.World.ItemRepository.GetItemType(book));
                            unit.SetItem(books[0], 1);
                            books.RemoveAt(0);
                        }
                    }
                }
            }
        }
    }
}

internal enum MovementMode
{
    Standard,
    Flight,
    Aquatic
}