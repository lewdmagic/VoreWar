using MapObjects;
using OdinSerializer;
using System.Collections.Generic;
using System.Linq;


public class World
{
    [OdinSerialize]
    public int Turn = 1;
    [OdinSerialize]
    public string SaveVersion;
    [OdinSerialize]
    public List<Empire> EmpireOrder;
    public StrategicTileType[,] Tiles;
    public StrategicDoodadType[,] Doodads;
    public Village[] Villages;

    public IReadOnlyList<Empire> MainEmpires => MainEmpiresWritable;
    public List<Empire> MainEmpiresWritable;
    
    public List<Empire> AllActiveEmpiresWritable;
    public IEnumerable<Empire> AllActiveEmpires => AllActiveEmpiresWritable;
    public int AllActiveEmpiresCount => AllActiveEmpiresWritable.Count;
    
    ///////////////////////////////////
    ///////////////////////////////////

    internal void SortMainEmpiresBySide()
    {
        MainEmpiresWritable = MainEmpiresWritable.OrderBy(s => s.Side).ToList();
    }
    
    ///////////////////////////////////
    
    
    /// <summary>
    /// Deprecated, only left in for compatibility
    /// </summary>
    public Empire[] Empires;
    public Empire ActingEmpire;
    public ItemRepository ItemRepository;
    public WorldConfig ConfigStorage;
    public StrategicStats Stats;
    public TacticalData TacticalData;

    [OdinSerialize]
    internal Dictionary<Side, Dictionary<Side, Relationship>> Relations = new Dictionary<Side, Dictionary<Side, Relationship>>();

    [OdinSerialize]
    public bool crazyBuildings = false;

    [OdinSerialize]
    internal SavedCameraState SavedCameraState;

    public MonsterEmpire[] MonsterEmpires;

    public MercenaryHouse[] MercenaryHouses;
    [OdinSerialize]
    internal ClaimableBuilding[] Claimables;


    [OdinSerialize]
    public List<Reincarnator> Reincarnators;

    [OdinSerialize]
    public bool IsNight = false;
	
    public World(bool MapEditorVersion)
    {

        Config.World.resetVillagesPerEmpire();
        Config.ResetCenteredEmpire();
        State.World = this;
        ConfigStorage = Config.World;
        ItemRepository = new ItemRepository();
        if (MapEditorVersion)
        {
            MainEmpiresWritable = new List<Empire>();
            Villages = new Village[0];
            foreach (Race race in RaceFuncs.MainRaceEnumerable())
            {
                int bannerType = State.RaceSettings.Exists(race) ? State.RaceSettings.Get(race).BannerType : 1;
                MainEmpiresWritable.Add(new Empire(new Empire.ConstructionArgs(race, race.ToSide(), CreateStrategicGame.ColorFromRace(race), UnityEngine.Color.white, bannerType, StrategyAIType.None, TacticalAIType.None, 0, 16, 16)));
            }
            WorldGenerator worldGen = new WorldGenerator();
            worldGen.GenerateOnlyTerrain(ref Tiles);
            Doodads = new StrategicDoodadType[Config.StrategicWorldSizeX, Config.StrategicWorldSizeY];
        }
        // Make a copy instead of copying referrence
        AllActiveEmpiresWritable = MainEmpiresWritable?.ToList();
        MercenaryHouses = new MercenaryHouse[0];
        Claimables = new ClaimableBuilding[0];
    }

    internal World(StrategicCreationArgs args, Map map)
    {
        State.World = this;
        StrategyPathfinder.Initialized = false;
        ConfigStorage = Config.World;

        if (map == null)
        {
            WorldGenerator worldGen = new WorldGenerator();
            int empireCount = Config.World.VillagesPerEmpire.Values.Where(s => s > 0).Count();
            worldGen.GenerateWorld(ref Tiles, ref Villages, args.Team, args.MapGen);
            Claimables = new ClaimableBuilding[0];
            worldGen.PlaceMercenaryHouses(args.MercCamps);
            worldGen.PlaceGoldMines(args.GoldMines);
            Doodads = new StrategicDoodadType[Config.StrategicWorldSizeX, Config.StrategicWorldSizeY];
            WorldGenerator.ClearVillagePaths(args.MapGen);
        }
        else
        {
            Tiles = map.Tiles;
            Doodads = map.Doodads;
            MapVillagePopulator pop = new MapVillagePopulator(Tiles);
            pop.PopulateVillages(map, ref Villages);
            pop.PopulateMercenaryHouses(map, ref MercenaryHouses);
            pop.PopulateClaimables(map, ref Claimables);
        }


        MainEmpiresWritable = new List<Empire>();
        foreach (Race race in RaceFuncs.MainRaceEnumerable())
        {
            MainEmpiresWritable.Add(new Empire(args.empireArgs[race]));
        }
        
        // for (int i = 0; i < MainEmpires.Count; i++)
        // {
        //     MainEmpires[i].CalcIncome(Villages);
        //     MainEmpires[i].CanVore = args.CanVore[RaceFuncs.IntToRace(i)]; // wrong int cast to Race
        //     MainEmpires[i].TurnOrder = args.TurnOrder[RaceFuncs.IntToRace(i)];
        // }
        foreach (Empire empire in MainEmpires)
        {
            empire.CalcIncome(Villages);
            empire.CanVore = args.CanVore[empire.Race];
            empire.TurnOrder = args.TurnOrder[empire.Race];
        }


        Empire rebelsEmpire = new Empire(new Empire.ConstructionArgs(null, Side.RebelSide, UnityEngine.Color.red, new UnityEngine.Color(.6f, 0, 0), 5, StrategyAIType.Basic, TacticalAIType.Full, 700, 16, 16));
        rebelsEmpire.Name = "Rebels";
        rebelsEmpire.ReplacedRace = Race.Tigers;
        MainEmpiresWritable.Add(rebelsEmpire);

        Empire banditsEmpire = new Empire(new Empire.ConstructionArgs(null, Side.BanditSide, UnityEngine.Color.red, new UnityEngine.Color(.6f, 0, 0), 5, StrategyAIType.Basic, TacticalAIType.Full, 701, 16, 16));
        banditsEmpire.Name = "Bandits";
        MainEmpiresWritable.Add(banditsEmpire);
        
        
        /*      MainEmpires.Add(new Empire(new Empire.ConstructionArgs(702, UnityEngine.Color.red, new UnityEngine.Color(.6f, 0, 0), 5, StrategyAIType.Basic, TacticalAIType.Full, 702, 16, 16)));
                MainEmpires.Last().Name = "Outcasts";
                MainEmpires.Last().ReplacedRace = Race.Tigers; */
        UpdateBanditLimits();
        crazyBuildings = args.crazyBuildings;
        VillageBuildingList.SetBuildings(crazyBuildings);


        ItemRepository = new ItemRepository();
        Stats = new StrategicStats();
        InitializeMonsters();

        //Added to get Bandits and Rebels to generate correctly
        RefreshEmpires();
        RefreshTurnOrder();

        State.GameManager.StrategyMode.Setup();
        State.GameManager.StrategyMode.RedrawTiles();
        State.GameManager.StrategyMode.RedrawVillages();
        RelationsManager.ResetRelations();
        State.GameManager.StrategyMode.BeginTurn();
        State.GameManager.StrategyMode.RebuildSpawners();
        MercenaryHouse.UpdateStaticStock();
        RenameBunnyTownsAsPreyTowns();


    }

    internal void UpdateBanditLimits()
    {
        int minGarrison = 48;
        int minArmySize = 48;
        foreach (var empire in MainEmpires)
        {


            if (RaceFuncs.IsMainRaceOrMerc(empire.Side))
            {
                if (empire.KnockedOut)
                    continue;
                if (empire.MaxGarrisonSize < minGarrison)
                    minGarrison = empire.MaxGarrisonSize;
                if (empire.MaxArmySize < minArmySize)
                    minArmySize = empire.MaxArmySize;
            }
            else
            {
                empire.MaxArmySize = minArmySize;
                empire.MaxGarrisonSize = minGarrison;
            }



        }
    }

    internal void RefreshMonstersKeepingArmies()
    {
        var monsterArmies = StrategicUtilities.GetAllArmies();
        var oldMons = MonsterEmpires;
        InitializeMonsters();
        if (oldMons != null && oldMons.Length > 0)
        {
            int i = 0;
            foreach (var empire in MonsterEmpires)
            {
                foreach (var army in monsterArmies)
                {
                    if (Equals(army.Side, empire.Side))
                    {
                        empire.Armies.Add(army);
                        army.SetEmpire(empire);
                    }
                }
                if (oldMons.Length < i) //Not sure how this would trigger, but this conditional is intended to stop an exception that's happening.  
                {
                    empire.ReplacedRace = oldMons[i].ReplacedRace;
                    i++;
                }

            }
        }

    }

    internal void InitializeMonsters()
    {

        MonsterEmpires = new MonsterEmpire[RaceFuncs.SpawnerElligibleMonsterRaces.Count];
        int i = 0;
        foreach (var entry in RaceFuncs.SpawnerElligibleMonsterRaces)
        {
            Race race = entry.Key;
            RaceFuncs.MonsterData data = entry.Value;
            MonsterEmpires[i] = new MonsterEmpire(new Empire.ConstructionArgs(race, race.ToSide(), UnityEngine.Color.white, UnityEngine.Color.white, data.BannerType, data.StrategicAI, data.TacticalAI, data.Team, data.MaxArmySize, data.MaxGarrisonSize));

            i++;
        }
        
		foreach (var emp in MonsterEmpires)
        {
            SpawnerInfo spawner = Config.World.GetSpawner(emp.Race);
            if (spawner == null)
                continue;
            emp.Team = spawner.Team;
        }
        List<Empire> allEmps = MainEmpires.ToList();
        allEmps.AddRange(MonsterEmpires);
        AllActiveEmpiresWritable = allEmps;
    }

    internal void RefreshEmpires()
    {
        if (MonsterEmpires == null)
        {
            InitializeMonsters();
            return;
        }
        List<Empire> allEmps = MainEmpires.ToList();
        allEmps.AddRange(MonsterEmpires);
        AllActiveEmpiresWritable = allEmps;
    }

    internal void PopulateMonsterTurnOrders()
    {
        foreach (var empire in MonsterEmpires)
        {
            SpawnerInfo spawner = Config.World.GetSpawner(empire.Race);
            if (spawner == null)
                continue;
            empire.TurnOrder = spawner.TurnOrder;
            empire.Team = spawner.Team;
        }

    }

    internal void RefreshTurnOrder()
    {
        EmpireOrder = AllActiveEmpires.OrderBy(s => s.TurnOrder).ThenBy(s => s.Race).ToList();
    }

    internal Empire GetEmpireOfRace(Race race)
    {
        if (AllActiveEmpires == null)
            return null;
        foreach (Empire empire in AllActiveEmpires)
        {
            if (Equals(empire.Race, race))
                return empire;
        }
        foreach (Empire empire in AllActiveEmpires)
        {
            if (Equals(empire.ReplacedRace, race))
                return empire;
        }
        return null;
    }

    internal Empire GetEmpireOfSide(Side side)
    {
        if (AllActiveEmpires == null)
            return null;
        foreach (Empire empire in AllActiveEmpires)
        {
            if (Equals(empire.Side, side))
                return empire;
        }
        return null;
    }

    private void RenameBunnyTownsAsPreyTowns()
    {
        int nameIndex = 1;

        foreach (Village village in Villages.Where(s => Equals(s.Race, Race.Bunnies) && s.Empire.CanVore == false))
        {
            if (village.Capital)
                village.Name = State.NameGen.GetAlternateTownName(Race.Bunnies, 0);
            else
            {
                village.Name = State.NameGen.GetAlternateTownName(Race.Bunnies, nameIndex);
                nameIndex++;
            }
        }
    }
}
