using LegacyAI;
using OdinSerializer;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Empire
{
    [OdinSerialize]
    private bool _knockedOut;
    public bool KnockedOut { get => _knockedOut; set => _knockedOut = value; }
    [OdinSerialize]
    private int _team;
    public int Team { get => _team; set => _team = value; }
    [OdinSerialize]
    public Side Side { get; private set; }
    [OdinSerialize]
    private Race _race;
    public Race Race { get => _race; set => _race = value; }
    [OdinSerialize]
    private Race _replacedRace;
    public Race ReplacedRace { get => _replacedRace; set => _replacedRace = value; }
    [OdinSerialize]
    private int _gold;
    int gold { get => _gold; set => _gold = value; }
    [OdinSerialize]
    public int Income { get; private set; }
    [OdinSerialize]
    private List<Army> _armies;
    public List<Army> Armies { get => _armies; set => _armies = value; }

    [OdinSerialize]
    private int _maxArmySize;
    public int MaxArmySize { get => _maxArmySize; set => _maxArmySize = value; }
    [OdinSerialize]
    private int _maxGarrisonSize;
    public int MaxGarrisonSize { get => _maxGarrisonSize; set => _maxGarrisonSize = value; }

    [OdinSerialize]
    private string _name;
    public string Name { get => _name; set => _name = value; }

    [OdinSerialize]
    private int _armiesCreated;
    public int ArmiesCreated { get => _armiesCreated; set => _armiesCreated = value; }

    [OdinSerialize]
    private List<StrategicReport> _reports;
    internal List<StrategicReport> Reports { get => _reports; set => _reports = value; }

    [OdinSerialize]
    private Leader _leader;
    public Leader Leader { get => _leader; set => _leader = value; }

    [OdinSerialize]
    private string _fakeLeaderName;
    public string FakeLeaderName { get => _fakeLeaderName; set => _fakeLeaderName = value; }

    private Village _capitalCity;

    [OdinSerialize]
    private Color _unityColor;
    public Color UnityColor { get => _unityColor; set => _unityColor = value; }
    [OdinSerialize]
    private Color _unitySecondaryColor;
    public Color UnitySecondaryColor { get => _unitySecondaryColor; set => _unitySecondaryColor = value; }
    [OdinSerialize]
    private int _bannerType;
    public int BannerType { get => _bannerType; set => _bannerType = value; }
    [OdinSerialize]
    private IStrategicAI _strategicAI;
    public IStrategicAI StrategicAI { get => _strategicAI; set => _strategicAI = value; }
    [OdinSerialize]
    private TacticalAIType _tacticalAIType;
    public TacticalAIType TacticalAIType { get => _tacticalAIType; set => _tacticalAIType = value; }

    [OdinSerialize]
    public Dictionary<int, bool> EventHappened;

    [OdinSerialize]
    private List<int> _recentEvents;
    public List<int> RecentEvents { get => _recentEvents; set => _recentEvents = value; }

    [OdinSerialize]
    private int _turnOrder;
    public int TurnOrder { get => _turnOrder; set => _turnOrder = value; }

    [OdinSerialize]
    private bool _canVore = true;
    public bool CanVore { get => _canVore; set => _canVore = value; }

    public int VillageCount => State.World.Villages.Where(s => Equals(s.Side, Side)).Count();

    public bool IsAlly(Empire empire)
    {
        if (empire == null)
            return false;
        if (Equals(Side, empire.Side) || RelationsManager.GetRelation(Side, empire.Side).Type == RelationState.Allied)
            return true;
        return false;
    }

    public bool IsEnemy(Empire empire)
    {
        if (empire == null)
            return false;
        if (Equals(Side, empire.Side))
            return false;
        if (RelationsManager.GetRelation(Side, empire.Side).Type == RelationState.Enemies)
            return true;
        return false;
    }

    public bool IsNeutral(Empire empire)
    {
        if (empire == null)
            return false;
        if (Equals(Side, empire.Side))
            return false;
        if (RelationsManager.GetRelation(Side, empire.Side).Type == RelationState.Neutral)
            return true;
        return false;
    }

    public int StartingXP
    {
        get
        {
            if (Boosts == null)
                RecalculateBoosts(State.World.Villages);
            if (StrategicAI != null && StrategicAI is StrategicAI ai)
            {
                if (ai.CheatLevel == 2)
                    return Mathf.Max(StrategicUtilities.Get80thExperiencePercentile() / 4, (int)(Boosts.StartingExpAdd * 1.2f));
                if (ai.CheatLevel == 3)
                    return Mathf.Max((int)(StrategicUtilities.Get80thExperiencePercentile() / 1.5f), (int)(Boosts.StartingExpAdd * 1.5f));
                return Boosts.StartingExpAdd;
            }
            else
                return Boosts.StartingExpAdd;
        }
    }

    public struct ConstructionArgs
    {
        internal Race race;
        internal Side side;
        internal Color color;
        internal Color secColor;
        internal int bannerType;
        internal StrategyAIType strategicAI;
        internal TacticalAIType tacticalAI;
        internal int team;
        internal int maxArmySize;
        internal int maxGarrisonSize;

        public ConstructionArgs(Race race, Side side, Color color, Color secColor, int bannerType, StrategyAIType strategicAI, TacticalAIType tacticalAI, int team, int maxArmySize, int maxGarrisonSize)
        {
            this.race = race;
            this.side = side;
            this.color = color;
            this.secColor = secColor;
            this.bannerType = bannerType;
            this.strategicAI = strategicAI;
            this.tacticalAI = tacticalAI;
            this.team = team;
            this.maxArmySize = maxArmySize;
            this.maxGarrisonSize = maxGarrisonSize;
        }
    }

    public EmpireBoosts Boosts = new EmpireBoosts();

    public Empire(ConstructionArgs args)
    {
        Reports = new List<StrategicReport>();
        KnockedOut = false;
        BannerType = args.bannerType;
        UnityColor = args.color;
        UnitySecondaryColor = args.secColor;
        gold = Config.StartingGold;
        Income = 0;
        Race = args.race;
        ReplacedRace = Race;
        Side = args.side;
        Team = args.team;
        MaxArmySize = args.maxArmySize;
        MaxGarrisonSize = args.maxGarrisonSize;
        Armies = new List<Army>();
        
        Name = Race?.ToString() ?? args.side.ToString();
        if (args.strategicAI == StrategyAIType.None)
            StrategicAI = null;
        else if (args.strategicAI == StrategyAIType.Passive)
            StrategicAI = new PassiveAI(args.side);
        else if (args.strategicAI == StrategyAIType.Basic)
            StrategicAI = new StrategicAI(this, 0, false);
        else if (args.strategicAI == StrategyAIType.Advanced)
            StrategicAI = new StrategicAI(this, 0, true);
        else if (args.strategicAI == StrategyAIType.Cheating1)
            StrategicAI = new StrategicAI(this, 1, true);
        else if (args.strategicAI == StrategyAIType.Cheating2)
            StrategicAI = new StrategicAI(this, 2, true);
        else if (args.strategicAI == StrategyAIType.Cheating3)
            StrategicAI = new StrategicAI(this, 3, true);
        else if (args.strategicAI == StrategyAIType.Legacy)
            StrategicAI = new LegacyStrategicAI(args.side);
        TacticalAIType = args.tacticalAI;
        Boosts = new EmpireBoosts();
        EventHappened = new Dictionary<int, bool>();
        RecentEvents = new List<int>();

        var raceFlags = State.RaceSettings.GetRaceTraits(Race);
        if (raceFlags != null)
        {
            if (raceFlags.Contains(TraitType.Prey))
                CanVore = false;
        }
    }

    //Dummy implementation for the pure tactical
    public Empire()
    {
        Boosts = new EmpireBoosts();

    }

    public void LoadFix()
    {
        if (Reports == null)
            Reports = new List<StrategicReport>();
        if (EventHappened == null)
            EventHappened = new Dictionary<int, bool>();
    }

    internal void CheckEvent()
    {
        State.EventList.CheckStartEvent(this);
    }



    public void CalcIncome(Village[] villages, bool AddToStats = false)
    {
        RecalculateBoosts(villages);

        Income = 0;
        for (int i = 0; i < villages.Length; i++)
        {
            if (Equals(villages[i].Side, Side))
            {
                villages[i].UpdateNetBoosts();
                int value = villages[i].GetIncome();
                Income = Income + value;
            }
        }
        if (RaceFuncs.IsMainRace2(Side))
        {
            for (int i = 0; i < Armies.Count; i++)
            {
                Income = Income - (Armies[i].Units.Count * Config.World.ArmyUpkeep);
                if (AddToStats)
                {
                    State.World.Stats.CollectedGold(Armies[i].Units.Count * 2, Armies[i].Side);
                    State.World.Stats.SpentGoldOnArmyMaintenance(Armies[i].Units.Count * 2, Armies[i].Side);
                }
            }
        }
        foreach (ClaimableBuilding claimable in State.World.Claimables)
        {
            if (claimable is GoldMine && claimable.Owner == this)
            {
                Income += Config.GoldMineIncome;
            }
        }
        Income += Boosts.WealthAdd;
    }

    public void SpendGold(int gold)
    {
        this.gold -= gold;
        State.World.Stats.SpentGold(gold, Side);
    }

    public void AddGold(int gold)
    {
        this.gold += gold;
        if (gold > 0)
            State.World.Stats.CollectedGold(gold, Side);
        else
            State.World.Stats.SpentGold(-gold, Side);


    }

    public int Gold => gold;

    public Village CapitalCity
    {
        get
        {
            if (_capitalCity == null)
            {
                _capitalCity = State.World.Villages.Where(s => s.Capital && Equals(s.OriginalRace, Race)).FirstOrDefault();
                if (_capitalCity == null)
                {
                    _capitalCity = State.World.Villages.Where(s => Equals(s.OriginalRace, Race)).FirstOrDefault();
                }
            }


            return _capitalCity;
        }
    }

    public void ArmyCleanup()
    {
        foreach (Army army in Armies.ToList())
        {
            army.Prune();
            army.JustCreated = false;
        }

    }

    public void Regenerate()
    {
        for (int i = 0; i < Armies.Count; i++)
        {
            Armies[i].Refresh();
        }
        if (Config.FactionLeaders && RaceFuncs.IsMainRace2(Side))
        {
            if (Leader == null)
                GenerateLeader();
            if (Leader != null)
            {
                if (Leader.IsDead == false && GetAllUnitsIncludingTravelersAndStandby().Contains(Leader) == false && Config.DontFixLeaders == false)
                    Leader.Health = -99999;
                if (StrategicAI != null && Leader.IsDead == false)
                {
                    if (Leader.Items[0] == null)
                    {
                        if (State.Rand.Next(2) == 0)
                            Shop.BuyItem(this, Leader, State.World.ItemRepository.GetItem(ItemType.CompoundBow));
                        else
                            Shop.BuyItem(this, Leader, State.World.ItemRepository.GetItem(ItemType.Axe));
                    }
                    else if (Leader.Items[0] is Weapon == false)
                    {
                        Debug.Log("Sold invalid leader item");
                        Shop.SellItem(this, Leader, 0);

                        if (State.Rand.Next(2) == 0)
                            Shop.BuyItem(this, Leader, State.World.ItemRepository.GetItem(ItemType.CompoundBow));
                        else
                            Shop.BuyItem(this, Leader, State.World.ItemRepository.GetItem(ItemType.Axe));
                    }
                    else if (Leader.Items[1] == null)
                        Shop.BuyItem(this, Leader, State.World.ItemRepository.GetItem(ItemType.Helmet));
                    else if (Leader.Items.Length > 2 && Leader.Items[2] == null)
                        Shop.BuyItem(this, Leader, State.World.ItemRepository.GetItem(ItemType.BodyArmor));
                }
            }



        }

    }

    void GenerateLeader()
    {
        var capital = CapitalCity;
        if (capital == null)
            capital = State.World.Villages.Where(s => Equals(s.Side, Side)).FirstOrDefault();
        if (capital == null)
            return;
        Leader = new Leader(Side, State.RaceSettings.GetLeaderRace(Race), 0);
        PlaceLeader(capital);
    }

    private void PlaceLeader(Village capital)
    {
        var CapitalArmy = StrategicUtilities.ArmyAt(capital.Position);
        if (CapitalArmy != null && CapitalArmy.Units.Count < MaxArmySize && Equals(CapitalArmy.Side, Side))
            CapitalArmy.Units.Add(Leader);
        else
        {
            if (CapitalArmy == null)
            {
                var army = new Army(this, new Vec2i(capital.Position.X, capital.Position.Y), Side);
                Armies.Add(army);
                army.Units.Add(Leader);
            }
            else
            {
                Leader.Health = -1;
            }

        }
    }


    public List<Unit> GetAllUnits()
    {
        List<Unit> units = new List<Unit>();
        foreach (Army army in Armies)
        {
            foreach (Unit unit in army.Units)
            {
                units.Add(unit);
            }
        }
        return units;
    }

    public List<Unit> GetAllUnitsIncludingTravelersAndStandby()
    {
        List<Unit> units = new List<Unit>();
        foreach (Army army in Armies)
        {
            foreach (Unit unit in army.Units)
            {
                units.Add(unit);
            }
        }
        foreach (Village village in State.World.Villages)
        {
            foreach (var unit in village.GetRecruitables())
            {
                units.Add(unit);
            }
            if (village.travelers != null)
            {
                foreach (var unit in village.travelers)
                {
                    units.Add(unit.unit);
                }
            }

        }
        return units;
    }

    public EmpireBoosts RecalculateBoosts(Village[] villages)
    {
        if (Boosts == null) Boosts = new EmpireBoosts();
        Boosts.ResetValues();

        var teamVillageList = new List<Village>();
        for (var i = 0; i < villages.Length; i++)
        {
            var theVillage = villages[i];
            if (theVillage != null && (theVillage.Empire?.IsAlly(this) ?? false))
            {
                teamVillageList.Add(theVillage);
            }
        }

        foreach (var village in teamVillageList)
        {
            Boosts.WealthAdd += village.NetBoosts.TeamWealthAdd;
            Boosts.StartingExpAdd += village.NetBoosts.TeamStartingExpAdd;
        }

        return Boosts;
    }

    internal void CheckAutoLevel()
    {
        foreach (Army army in Armies)
        {
            foreach (Unit unit in army.Units)
            {
                if (unit.AutoLeveling)
                {
                    StrategicUtilities.SpendLevelUps(unit);
                }
            }
        }
    }
}
